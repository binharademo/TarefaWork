using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio.Entity;

namespace Tests_Tarefas.RepositorioEntity
{
    public class TesteComentarioEF
    {
        private const string connectionString = "Data Source=TestTarefas.db";

        [Fact]
        public void CriarComentario()
        {
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
            bool resultado = usuarioRepositorio.Cadastrar(usuario);

            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();

            // Act
            tarefaRepositorio.Salvar(tarefa);
            var comentario = new Comentario("Comentario de teste", DateTime.Now, tarefa.Id, usuario.Id);
            var comentario2 = new Comentario("Comentario de teste2", DateTime.Now, tarefa.Id, usuario.Id);
            var resultado1 = comentarioRepositorio.Cadastrar(comentario);
            var resultado2 = comentarioRepositorio.Cadastrar(comentario2);

            // Assert
            Assert.True(resultado1);
            Assert.True(resultado2);
        }

        [Fact]
        public void DeletarComentario()
        {
            // Arrange
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
            bool resultado = usuarioRepositorio.Cadastrar(usuario);

            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();

            // Act
            tarefaRepositorio.Salvar(tarefa);
            var comentario = new Comentario("Comentario de teste", DateTime.Now, tarefa.Id, usuario.Id);
            var comentario2 = new Comentario("Comentario de teste2", DateTime.Now, tarefa.Id, usuario.Id);
            var resultado1 = comentarioRepositorio.Cadastrar(comentario);
            var resultado2 = comentarioRepositorio.Cadastrar(comentario2);

            bool resultadodeletar = comentarioRepositorio.Remover(comentario);
            bool resultadodeletar2 = comentarioRepositorio.Remover(comentario2);

            // Assert
            Assert.True(resultadodeletar);
            Assert.True(resultadodeletar2);
        }

        [Fact]
        public void EditarComentario()
        {
            // Arrange
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
            bool resultado = usuarioRepositorio.Cadastrar(usuario);

            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();
            tarefaRepositorio.Salvar(tarefa);
            var comentario = new Comentario("teste editar", DateTime.Now, tarefa.Id, usuario.Id);
            var resultado1 = comentarioRepositorio.Cadastrar(comentario);
            

            // Act
            comentario.Descricao = "editar testes";
            bool resultadoEditar =comentarioRepositorio.Editar(comentario);
            // Assert
            Assert.True(resultadoEditar);

        }

    }
}
