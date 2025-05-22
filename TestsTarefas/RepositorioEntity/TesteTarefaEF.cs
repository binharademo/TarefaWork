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
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new TarefasLibrary.Repositorio.Entity.SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();
            tarefaRepositorio.InicializarBancoDados();
           
            // Garante que a empresa e o setor existam
            var empresa = empresaRepositorio.Listar().FirstOrDefault();
            if (empresa == null)
            {
                empresa = new Empresa("Empresa Teste 2", "9999999999999");
                empresaRepositorio.Cadastrar(empresa);
                empresa = empresaRepositorio.Listar().First(); // garante ID preenchido
            }

            var setor = setorRepositorio.Listar().FirstOrDefault(s => s.EmpresaId == empresa.Id);
            if (setor == null)
            {
                setor = new Setor("Setor Lavanderia", empresa);
                setorRepositorio.Cadastrar(setor);
                setor = setorRepositorio.Listar().First(s => s.EmpresaId == empresa.Id);
            }
            // Arrange
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo, 
                new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor.Id), 
                new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor.Id), 
                DateTime.Now.AddDays(5), "Descricao", 
                Tarefa.Prioridade.Alta);

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
