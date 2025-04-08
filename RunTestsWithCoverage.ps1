# Script para executar testes com cobertura de código
Write-Host "Executando testes com cobertura de código..." -ForegroundColor Green

# Limpar resultados anteriores
if (Test-Path "./TestResults") {
    Remove-Item -Recurse -Force "./TestResults"
}

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage" --results-directory:"./TestResults"

# Verificar se o ReportGenerator está instalado
$reportGeneratorInstalled = dotnet tool list -g | Select-String "dotnet-reportgenerator-globaltool"
if (-not $reportGeneratorInstalled) {
    Write-Host "Instalando ReportGenerator..." -ForegroundColor Yellow
    dotnet tool install -g dotnet-reportgenerator-globaltool
}

# Gerar relatório HTML
Write-Host "Gerando relatório HTML..." -ForegroundColor Green
reportgenerator "-reports:./TestResults/**/coverage.cobertura.xml" "-targetdir:./TestResults/CoverageReport" "-reporttypes:Html"

# Abrir o relatório no navegador
Write-Host "Relatório gerado com sucesso!" -ForegroundColor Green
Write-Host "Abrindo relatório no navegador..." -ForegroundColor Green
Start-Process "./TestResults/CoverageReport/index.html"
