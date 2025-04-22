# TODOs para TesteComentarios.cs

## TODO 1: Melhorar o teste para validar casos extremos como strings muito longas e caracteres especiais.

```csharp
[Theory]
[InlineData("ABCDEFG")]
[InlineData("abcdefg")]
[InlineData("!@#$%¨&*()")]
[InlineData("1234567890")]
[InlineData("")]
// Adicionar casos extremos
[InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.")] // String muito longa
[InlineData("♥♦♣♠")]  // Caracteres especiais Unicode
[InlineData("😀🙂🙁😢")] // Emojis
public void AdicionarComentario(string descricao)
{
    //arrange
    DateTime dataCriacao = new DateTime(2024, 04, 01);
    //act
    Comentario comentario01 = new Comentario(descricao, dataCriacao);

    //assert
    Assert.Equal(descricao, comentario01.Descricao);
    Assert.Equal(new DateTime(2024, 04, 01), comentario01.DataCriacao);
    Assert.NotNull(descricao);
}
```

## TODO 2: Adicionar validação para o caso de string vazia, que deveria lançar uma exceção.

```csharp
[Fact]
public void AdicionarComentarioVazio_DeveLancarExcecao()
{
    //arrange
    DateTime dataCriacao = new DateTime(2024, 04, 01);
    string descricaoVazia = "";
    
    //act & assert
    var exception = Assert.Throws<ArgumentException>(() => 
        new Comentario(descricaoVazia, dataCriacao));
    
    Assert.Contains("descrição", exception.Message.ToLower());
}
```

## TODO 3: Melhorar o teste para verificar se o comentário específico foi adicionado à lista

```csharp
[Fact]
public void ListarComentario()
{
    //arrange
    Comentario.ListaComentarios.Clear(); // Limpar lista para garantir isolamento
    DateTime dataCriacao = new DateTime(2025, 04, 01);
    Comentario comentario01 = new Comentario("Teste Comentario", dataCriacao);
    
    //act
    comentario01.SalvarComentario();
    
    //assert
    Assert.NotEmpty(Comentario.ListaComentarios);
    Assert.Contains(comentario01, Comentario.ListaComentarios);
    Assert.Equal("Teste Comentario", Comentario.ListaComentarios.First().Descricao);
    Assert.Equal(dataCriacao, Comentario.ListaComentarios.First().DataCriacao);
}
```

## TODO 4: Adicionar limpeza da lista de comentários antes do teste para garantir isolamento

```csharp
[Fact]
public void ListarComentario()
{
    //arrange
    Comentario.ListaComentarios.Clear(); // Limpar lista para garantir isolamento
    DateTime dataCriacao = new DateTime(2025, 04, 01);
    Comentario comentario01 = new Comentario("Teste Comentario", dataCriacao);
    
    //act
    comentario01.SalvarComentario();
    
    //assert
    Assert.NotEmpty(Comentario.ListaComentarios);
}
```

## TODO 5: Adicionar teste para busca de comentário inexistente

```csharp
[Fact]
public void BuscaComentarioInexistente()
{
    //arrange
    Comentario.ListaComentarios.Clear(); // Limpar lista para garantir isolamento
    Guid idInexistente = Guid.NewGuid();
    
    //act
    Comentario comentarioInexistente = new Comentario(idInexistente);
    bool resultado = comentarioInexistente.BuscarComentario();
    
    //assert
    Assert.False(resultado);
    Assert.Null(comentarioInexistente.Descricao);
}
```

## TODO 6: Limpar a lista de comentários antes do teste para garantir isolamento

```csharp
[Fact]
public void BuscaComentario()
{
    //arrange
    Comentario.ListaComentarios.Clear(); // Limpar lista para garantir isolamento
    
    DateTime dataCriacao = new DateTime(2025, 04, 04);
    string descricao = "oiii";
    Comentario comentario = new Comentario(descricao, dataCriacao);
    
    //act
    comentario.SalvarComentario();
    
    Comentario Cresultado = new Comentario(comentario.Id);
    bool result = Cresultado.BuscarComentario();
    
    //Assert
    Assert.Equal(descricao, Cresultado.Descricao);
    Assert.Equal(dataCriacao, Cresultado.DataCriacao);
    Assert.True(result);
}
```

