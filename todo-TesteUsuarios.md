# TODOs para TesteUsuarios.cs

## TODO 1: Verificar se o usuário foi realmente persistido no repositório

```csharp
[Fact]
public void CriarUsuario_DeveAdicionarUsuarioNoRepositorio()
{
    //arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);
    
    //act
    Usuario usuario01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario01);

    //assert
    Assert.NotNull(usuario01);
    
    // Verificar se o usuário está no repositório
    var usuarioRecuperado = servico.Buscar(usuario01.Id);
    Assert.NotNull(usuarioRecuperado);
    Assert.Equal(usuario01.Id, usuarioRecuperado.Id);
    Assert.Equal("binhara", usuarioRecuperado.Nome);
}
```

## TODO 2: Testar criação de usuário com dados inválidos (nome vazio, senha fraca, etc.)

```csharp
[Theory]
[InlineData("", "123456", "Nome não pode ser vazio")]
[InlineData("usuario", "", "Senha não pode ser vazia")]
[InlineData(null, "123456", "Nome não pode ser nulo")]
[InlineData("usuario", null, "Senha não pode ser nula")]
public void CriarUsuarioComDadosInvalidos_DeveLancarExcecao(string nome, string senha, string mensagemEsperada)
{
    //arrange
    var servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    
    //act & assert
    var exception = Assert.Throws<ArgumentException>(() => 
        new Usuario(nome, senha, Usuario.Funcao.Dev, Usuario.Setor.Ti));
    
    Assert.Contains(mensagemEsperada.ToLower(), exception.Message.ToLower());
}
```

## TODO 3: Verificar se o ID do usuário é atribuído corretamente

```csharp
[Fact]
public void CriarUsuario_DeveAtribuirIdUnico()
{
    //arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);
    
    //act
    Usuario usuario1 = new Usuario("usuario1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario1);
    
    Usuario usuario2 = new Usuario("usuario2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario2);
    
    //assert
    Assert.NotEqual(0, usuario1.Id);
    Assert.NotEqual(0, usuario2.Id);
    Assert.NotEqual(usuario1.Id, usuario2.Id);
}
```

## TODO 4: Este método não tem a anotação [Fact] ou [Theory], portanto não é executado como teste

```csharp
[Fact]
public void SalvarEBuscar_DeveEncontrarUsuarioSalvo()
{
    // arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
 
    Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(u01);
    
    string nome = "binhara";
    string senha = "123";
    Usuario.Funcao funcao = Usuario.Funcao.Dev;
    Usuario.Setor setor = Usuario.Setor.Ti;
    int id = u01.Id;

    //act
    var result = servico.Buscar(id);

    //assert
    Assert.NotNull(result);
    Assert.Equal(id, result.Id);
    Assert.Equal(nome, result.Nome);
    Assert.Equal(senha, result.Senha);
    Assert.Equal(funcao, result.FuncaoUsuario);
    Assert.Equal(setor, result.SetorUsuario);
}
```

## TODO 5: Os usuários são criados mas não são salvos no repositório

```csharp
[Fact]
public void SalvarEBuscar_DeveEncontrarTodosUsuariosSalvos()
{
    // arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
 
    Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    
    // Salvar os usuários no repositório
    servico.Criar(u01);
    servico.Criar(u02);
    servico.Criar(u03);

    //act
    var result1 = servico.Buscar(u01.Id);
    var result2 = servico.Buscar(u02.Id);
    var result3 = servico.Buscar(u03.Id);

    //assert
    Assert.NotNull(result1);
    Assert.NotNull(result2);
    Assert.NotNull(result3);
    
    Assert.Equal(u01.Nome, result1.Nome);
    Assert.Equal(u02.Nome, result2.Nome);
    Assert.Equal(u03.Nome, result3.Nome);
}
```

## TODO 6: O teste tenta buscar um usuário por ID sem ter salvo nenhum usuário com esse ID

```csharp
[Fact]
public void BuscarUsuarioInexistente_DeveRetornarNull()
{
    // arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    
    int idInexistente = 9999;

    //act
    var result = servico.Buscar(idInexistente);

    //assert
    Assert.Null(result);
}
```

## TODO 7: Verificar se os usuários retornados são exatamente os mesmos que foram criados

