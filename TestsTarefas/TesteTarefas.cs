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
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            int id = 1;
            string titulo = "Estudar C#";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.TODO);
            //string criador = "Gabriel";
            //string responsavel = "Vinicius";
            DateTime prazo = new DateTime(2025, 12, 01);
            string descricao = "Estudar C# para ser um bom programador";

            //Act
            Tarefa tarefa01 = new Tarefa(id, titulo, tarefa, criador, responsavel, prazo, descricao);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(tarefa, tarefa01.Status);
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
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            int id = 177;
            string titulo = "Estudar C#";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.TODO);
            // string criador = "Gabriel";
            //string responsavel = "Vinicius";
            //DateTime prazo = new DateTime(2025, 12, 02);
            DateTime prazo = new DateTime(2025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador";

            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);

            //Act
            tarefaServico.Salvar(tarefa01);
            Tarefa Tresultado = tarefaServico.BuscarPorId(tarefa01.Id);
            
            //Assert
            Assert.NotNull(Tresultado);
            Assert.Equal(titulo, Tresultado.Titulo);
            Assert.Equal(tarefa, Tresultado.Status);
            Assert.Equal(criador.Nome, Tresultado.Criador.Nome);
            Assert.Equal(responsavel.Nome, Tresultado.Responsavel.Nome);
            Assert.Equal(prazo, Tresultado.Prazo);
            Assert.Equal(descricao, Tresultado.Descricao);


        }

        [Fact]
        public void SalvarTarefa_ValoresNulosOuVazios()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            int id = 1;
            string titulo = "";
            StatusTarefa tarefa = null;
            DateTime prazo = new DateTime();
            string descricao = "";

            Tarefa tarefa01 = new Tarefa( titulo, tarefa, criador, responsavel, prazo, descricao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(tarefa, tarefa01.Status);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
            Assert.Equal(prazo, tarefa01.Prazo);
            Assert.Equal(descricao, tarefa01.Descricao);
        }

        [Fact]
        public void SalvarTarefa_ValoresEspeciais()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            int id = 1;
            string titulo = "Estudar C# @ 2025!";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.Done);
            DateTime prazo = new DateTime(4025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador!";

            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.Equal(titulo, tarefa01.Titulo);
            Assert.Equal(tarefa, tarefa01.Status);
            Assert.Equal(criador, tarefa01.Criador);
            Assert.Equal(responsavel, tarefa01.Responsavel);
        }

        [Fact]
        public void ListarTarefas()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            int id = 1;
            string titulo = "Estudar C#";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.TODO);
            DateTime prazo = new DateTime(2025, 12, 31);
            string descricao = "Estudar C# para ser um bom programador";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);

            //Act
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Assert
            Assert.NotEmpty(tarefaServico.ListarTodas());

        }

        [Theory]
        [InlineData(1, "Estudar C#", "TODO", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(2, "Estudar 1 C#", "TODO", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(3, "Estudar 2 C#", "TODO", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(4, "Estudar 3 C#", "TODO", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(5, "Estudar 4 C#", "TODO", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        public void AtualizarTarefa(int id, string titulo, string statusStr, string nomeCriador, string nomeResponsavel, DateTime prazo, string descricao)
        {
            if (!Enum.TryParse(statusStr, out StatusTarefa.Status statusEnum))
            {
                throw new ArgumentException("Status inválido");
            }
            StatusTarefa tarefa = new StatusTarefa(statusEnum);
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario(nomeCriador, "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario(nomeResponsavel, "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Act
            StatusTarefa novostatus = new StatusTarefa(StatusTarefa.Status.TODO);
            string novadescricao = "Estudar VB";
            DateTime novoprazo = new DateTime(2025, 12, 05);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus, novadescricao, novoprazo);

            //Assert
            Assert.True(resultado);
            Assert.Equal(tarefa.getStatus(), tarefa01.Status.getStatus());
            Assert.Equal("Estudar VB", tarefa01.Descricao);
            Assert.Equal(new DateTime(2025, 12, 05), tarefa01.Prazo);

        }


        [Fact]
        public void AtualizarTarefa_02()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            int id = 1000;
            string titulo = "Estudar C#";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.TODO);
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Act
            StatusTarefa novostatus = new StatusTarefa(StatusTarefa.Status.TODO);
            string novadescricao = "Estudar PHP";
            DateTime novoprazo = new DateTime(2025, 12, 09);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus, novadescricao, novoprazo);

            //Assert
            Assert.True(resultado);
            Assert.Equal(tarefa.getStatus(), tarefa01.Status.getStatus());
            Assert.Equal("Estudar PHP", tarefa01.Descricao);
            Assert.Equal(new DateTime(2025, 12, 09), tarefa01.Prazo);


        }


        //[Fact]
        //public void AtualizarTarefa_OutraTentatic()
        //{
        //    UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
        //    Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
        //    servico.Criar(criador);
        //    Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
        //    servico.Criar(responsavel);

        //    //Arrange
        //    int id = 52;
        //    string titulo = "Estudar C#";
        //    string status = "Pendente";
        //    DateTime prazo = new DateTime(2025, 05, 20);
        //    string descricao = "Estudar C#";
        //    Tarefa tarefa01 = new Tarefa(titulo, status, criador, responsavel, prazo, descricao);
        //    TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
        //    tarefaServico.Salvar(tarefa01);

        //    //Act
        //    string novostatus = "";
        //    string novadescricao = "Estudar VB";
        //    DateTime novoprazo = new DateTime(2025, 12, 11);
        //    bool resultado = tarefaServico.Atualizar(tarefa01, novostatus, novadescricao, novoprazo);

        //    //Assert
        //    Assert.False(resultado);
        //    Assert.Equal("Pendente", tarefa01.Status);
        //    Assert.Equal("Estudar C#", tarefa01.Descricao);
        //    Assert.Equal(new DateTime(2025, 05, 10), tarefa01.Prazo);

        //}

        [Fact]
        public void Listar_Tarefas_Por_Usuario()
        {
            //Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            int id = 1;
            string titulo = "Estudar C#";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.TODO);

            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);

            tarefaServico.Salvar(tarefa01);
            // act
            tarefaServico.ListarPorUsuario(responsavel.Id);

            // assert
            Assert.NotEmpty(tarefaServico.ListarPorUsuario(responsavel.Id));
            Assert.NotEmpty(tarefaServico.ListarPorUsuario(criador.Id));
            Assert.Contains(tarefa01, tarefaServico.ListarPorUsuario(responsavel.Id));

        }
        [Fact]
        public void Atualizar_Status_Tarefa()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            int id = 52;
            string titulo = "Estudar C#";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.TODO);
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);


            StatusTarefa statusTarefa = new StatusTarefa(StatusTarefa.Status.TODO);

            //Act
            StatusTarefa novostatus = new StatusTarefa(StatusTarefa.Status.TODO);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus);

            //Assert
            Assert.Equal(tarefa.getStatus(), tarefa01.Status.getStatus());

        }




    }
}
