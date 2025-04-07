using TarefasLibrary;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Negocio;

namespace ComentariosTests
{
    public class TesteComentarios
    {
        [Theory]
        [InlineData("ABCDEFG")]
        [InlineData("abcdefg")]
        [InlineData("!@#$%¨&*()")]
        [InlineData("1234567890")]
        [InlineData("")]

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

        [Fact]
        public void ListarComentario()
        {
            //arrange
            DateTime dataCriacao = new DateTime(2025, 04, 01);
            Comentario comentario01 = new Comentario("Teste Comentario", dataCriacao);
            //act
            comentario01.SalvarComentario();
            //assert
            Assert.NotEmpty(Comentario.ListaComentarios);
        }

        [Fact]
        public void ListarComentarioPorData()
        {
            // arrange

            DateTime data1 = new DateTime(2025, 03, 04);
            DateTime data2 = new DateTime(2025, 04, 04);

            Comentario comentario1 = new Comentario("ola", data1);
            Comentario comentario2 = new Comentario("oiii", data2);

            //act
            comentario1.SalvarComentario();
            comentario2.SalvarComentario();

            var ListaOrdenada = Comentario.ListaComentarios.OrderBy(c => c.DataCriacao).ToList();

            //assert

            Assert.Equal(comentario1, ListaOrdenada.First());
            Assert.Equal(comentario2, ListaOrdenada.Last());

        }

        [Fact]
        public void BuscaComentario()
        {
            //arrange

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

        [Fact]
        public void TestVicularUmComentarioAUmTarefa()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //arrange
            Tarefa tarefa01 = new Tarefa(01, "titulo", "iniciado", criador, responsavel, new DateTime(2025, 12, 31), "descricao");
            Tarefa tarefa02 = new Tarefa(02, "titulo", "iniciado", criador, responsavel, new DateTime(2025, 12, 31), "descricao");

            tarefa01.Adicionar(new Comentario("Comentario 1", new DateTime(2025, 12, 31)));
            tarefa01.Adicionar(new Comentario("Comentario 2", new DateTime(2025, 12, 31)));
            tarefa01.Adicionar(new Comentario("Comentario 3", new DateTime(2025, 12, 31)));

            tarefa02.Adicionar(new Comentario("Comentario 4", new DateTime(2025, 12, 31)));
            tarefa02.Adicionar(new Comentario("Comentario 5", new DateTime(2025, 12, 31)));

            //act
            List<Comentario> result1 = tarefa01.ListarComentarios();
            List<Comentario> result2 = tarefa02.ListarComentarios();


            //Assert
            Assert.Equal(result1.Count, 3);
            Assert.Equal(result2.Count, 2);

        }
        //+---------------------------------------------------------------------------------------------------------------------------------------------------+


        [Fact]
        public void ListarComentarioServices()
        {
            //arrange
            DateTime dataCriacao = new DateTime(2025, 04, 01);
            Comentario comentario01 = new Comentario("Teste Comentario", dataCriacao);

            var repository = new ComentarioRepository();
            var service = new ComentarioServices(repository);

            //act
            service.SalvarComentario(comentario01);


            //assert
            var comentarioSalvo = service.BuscarComentario(comentario01.Id);
            Assert.NotNull(comentarioSalvo);
        }

        [Fact]
        public void ListarComentarioPorDataServices()
        {
            // Arrange
            DateTime data1 = new DateTime(2025, 03, 04);
            DateTime data2 = new DateTime(2025, 04, 04);

            Comentario comentario1 = new Comentario("ola", data1);
            Comentario comentario2 = new Comentario("oiii", data2);

            var repository = new ComentarioRepository();
            var service = new ComentarioServices(repository);

            // Act
            service.SalvarComentario(comentario1);
            service.SalvarComentario(comentario2);

            var listaComentarios = service.ListarComentarios();
            var listaOrdenada = listaComentarios.OrderBy(c => c.DataCriacao).ToList();

            // Assert
            Assert.Equal(comentario1.Id, listaOrdenada.First().Id);
            Assert.Equal(comentario2.Id, listaOrdenada.Last().Id);
        }

    }
}


