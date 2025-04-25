using TarefasLibrary.Repositorio;
using TarefasLibrary.Negocio;
using TarefasLibrary.Modelo;

namespace Tests_Tarefas
{
    public class TesteComentarios
    {
        // TODO: Melhorar o teste para validar casos extremos como strings muito longas e caracteres especiais.
        // TODO: Adicionar validação para o caso de string vazia, que deveria lançar uma exceção.
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

        // TODO: Melhorar o teste para verificar se o comentário específico foi adicionado à lista
        // TODO: Adicionar limpeza da lista de comentários antes do teste para garantir isolamento
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

        // TODO: Adicionar teste para busca de comentário inexistente
        // TODO: Limpar a lista de comentários antes do teste para garantir isolamento
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

        // TODO: Reorganizar o teste para separar claramente as fases de Arrange, Act e Assert
        // TODO: Verificar o conteúdo específico dos comentários retornados, não apenas a contagem
        // TODO: Testar a remoção de comentários de uma tarefa
        [Fact]
        public void TestVicularUmComentarioAUmTarefa()
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa(01, "titulo", tarefa, criador, responsavel, new DateTime(2025, 12, 31), "descricao", Tarefa.Prioridade.Alta);
            Tarefa tarefa02 = new Tarefa(02, "titulo", tarefa, criador, responsavel, new DateTime(2025, 12, 31), "descricao", Tarefa.Prioridade.Alta);

            tarefa01.Adicionar(new Comentario("Comentario 1", new DateTime(2025, 12, 31)));
            tarefa01.Adicionar(new Comentario("Comentario 2", new DateTime(2025, 12, 31)));
            tarefa01.Adicionar(new Comentario("Comentario 3", new DateTime(2025, 12, 31)));

            tarefa02.Adicionar(new Comentario("Comentario 4", new DateTime(2025, 12, 31)));
            tarefa02.Adicionar(new Comentario("Comentario 5", new DateTime(2025, 12, 31)));

            //act
            List<Comentario> result1 = tarefa01.ListarComentarios();
            List<Comentario> result2 = tarefa02.ListarComentarios();


            //Assert
            Assert.Equal(3, result1.Count);
            Assert.Equal(2, result2.Count);

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