```csharp
[Fact]
public void ListarUsuarios_DeveRetornarTodosUsuariosCriados()
{
    //arrange
    Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);

    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    servico.Criar(u01);
    servico.Criar(u02);
    servico.Criar(u03);

    //act
    var resultado = servico.ListarUsuario();

    //assert
    Assert.NotEmpty(resultado);
    Assert.Equal(3, resultado.Count);
    
    // Verificar se os usuários específicos estão na lista
    Assert.Contains(u01, resultado);
    Assert.Contains(u02, resultado);
    Assert.Contains(u03, resultado);
    
    // Verificar os dados dos usuários
    Assert.Contains(resultado, u => u.Nome == "binhara");
    Assert.Contains(resultado, u => u.Nome == "binhara1");
    Assert.Contains(resultado, u => u.Nome == "binhara2");
}
```

## TODO 8: Testar o caso de listar quando não há usuários cadastrados

```csharp
[Fact]
public void ListarUsuariosQuandoNaoHaUsuarios_DeveRetornarListaVazia()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
    
    //act
    var resultado = servico.ListarUsuario();

    //assert
    Assert.Empty(resultado);
}
```

## TODO 9: Considerar usar SetUp/TearDown para inicializar o repositório e evitar duplicação de código

```csharp
public class UsuarioTeste : IDisposable
{
    private readonly UsuarioServico _servico;
    private readonly Usuario _usuario1;
    private readonly Usuario _usuario2;
    private readonly Usuario _usuario3;
    
    public UsuarioTeste()
    {
        // Setup - executado antes de cada teste
        var repositorio = new UsuarioMemoriaRepositorio();
        _servico = new UsuarioServico(repositorio);
        
        _usuario1 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        _usuario2 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        _usuario3 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        
        _servico.Criar(_usuario1);
        _servico.Criar(_usuario2);
        _servico.Criar(_usuario3);
    }
    
    public void Dispose()
    {
        // TearDown - executado após cada teste
        // Limpar recursos, se necessário
    }
    
    [Fact]
    public void CriarUsuario_DeveAdicionarUsuarioNoRepositorio()
    {
        //arrange
        Usuario novoUsuario = new Usuario("novoUsuario", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        
        //act
        _servico.Criar(novoUsuario);

        //assert
        var usuarioRecuperado = _servico.Buscar(novoUsuario.Id);
        Assert.NotNull(usuarioRecuperado);
        Assert.Equal(novoUsuario.Id, usuarioRecuperado.Id);
        Assert.Equal("novoUsuario", usuarioRecuperado.Nome);
    }
    
    [Fact]
    public void ListarUsuarios_DeveRetornarTodosUsuariosCriados()
    {
        //act
        var resultado = _servico.ListarUsuario();

        //assert
        Assert.NotEmpty(resultado);
        Assert.Equal(3, resultado.Count);
        Assert.Contains(_usuario1, resultado);
        Assert.Contains(_usuario2, resultado);
        Assert.Contains(_usuario3, resultado);
    }
    
    // Outros testes...
}
```

## TODO 10: Evitar usar prefixos numéricos nos nomes dos testes (t1_) - use nomes descritivos

```csharp
[Fact]  
public void CadastrarUsuarioSQLITE_DeveCadastrarOUsuarioNoBD()
{
    //arrange
    UsuarioServico servico = new UsuarioServico(new UsuarioRepositorioSQLITE());

    //act
    Usuario usuario01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario01);

    //assert
    Assert.NotNull(usuario01);
}
```

## TODO 11: Verificar se o usuário foi realmente persistido no banco de dados (buscar e confirmar)

```csharp
[Fact]  
public void CadastrarUsuarioSQLITE_DevePersistirUsuarioNoBD()
{
    //arrange
    var repositorio = new UsuarioRepositorioSQLITE();
    var servico = new UsuarioServico(repositorio);

    //act
    Usuario usuario = new Usuario("usuarioTeste", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario);
    
    // Criar nova instância do serviço para garantir que estamos buscando do BD
    var novoServico = new UsuarioServico(new UsuarioRepositorioSQLITE());
    var usuarioRecuperado = novoServico.Buscar(usuario.Id);

    //assert
    Assert.NotNull(usuarioRecuperado);
    Assert.Equal(usuario.Id, usuarioRecuperado.Id);
    Assert.Equal("usuarioTeste", usuarioRecuperado.Nome);
    Assert.Equal("123", usuarioRecuperado.Senha);
    Assert.Equal(Usuario.Funcao.Dev, usuarioRecuperado.FuncaoUsuario);
    Assert.Equal(Usuario.Setor.Ti, usuarioRecuperado.SetorUsuario);
}
```

