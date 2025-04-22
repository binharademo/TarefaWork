# TODOs para TesteEditarUsuario.cs

## TODO 1: Este teste modifica o objeto diretamente, mas não chama nenhum método de persistência

```csharp
[Fact]
public void EditarUsuarioManual_DeveModificarEPersistirAlteracoes()
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);

    Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario);

    // Act
    var usuarioParaEditar = servico.Buscar(usuario.Id);
    usuarioParaEditar.Nome = "binhara_editado";
    usuarioParaEditar.Senha = "456";
    usuarioParaEditar.FuncaoUsuario = Usuario.Funcao.Dev;
    usuarioParaEditar.SetorUsuario = Usuario.Setor.Ti;
    
    // Chamar método de persistência
    bool resultadoEdicao = servico.Editar(usuarioParaEditar.Id, usuarioParaEditar.Nome, 
        usuarioParaEditar.Senha, usuarioParaEditar.FuncaoUsuario, usuarioParaEditar.SetorUsuario);

    // Assert
    Assert.True(resultadoEdicao);
    var usuarioEditado = servico.Buscar(usuario.Id);
    Assert.NotNull(usuarioEditado);
    Assert.Equal("binhara_editado", usuarioEditado.Nome);
    Assert.Equal("456", usuarioEditado.Senha);
    Assert.Equal(Usuario.Funcao.Dev, usuarioEditado.FuncaoUsuario);
    Assert.Equal(Usuario.Setor.Ti, usuarioEditado.SetorUsuario);
}
```

## TODO 2: Adicionar verificação se as alterações são persistidas no repositório

```csharp
[Fact]
public void EditarUsuarioManual_DeveVerificarPersistenciaDasAlteracoes()
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);

    Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario);

    // Act
    var usuarioParaEditar = servico.Buscar(usuario.Id);
    usuarioParaEditar.Nome = "binhara_editado";
    usuarioParaEditar.Senha = "456";
    usuarioParaEditar.FuncaoUsuario = Usuario.Funcao.Dev;
    usuarioParaEditar.SetorUsuario = Usuario.Setor.Ti;
    
    // Obter uma nova instância do serviço com o mesmo repositório para verificar persistência
    var novoServico = new UsuarioServico(repositorio);
    
    // Assert
    var usuarioEditado = novoServico.Buscar(usuario.Id);
    Assert.NotNull(usuarioEditado);
    Assert.Equal("binhara_editado", usuarioEditado.Nome);
    Assert.Equal("456", usuarioEditado.Senha);
    Assert.Equal(Usuario.Funcao.Dev, usuarioEditado.FuncaoUsuario);
    Assert.Equal(Usuario.Setor.Ti, usuarioEditado.SetorUsuario);
}
```

## TODO 3: Testar caso de edição de usuário inexistente

```csharp
[Fact]
public void EditarUsuarioInexistente_DeveRetornarFalso()
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);
    
    int idInexistente = 9999;

    // Act
    bool resultado = servico.Editar(idInexistente, "Nome Qualquer", "senha123", 
        Usuario.Funcao.Dev, Usuario.Setor.Ti);

    // Assert
    Assert.False(resultado);
}
```

## TODO 4: Corrigir a verificação no Assert - está verificando a função quando deveria verificar o setor

```csharp
[Fact]
public void EditarSetorUsuario()
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);

    Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
    servico.Criar(usuario);

    // Act
    var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha, 
        usuario.FuncaoUsuario, Usuario.Setor.Marketing);

    // Assert
    Assert.True(usuarioEditado);
    // Corrigir verificação para checar o setor, não a função
    Assert.Equal(Usuario.Setor.Marketing, usuario.SetorUsuario);
}
```

## TODO 5: Adicionar verificação para confirmar que apenas o setor foi alterado

```csharp
[Fact]
public void EditarSetorUsuario_DeveAlterarApenasOSetor()
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);

    string nomeOriginal = "binhara";
    string senhaOriginal = "123";
    Usuario.Funcao funcaoOriginal = Usuario.Funcao.Marketing;
    
    Usuario usuario = new Usuario(nomeOriginal, senhaOriginal, funcaoOriginal, Usuario.Setor.Ti);
    servico.Criar(usuario);

    // Act
    var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha,
        usuario.FuncaoUsuario, Usuario.Setor.Marketing);

    // Assert
    Assert.True(usuarioEditado);
    Assert.Equal(Usuario.Setor.Marketing, usuario.SetorUsuario);
    
    // Verificar que outros campos não foram alterados
    Assert.Equal(nomeOriginal, usuario.Nome);
    Assert.Equal(senhaOriginal, usuario.Senha);
    Assert.Equal(funcaoOriginal, usuario.FuncaoUsuario);
}
```

## TODO 6: Adicionar teste para edição de usuário com ID inválido

```csharp
[Fact]
public void EditarUsuarioComIdInvalido_DeveRetornarFalso()
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);

    // Act
    var resultado = servico.Editar(-1, "Nome Inválido", "senha123", 
        Usuario.Funcao.Dev, Usuario.Setor.Ti);

    // Assert
    Assert.False(resultado);
}
```

## TODO 7: Verificar se o objeto retornado é o mesmo que foi editado (comparação de referência)

```csharp
[Fact]
public void EditarUsuario_DeveRetornarOMesmoObjeto()
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);

    Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
    servico.Criar(usuario);

    // Act
    var usuarioEditado = servico.Editar(usuario.Id, "Alessandro Binhara", "SenhaNova", 
        Usuario.Funcao.Analista, Usuario.Setor.Marketing);
    var usuarioRecuperado = servico.Buscar(usuario.Id);

    // Assert
    Assert.True(usuarioEditado);
    Assert.Same(usuario, usuarioRecuperado); // Verifica se é a mesma referência de objeto
}
```

## TODO 8: Considerar adicionar validações para campos inválidos (nome vazio, senha fraca, etc.)

```csharp
[Theory]
[InlineData("", "senha123", "Nome não pode ser vazio")]
[InlineData("Nome", "", "Senha não pode ser vazia")]
[InlineData("Nome", "123", "Senha deve ter pelo menos 6 caracteres")]
public void EditarUsuarioComDadosInvalidos_DeveLancarExcecao(string nome, string senha, string mensagemEsperada)
{
    // Arrange
    var repositorio = new UsuarioMemoriaRepositorio();
    var servico = new UsuarioServico(repositorio);

    Usuario usuario = new Usuario("binhara", "senha123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
    servico.Criar(usuario);

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => 
        servico.Editar(usuario.Id, nome, senha, usuario.FuncaoUsuario, usuario.SetorUsuario));
    
    Assert.Contains(mensagemEsperada, exception.Message);
}
```
