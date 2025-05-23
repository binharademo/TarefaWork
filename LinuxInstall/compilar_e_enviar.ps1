# Script para compilar e enviar a aplicação .NET para o servidor Linux
# Data: 05/05/2025
# Descrição: Este script compila a aplicação .NET, cria um pacote de publicação
#            e envia para o servidor Linux usando SCP.

# Parâmetros do script
param(
    [Parameter(Mandatory=$true)]
    [string]$ServidorIP,
    
    [Parameter(Mandatory=$true)]
    [string]$Usuario,
    
    [Parameter(Mandatory=$true)]
    [string]$Senha,
    
    [Parameter(Mandatory=$false)]
    [string]$CaminhoProjeto = ".\",
    
    [Parameter(Mandatory=$false)]
    [string]$DiretorioPublicacao = ".\publicacao"
)
$OutputEncoding = [Console]::InputEncoding = [Console]::OutputEncoding = New-Object System.Text.UTF8Encoding

$arquivoZip = "aplicacao_dotnet.zip"

# Função para exibir mensagens com formatação
function Escrever-Mensagem {
    param (
        [string]$mensagem,
        [string]$tipo = "INFO" # INFO, SUCESSO, ERRO, AVISO
    )
    
    $corOriginal = $host.UI.RawUI.ForegroundColor
    
    switch ($tipo) {
        "INFO" { 
            $host.UI.RawUI.ForegroundColor = "Cyan"
            Write-Host "[INFO] " -NoNewline
        }
        "SUCESSO" { 
            $host.UI.RawUI.ForegroundColor = "Green"
            Write-Host "[SUCESSO] " -NoNewline
        }
        "ERRO" { 
            $host.UI.RawUI.ForegroundColor = "Red"
            Write-Host "[ERRO] " -NoNewline
        }
        "AVISO" { 
            $host.UI.RawUI.ForegroundColor = "Yellow"
            Write-Host "[AVISO] " -NoNewline
        }
    }
    
    $host.UI.RawUI.ForegroundColor = $corOriginal
    Write-Host $mensagem
}

# Verificar se o diretório do projeto existe
if (-not (Test-Path $CaminhoProjeto)) {
    Escrever-Mensagem "O diretório do projeto não existe: $CaminhoProjeto" "ERRO"
    exit 1
}

# Criar diretório de publicação se não existir
if (-not (Test-Path $DiretorioPublicacao)) {
    Escrever-Mensagem "Criando diretório de publicação: $DiretorioPublicacao"
    New-Item -ItemType Directory -Path $DiretorioPublicacao -Force | Out-Null
}

# Navegar para o diretório do projeto
#Escrever-Mensagem "Navegando para o diretório do projeto: $CaminhoProjeto"
#Set-Location $CaminhoProjeto

# Limpar publicações anteriores
Escrever-Mensagem "Limpando diretório de publicação anterior..."
if (Test-Path $DiretorioPublicacao) {
    Remove-Item -Path "$DiretorioPublicacao\*" -Recurse -Force
}

# Restaurar pacotes NuGet
Escrever-Mensagem "Restaurando pacotes NuGet..."
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Escrever-Mensagem "Falha ao restaurar pacotes NuGet." "ERRO"
    exit 1
}

# Compilar o projeto em modo Release
Escrever-Mensagem "Compilando o projeto em modo Release..."
dotnet build --configuration Release
if ($LASTEXITCODE -ne 0) {
    Escrever-Mensagem "Falha ao compilar o projeto." "ERRO"
    exit 1
}

# Publicar o projeto para o diretório de publicação
Escrever-Mensagem "Publicando o projeto para: $DiretorioPublicacao"
dotnet publish --configuration Release --output $DiretorioPublicacao --runtime linux-x64
if ($LASTEXITCODE -ne 0) {
    Escrever-Mensagem "Falha ao publicar o projeto." "ERRO"
    exit 1
}

# Verificar se a publicação foi bem-sucedida
if (-not (Test-Path "$DiretorioPublicacao\*.dll")) {
    Escrever-Mensagem "Não foram encontrados arquivos DLL no diretório de publicação." "ERRO"
    exit 1
}

