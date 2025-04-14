using TarefasLibrary;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;


namespace Tests_Tarefas
{
    public class UsuarioTeste
    {

        [Fact]
        public void CriarUsuario()
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            
            //act
            Usuario usuario01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(usuario01);


            //assert
            Assert.NotNull(usuario01);
         
        }


        [Fact]
        public void ListarUsuario(/*int id, string nome, string senha, string funcao, string setor*/)
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);

            servico.Criar(u01);
            servico.Criar(u02);
            servico.Criar(u03);


            //act
            var resultado = servico.ListarUsuario();

            //assert
            Assert.NotEmpty(resultado);
            Assert.Equal(3, resultado.Count);
        }





        public void SalvarEBuscar(int id, string nome, string senha, Usuario.Funcao funcao, Usuario.Setor setor)
        {
            // arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
         
            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);

            //act
            var result = servico.Buscar(id);

            //assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(nome, result.Nome);
            Assert.Equal(senha, result.Senha);
            Assert.Equal(Usuario.Funcao.Dev, result.FuncaoUsuario);
            Assert.Equal(setor, result.SetorUsuario);
        }

        [Fact]
        public void ListarUsuarios()
        {
            //arrange
            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);


            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio()); // configuracao
            servico.Criar(u01);
            servico.Criar(u02);
            servico.Criar(u03);

            //act
            var resultado = servico.ListarUsuario();

            //assert
            Assert.NotEmpty(resultado);
            Assert.Equal(3, resultado.Count);
        }

        [Fact]  
        public void t1_CadastrarUsuarioSQLITE_DeveCadastrarOUsuarioNoBD()
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioRepositorioSQLITE());

            //act
            Usuario usuario01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(usuario01);


            //assert
            Assert.NotNull(usuario01);
        }

        [Fact]
        public void t2_AtualizarUsuarioSQLITE_PegaOIDEAtualizaEleNoBD()
        {
            // Arrange
            var repositorio = new UsuarioRepositorioSQLITE();
            var servico = new UsuarioServico(repositorio);

            // Act
            bool usuarioEditado = servico.Editar(1, "guilherme", "99999", Usuario.Funcao.Dev, Usuario.Setor.Ti);


            // Assert

            Assert.True(usuarioEditado);

        }

        [Fact]
        public void t3_ListarUsuarioSQLITE_DeveListarTodosOsUsuarios()
        {
            //arrange
            var repositorio = new UsuarioRepositorioSQLITE();
            var servico = new UsuarioServico(repositorio);

            //Usuario u01 = new Usuario("binhara", "123", "dev", "ti");
            //Usuario u02 = new Usuario("binhara1", "123", "dev", "ti");
            //Usuario u03 = new Usuario("binhara2", "123", "dev", "ti");

            //servico.Criar(u01);
            //servico.Criar(u02);
            //servico.Criar(u03);


            //act
            var resultado = servico.ListarUsuario();

            //assert
            Assert.NotEmpty(resultado);
        }

        [Fact]
        public void t4_DeletarUsuarioSQLITE_DeveDeletarOUltimo()
        {
            //arrange
            var repositorio = new UsuarioRepositorioSQLITE();
            var servico = new UsuarioServico(repositorio);

            //act
            var usuario = servico.ListarUsuario().Last();
            var resultado = servico.Remover(usuario);

            //asserts

            Assert.True(resultado);
        }

        [Fact]
        public void t5_DeletarUsuarioSQLITE_NaoDeveDeletarIdInexistente()
        {
            //arrange
            var repositorio = new UsuarioRepositorioSQLITE();
            var servico = new UsuarioServico(repositorio);

            //act
            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var resultado = servico.Remover(u01);

            //asserts

            Assert.False(resultado);
        }
    }

}




