#!/bin/bash

# Script para instalar e configurar o Ubuntu para rodar aplicações .NET Core 8 com Nginx
# Data: 05/05/2025
# Descrição: Este script automatiza a instalação do .NET 8 SDK, runtime e Nginx no Ubuntu,
#            configurando o ambiente para hospedar aplicações .NET Core.

echo "Iniciando a instalação e configuração do ambiente para .NET Core 8 com Nginx..."

# Atualizar os repositórios e pacotes do sistema
echo "Atualizando os repositórios e pacotes do sistema..."
apt-get update
apt-get upgrade -y

# Instalar dependências necessárias
echo "Instalando dependências necessárias..."
apt-get install -y wget apt-transport-https software-properties-common

# Adicionar o repositório do Microsoft .NET
echo "Adicionando o repositório Microsoft..."
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Atualizar os repositórios novamente após adicionar o repositório Microsoft
echo "Atualizando repositórios após adicionar o repositório Microsoft..."
apt-get update

# Instalar o .NET SDK e Runtime
echo "Instalando .NET 8 SDK e Runtime..."
apt-get install -y dotnet-sdk-8.0
apt-get install -y aspnetcore-runtime-8.0

# Verificar a instalação do .NET
echo "Verificando a instalação do .NET..."
dotnet --version

# Instalar e configurar o Nginx
echo "Instalando e configurando o Nginx..."
apt-get install -y nginx

# Iniciar o Nginx e configurá-lo para iniciar na inicialização do sistema
echo "Iniciando o Nginx e configurando para iniciar automaticamente..."
systemctl start nginx
systemctl enable nginx

# Configurar o firewall (se estiver ativo)
echo "Configurando o firewall..."
apt-get install -y ufw
ufw allow 'Nginx Full'
ufw allow 22/tcp  # SSH

# Criar diretório para a aplicação
echo "Criando diretório para a aplicação .NET..."
mkdir -p /var/www/dotnet

# Configurar permissões
echo "Configurando permissões..."
chown -R www-data:www-data /var/www/dotnet
chmod -R 755 /var/www/dotnet

# Criar um arquivo de configuração Nginx para a aplicação .NET
echo "Criando arquivo de configuração Nginx para a aplicação Blazor..."
cat > /etc/nginx/sites-available/dotnet-app <<EOF
server {
    listen 80;
    server_name 192.168.0.39;  # Substitua pelo seu domínio ou IP
    access_log  /var/log/nginx/app.access.log;
    error_log /var/log/nginx/app.error.log;
    
    location / {
        proxy_pass http://localhost:53101;  # Porta da aplicacao Blazor
        #proxy_http_version 1.1;
        #proxy_set_header Upgrade \$http_upgrade;
        #proxy_set_header Connection keep-alive;
        #proxy_set_header Host \$host;
        #proxy_cache_bypass \$http_upgrade;
        #proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        #proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
EOF

echo "Criando arquivo de configuração Nginx para a aplicação ApiRest..."
cat > /etc/nginx/sites-available/dotnet-api <<EOF
server {
    listen 53011;
    server_name 192.168.0.39:53011;  # Substitua pelo seu domínio ou IP
    access_log  /var/log/nginx/api.access.log;
    error_log /var/log/nginx/api.error.log;
    
    location / {
        proxy_pass http://localhost:53111;  # Porta da aplicacao ApiRest
        #proxy_http_version 1.1;
        #proxy_set_header Upgrade \$http_upgrade;
        #proxy_set_header Connection keep-alive;
        #proxy_set_header Host \$host;
        #proxy_cache_bypass \$http_upgrade;
        #proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        #proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
EOF

echo "Criando arquivo de configuração Nginx para o front React..."
cat > /etc/nginx/sites-available/react <<EOF
server {
    listen 8082;
    server_name 192.168.0.39;  # Substitua pelo seu domínio ou IP
    access_log  /var/log/nginx/react.access.log;
    error_log /var/log/nginx/react.error.log;

    root /var/www/react/;
    index index.html index.htm;

    location / {
        try_files $uri $uri/ index.html =404;
    }
}
EOF

echo "Criando arquivo de configuração Nginx para a aplicação WebAssembly..."
cat > /etc/nginx/sites-available/dotnet-webAssembly <<EOF
limit_req_zone $http_x_forwarded_for zone=webassembly:20m rate=100r/s; # you missed line like this
server {
    listen 8083;
    server_name 192.168.0.39;  # Substitua pelo seu domínio ou IP
    access_log  /var/log/nginx/webassembly.access.log;
    error_log /var/log/nginx/webAssembly.error.log;


    location / {
        root /var/www/dotnet/wwwroot;
        limit_req zone=webassembly burst=60 nodelay;
        try_files $uri $uri/ index.html =404;
        include /etc/nginx/mime.types;
        types {
                application/wasm wasm;
        }
        default_type application/octet-stream;

        location /_framework/ {
                gzip_static on;
        }
    }
}

EOF

# Ativar o site
echo "Ativando o site no Nginx..."
ln -s /etc/nginx/sites-available/dotnet-app /etc/nginx/sites-enabled/
rm -f /etc/nginx/sites-enabled/default  # Remover o site padrão

# Testar a configuração do Nginx
echo "Testando a configuração do Nginx..."
nginx -t

# Reiniciar o Nginx para aplicar as configurações
echo "Reiniciando o Nginx para aplicar as configurações..."
systemctl restart nginx

# Criar um serviço systemd para a aplicação .NET
echo "Criando um serviço systemd para a aplicação .NET..."

cat > "/etc/systemd/system/dotnet-app.service" <<EOF
[Unit]
Description=Aplicação .NET Core
After=network.target

[Service]
WorkingDirectory=/var/www/dotnet
ExecStart=/usr/bin/dotnet /var/www/dotnet/BlazorTarefas.dll
Restart=always
# Reiniciar o serviço após 10 segundos se falhar
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-app
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=ASPNETCORE_URLS=http://127.0.0.1:53101

[Install]
WantedBy=multi-user.target
EOF

cat > "/etc/systemd/system/dotnet-api.service" <<EOF
[Unit]
Description=Aplicação .NET Core
After=network.target

[Service]
WorkingDirectory=/var/www/dotnet
ExecStart=/usr/bin/dotnet /var/www/dotnet/ApiRest.dll
Restart=always
# Reiniciar o serviço após 10 segundos se falhar
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-app
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=ASPNETCORE_URLS=http://127.0.0.1:53111

[Install]
WantedBy=multi-user.target
EOF

# Recarregar o systemd
exibir_mensagem "Recarregando o systemd..."
systemctl daemon-reload

# Habilitar o serviço para iniciar na inicialização do sistema
exibir_mensagem "Habilitando o serviço para iniciar na inicialização do sistema..."
systemctl enable "dotnet-app"
systemctl enable "dotnet-api"

echo "Instalação e configuração concluídas com sucesso!"
echo "Para iniciar a aplicação após o deploy, execute: systemctl start dotnet-app.service"
echo "Para verificar o status da aplicação, execute: systemctl status dotnet-app.service"