## TODO 12: Limpar o banco de dados antes/depois do teste para garantir isolamento

```csharp
public class UsuarioTesteSQLITE : IDisposable
{
    private readonly UsuarioServico _servico;
    private readonly UsuarioRepositorioSQLITE _repositorio;
    
    public UsuarioTesteSQLITE()
    {
        // Setup - executado antes de cada teste
        _repositorio = new UsuarioRepositorioSQLITE();
        _servico = new UsuarioServico(_repositorio);
        
        // Limpar o banco de dados antes do teste
        LimparBancoDados();
    }
    
    public void Dispose()
    {
        // TearDown - executado após cada teste
        LimparBancoDados();
    }
    
    private void LimparBancoDados()
    {
        // Implementar lógica para limpar os dados de teste
        // Por exemplo:
        var usuarios = _servico.ListarUsuario();
        foreach (var usuario in usuarios)
        {
            _servico.Remover(usuario);
        }
    }
    
    [Fact]  
    public void CadastrarUsuarioSQLITE_DeveCadastrarOUsuarioNoBD()
    {
        //arrange & act
        Usuario usuario = new Usuario("usuarioTeste", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
        _servico.Criar(usuario);

        //assert
        var usuarioRecuperado = _servico.Buscar(usuario.Id);
        Assert.NotNull(usuarioRecuperado);
        Assert.Equal("usuarioTeste", usuarioRecuperado.Nome);
    }
    
    // Outros testes...
}
```

## TODO 13: Evitar usar prefixos numéricos nos nomes dos testes (t4_) - use nomes descritivos

```csharp
[Fact]
public void DeletarUsuarioSQLITE_DeveDeletarUsuarioExistente()
{
    //arrange
    var repositorio = new UsuarioRepositorioSQLITE();
    var servico = new UsuarioServico(repositorio);
    
    // Criar um usuário para depois deletá-lo
    Usuario usuario = new Usuario("usuarioParaDeletar", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario);

    //act
    var resultado = servico.Remover(usuario);

    //asserts
    Assert.True(resultado);
    
    // Verificar que o usuário não existe mais
    var usuarioRecuperado = servico.Buscar(usuario.Id);
    Assert.Null(usuarioRecuperado);
}
```

## TODO 14: Não depender de dados existentes no banco - criar o usuário a ser removido no próprio teste

```csharp
[Fact]
public void DeletarUsuarioSQLITE_DeveDeletarUsuarioCriado()
{
    //arrange
    var repositorio = new UsuarioRepositorioSQLITE();
    var servico = new UsuarioServico(repositorio);
    
    // Limpar dados existentes
    var usuariosExistentes = servico.ListarUsuario();
    foreach (var u in usuariosExistentes)
    {
        servico.Remover(u);
    }
    
    // Criar um usuário para o teste
    Usuario usuario = new Usuario("usuarioParaDeletar", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario);
    
    // Confirmar que o usuário foi criado
    var usuariosCriados = servico.ListarUsuario();
    Assert.Single(usuariosCriados);

    //act
    var resultado = servico.Remover(usuario);

    //asserts
    Assert.True(resultado);
    
    // Verificar que não há mais usuários no banco
    var usuariosAposDeletar = servico.ListarUsuario();
    Assert.Empty(usuariosAposDeletar);
}
```

## TODO 15: Verificar se o usuário foi realmente removido do banco (buscar e confirmar que não existe mais)

```csharp
[Fact]
public void DeletarUsuarioSQLITE_DeveRemoverUsuarioDoBanco()
{
    //arrange
    var repositorio = new UsuarioRepositorioSQLITE();
    var servico = new UsuarioServico(repositorio);
    
    // Criar um usuário para o teste
    Usuario usuario = new Usuario("usuarioParaDeletar", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario);
    
    // Guardar o ID para verificação posterior
    int idUsuario = usuario.Id;

    //act
    var resultado = servico.Remover(usuario);

    //asserts
    Assert.True(resultado);
    
    // Criar nova instância do serviço para garantir que estamos buscando do BD
    var novoServico = new UsuarioServico(new UsuarioRepositorioSQLITE());
    var usuarioRecuperado = novoServico.Buscar(idUsuario);
    
    Assert.Null(usuarioRecuperado);
}
```
