# TODOs para TesteRelatorio.cs

## TODO 1: Melhorar o nome do teste para indicar claramente o que está sendo testado

```csharp
[Fact]
public void GerarRelatorio_DeveListarTarefasComTempoRegistrado()
{ 
    TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

    Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefaServico.Salvar(tarefa01);

    //act
    tarefaServico.IniciaCronometro(tarefa01);
    Thread.Sleep(100);
    tarefaServico.PausaCronometro(tarefa01);

    Tarefa tarefa02 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefaServico.Salvar(tarefa02);

    tarefaServico.IniciaCronometro(tarefa02);
    Thread.Sleep(100);
    tarefaServico.PausaCronometro(tarefa02);

    var resultado = tarefaServico.ListarPorUsuario(criador.Id);
    var resultado2 = tarefaServico.ListarPorUsuario(criador.Id);
    
    //assert
    Assert.Equal(tarefa01.TempoTotal, resultado[0].TempoTotal);
    Assert.Equal(tarefa02.TempoTotal, resultado[1].TempoTotal);
    Assert.Equal(tarefa01.Responsavel, resultado[0].Responsavel);
    Assert.Equal(tarefa02.Responsavel, resultado[1].Responsavel);
}
```

## TODO 2: Separar claramente as fases de Arrange, Act e Assert

```csharp
[Fact]
public void GerarRelatorio_DeveListarTarefasComTempoRegistrado()
{ 
    // Arrange
    TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    
    StatusTarefa status = new StatusTarefa(StatusTarefa.Status.ToDo);

    Tarefa tarefa01 = new Tarefa("titulo", status, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefaServico.Salvar(tarefa01);
    
    Tarefa tarefa02 = new Tarefa("titulo", status, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefaServico.Salvar(tarefa02);

    // Act
    // Registrar tempo para tarefa01
    tarefaServico.IniciaCronometro(tarefa01);
    Thread.Sleep(100);
    tarefaServico.PausaCronometro(tarefa01);

    // Registrar tempo para tarefa02
    tarefaServico.IniciaCronometro(tarefa02);
    Thread.Sleep(100);
    tarefaServico.PausaCronometro(tarefa02);

    // Obter relatório
    var resultado = tarefaServico.ListarPorUsuario(criador.Id);
    
    // Assert
    Assert.Equal(2, resultado.Count);
    Assert.Equal(tarefa01.TempoTotal, resultado[0].TempoTotal);
    Assert.Equal(tarefa02.TempoTotal, resultado[1].TempoTotal);
    Assert.Equal(tarefa01.Responsavel, resultado[0].Responsavel);
    Assert.Equal(tarefa02.Responsavel, resultado[1].Responsavel);
}
```

## TODO 3: Evitar uso de Thread.Sleep em testes, pois torna os testes mais lentos e potencialmente instáveis

```csharp
[Fact]
public void GerarRelatorio_DeveListarTarefasComTempoRegistrado()
{ 
    // Arrange
    var mockCronometro = new Mock<ICronometro>();
    mockCronometro.Setup(c => c.Total).Returns(TimeSpan.FromMilliseconds(150));
    
    TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    
    StatusTarefa status = new StatusTarefa(StatusTarefa.Status.ToDo);

    // Criar tarefas com cronômetro mockado
    Tarefa tarefa01 = new Tarefa("titulo", status, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefa01.DefinirCronometro(mockCronometro.Object);
    tarefaServico.Salvar(tarefa01);
    
    Tarefa tarefa02 = new Tarefa("titulo", status, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefa02.DefinirCronometro(mockCronometro.Object);
    tarefaServico.Salvar(tarefa02);

    // Act
    // Simular registro de tempo sem Thread.Sleep
    tarefaServico.IniciaCronometro(tarefa01);
    tarefaServico.PausaCronometro(tarefa01);

    tarefaServico.IniciaCronometro(tarefa02);
    tarefaServico.PausaCronometro(tarefa02);

    // Obter relatório
    var resultado = tarefaServico.ListarPorUsuario(criador.Id);
    
    // Assert
    Assert.Equal(2, resultado.Count);
    Assert.Equal(TimeSpan.FromMilliseconds(150), resultado[0].TempoTotal);
    Assert.Equal(TimeSpan.FromMilliseconds(150), resultado[1].TempoTotal);
}
```

Observação: Para implementar este exemplo, seria necessário refatorar a classe `Tarefa` para aceitar uma interface `ICronometro` que poderia ser mockada para testes.

## TODO 4: A variável resultado2 é criada mas não utilizada

```csharp
[Fact]
public void GerarRelatorio_DeveListarTarefasComTempoRegistrado()
{ 
    // Arrange
    TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

    Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefaServico.Salvar(tarefa01);

    Tarefa tarefa02 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
    tarefaServico.Salvar(tarefa02);

    //act
    tarefaServico.IniciaCronometro(tarefa01);
    Thread.Sleep(100);
    tarefaServico.PausaCronometro(tarefa01);

    tarefaServico.IniciaCronometro(tarefa02);
    Thread.Sleep(100);
    tarefaServico.PausaCronometro(tarefa02);

    // Obter relatório apenas uma vez
    var resultado = tarefaServico.ListarPorUsuario(criador.Id);
    
    //assert
    Assert.Equal(tarefa01.TempoTotal, resultado[0].TempoTotal);
    Assert.Equal(tarefa02.TempoTotal, resultado[1].TempoTotal);
    Assert.Equal(tarefa01.Responsavel, resultado[0].Responsavel);
    Assert.Equal(tarefa02.Responsavel, resultado[1].Responsavel);
}
```

