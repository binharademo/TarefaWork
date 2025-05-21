using TarefasLibrary;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace Tests_Tarefas
{
    public class TesteTarefas
    {
        [Fact]
        public void SalvarTarefa_Testes()
        {
            //Arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            int id = 1;
            string titulo = "Estudar C#";
            DateTime prazo = new DateTime(2025, 12, 01);
            string descricao = "Estudar C# para ser um bom programador";

            //Act
            Tarefa tarefa01 = new Tarefa(id, titulo, Tarefa.Status.ToDo, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(Tarefa.Status.ToDo, tarefa01.StatusTarefa);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
            Assert.Equal(prazo, tarefa01.Prazo);
            Assert.Equal(descricao, tarefa01.Descricao);
        }


        [Fact]
        public void TesteSalvarEBuscar()
        {
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            //Arrange
            int id = 177;
            string titulo = "Estudar C#";

            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador";

            Tarefa tarefa01 = new Tarefa(titulo, Tarefa.Status.ToDo, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta, dataCriacao);

            //Act
            tarefaServico.Salvar(tarefa01);
            Tarefa Tresultado = tarefaServico.BuscarPorId(tarefa01.Id);

            //Assert
            Assert.NotNull(Tresultado);
            Assert.Equal(titulo, Tresultado.Titulo);
            Assert.Equal(Tarefa.Status.ToDo, tarefa01.StatusTarefa);
            Assert.Equal(criador.Nome, Tresultado.Criador.Nome);
            Assert.Equal(responsavel.Nome, Tresultado.Responsavel.Nome);
            Assert.Equal(prazo, Tresultado.Prazo);
            Assert.Equal(descricao, Tresultado.Descricao);


        }

        [Fact]
        public void SalvarTarefa_ValoresNulosOuVazios()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            //Arrange
            int id = 1;
            string titulo = "";
            DateTime prazo = new DateTime();
            string descricao = "";

            Tarefa tarefa01 = new Tarefa(titulo, Tarefa.Status.ToDo, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(Tarefa.Status.ToDo, tarefa01.StatusTarefa);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
            Assert.Equal(prazo, tarefa01.Prazo);
            Assert.Equal(descricao, tarefa01.Descricao);
        }

        [Fact]
        public void SalvarTarefa_ValoresEspeciais()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            //Arrange
            int id = 1;
            string titulo = "Estudar C# @ 2025!";
            DateTime prazo = new DateTime(4025, 12, 31);
            DateTime dataCriacao = new DateTime(4025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador!";

            Tarefa tarefa01 = new Tarefa(titulo, Tarefa.Status.Done, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta, dataCriacao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(Tarefa.Status.Done, tarefa01.StatusTarefa);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
        }

        [Fact]
        public void ListarTarefas()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            //Arrange
            int id = 1;
            string titulo = "Estudar C#";
            var tarefa = Tarefa.Status.ToDo;
            DateTime prazo = new DateTime(2025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.NotEmpty(tarefaServico.ListarTodas());

        }

        [Theory]
        [InlineData(1, "Estudar C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#", Tarefa.Prioridade.Alta)]
        [InlineData(2, "Estudar 1 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#", Tarefa.Prioridade.Alta)]
        [InlineData(3, "Estudar 2 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#", Tarefa.Prioridade.Alta)]
        [InlineData(4, "Estudar 3 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#", Tarefa.Prioridade.Alta)]
        [InlineData(5, "Estudar 4 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#", Tarefa.Prioridade.Alta)]
        public void AtualizarTarefa(int id, string titulo, string statusStr, string nomeCriador, string nomeResponsavel, string prazoStr, string descricao, Tarefa.Prioridade prioridade)
        {
            // Conversão do status
            if (!Enum.TryParse(statusStr, out Tarefa.Status statusEnum))
            {
                throw new ArgumentException("Status inválido");
            }

            // Conversão da data
            DateTime prazo = DateTime.Parse(prazoStr);

            // Arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario(nomeCriador, "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario(nomeResponsavel, "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            Tarefa tarefa01 = new Tarefa(id, titulo, statusEnum, criador, responsavel, prazo, descricao, prioridade);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            // Act
            string novaDescricao = "Estudar VB";
            DateTime novoPrazo = new DateTime(2025, 12, 05);
            tarefa01.Descricao = novaDescricao;
            tarefa01.Titulo = "Estudar VB";
            bool resultado = tarefaServico.Atualizar(tarefa01, Tarefa.Status.Done, novaDescricao, novoPrazo, titulo, Tarefa.Prioridade.Alta);

            // Assert
            Assert.True(resultado);
            Assert.Equal(Tarefa.Status.Done, tarefa01.StatusTarefa);
            Assert.Equal(novaDescricao, tarefa01.Descricao);
            Assert.Equal(novoPrazo, tarefa01.Prazo);
        }



        [Fact]
        public void AtualizarTarefa_02()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            //Arrange
            int id = 1000;
            string titulo = "Estudar C#";
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(titulo, Tarefa.Status.ToDo, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Act
            string novadescricao = "Estudar PHP";
            DateTime novoprazo = new DateTime(2025, 12, 09);
            bool resultado = tarefaServico.Atualizar(tarefa01, Tarefa.Status.Done, novadescricao, novoprazo, titulo, Tarefa.Prioridade.Alta);

            //Assert
            Assert.True(resultado);
            Assert.Equal(Tarefa.Status.Done, tarefa01.StatusTarefa);
            Assert.Equal("Estudar PHP", tarefa01.Descricao);
            Assert.Equal(new DateTime(2025, 12, 09), tarefa01.Prazo);
        }


        [Fact]
        public void Listar_Tarefas_Por_Usuario()
        {
            //Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            int id = 1;
            string titulo = "Estudar C#";
            var tarefa = Tarefa.Status.ToDo;

            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta);

            tarefaServico.Salvar(tarefa01);
            // act
            tarefaServico.ListarPorUsuario(responsavel.Id);

            // assert
            Assert.NotEmpty(tarefaServico.ListarPorUsuario(responsavel.Id));
            Assert.NotEmpty(tarefaServico.ListarPorUsuario(criador.Id));
            Assert.Contains(tarefa01, tarefaServico.ListarPorUsuario(responsavel.Id));

        }
        [Fact]
        public void Atualizar_Status_VerificaSeOStatusAntigoEhDiferenteDoNovo()
        {
            // Arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            int id = 52;
            string titulo = "Estudar C#";
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";

            var tarefa01 = new Tarefa(titulo, Tarefa.Status.ToDo, criador, responsavel, prazo, descricao, Tarefa.Prioridade.Alta);
            var tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            var statusAntigo = tarefa01.StatusTarefa;

            // Act
            bool resultado = tarefaServico.Atualizar(tarefa01, Tarefa.Status.Done);

            // Assert
            Assert.True(resultado);
            Assert.NotEqual(statusAntigo, tarefa01.StatusTarefa);
        }


        [Fact]
        public void MarcarUsuarios()
        {

            //Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            Usuario membro = new Usuario("Guilherme", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(membro);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);

            tarefaServico.Salvar(tarefa01);
            var resultado = tarefaServico.MarcarMembro(tarefa01, membro);

            Assert.True(resultado);
            Assert.Contains(membro, tarefa01.Membros);
        }


        [Fact]
        public void MarcarUsuarioSemTarefa()
        {
            //Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            Usuario membro = new Usuario("Guilherme", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(membro);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);

            var resultado = tarefaServico.MarcarMembro(tarefa01, membro);

            Assert.False(resultado);
            Assert.DoesNotContain(membro, tarefa01.Membros);
        }

        [Fact]
        public void MarcarUsuarioQueJaExisteNaTarefa()
        {

            //Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);

            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            Usuario membroExistente = new Usuario("Guilherme", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(membroExistente);

            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);

            tarefaServico.Salvar(tarefa01);
            tarefaServico.MarcarMembro(tarefa01, membroExistente);

            var resultado = tarefaServico.MarcarMembro(tarefa01, membroExistente);

            Assert.False(resultado);
            Assert.Contains(membroExistente, tarefa01.Membros);
            Assert.Single(tarefa01.Membros, membroExistente);
        }
        [Fact]
        public void DefinirPrioridadeTarefa()
        {
            //Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            var tarefa = Tarefa.Status.ToDo;
            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta, new DateTime(2025, 05, 20));
            tarefaServico.Salvar(tarefa01);

            Assert.Equal(Tarefa.Prioridade.Alta, tarefa01.PrioridadeTarefa);

        }

        [Fact]
        public void CronometroCriacao()
        {
            // arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa01);

            //act
            tarefaServico.IniciaCronometro(tarefa01);
            tarefaServico.PausaCronometro(tarefa01);

            tarefaServico.IniciaCronometro(tarefa01);
            tarefaServico.PausaCronometro(tarefa01);

            //assert
            Assert.NotEmpty(tarefa01.Tempos);
            Assert.Equal(2, tarefa01.Tempos.Count);
        }

        [Fact]
        public void CronometroTentaStartarSemPause()
        {
            // arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa01);
            tarefaServico.IniciaCronometro(tarefa01);

            //act
            bool result = tarefaServico.IniciaCronometro(tarefa01);

            //assert
            Assert.NotEmpty(tarefa01.Tempos);
            Assert.False(result);
        }

        [Fact]
        public void CronometroPausaSemInicio()
        {
            // arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa01);

            //act
            var result = tarefaServico.PausaCronometro(tarefa01);

            //assert
            Assert.Empty(tarefa01.Tempos);
            Assert.Equal(TimeSpan.Zero, result);
        }

        [Fact]
        public void CronometroCalculaTempo()
        {
            // arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            var tarefa = Tarefa.Status.ToDo;

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa01);

            // arrange

            tarefaServico.IniciaCronometro(tarefa01);
            Thread.Sleep(100);
            tarefaServico.PausaCronometro(tarefa01);
            tarefaServico.IniciaCronometro(tarefa01);
            Thread.Sleep(100);

            //act
            TimeSpan result = tarefaServico.PausaCronometro(tarefa01);

            //assert
            Assert.True(result.Milliseconds >= 200);
        }


        [Fact]
        public void StatusFinalizarTarefa()
        {
            // Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);

            Tarefa tarefa01 = new Tarefa("titulo", Tarefa.Status.ToDo, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa01);

            tarefaServico.IniciaCronometro(tarefa01);
            Thread.Sleep(100); // Simula algum tempo de trabalho
            tarefaServico.PausaCronometro(tarefa01);

            // Act
            bool result = tarefaServico.Finalizar(tarefa01);

            // Assert
            Assert.True(result);
            Assert.Equal(Tarefa.Status.Done, tarefa01.StatusTarefa);

        }
    }
}
