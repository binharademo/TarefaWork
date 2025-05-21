using TarefasLibrary;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Negocio;
using TarefasLibrary.Modelo;

namespace Tests_Tarefas
{
    public class TesteEditarUsuario
    {
        [Fact]
        public void EditarUsuarioManual_ModificaEPersisteValores()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);
            var setor = new Setor("Setor Teste", 1);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, setor);
            servico.Criar(usuario);

            // Act
            var usuarioParaEditar = servico.Buscar(usuario.Id);
            usuarioParaEditar.Nome = "binhara_editado";
            usuarioParaEditar.Senha = "456";
            usuarioParaEditar.FuncaoUsuario = Usuario.Funcao.Dev;
            usuarioParaEditar.SetorUsuario = setor;

            bool resultadoEdicao = servico.Editar(usuarioParaEditar.Id, usuarioParaEditar.Nome, usuarioParaEditar.Senha, usuarioParaEditar.FuncaoUsuario, usuarioParaEditar.SetorUsuario);

            // Assert
            Assert.True(resultadoEdicao);
            var usuarioEditado = servico.Buscar(usuario.Id);
            Assert.NotNull(usuarioEditado);
            Assert.Equal("binhara_editado", usuarioEditado.Nome);
            Assert.Equal("456", usuarioEditado.Senha);
            Assert.Equal(Usuario.Funcao.Dev, usuarioEditado.FuncaoUsuario);
            Assert.Equal(setor, usuarioEditado.SetorUsuario);

            var usuarioPersistido = repositorio.BuscarPorId(usuario.Id); 
            Assert.NotNull(usuarioPersistido);
            Assert.Equal("binhara_editado", usuarioPersistido.Nome);
            Assert.Equal("456", usuarioPersistido.Senha);
            Assert.Equal(Usuario.Funcao.Dev, usuarioPersistido.FuncaoUsuario);
            Assert.Equal(setor, usuarioPersistido.SetorUsuario);
        }

        [Fact]
        public void EditarUsuario_Inexistente_DeveRetornarFalse()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);
            var setor = new Setor("Setor Teste", 1);

            int idInexistente = -1; 

            // Act
            var resultado = servico.Editar(idInexistente,"adair da silva","nova_senha",Usuario.Funcao.Analista,setor);

            // Assert
            Assert.False(resultado); 
        }

        [Fact]
        public void EditarNomeUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);
            var setor = new Setor("Setor Teste", 1);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, setor);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, "marcelo", usuario.Senha, usuario.FuncaoUsuario, usuario.SetorUsuario);
            

            // Assert
            
            Assert.True(usuarioEditado);
            Assert.Equal("marcelo", usuario.Nome);
        }

        [Fact]
        public void EditarSenhaUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);
            var setor = new Setor("Setor Teste", 1);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, setor);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id,usuario.Nome,"NovaSenha", usuario.FuncaoUsuario, usuario.SetorUsuario);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal("NovaSenha", usuario.Senha);
        }


        [Fact]
        public void EditarFuncaoUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);
            var setor = new Setor("Setor Teste", 1);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, setor);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha, Usuario.Funcao.Analista, usuario.SetorUsuario);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal(Usuario.Funcao.Analista, usuario.FuncaoUsuario);
        }

        [Fact]
        public void EditarSetorUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);

            var setor = new Setor("Setor Teste", 1);
            var setor2 = new Setor("Setor Teste 2", 2);

            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, setor);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha, usuario.FuncaoUsuario, setor2);

            // Assert
            Assert.True(usuarioEditado);
            Assert.Equal(setor2, usuario.SetorUsuario); // corrigido
            Assert.Equal("binhara", usuario.Nome);
            Assert.Equal("123", usuario.Senha);
            Assert.Equal(Usuario.Funcao.Marketing, usuario.FuncaoUsuario);
        }


        // TODO: Considerar adicionar validações para campos inválidos (nome vazio, senha fraca, etc.)
        [Fact]
        public void EditarUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);
            var setor = new Setor("Setor Teste", 1);
            var setor2 = new Setor("Setor Teste 2", 2);

            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, setor);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, "Alessandro Binhara", "SenhaNova", Usuario.Funcao.Analista, setor2);

            // Assert
            Assert.True(usuarioEditado);
            Assert.Equal("Alessandro Binhara", usuario.Nome);
            Assert.Equal("SenhaNova", usuario.Senha);
            Assert.Equal(Usuario.Funcao.Analista, usuario.FuncaoUsuario);
            Assert.Equal(setor2, usuario.SetorUsuario); 

            var usuarioDoRepositorio = repositorio.BuscarPorId(usuario.Id);
            Assert.Same(usuario, usuarioDoRepositorio);
        }

        [Fact]
        public void EditarUsuario_ComIdInvalido_DeveRetornarFalse()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);
            var idInvalido = -1;
            var setor = new Setor("Setor Teste", 1);

            // Act
            var resultado = servico.Editar(idInvalido, "Nome", "Senha123", Usuario.Funcao.Analista, setor);

            // Assert
            Assert.False(resultado);
        }


    }
}
