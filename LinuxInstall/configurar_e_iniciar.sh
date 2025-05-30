#!/bin/bash

# Script para configurar e iniciar a aplicação .NET no servidor Linux
# Data: 05/05/2025
# Descrição: Este script deve ser executado no servidor Linux após receber os arquivos da aplicação.
#            Ele configura o ambiente, copia os arquivos para o diretório correto e inicia o serviço.

# Parâmetros
DIRETORIO_TEMP="/tmp/aplicacao_dotnet"
DIRETORIO_DESTINO="/var/www/dotnet"
DIRETORIO_REACT="/var/www/react"
NOME_SERVICO="dotnet-app"
NOME_SERVICO_API="dotnet-api"
ARQUIVO_ZIP="aplicacao_dotnet.zip"

# Função para exibir mensagens formatadas
exibir_mensagem() {
    local mensagem="$1"
    local tipo="${2:-INFO}"  # INFO, SUCESSO, ERRO, AVISO
    
    case "$tipo" in
        "INFO")
            echo -e "\e[36m[INFO]\e[0m $mensagem"
            ;;
        "SUCESSO")
            echo -e "\e[32m[SUCESSO]\e[0m $mensagem"
            ;;
        "ERRO")
            echo -e "\e[31m[ERRO]\e[0m $mensagem"
            ;;
        "AVISO")
            echo -e "\e[33m[AVISO]\e[0m $mensagem"
            ;;
    esac
}

# Verificar se o script está sendo executado como root
if [ "$(id -u)" -ne 0 ]; then
    exibir_mensagem "Este script deve ser executado como root ou com sudo." "ERRO"
    exit 1
fi

# Verificar se o arquivo ZIP existe
if [ ! -f "$ARQUIVO_ZIP" ]; then
    exibir_mensagem "Arquivo ZIP não encontrado: $ARQUIVO_ZIP" "ERRO"
    exit 1
fi

# Criar diretório temporário para descompactar os arquivos
exibir_mensagem "Criando diretório temporário..."
rm -rf "$DIRETORIO_TEMP"
mkdir -p "$DIRETORIO_TEMP"

# Descompactar o arquivo ZIP
exibir_mensagem "Descompactando arquivo ZIP..."
unzip -qq -o "$ARQUIVO_ZIP" -d "$DIRETORIO_TEMP" 
if [ $? -gt 1 ]; then
    exibir_mensagem "Falha ao descompactar o arquivo ZIP." "ERRO"
    exit 1
fi

# Parar o serviço .NET se estiver em execução
exibir_mensagem "Parando o serviço .NET se estiver em execução..."
systemctl stop "$NOME_SERVICO" || true
systemctl stop "$NOME_SERVICO_API" || true

# Limpar o diretório de destino, mantendo arquivos de dados
exibir_mensagem "Limpando o diretório de destino..."
find "$DIRETORIO_DESTINO"/ -maxdepth 1 -mindepth 1 ! -name '*.db' -exec rm -Rf {} +

# Copiar os arquivos para o diretório de destino
exibir_mensagem "Copiando arquivos para o diretório de destino..."
mkdir -p "$DIRETORIO_DESTINO"
cp -r "$DIRETORIO_TEMP"/* "$DIRETORIO_DESTINO"/

# Configurar permissões
exibir_mensagem "Configurando permissões..."
chown -R www-data:www-data "$DIRETORIO_DESTINO"
chmod -R 755 "$DIRETORIO_DESTINO"

#Move arquivos React
exibir_mensagem "Configurando REACT..."
find "$DIRETORIO_REACT"/ -maxdepth 1 -mindepth 1 ! -name '.env' -exec rm -Rf {} +
mv "$DIRETORIO_DESTINO"/react/* "$DIRETORIO_REACT"/

# Iniciar o serviço
exibir_mensagem "Iniciando o serviço..."
systemctl start "$NOME_SERVICO"
systemctl start "$NOME_SERVICO_API"
systemctl restart nginx

# Verificar se o serviço está em execução
if systemctl is-active --quiet "$NOME_SERVICO"; then
    exibir_mensagem "$NOME_SERVICO iniciado com sucesso!" "SUCESSO"
else
    exibir_mensagem "$NOME_SERVICO Falha ao iniciar. Verifique os logs com: journalctl -u $NOME_SERVICO" "ERRO"
    exit 1
fi

# Verificar se o serviço está em execução
if systemctl is-active --quiet "$NOME_SERVICO_API"; then
    exibir_mensagem "$NOME_SERVICO_API iniciado com sucesso!" "SUCESSO"
else
    exibir_mensagem "$NOME_SERVICO_API Falha ao iniciar. Verifique os logs com: journalctl -u $NOME_SERVICO_API" "ERRO"
    exit 1
fi

# Limpar arquivos temporários
exibir_mensagem "Limpando arquivos temporários..."
rm -rf "$DIRETORIO_TEMP"
rm -f "$ARQUIVO_ZIP"

exibir_mensagem "Configuração e inicialização concluídas com sucesso!" "SUCESSO"
exibir_mensagem "A aplicação está disponível através do Nginx configurado." "SUCESSO"

# Exibir informações úteis
echo ""
echo "==================================================================="
echo "Informações úteis:"
echo "-------------------------------------------------------------------"
echo "Para verificar o status da aplicação: systemctl status $NOME_SERVICO"
echo "Para visualizar os logs da aplicação: journalctl -u $NOME_SERVICO -f"
echo "Para reiniciar a aplicação: systemctl restart $NOME_SERVICO"
echo "Para parar a aplicação: systemctl stop $NOME_SERVICO"
echo "==================================================================="
