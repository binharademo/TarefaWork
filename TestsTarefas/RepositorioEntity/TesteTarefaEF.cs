using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Repositorio.Entity;

namespace Tests_Tarefas.RepositorioEntity
{
    public class TesteTarefaEF
    {
        private const string connectionString = "Data Source=TestTarefas.db";

        [Fact]
        public void Cadastro_TarefaEF()
        {
            // Arrange
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo, 
                new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti), 
                new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti), 
                DateTime.Now.AddDays(5), "Descricao", 
                Tarefa.Prioridade.Alta);

            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            tarefaRepositorio.InicializarBancoDados();
            // Act
            bool resultado = tarefaRepositorio.Salvar(tarefa);
            // Assert
            Assert.True(resultado);
        }

        //[Fact]
        //public void AdicionarMembro()
        //{
        //    // Arrange
        //    var tarefaRepositorio = new TarefaRepositorio(connectionString);
        //    var usuarioRepositorio = new UsuarioRepositorio(connectionString);
        //    var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
        //        new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
        //        new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
        //        DateTime.Now.AddDays(5), "Descricao",
        //        Tarefa.Prioridade.Alta);
        //    var membro = new Usuario("membro", "senha", Usuario.Funcao.Analista, Usuario.Setor.Diretoria);

        //    tarefaRepositorio.InicializarBancoDados();
        //    tarefaRepositorio.Salvar(tarefa);
        //    usuarioRepositorio.Cadastrar(membro);

        //    // Act
        //    var resultado = tarefaRepositorio.MarcarMembro(tarefa, membro);
        //    var tarefaCriada = tarefaRepositorio.BuscarPorID(tarefa.Id);

        //    // Assert
        //    Assert.True(resultado);
        //    Assert.NotNull(tarefaCriada);
        //    Assert.NotNull(tarefaCriada.Membros);
        //    Assert.Single(tarefaCriada.Membros);
        //    Assert.Equal(membro.Nome, tarefaCriada.Membros.First().Nome);

        //}
    }
}
