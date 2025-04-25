# TODOs para TesteListaUsuario.cs

## TODO 1: Remover os parâmetros comentados do método de teste

```csharp
[Fact]
public void ListarUsuarioPorSetor()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

    Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Marketing);
    Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Diretoria);

    servico.Criar(u01);
    servico.Criar(u02);
    servico.Criar(u03);

    //act
    var resultado = servico.ListarUsuarioPorSetor(Usuario.Setor.Ti);

    //assert
    Assert.NotEmpty(resultado);
    Assert.Equal(Usuario.Setor.Ti, resultado[0].SetorUsuario);
}
```

## TODO 2: Verificar a quantidade exata de usuários retornados (deve ser 1)

```csharp
[Fact]
public void ListarUsuarioPorSetor_DeveRetornarApenasUsuariosDoSetorEspecificado()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

    Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Marketing);
    Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Diretoria);

    servico.Criar(u01);
    servico.Criar(u02);
    servico.Criar(u03);

    //act
    var resultado = servico.ListarUsuarioPorSetor(Usuario.Setor.Ti);

    //assert
    Assert.NotEmpty(resultado);
    Assert.Single(resultado); // Verifica que há exatamente 1 usuário
    Assert.Equal(Usuario.Setor.Ti, resultado[0].SetorUsuario);
    Assert.Equal("binhara", resultado[0].Nome); // Verifica que é o usuário correto
}
```

## TODO 3: Testar caso em que não existem usuários no setor especificado

```csharp
[Fact]
public void ListarUsuarioPorSetorInexistente_DeveRetornarListaVazia()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

    Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Marketing);

    servico.Criar(u01);
    servico.Criar(u02);

    //act
    var resultado = servico.ListarUsuarioPorSetor(Usuario.Setor.Diretoria);

    //assert
    Assert.Empty(resultado);
}
```

## TODO 4: Adicionar caso de teste para função que não tem nenhum usuário

```csharp
[Fact]
public void ListarUsuarioPorFuncaoInexistente_DeveRetornarListaVazia()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

    servico.Criar(new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));
    servico.Criar(new Usuario("binhara1", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti));
    servico.Criar(new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));

    //act
    var resultado = servico.ListarUsuarioPorFuncao(Usuario.Funcao.Analista);

    //assert
    Assert.Empty(resultado);
}
```

## TODO 5: Verificar se os usuários retornados são exatamente os esperados (não apenas a contagem)

```csharp
[Fact]
public void ListarUsuarioPorFuncao_DeveRetornarUsuariosCorretos()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

    Usuario u1 = new Usuario("usuario1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u2 = new Usuario("usuario2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u3 = new Usuario("usuario3", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
    
    servico.Criar(u1);
    servico.Criar(u2);
    servico.Criar(u3);

    //act
    var resultado = servico.ListarUsuarioPorFuncao(Usuario.Funcao.Dev);

    //assert
    Assert.Equal(2, resultado.Count);
    Assert.Contains(u1, resultado);
    Assert.Contains(u2, resultado);
    Assert.DoesNotContain(u3, resultado);
    
    // Verificar que todos os usuários retornados têm a função correta
    foreach (var usuario in resultado)
    {
        Assert.Equal(Usuario.Funcao.Dev, usuario.FuncaoUsuario);
    }
}
```

## TODO 6: Considerar usar SetUp/TearDown para inicializar o repositório e evitar duplicação de código

```csharp
public class TesteListaUsuario : IDisposable
{
    private readonly UsuarioServico _servico;
    private readonly Usuario _usuarioDev1;
    private readonly Usuario _usuarioDev2;
    private readonly Usuario _usuarioMarketing1;
    private readonly Usuario _usuarioMarketing2;
    private readonly Usuario _usuarioAnalista;
    
    public TesteListaUsuario()
    {
        // Setup - executado antes de cada teste
        _servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
        
        _usuarioDev1 = new Usuario("dev1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        _usuarioDev2 = new Usuario("dev2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        _usuarioMarketing1 = new Usuario("marketing1", "123", Usuario.Funcao.Marketing, Usuario.Setor.Marketing);
        _usuarioMarketing2 = new Usuario("marketing2", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
        _usuarioAnalista = new Usuario("analista", "123", Usuario.Funcao.Analista, Usuario.Setor.Diretoria);
        
        _servico.Criar(_usuarioDev1);
        _servico.Criar(_usuarioDev2);
        _servico.Criar(_usuarioMarketing1);
        _servico.Criar(_usuarioMarketing2);
        _servico.Criar(_usuarioAnalista);
    }
    
    public void Dispose()
    {
        // TearDown - executado após cada teste
        // Limpar recursos, se necessário
    }
    
    [Fact]
    public void ListarUsuarioPorSetor_DeveRetornarApenasUsuariosDoSetorEspecificado()
    {
        // Act
        var resultado = _servico.ListarUsuarioPorSetor(Usuario.Setor.Ti);
        
        // Assert
        Assert.Equal(3, resultado.Count);
        Assert.Contains(_usuarioDev1, resultado);
        Assert.Contains(_usuarioDev2, resultado);
        Assert.Contains(_usuarioMarketing2, resultado);
        Assert.DoesNotContain(_usuarioMarketing1, resultado);
        Assert.DoesNotContain(_usuarioAnalista, resultado);
    }
    
    [Theory]
    [InlineData(Usuario.Funcao.Analista, 1)]
    [InlineData(Usuario.Funcao.Marketing, 2)]
    [InlineData(Usuario.Funcao.Dev, 2)]
    public void ListarUsuarioPorFuncao_DeveRetornarQuantidadeCorretaDeUsuarios(Usuario.Funcao funcao, int total)
    {
        // Act
        var resultado = _servico.ListarUsuarioPorFuncao(funcao);
        
        // Assert
        Assert.Equal(total, resultado.Count);
        Assert.Empty(resultado.Where(u => u.FuncaoUsuario != funcao));
    }
}
```
