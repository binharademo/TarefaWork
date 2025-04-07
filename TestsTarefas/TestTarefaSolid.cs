using TarefasLibrary.ExemploTarefaSolid;

namespace Tests_Tarefas
{
    public class TestTarefaSolid
    {
        [Fact]
        public void CriarTarefa_DeveDefinirPropriedadesCorretamente()
        {
            // Arrange
            int id = 1;
            string titulo = "Tarefa de Teste";
            string status = "Em andamento";
            string criador = "João";
            string responsavel = "Maria";
            DateTime prazo = DateTime.Now.AddDays(7);
            string descricao = "Descrição da tarefa de teste";

            // Act
            var tarefa = new TarefaSolidExemplo(id, titulo, status, criador, responsavel, prazo, descricao);

            // Assert
            Assert.Equal(id, tarefa.Id);
            Assert.Equal(titulo, tarefa.Titulo);
            Assert.Equal(status, tarefa.Status);
            Assert.Equal(criador, tarefa.Criador);
            Assert.Equal(responsavel, tarefa.Responsavel);
            Assert.Equal(prazo, tarefa.Prazo);
            Assert.Equal(descricao, tarefa.Descricao);
            Assert.True(tarefa.DataCriacao <= DateTime.Now);
        }

        [Fact]
        public void CriarTarefa_ComTituloVazio_DeveLancarExcecao()
        {
            // Arrange
            string tituloVazio = "";
            string status = "Em andamento";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new TarefaSolidExemplo(1, tituloVazio, status, "João", "Maria", DateTime.Now.AddDays(7), "Descrição"));

            Assert.Contains("título", exception.Message.ToLower());
        }

        [Fact]
        public void CriarTarefa_ComStatusVazio_DeveLancarExcecao()
        {
            // Arrange
            string titulo = "Tarefa de Teste";
            string statusVazio = "";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new TarefaSolidExemplo(1, titulo, statusVazio, "João", "Maria", DateTime.Now.AddDays(7), "Descrição"));

