using NuGet.Frameworks;
using Tarefas_Library;
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
            tarefa01.Salvar();


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

            Tarefa tarefa01 = new Tarefa(id, titulo, status, criador, responsavel, prazo, descricao);

            //Act
            tarefa01.Salvar();

            Tarefa Tresultado = new Tarefa(1);
            bool result = Tresultado.Buscar();

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
            tarefa01.Salvar();

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
            tarefa01.Salvar();

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
            tarefa01.Salvar();

            //Assert
            Assert.NotEmpty(tarefa01.ListarTodas());

        }

     


    }
}
