# TODOs para TesteCronometro.cs

## TODO 1: Melhorar o nome do teste para indicar claramente o comportamento esperado

```csharp
[Fact]
public void NovoCronometro_DeveIniciarEmAndamento()
{
    // Arrange & Act
    var c = new Cronometro();

    // Assert
    Assert.True(c.EmAndamento());
}
```

## TODO 2: Adicionar verificações adicionais para outras propriedades iniciais do cronômetro

```csharp
[Fact]
public void NovoCronometro_DeveIniciarComPropriedadesCorretas()
{
    // Arrange & Act
    var c = new Cronometro();
    
    // Assert
    Assert.True(c.EmAndamento());
    Assert.Equal(TimeSpan.Zero, c.Total);
    Assert.NotEqual(default(DateTime), c.DataInicio);
    Assert.True(c.DataInicio <= DateTime.Now);
}
```

## TODO 3: Seguir convenção de nomenclatura PascalCase para nomes de métodos

```csharp
[Fact]
public void StopCronometro_DeveInterromperCronometroERegistrarTempo()
{
    // Arrange
    var c = new Cronometro();
    Thread.Sleep(100);
    
    // Act
    c.Stop();

    // Assert
    Assert.False(c.EmAndamento());
    Assert.True(c.Total.Milliseconds >= 100); 
}
```

## TODO 4: Evitar uso de Thread.Sleep em testes, pois torna os testes mais lentos e potencialmente instáveis

```csharp
[Fact]
public void StopCronometro_DeveInterromperCronometro()
{
    // Arrange
    var c = new Cronometro();
    
    // Act
    // Simular passagem de tempo sem Thread.Sleep
    var tempoInicial = c.DataInicio;
    c.Stop();

    // Assert
    Assert.False(c.EmAndamento());
    Assert.True(c.Total >= TimeSpan.Zero);
}
```

## TODO 5: Implementar um mock para o tempo para tornar o teste mais determinístico

```csharp
[Fact]
public void StopCronometro_DeveRegistrarTempoCorretamente()
{
    // Arrange
    // Criar um mock para o relógio
    var relogioMock = new Mock<IRelogio>();
    var tempoInicial = new DateTime(2025, 1, 1, 10, 0, 0);
    var tempoFinal = tempoInicial.AddMilliseconds(150);
    
    // Configurar o mock para retornar os tempos específicos
    relogioMock.SetupSequence(r => r.Agora())
        .Returns(tempoInicial)
        .Returns(tempoFinal);
    
    // Injetar o mock no cronômetro
    var c = new Cronometro(relogioMock.Object);
    
    // Act
    c.Stop();
    
    // Assert
    Assert.False(c.EmAndamento());
    Assert.Equal(TimeSpan.FromMilliseconds(150), c.Total);
}
```

Observação: Para implementar o último exemplo, seria necessário refatorar a classe `Cronometro` para aceitar uma interface `IRelogio` que forneceria o tempo atual, permitindo assim a injeção de um mock para testes. A interface poderia ser assim:

```csharp
public interface IRelogio
{
    DateTime Agora();
}

public class RelogioReal : IRelogio
{
    public DateTime Agora() => DateTime.Now;
}

public class Cronometro
{
    private readonly IRelogio _relogio;
    private DateTime _dataInicio;
    private bool _emAndamento;
    
    public Cronometro(IRelogio relogio = null)
    {
        _relogio = relogio ?? new RelogioReal();
        _dataInicio = _relogio.Agora();
        _emAndamento = true;
        Total = TimeSpan.Zero;
    }
    
    public DateTime DataInicio => _dataInicio;
    public TimeSpan Total { get; private set; }
    
    public bool EmAndamento() => _emAndamento;
    
    public void Stop()
    {
        if (_emAndamento)
        {
            Total = _relogio.Agora() - _dataInicio;
            _emAndamento = false;
        }
    }
}
```
