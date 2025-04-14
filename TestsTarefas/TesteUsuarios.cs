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
            Usuario usuario01 = new Usuario("binhara", "123", "dev", "ti");
            servico.Criar(usuario01);


            //assert
            Assert.NotNull(usuario01);
         
        }


        [Fact]
        public void ListarUsuario(/*int id, string nome, string senha, string funcao, string setor*/)
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario u01 = new Usuario("binhara", "123", "dev", "ti");
            Usuario u02 = new Usuario("binhara1", "123", "dev", "ti");
            Usuario u03 = new Usuario("binhara2", "123", "dev", "ti");

            servico.Criar(u01);
            servico.Criar(u02);
            servico.Criar(u03);


            //act
            var resultado = servico.ListarUsuario();

            //assert
            Assert.NotEmpty(resultado);
            Assert.Equal(3, resultado.Count);
        }

        public void SalvarEBuscar(int id, string nome, string senha, string funcao, string setor)
        {
            // arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
         
            Usuario u01 = new Usuario("binhara", "123", "dev", "ti");
            Usuario u02 = new Usuario("binhara1", "123", "dev", "ti");
            Usuario u03 = new Usuario("binhara2", "123", "dev", "ti");

            //act
            var result = servico.Buscar(id);

            //assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(nome, result.Nome);
            Assert.Equal(senha, result.Senha);
            Assert.Equal(funcao, result.Funcao);
            Assert.Equal(setor, result.Setor);
        }

        [Fact]
        public void ListarUsuarios()
        {
            //arrange
            Usuario u01 = new Usuario("binhara", "123", "dev", "ti");
            Usuario u02 = new Usuario("binhara1", "123", "dev", "ti");
            Usuario u03 = new Usuario("binhara2", "123", "dev", "ti");


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
        public void CadastrarUsuarioSQLITE_DeveCadastrarOUsuarioNoBD()
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioRepositorioSQLITE());

            //act
            Usuario usuario01 = new Usuario("binhara", "123", "dev", "ti");
            servico.Criar(usuario01);


            //assert
            Assert.NotNull(usuario01);
        }

        [Fact]
        public void AtualizarUsuarioSQLITE_PegaOIDEAtualizaEleNoBD()
        {
            // Arrange
            var repositorio = new UsuarioRepositorioSQLITE();
            var servico = new UsuarioServico(repositorio);

            // Act
            bool usuarioEditado = servico.Editar(1, "guilherme", "99999", "111111", "222222");


            // Assert

            Assert.True(usuarioEditado);

        }

        [Fact]
        public void ListarUsuarioSQLITE_DeveListarTodosOsUsuarios()
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
        public void DeletarUsuarioSQLITE_DeveDeletarOUltimo()
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
        public void DeletarUsuarioSQLITE_NaoDeveDeletarIdInexistente()
        {
            //arrange
            var repositorio = new UsuarioRepositorioSQLITE();
            var servico = new UsuarioServico(repositorio);

            //act
            Usuario u01 = new Usuario("binhara", "123", "dev", "ti");
            var resultado = servico.Remover(u01);

            //asserts

            Assert.False(resultado);
        }
    }
}




