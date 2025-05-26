# Scripts para Instalação e Deploy de Aplicação .NET Core no Linux

Este diretório contém scripts para automatizar a instalação, configuração e deploy de uma aplicação .NET Core 8 em um servidor Linux (Ubuntu) com Nginx.

## Conteúdo

1. `instalar_dotnet_nginx.sh` - Script para instalar e configurar o Ubuntu para rodar aplicações .NET Core 8 com Nginx
2. `compilar_e_enviar.ps1` - Script PowerShell para compilar a aplicação .NET e enviar para o servidor Linux
3. `configurar_e_iniciar.sh` - Script para configurar e iniciar a aplicação no servidor Linux após o recebimento dos arquivos

## Como Usar

### 1. Preparar o Servidor Linux

1. Copie o script `instalar_dotnet_nginx.sh` para o servidor Ubuntu
2. Dê permissão de execução ao script:
   ```bash
   chmod +x instalar_dotnet_nginx.sh
   ```
3. Execute o script como root:
   ```bash
   sudo ./instalar_dotnet_nginx.sh
   ```

Este script irá:
- Atualizar o sistema
- Instalar o .NET 8 SDK e Runtime
- Instalar e configurar o Nginx
- Configurar o firewall
- Criar diretórios para a aplicação
- Configurar o serviço systemd para a aplicação

### 2. Compilar e Enviar a Aplicação

No Windows, execute o script PowerShell `compilar_e_enviar.ps1` com os parâmetros necessários:


# .\LinuxInstall\compilar_e_enviar.ps1 -ServidorIP 192.168.0.39 -Usuario winaudio -Senha Win@dmin
```powershell
.\compilar_e_enviar.ps1 -ServidorIP "seu_ip_aqui" -Usuario "seu_usuario" -Senha "sua_senha"
```

Parâmetros opcionais:
- `-CaminhoProjeto`: Caminho para o diretório do projeto (padrão: "c:\Projetos\workshop\repo-binhara-azuris\TarefaWork")
- `-DiretorioPublicacao`: Diretório onde os arquivos compilados serão armazenados temporariamente (padrão: "c:\Projetos\workshop\repo-binhara-azuris\TarefaWork\publicacao")

Este script irá:
- Compilar a aplicação .NET em modo Release
- Publicar a aplicação para o diretório de publicação
- Comprimir os arquivos
- Enviar para o servidor Linux via SCP
- Executar comandos remotos para descompactar e configurar a aplicação

### 3. Configurar e Iniciar a Aplicação no Servidor (Alternativa)

Se preferir configurar manualmente a aplicação no servidor após o envio dos arquivos, você pode usar o script `configurar_e_iniciar.sh`:

1. Copie o script para o servidor
2. Dê permissão de execução:
   ```bash
   chmod +x configurar_e_iniciar.sh
   ```
3. Execute como root:
   ```bash
   sudo ./configurar_e_iniciar.sh
   ```

## Requisitos

### No Windows:
- PowerShell 5.1 ou superior
- .NET SDK 8.0 ou superior
- Módulo Posh-SSH (será instalado automaticamente pelo script se não estiver presente)

### No Linux:
- Ubuntu 20.04 ou superior
- Acesso root ou sudo

## Observações

- Os scripts estão configurados para uma aplicação .NET Core genérica. Pode ser necessário ajustar alguns parâmetros dependendo da estrutura específica da sua aplicação.
- Por segurança, considere usar autenticação por chave SSH em vez de senha para o acesso ao servidor Linux.
- Verifique sempre os logs do sistema após o deploy para garantir que a aplicação está funcionando corretamente.

## Comandos Úteis no Servidor Linux

- Verificar status da aplicação: `sudo systemctl status dotnet-app`
- Visualizar logs: `sudo journalctl -u dotnet-app -f`
- Reiniciar aplicação: `sudo systemctl restart dotnet-app`
- Verificar status do Nginx: `sudo systemctl status nginx`
