using TarefasLibrary.Repositorio;
using TarefasLibrary.Negocio;
using TarefasLibrary.Modelo;

namespace Tests_Tarefas
{
    public class TesteComentarios
    {
        [Theory]
        [InlineData("ABCDEFG")]
        [InlineData("abcdefg")]
        [InlineData("!@#$%¨&*()")]
        [InlineData("1234567890")]
        //[InlineData("")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.")]
        [InlineData("♥♦♣♠")]
        [InlineData("😀🙂🙁😢")]

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

        [Fact]
        public void ListarComentario()
        {
            //arrange
            Comentario.ListaComentarios.Clear();
            DateTime dataCriacao = new DateTime(2025, 04, 01);
            Comentario comentario01 = new Comentario("teste", dataCriacao);
            //act
            comentario01.SalvarComentario();
            //assert
            Assert.NotEmpty(Comentario.ListaComentarios);
            Assert.Contains(comentario01, Comentario.ListaComentarios);
            Assert.Equal("teste", Comentario.ListaComentarios.First().Descricao);
            Assert.Equal(dataCriacao, Comentario.ListaComentarios.First().DataCriacao);
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
            Comentario.ListaComentarios.Clear();
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

        // TODO: Testar a remoção de comentários de uma tarefa
        [Fact]
        public void TestVincularUmComentarioAUmTarefa()
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa(01, "titulo", tarefa, criador, responsavel, new DateTime(2025, 12, 31), "descricao", Tarefa.Prioridade.Alta);

            Comentario comentario1 = new Comentario("Comentario 01", new DateTime(2025, 12, 31));
            Comentario comentario2 = new Comentario("Comentario 02", new DateTime(2025, 12, 31));
            Comentario comentario3 = new Comentario("Comentario 03", new DateTime(2025, 12, 31));

            //act
            tarefa01.Adicionar(comentario1);
            tarefa01.Adicionar(comentario2);
            tarefa01.Adicionar(comentario3);

            List<Comentario> result = tarefa01.ListarComentarios();

            //Assert
            Assert.Equal(3, result.Count);
           
        }

        [Fact]
        public void BuscaComentarioInexistente()
        {
            //arrange
            Comentario.ListaComentarios.Clear();
            int idInexistente = 999;

            //act
            Comentario comentarioInexistente = new Comentario(idInexistente);
            bool resultado = comentarioInexistente.BuscarComentario();
            //assert
            Assert.False(resultado);
            Assert.Null(comentarioInexistente.Descricao);
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


//FALTA:
// 1 - Testar a remoção de comentários de uma tarefa