## TODO 5: Melhorar o teste para verificar explicitamente que tarefa02 não está no resultado

```csharp
[Fact]
public void GeraRelatorioComTarefaNaoExistente_NaoDeveIncluirTarefaNaoSalva()
{
    // Arrange
    TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

    // Tarefa salva no repositório
    Tarefa tarefa01 = new Tarefa("titulo1", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao1", Tarefa.Prioridade.Alta);
    tarefaServico.Salvar(tarefa01);

    // Tarefa não salva no repositório
    Tarefa tarefa02 = new Tarefa("titulo2", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao2", Tarefa.Prioridade.Alta);

    // Act
    tarefaServico.IniciaCronometro(tarefa01);
    Thread.Sleep(100);
    tarefaServico.PausaCronometro(tarefa01);

    var resultado = tarefaServico.ListarPorUsuario(criador.Id);

    // Assert
    Assert.Single(resultado);
    Assert.Equal(tarefa01.TempoTotal, resultado[0].TempoTotal);
    Assert.Equal(tarefa01.Responsavel, resultado[0].Responsavel);
    Assert.Equal(tarefa01.Titulo, resultado[0].Titulo);
    
    // Verificar explicitamente que tarefa02 não está no resultado
    Assert.DoesNotContain(resultado, t => t.Titulo == tarefa02.Titulo);
    Assert.DoesNotContain(resultado, t => t.Id == tarefa02.Id);
}
```

## TODO 6: Verificar se o método está realmente testando o comportamento esperado com tarefas não existentes

```csharp
[Fact]
public void GeraRelatorioComTarefaNaoExistente_DeveLancarExcecao()
{
    // Arrange
    TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

    // Tarefa não salva no repositório
    Tarefa tarefaNaoSalva = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);

    // Act & Assert
    // Verificar que tentar iniciar o cronômetro em uma tarefa não salva lança exceção
    var exception = Assert.Throws<InvalidOperationException>(() => 
        tarefaServico.IniciaCronometro(tarefaNaoSalva));
    
    Assert.Contains("não existe", exception.Message.ToLower());
}
```

## TODO 7: Considerar extrair configuração comum entre os testes para um método de inicialização

```csharp
public class TesteRelatorio : IDisposable
{
    private readonly TarefaServico _tarefaServico;
    private readonly UsuarioServico _usuarioServico;
    private readonly Usuario _criador;
    private readonly Usuario _responsavel;
    private readonly StatusTarefa _statusTarefa;
    
    public TesteRelatorio()
    {
        // Setup - executado antes de cada teste
        _tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
        _usuarioServico = new UsuarioServico(new UsuarioMemoriaRepositorio());
        
        _criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        _usuarioServico.Criar(_criador);
        
        _responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        _usuarioServico.Criar(_responsavel);
        
        _statusTarefa = new StatusTarefa(StatusTarefa.Status.ToDo);
    }
    
    public void Dispose()
    {
        // TearDown - executado após cada teste
        // Limpar recursos, se necessário
    }
    
    [Fact]
    public void GerarRelatorio_DeveListarTarefasComTempoRegistrado()
    { 
        // Arrange
        Tarefa tarefa01 = new Tarefa("titulo", _statusTarefa, _criador, _responsavel, 
            new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
        _tarefaServico.Salvar(tarefa01);
        
        Tarefa tarefa02 = new Tarefa("titulo", _statusTarefa, _criador, _responsavel, 
            new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
        _tarefaServico.Salvar(tarefa02);

        // Act
        _tarefaServico.IniciaCronometro(tarefa01);
        Thread.Sleep(100);
        _tarefaServico.PausaCronometro(tarefa01);

        _tarefaServico.IniciaCronometro(tarefa02);
        Thread.Sleep(100);
        _tarefaServico.PausaCronometro(tarefa02);

        var resultado = _tarefaServico.ListarPorUsuario(_criador.Id);
        
        // Assert
        Assert.Equal(2, resultado.Count);
        Assert.Equal(tarefa01.TempoTotal, resultado[0].TempoTotal);
        Assert.Equal(tarefa02.TempoTotal, resultado[1].TempoTotal);
    }
    
    [Fact]
    public void GeraRelatorioComTarefaNaoExistente_NaoDeveIncluirTarefaNaoSalva()
    {
        // Arrange
        Tarefa tarefa01 = new Tarefa("titulo1", _statusTarefa, _criador, _responsavel, 
            new DateTime(2025, 05, 20), "descricao1", Tarefa.Prioridade.Alta);
        _tarefaServico.Salvar(tarefa01);

        Tarefa tarefa02 = new Tarefa("titulo2", _statusTarefa, _criador, _responsavel, 
            new DateTime(2025, 05, 20), "descricao2", Tarefa.Prioridade.Alta);
        // Não salva tarefa02

        // Act
        _tarefaServico.IniciaCronometro(tarefa01);
        Thread.Sleep(100);
        _tarefaServico.PausaCronometro(tarefa01);

        var resultado = _tarefaServico.ListarPorUsuario(_criador.Id);

        // Assert
        Assert.Single(resultado);
        Assert.Equal(tarefa01.Id, resultado[0].Id);
    }
}
```