# Comprimir os arquivos para envio
$arquivoZip = "$env:TEMP\aplicacao_dotnet.zip"
Escrever-Mensagem "Comprimindo arquivos para envio: $arquivoZip"
Compress-Archive -Path "$DiretorioPublicacao\*" -DestinationPath $arquivoZip -Force
if (-not (Test-Path $arquivoZip)) {
    Escrever-Mensagem "Falha ao comprimir os arquivos para envio." "ERRO"
    exit 1
}


# Converter a senha para SecureString 
$senhaSegura = ConvertTo-SecureString $Senha -AsPlainText -Force
$credencial = New-Object System.Management.Automation.PSCredential ($Usuario, $senhaSegura)

# Verificar se o módulo Posh-SSH está instalado
if (-not (Get-Module -ListAvailable -Name Posh-SSH)) {
    Escrever-Mensagem "O módulo Posh-SSH não está instalado. Instalando..." "AVISO"
    Install-Module -Name Posh-SSH -Force -Scope CurrentUser
    if ($LASTEXITCODE -ne 0) {
        Escrever-Mensagem "Falha ao instalar o módulo Posh-SSH. Instale manualmente com: Install-Module -Name Posh-SSH -Force -Scope CurrentUser" "ERRO"
        exit 1
    }
}

# Importar o módulo Posh-SSH
Import-Module Posh-SSH

# Enviar o arquivo para o servidor Linux
Escrever-Mensagem "Enviando arquivo para o servidor Linux: $ServidorIP"
try {
    # Estabelecer sessão SSH
    $sessaoSSH = New-SSHSession -ComputerName $ServidorIP -Credential $credencial -AcceptKey
    
    # Enviar o arquivo
    Set-SCPItem -ComputerName $ServidorIP -Credential $credencial -Path $arquivoZip -Destination "./"
    Set-SCPItem -ComputerName $ServidorIP -Credential $credencial -Path LinuxInstall\configurar_e_iniciar.sh -Destination "./"
    
    $stream = $sessaoSSH.Session.CreateShellStream("PS-SSH", 0, 0, 0, 0, 100)
    
    Escrever-Mensagem "Executando publicação (./configurar_e_iniciar.sh)"
    $resultado = Invoke-SSHStreamExpectSecureAction -ShellStream $stream -Command "sudo ./configurar_e_iniciar.sh && echo SUCCESS || echo FAILURE" -ExpectString "[sudo] password for $($Usuario):" -SecureAction $senhaSegura
    Start-Sleep -Milliseconds 2000  # Wait a bit for output to appear
    $saida = $stream.Read()
            
#    if (-not($saida -match "SUCCESS")) {
        $linhasValidas = $saida -split '\r?\n' | Where-Object {
            # remove linhas em branco
            if ($_ -match '^\s*$') { return $false }
            # remove prompt do sudo
            if ($_ -match "^\[sudo\] password for $Usuario\:") { return $false }
            # remove eco do próprio comando (caso o shell o repita)
            if ($_ -eq "sudo ./configurar_e_iniciar.sh") { return $false }
            # opcional: remova as linhas SUCCESS/FAILURE
            if ($_ -match '^(SUCCESS|FAILURE)$') { return $false }
            # remove o shell prompt (ex.: usuario@servidor:~$)
            if ($_ -match '^[^@\s]+@[^:\s]+:[^$\r\n]+\$') { return $false }
            # caso contrário, mantém a linha
            return $true
        }
        $saida = $linhasValidas -join "`n"
        Escrever-Mensagem "Saida: $saida" "ERRO"
#    }
        
    # Encerrar a sessão SSH
    Remove-SSHSession -SessionId $sessaoSSH.SessionId | Out-Null
    
    Escrever-Mensagem "Deploy concluído com sucesso!" "SUCESSO"
} catch {
    Escrever-Mensagem "Erro durante o envio ou configuração: $_" "ERRO"
    exit 1
}

# Limpar arquivos temporários
Escrever-Mensagem "Limpando arquivos temporários..."
Remove-Item $arquivoZip -Force

Escrever-Mensagem "Processo de compilação e deploy concluído com sucesso!" "SUCESSO"
Escrever-Mensagem "A aplicação está disponível em: http://$ServidorIP/" "SUCESSO"
