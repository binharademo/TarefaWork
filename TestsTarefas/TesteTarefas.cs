using NuGet.Frameworks;
using TarefasLibrary;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;
using Xunit;

namespace Tests_Tarefas
{
    public class TesteTarefas
    {
        [Fact]
        public void SalvarTarefa_Testes()
        {
            //Arrange
            int id = 1;
            string titulo = "Estudar C#";
            string status = "Pendente";
            string criador = "Gabriel";
            string responsavel = "Vinicius";
            DateTime prazo = new DateTime(2025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador";
            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(status, tarefa01.Status);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
            Assert.Equal(prazo, tarefa01.Prazo);
            Assert.Equal(descricao, tarefa01.Descricao);
        }


        [Fact]
        public void TesteSalvarEBuscar()
        {
            //Arrange
            int id = 1;
            string titulo = "Estudar C#";
            string status = "Pendente";
            string criador = "Gabriel";
            string responsavel = "Vinicius";
            DateTime prazo = new DateTime(2025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador";

            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, destinario, responsavel, prazo, descricao);
           
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());


            //Act
            tarefaServico.Salvar(tarefa01);

            Tarefa Tresultado = tarefaServico.BuscarPorId(tarefa01.Id);
            bool result = Tresultado != null;

            //Assert
            Assert.Equal(titulo, Tresultado.Titulo);
            Assert.Equal(status, Tresultado.Status);
            Assert.Equal(criador, Tresultado.Criador);
            Assert.Equal(responsavel, Tresultado.Responsavel);
            Assert.Equal(prazo, Tresultado.Prazo);
            Assert.Equal(descricao, Tresultado.Descricao);

            Assert.True(result);
        }

        [Fact]
        public void SalvarTarefa_ValoresNulosOuVazios()
        {
            //Arrange
            int id = 1;
            string titulo = "";
            string status = "";
            string criador = "";
            string responsavel = "";
            DateTime prazo = new DateTime();
            string descricao = "";

            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(status, tarefa01.Status);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
            Assert.Equal(prazo, tarefa01.Prazo);
            Assert.Equal(descricao, tarefa01.Descricao);
        }

        [Fact]
        public void SalvarTarefa_ValoresEspeciais()
        {
            //Arrange
            int id = 1;
            string titulo = "Estudar C# @ 2025!";
            string status = "Concluído";
            string criador = "Gabriel#1";
            string responsavel = "Vinicius_2";
            DateTime prazo = new DateTime(4025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador!";

            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(status, tarefa01.Status);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
        }

        [Fact]
        public void ListarTarefas()
        {
            //Arrange
            int id = 1;
            string titulo = "Estudar C#";
            string status = "Pendente";
            string criador = "Gabriel";
            string responsavel = "Vinicius";
            DateTime prazo = new DateTime(2025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador";
            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.NotEmpty(tarefaServico.ListarTodas());

        }

        [Theory]
        [InlineData(1, "Estudar C#", "Pendente", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(2, "Estudar 1 C#", "Pendente", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(3, "Estudar 2 C#", "Pendente", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(4, "Estudar 3 C#", "Pendente", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(5, "Estudar 4 C#", "Pendente", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        public void AtualizarTarefa(int id, string titulo, string status, string criador, string responsavel, DateTime prazo, string descricao)
        {
            //Arrange
            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Act
            string novostatus = "Finalizado";
            string novadescricao = "Estudar VB";
            DateTime novoprazo = new DateTime(2025, 12, 01);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus, novadescricao, novoprazo);

            //Assert
            Assert.True(resultado);
            Assert.Equal("Finalizado", tarefa01.Status);
            Assert.Equal("Estudar VB", tarefa01.Descricao);
            Assert.Equal(new DateTime(2025, 12, 01), tarefa01.Prazo);

        }

         
        [Fact]
        public void AtualizarTarefa_02()
        {
            //Arrange
            int id = 1000;
            string titulo = "Estudar C#";
            string status = "Pendente";
            string criador = "Gabriel";
            string responsavel = "Vinicius";
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Act
            string novostatus = "Atendimento";
            string novadescricao = "Estudar PHP";
            DateTime novoprazo = new DateTime(2025, 12, 31);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus, novadescricao, novoprazo);

            //Assert
            Assert.True(resultado);
            Assert.Equal("Atendimento", tarefa01.Status);
            Assert.Equal("Estudar PHP", tarefa01.Descricao);
            Assert.Equal(new DateTime(2025, 12, 31), tarefa01.Prazo);


        }


        [Fact]
        public void AtualizarTarefa_OutraTentatic()
        {
            //Arrange
            int id = 1;
            string titulo = "Estudar C#";
            string status = "Pendente";
            string criador = "Gabriel";
            string responsavel = "Vinicius";
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Act
            string novostatus = "";
            string novadescricao = "Estudar VB";
            DateTime novoprazo = new DateTime(2025, 12, 31);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus, novadescricao, novoprazo);

            //Assert
            Assert.False(resultado);
            Assert.Equal("Pendente", tarefa01.Status);
            Assert.Equal("Estudar C#", tarefa01.Descricao);
            Assert.Equal(new DateTime(2025, 05, 20), tarefa01.Prazo);

        }



    }
}
