# TODOs para TesteSetores.cs

## TODO 1: Verificar se o setor foi realmente cadastrado no repositório, não apenas o retorno do método

```csharp
[Fact]
public void Cadastrar_Setor_DeveAdicionarSetorNoRepositorio()
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);
    string nomeSetor = "Comercial";

    // Act
    Setor setor = new Setor(nomeSetor);
    var resultado = servico.Cadastrar(setor);

    // Assert
    Assert.True(resultado);
    
    // Verificar se o setor está realmente no repositório
    var setores = servico.Listar();
    Assert.Contains(setores, s => s.Id == setor.Id);
    Assert.Contains(setores, s => s.Nome == nomeSetor);
}
```

## TODO 2: Testar cadastro de setor com nome vazio ou inválido

```csharp
[Theory]
[InlineData("")]
[InlineData(null)]
[InlineData("   ")]
public void Cadastrar_SetorComNomeInvalido_DeveLancarExcecao(string nomeInvalido)
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => 
        new Setor(nomeInvalido));
    
    Assert.Contains("nome", exception.Message.ToLower());
}
```

## TODO 3: Testar tentativa de cadastrar setor com nome duplicado

```csharp
[Fact]
public void Cadastrar_SetorComNomeDuplicado_DeveRetornarFalso()
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);
    string nomeSetor = "Comercial";

    Setor setor1 = new Setor(nomeSetor);
    servico.Cadastrar(setor1);

    // Act
    Setor setor2 = new Setor(nomeSetor);
    var resultado = servico.Cadastrar(setor2);

    // Assert
    Assert.False(resultado);
    
    // Verificar que apenas um setor com esse nome existe no repositório
    var setores = servico.Listar().Where(s => s.Nome == nomeSetor).ToList();
    Assert.Single(setores);
}
```

## TODO 4: Testar edição de setor inexistente (deve retornar false)

```csharp
[Fact]
public void EditarSetorInexistente_DeveRetornarFalso()
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);
    
    int idInexistente = 9999;

    // Act
    var resultadoEdicao = servico.Editar(idInexistente, "Novo Nome", true);

    // Assert
    Assert.False(resultadoEdicao);
}
```

## TODO 5: Verificar se a edição persiste no repositório

```csharp
[Fact]
public void EditarSetor_DeveAtualizarNomeNoRepositorio()
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);

    Setor setor = new Setor("Comercial");
    servico.Cadastrar(setor);
    
    string novoNome = "Financeiro";

    // Act
    var resultadoEdicao = servico.Editar(setor.Id, novoNome, true);
    
    // Obter uma nova instância do serviço com o mesmo repositório para verificar persistência
    var novoServico = new SetorServico(repositorio);
    var setorEditado = novoServico.Listar().FirstOrDefault(s => s.Id == setor.Id);

    // Assert
    Assert.True(resultadoEdicao);
    Assert.NotNull(setorEditado);
    Assert.Equal(novoNome, setorEditado.Nome);
}
```

## TODO 6: Testar edição com nome inválido ou duplicado

```csharp
[Theory]
[InlineData("")]
[InlineData(null)]
[InlineData("   ")]
public void EditarSetorComNomeInvalido_DeveLancarExcecao(string nomeInvalido)
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);

    Setor setor = new Setor("Comercial");
    servico.Cadastrar(setor);

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => 
        servico.Editar(setor.Id, nomeInvalido, true));
    
    Assert.Contains("nome", exception.Message.ToLower());
}

[Fact]
public void EditarSetorComNomeDuplicado_DeveRetornarFalso()
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);

    Setor setor1 = new Setor("Comercial");
    servico.Cadastrar(setor1);
    
    Setor setor2 = new Setor("Financeiro");
    servico.Cadastrar(setor2);

    // Act
    var resultadoEdicao = servico.Editar(setor2.Id, "Comercial", true);

    // Assert
    Assert.False(resultadoEdicao);
    
    // Verificar que o nome não foi alterado
    var setorNaoEditado = servico.Listar().FirstOrDefault(s => s.Id == setor2.Id);
    Assert.Equal("Financeiro", setorNaoEditado.Nome);
}
```

## TODO 7: Verificar se o setor foi realmente removido do repositório (listar e confirmar que não está presente)

```csharp
[Fact]
public void RemoverSetor_DeveRemoverDoRepositorio()
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);
    Setor setor = new Setor("Comercial");
    servico.Cadastrar(setor);
    
    // Guardar o ID para verificação posterior
    int idSetor = setor.Id;
    
    // Act
    var resultadoRemocao = servico.Remover(setor);
    
    // Assert
    Assert.True(resultadoRemocao);
    
    // Verificar que o setor não está mais no repositório
    var setores = servico.Listar();
    Assert.DoesNotContain(setores, s => s.Id == idSetor);
    Assert.DoesNotContain(setores, s => s.Nome == "Comercial");
}
```

## TODO 8: Testar remoção de setor inexistente

```csharp
[Fact]
public void RemoverSetorInexistente_DeveRetornarFalso()
{
    // Arrange
    var repositorio = new SetorRepositorio();
    var servico = new SetorServico(repositorio);
    
    // Criar um setor que não foi adicionado ao repositório
    Setor setorInexistente = new Setor("SetorInexistente");
    
    // Act
    var resultadoRemocao = servico.Remover(setorInexistente);
    
    // Assert
    Assert.False(resultadoRemocao);
}
```

## TODO 9: Testar remoção de setor que está sendo utilizado por usuários (deve falhar ou tratar adequadamente)

```csharp
[Fact]
public void RemoverSetorEmUso_DeveRetornarFalsoOuLancarExcecao()
{
    // Arrange
    var repositorioSetor = new SetorRepositorio();
    var servicoSetor = new SetorServico(repositorioSetor);
    
    var repositorioUsuario = new UsuarioMemoriaRepositorio();
    var servicoUsuario = new UsuarioServico(repositorioUsuario);
    
    // Criar e cadastrar o setor
    Setor setor = new Setor("TI");
    servicoSetor.Cadastrar(setor);
    
    // Criar e cadastrar um usuário que usa o setor
    Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servicoUsuario.Criar(usuario);
    
    // Act
    var resultadoRemocao = servicoSetor.Remover(setor);
    
    // Assert - O comportamento esperado depende da implementação
    // Opção 1: Deve retornar falso se o setor estiver em uso
    Assert.False(resultadoRemocao);
    
    // Opção 2: Ou deve lançar uma exceção informando que o setor está em uso
    // var exception = Assert.Throws<InvalidOperationException>(() => servicoSetor.Remover(setor));
    // Assert.Contains("em uso", exception.Message.ToLower());
    
    // Verificar que o setor ainda existe no repositório
    var setores = servicoSetor.Listar();
    Assert.Contains(setores, s => s.Id == setor.Id);
}
```
