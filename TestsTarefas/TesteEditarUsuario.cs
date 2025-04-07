using Xunit;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Negocio;
using TarefasLibrary.Modelo;

namespace Tests_Tarefas
{
    public class TesteEditarUsuario
    {
        [Fact]
        public void EditarUsuarioManual()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", "dev", "ti");
            servico.Criar(usuario);

            // Act
            var usuarioParaEditar = servico.Buscar(usuario.Id);
            usuarioParaEditar.Nome = "binhara_editado";
            usuarioParaEditar.Senha = "456";
            usuarioParaEditar.Funcao = "dev1";
            usuarioParaEditar.Setor = "ti1";

            // Assert
            var usuarioEditado = servico.Buscar(usuario.Id);
            Assert.NotNull(usuarioEditado);
            Assert.Equal("binhara_editado", usuarioEditado.Nome);
            Assert.Equal("456", usuarioEditado.Senha);
            Assert.Equal("dev1", usuarioEditado.Funcao);
            Assert.Equal("ti1", usuarioEditado.Setor);
        }

        [Fact]
        public void EditarNomeUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", "dev", "ti");
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, "marcelo",usuario.Senha, usuario.Funcao, usuario.Setor);
            

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


            Usuario usuario = new Usuario("binhara", "123", "dev", "ti");
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id,usuario.Nome,"NovaSenha", usuario.Funcao, usuario.Setor);


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


            Usuario usuario = new Usuario("binhara", "123", "Marketing", "ti");
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha,"analista", usuario.Setor);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal("analista", usuario.Funcao);
        }


        [Fact]
        public void EditarSetorUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", "Marketing", "ti");
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha,usuario.Funcao,"Marketing");


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal("Marketing", usuario.Funcao);
        }


        [Fact]
        public void EditarUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", "Marketing", "ti");
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id,"Alessandro Binhara", "SenhaNova","Analista Pleno", "Marketing");


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal("Alessandro Binhara", usuario.Nome);
            Assert.Equal("SenhaNova", usuario.Senha);
            Assert.Equal("Analista Pleno", usuario.Funcao);
            Assert.Equal("Marketing", usuario.Setor);
        }
    }
}