## TODO 7: Reorganizar o teste para separar claramente as fases de Arrange, Act e Assert

```csharp
[Fact]
public void TestVincularUmComentarioAUmaTarefa()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    StatusTarefa status = new StatusTarefa(StatusTarefa.Status.ToDo);
    
    Tarefa tarefa01 = new Tarefa(01, "titulo", status, criador, responsavel, new DateTime(2025, 12, 31), "descricao", Tarefa.Prioridade.Alta);
    Tarefa tarefa02 = new Tarefa(02, "titulo", status, criador, responsavel, new DateTime(2025, 12, 31), "descricao", Tarefa.Prioridade.Alta);
    
    Comentario comentario1 = new Comentario("Comentario 1", new DateTime(2025, 12, 31));
    Comentario comentario2 = new Comentario("Comentario 2", new DateTime(2025, 12, 31));
    Comentario comentario3 = new Comentario("Comentario 3", new DateTime(2025, 12, 31));
    Comentario comentario4 = new Comentario("Comentario 4", new DateTime(2025, 12, 31));
    Comentario comentario5 = new Comentario("Comentario 5", new DateTime(2025, 12, 31));
    
    //act
    tarefa01.Adicionar(comentario1);
    tarefa01.Adicionar(comentario2);
    tarefa01.Adicionar(comentario3);
    
    tarefa02.Adicionar(comentario4);
    tarefa02.Adicionar(comentario5);
    
    List<Comentario> result1 = tarefa01.ListarComentarios();
    List<Comentario> result2 = tarefa02.ListarComentarios();
    
    //assert
    Assert.Equal(3, result1.Count);
    Assert.Equal(2, result2.Count);
}
```

## TODO 8: Verificar o conteúdo específico dos comentários retornados, não apenas a contagem

```csharp
[Fact]
public void TestVincularUmComentarioAUmaTarefa()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    StatusTarefa status = new StatusTarefa(StatusTarefa.Status.ToDo);
    
    Tarefa tarefa01 = new Tarefa(01, "titulo", status, criador, responsavel, new DateTime(2025, 12, 31), "descricao", Tarefa.Prioridade.Alta);
    
    Comentario comentario1 = new Comentario("Comentario 1", new DateTime(2025, 12, 31));
    Comentario comentario2 = new Comentario("Comentario 2", new DateTime(2025, 12, 31));
    Comentario comentario3 = new Comentario("Comentario 3", new DateTime(2025, 12, 31));
    
    //act
    tarefa01.Adicionar(comentario1);
    tarefa01.Adicionar(comentario2);
    tarefa01.Adicionar(comentario3);
    
    List<Comentario> result = tarefa01.ListarComentarios();
    
    //assert
    Assert.Equal(3, result.Count);
    Assert.Contains(comentario1, result);
    Assert.Contains(comentario2, result);
    Assert.Contains(comentario3, result);
    Assert.Equal("Comentario 1", result[0].Descricao);
    Assert.Equal("Comentario 2", result[1].Descricao);
    Assert.Equal("Comentario 3", result[2].Descricao);
}
```

## TODO 9: Testar a remoção de comentários de uma tarefa

```csharp
[Fact]
public void RemoverComentarioDeTarefa()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(criador);
    Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(responsavel);
    StatusTarefa status = new StatusTarefa(StatusTarefa.Status.ToDo);
    
    Tarefa tarefa = new Tarefa(01, "titulo", status, criador, responsavel, new DateTime(2025, 12, 31), "descricao", Tarefa.Prioridade.Alta);
    
    Comentario comentario1 = new Comentario("Comentario 1", new DateTime(2025, 12, 31));
    Comentario comentario2 = new Comentario("Comentario 2", new DateTime(2025, 12, 31));
    
    tarefa.Adicionar(comentario1);
    tarefa.Adicionar(comentario2);
    
    //act
    bool removido = tarefa.RemoverComentario(comentario1);
    List<Comentario> comentarios = tarefa.ListarComentarios();
    
    //assert
    Assert.True(removido);
    Assert.Single(comentarios);
    Assert.DoesNotContain(comentario1, comentarios);
    Assert.Contains(comentario2, comentarios);
}
```