            Assert.Contains("status", exception.Message.ToLower());
        }

        [Fact]
        public void TarefaMemoryRepository_AdicionarTarefa_DeveAdicionarCorretamente()
        {
            // Arrange
            // Aplicar o o solid e criar o repositorio da tarefa 
            var repository = new TarefaMemoryRepositoryExemplo();
            var tarefa = new TarefaSolidExemplo(1, "Tarefa de Teste", "Em andamento", "João", "Maria",
                                    DateTime.Now.AddDays(7), "Descrição da tarefa");

            // Act
            repository.Adicionar(tarefa);
            var tarefas = repository.ListarTodas();

            // Assert
            Assert.Single(tarefas);
            Assert.Contains(tarefa, tarefas);
        }

        [Fact]
        public void TarefaMemoryRepository_AdicionarTarefaComIdZero_DeveGerarNovoId()
        {
            // Arrange
            // Aplicaro o solid e criar o repositorio da tarefa 
            var repository = new TarefaMemoryRepositoryExemplo();
            var tarefa = new TarefaSolidExemplo(0, "Tarefa de Teste", "Em andamento", "João", "Maria",
                                    DateTime.Now.AddDays(7), "Descrição da tarefa");

            // Act
            repository.Adicionar(tarefa);

            // Assert
            Assert.Equal(1, tarefa.Id);
        }

        [Fact]
        public void TarefaMemoryRepository_AdicionarTarefaComIdDuplicado_DeveLancarExcecao()
        {
            // Arrange
            // Aplicaro o solid e criar o repositorio da tarefa 
            var repository = new TarefaMemoryRepositoryExemplo();
            var tarefa1 = new TarefaSolidExemplo(1, "Tarefa 1", "Em andamento", "João", "Maria",
                                     DateTime.Now.AddDays(7), "Descrição 1");
            var tarefa2 = new TarefaSolidExemplo(1, "Tarefa 2", "Em andamento", "Pedro", "Ana",
                                     DateTime.Now.AddDays(10), "Descrição 2");

            // Act
            repository.Adicionar(tarefa1);

            // Assert
            Assert.Throws<InvalidOperationException>(() => repository.Adicionar(tarefa2));
        }

        [Fact]
        public void TarefaMemoryRepository_BuscarPorId_DeveRetornarTarefaCorreta()
        {
            // Arrange
            // Aplicaro o solid e criar o repositorio da tarefa 
            var repository = new TarefaMemoryRepositoryExemplo();
            var tarefa1 = new TarefaSolidExemplo(1, "Tarefa 1", "Em andamento", "João", "Maria",
                                     DateTime.Now.AddDays(7), "Descrição 1");
            var tarefa2 = new TarefaSolidExemplo(2, "Tarefa 2", "Em andamento", "Pedro", "Ana",
                                     DateTime.Now.AddDays(10), "Descrição 2");
            repository.Adicionar(tarefa1);
            repository.Adicionar(tarefa2);

            // Act
            var tarefaEncontrada = repository.BuscarPorId(2);

            // Assert
            Assert.NotNull(tarefaEncontrada);
            Assert.Equal(2, tarefaEncontrada.Id);
            Assert.Equal("Tarefa 2", tarefaEncontrada.Titulo);
        }

        [Fact]
        public void TarefaMemoryRepository_BuscarPorIdInexistente_DeveRetornarNull()
        {
            // Arrange
            // Aplicaro o solid e criar o repositorio da tarefa 
            var repository = new TarefaMemoryRepositoryExemplo();
            var tarefa = new TarefaSolidExemplo(1, "Tarefa 1", "Em andamento", "João", "Maria",
                                    DateTime.Now.AddDays(7), "Descrição 1");
            repository.Adicionar(tarefa);

            // Act
            var tarefaEncontrada = repository.BuscarPorId(999);

            // Assert
            Assert.Null(tarefaEncontrada);
        }

        [Fact]
        public void TarefaMemoryRepository_AtualizarTarefa_DeveAtualizarCorretamente()
        {
            // Arrange
            // Aplicaro o solid e criar o repositorio da tarefa 
            var repository = new TarefaMemoryRepositoryExemplo();
            var tarefa = new TarefaSolidExemplo(1, "Tarefa Original", "Em andamento", "João", "Maria",
                                    DateTime.Now.AddDays(7), "Descrição original");
            repository.Adicionar(tarefa);

            var tarefaAtualizada = new TarefaSolidExemplo(1, "Tarefa Atualizada", "Concluída", "João", "Maria",
                                             DateTime.Now.AddDays(5), "Descrição atualizada");

            // Act
            bool resultado = repository.Atualizar(tarefaAtualizada);
            var tarefaEncontrada = repository.BuscarPorId(1);

            // Assert
            Assert.True(resultado);
            Assert.Equal("Tarefa Atualizada", tarefaEncontrada.Titulo);
            Assert.Equal("Concluída", tarefaEncontrada.Status);
            Assert.Equal("Descrição atualizada", tarefaEncontrada.Descricao);
        }

        [Fact]
        public void TarefaMemoryRepository_AtualizarTarefaInexistente_DeveRetornarFalso()
        {
            // Arrange
            var repository = new TarefaMemoryRepositoryExemplo();
            var tarefaInexistente = new TarefaSolidExemplo(999, "Tarefa Inexistente", "Em andamento", "João", "Maria",
                                              DateTime.Now.AddDays(7), "Descrição");

            // Act
            bool resultado = repository.Atualizar(tarefaInexistente);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void TarefaService_CriarTarefa_DeveCriarEAdicionarTarefa()
        {
            // Arrange
            var repository = new TarefaMemoryRepositoryExemplo();
            var service = new TarefaServiceExemplo(repository);
            string titulo = "Tarefa de Teste";
            string status = "Em andamento";
            string criador = "João";
            string responsavel = "Maria";
            DateTime prazo = DateTime.Now.AddDays(7);
            string descricao = "Descrição da tarefa";

            // Act
            var tarefa = service.CriarTarefa(titulo, status, criador, responsavel, prazo, descricao);
            var tarefas = service.ListarTodasTarefas();

            // Assert
            Assert.NotNull(tarefa);
            Assert.Equal(titulo, tarefa.Titulo);
            Assert.Single(tarefas);
        }

        [Fact]
        public void TarefaService_BuscarTarefaPorId_DeveRetornarTarefaCorreta()
        {
            // Arrange
            var repository = new TarefaMemoryRepositoryExemplo();
            var service = new TarefaServiceExemplo(repository);
            var tarefa = service.CriarTarefa("Tarefa de Teste", "Em andamento", "João", "Maria",
                                            DateTime.Now.AddDays(7), "Descrição da tarefa");

            // Act
            var tarefaEncontrada = service.BuscarTarefaPorId(tarefa.Id);

            // Assert
            Assert.NotNull(tarefaEncontrada);
            Assert.Equal(tarefa.Id, tarefaEncontrada.Id);
            Assert.Equal(tarefa.Titulo, tarefaEncontrada.Titulo);
        }

        [Fact]
        public void TarefaService_AtualizarTarefa_DeveAtualizarCorretamente()
        {
            // Arrange
            var repository = new TarefaMemoryRepositoryExemplo();
            var service = new TarefaServiceExemplo(repository);
            var tarefa = service.CriarTarefa("Tarefa Original", "Em andamento", "João", "Maria",
                                            DateTime.Now.AddDays(7), "Descrição original");

            var tarefaAtualizada = new TarefaSolidExemplo(tarefa.Id, "Tarefa Atualizada", "Concluída", "João", "Maria",
                                             DateTime.Now.AddDays(5), "Descrição atualizada");

            // Act
            bool resultado = service.AtualizarTarefa(tarefaAtualizada);
            var tarefaEncontrada = service.BuscarTarefaPorId(tarefa.Id);

            // Assert
            Assert.True(resultado);
            Assert.Equal("Tarefa Atualizada", tarefaEncontrada.Titulo);
            Assert.Equal("Concluída", tarefaEncontrada.Status);
        }
    }

}
