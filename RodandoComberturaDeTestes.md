# Guia para Executar Testes com Cobertura de Código

Este guia explica como executar os testes do projeto e visualizar a cobertura de código usando ferramentas gratuitas.

## Pré-requisitos

- .NET SDK 8.0 ou superior
- PowerShell
- Navegador web

## Executando os Testes com Cobertura

### Método 1: Usando o Script Automatizado (Recomendado)

1. Abra um terminal PowerShell na raiz do projeto (`c:\Projetos\git\tc\TarefaWork`)
2. Execute o script:
   ```powershell
   .\RunTestsWithCoverage.ps1
   ```
3. O script irá:
   - Executar todos os testes
   - Coletar dados de cobertura
   - Gerar um relatório HTML
   - Abrir o relatório no navegador padrão

### Método 2: Comandos Manuais

Se preferir executar os comandos manualmente:

1. Execute os testes com coleta de cobertura:
   ```powershell
   dotnet test --collect:"XPlat Code Coverage" --results-directory:"./TestResults"
   ```

2. Instale o ReportGenerator (apenas na primeira vez):
   ```powershell
   dotnet tool install -g dotnet-reportgenerator-globaltool
   ```

3. Gere o relatório HTML:
   ```powershell
   reportgenerator "-reports:./TestResults/**/coverage.cobertura.xml" "-targetdir:./TestResults/CoverageReport" "-reporttypes:Html"
   ```

4. Abra o relatório HTML:
   ```powershell
   start ./TestResults/CoverageReport/index.html
   ```

## Visualizando o Relatório de Cobertura

O relatório HTML será aberto automaticamente no seu navegador padrão. Se isso não acontecer, você pode:

1. Navegar manualmente até a pasta `TestResults/CoverageReport/` 
2. Abrir o arquivo `index.html` no seu navegador

Alternativamente, você pode iniciar um servidor web local para visualizar o relatório:

```powershell
# Na raiz do projeto
python -m http.server 8000 --directory TestResults/CoverageReport
```

Em seguida, abra o navegador e acesse: http://localhost:8000

## Interpretando o Relatório

O relatório HTML fornece:

- **Visão geral da cobertura**: Porcentagem total de linhas e branches cobertos
- **Detalhamento por namespace e classe**: Cobertura detalhada por componente
- **Visualização do código-fonte**: Código destacado mostrando:
  - Linhas verdes: código coberto pelos testes
  - Linhas vermelhas: código não coberto
  - Linhas amarelas: branches parcialmente cobertos

## Personalizando a Análise de Cobertura

Para personalizar quais partes do código são incluídas na análise, edite o arquivo `coverlet.runsettings` na raiz do projeto. Você pode:

- Incluir/excluir namespaces específicos
- Ignorar arquivos específicos
- Configurar outras opções de análise

## Dicas para Melhorar a Cobertura

1. Foque nas áreas com menor cobertura (destacadas em vermelho)
2. Adicione testes para branches não cobertos (condicionais)
3. Verifique se todas as classes principais têm testes unitários
4. Considere adicionar testes de integração para fluxos complexos

## Problemas Comuns

- **Relatório não abre automaticamente**: Navegue manualmente até `TestResults/CoverageReport/index.html`
- **Erro "Ferramenta 'reportgenerator' não encontrada"**: Execute `dotnet tool install -g dotnet-reportgenerator-globaltool`
- **Dados de cobertura desatualizados**: Certifique-se de executar o script novamente após alterações no código
