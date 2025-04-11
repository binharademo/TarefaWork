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
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);
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
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);
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
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);
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
        [InlineData(1, "Estudar C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(2, "Estudar 1 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(3, "Estudar 2 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(4, "Estudar 3 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
        [InlineData(5, "Estudar 4 C#", "ToDo", "Gabriel", "Vinicius", "2025-12-31", "Estudar C#")]
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
            StatusTarefa novostatus = new StatusTarefa(StatusTarefa.Status.ToDo);
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
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);

            //Act
            StatusTarefa novostatus = new StatusTarefa(StatusTarefa.Status.ToDo);
            string novadescricao = "Estudar PHP";
            DateTime novoprazo = new DateTime(2025, 12, 09);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus, novadescricao, novoprazo);

            //Assert
            Assert.True(resultado);
            Assert.Equal(tarefa.getStatus(), tarefa01.Status.getStatus());
            Assert.Equal("Estudar PHP", tarefa01.Descricao);
            Assert.Equal(new DateTime(2025, 12, 09), tarefa01.Prazo);


        }


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
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

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
        public void Atualizar_Status_VerificaSeOStatusAntigoEhDiferenteDoNovo()
        {
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            //Arrange
            int id = 52;
            string titulo = "Estudar C#";
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);
            DateTime prazo = new DateTime(2025, 05, 20);
            string descricao = "Estudar C#";
            Tarefa tarefa01 = new Tarefa(titulo, tarefa, criador, responsavel, prazo, descricao);
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            tarefaServico.Salvar(tarefa01);



            //Act
            StatusTarefa novostatus = new StatusTarefa(StatusTarefa.Status.Done);
            bool resultado = tarefaServico.Atualizar(tarefa01, novostatus);

            //Assert
            Assert.NotEqual(tarefa.getStatus(), tarefa01.Status.getStatus());

        }

        [Fact]
        public void MarcarUsuarios()
        {

            //Arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);
            Usuario membro = new Usuario("Guilherme", "123456", "Desenvolvedor", "TI");
            servico.Criar(membro);
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao" );

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

            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);
            Usuario membro = new Usuario("Guilherme", "123456", "Desenvolvedor", "TI");
            servico.Criar(membro);
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao");

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

            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);

            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            Usuario membroExistente = new Usuario("Guilherme", "123456", "Desenvolvedor", "TI");
            servico.Criar(membroExistente);

            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao");

            tarefaServico.Salvar(tarefa01);
            tarefaServico.MarcarMembro(tarefa01, membroExistente);

            var resultado = tarefaServico.MarcarMembro(tarefa01, membroExistente);

            Assert.False(resultado);
            Assert.Contains(membroExistente, tarefa01.Membros);
            Assert.Single(tarefa01.Membros, membroExistente);
        }

        [Fact]
        public void CronometroCriacao()
        {
            // arrange
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            Tarefa tarefa01 = new Tarefa("titulo", "status", criador, responsavel, new DateTime(2025, 05, 20), "descricao");
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
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            Tarefa tarefa01 = new Tarefa("titulo", "status", criador, responsavel, new DateTime(2025, 05, 20), "descricao");
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
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            Tarefa tarefa01 = new Tarefa("titulo", "status", criador, responsavel, new DateTime(2025, 05, 20), "descricao");
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
            Usuario criador = new Usuario("Gabriel", "123456", "Desenvolvedor", "TI");
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", "Desenvolvedor", "TI");
            servico.Criar(responsavel);

            Tarefa tarefa01 = new Tarefa("titulo", "status", criador, responsavel, new DateTime(2025, 05, 20), "descricao");
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
    }
}
