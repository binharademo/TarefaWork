using TarefasLibrary;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Negocio;
using TarefasLibrary.Modelo;

namespace Tests_Tarefas
{
    public class TesteEditarUsuario
    {
        // TODO: Este teste modifica o objeto diretamente, mas não chama nenhum método de persistência
        // TODO: Adicionar verificação se as alterações são persistidas no repositório
        // TODO: Testar caso de edição de usuário inexistente
        [Fact]
        public void EditarUsuarioManual()
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

            // Assert
            var usuarioEditado = servico.Buscar(usuario.Id);
            Assert.NotNull(usuarioEditado);
            Assert.Equal("binhara_editado", usuarioEditado.Nome);
            Assert.Equal("456", usuarioEditado.Senha);
            Assert.Equal(Usuario.Funcao.Dev, usuarioEditado.FuncaoUsuario);
            Assert.Equal(Usuario.Setor.Ti, usuarioEditado.SetorUsuario);
        }

        [Fact]
        public void EditarNomeUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, "marcelo",usuario.Senha, usuario.FuncaoUsuario, usuario.SetorUsuario);
            

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


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
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


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha, Usuario.Funcao.Analista, usuario.SetorUsuario);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal(Usuario.Funcao.Analista, usuario.FuncaoUsuario);
        }


        // TODO: Corrigir a verificação no Assert - está verificando a função quando deveria verificar o setor
        // TODO: Adicionar verificação para confirmar que apenas o setor foi alterado
        [Fact]
        public void EditarSetorUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha,usuario.FuncaoUsuario,Usuario.Setor.Marketing);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal(Usuario.Funcao.Marketing, usuario.FuncaoUsuario);
        }


        // TODO: Adicionar teste para edição de usuário com ID inválido
        // TODO: Verificar se o objeto retornado é o mesmo que foi editado (comparação de referência)
        // TODO: Considerar adicionar validações para campos inválidos (nome vazio, senha fraca, etc.)
        [Fact]
        public void EditarUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id,"Alessandro Binhara", "SenhaNova", Usuario.Funcao.Analista, Usuario.Setor.Marketing);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal("Alessandro Binhara", usuario.Nome);
            Assert.Equal("SenhaNova", usuario.Senha);
            Assert.Equal(Usuario.Funcao.Analista, usuario.FuncaoUsuario);
            Assert.Equal(Usuario.Setor.Marketing, usuario.SetorUsuario);
        }


    }
}
