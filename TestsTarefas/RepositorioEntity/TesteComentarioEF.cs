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
            var setorRepositorio = new SetorRepositorio(connectionString);
            var empresaRepositorio = new EmpresaRepositorio(connectionString);

            var setor = setorRepositorio.Listar().FirstOrDefault();

            if (setor == null)
            {
                // Se não existir nenhum setor, cria uma empresa e um setor novo
                var empresa = empresaRepositorio.Listar().FirstOrDefault();

                if (empresa == null)
                {
                    empresa = new Empresa("Empresa Teste", "99999999999999");
                    empresaRepositorio.Cadastrar(empresa);
                    empresa = empresaRepositorio.Listar().First(); // garantir que o Id seja preenchido
                }
                setor = new Setor
                {
                    Nome = "Setor Teste",
                    Status = true,
                    EmpresaId = empresa.Id
                };
                setorRepositorio.Cadastrar(setor);
            }
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor);
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
        public void ListarComentarios()
        {
            var setorRepositorio = new SetorRepositorio(connectionString);
            var empresaRepositorio = new EmpresaRepositorio(connectionString);

            var setor = setorRepositorio.Listar().FirstOrDefault();

            if (setor == null)
            {
                // Se não existir nenhum setor, cria uma empresa e um setor novo
                var empresa = new Empresa("Empresa Teste", "99999999999999");
                empresaRepositorio.Cadastrar(empresa);

                setor = new Setor
                {
                    Nome = "Setor Teste",
                    Status = true,
                    EmpresaId = empresa.Id
                };
                setorRepositorio.Cadastrar(setor);
            }

            // Criação da tarefa
            var tarefa = new Tarefa("Testebuscar", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            // Criação do usuário
            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
            usuarioRepositorio.Cadastrar(usuario);

            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();
            tarefaRepositorio.Salvar(tarefa);

            var comentario1 = new Comentario("teste 000001", DateTime.Now, tarefa.Id, usuario.Id);
            var comentario2 = new Comentario("teste 000002", DateTime.Now, tarefa.Id, usuario.Id);
            var resultado1 = comentarioRepositorio.Cadastrar(comentario1);
            var resultado2 = comentarioRepositorio.Cadastrar(comentario2);

            // Act
            var resultado = comentarioRepositorio.BuscarPorTarefa(tarefa.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Any());
            Assert.Equal(2, resultado.Count);
        }


        [Fact]
        public void DeletarComentario()
        {
            var setorRepositorio = new SetorRepositorio(connectionString);
            var empresaRepositorio = new EmpresaRepositorio(connectionString);

            var setor = setorRepositorio.Listar().FirstOrDefault();

            if (setor == null)
            {
                // Se não existir nenhum setor, cria uma empresa e um setor novo
                var empresa = new Empresa("Empresa Teste", "99999999999999");
                empresaRepositorio.Cadastrar(empresa);

                setor = new Setor
                {
                    Nome = "Setor Teste",
                    Status = true,
                    EmpresaId = empresa.Id
                };
                setorRepositorio.Cadastrar(setor);
            }


            // Arrange
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor);
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
            var setorRepositorio = new SetorRepositorio(connectionString);
            var empresaRepositorio = new EmpresaRepositorio(connectionString);

            var setor = setorRepositorio.Listar().FirstOrDefault();

            if (setor == null)
            {
                // Se não existir nenhum setor, cria uma empresa e um setor novo
                var empresa = new Empresa("Empresa Teste", "99999999999999");
                empresaRepositorio.Cadastrar(empresa);

                setor = new Setor
                {
                    Nome = "Setor Teste",
                    Status = true,
                    EmpresaId = empresa.Id
                };
                setorRepositorio.Cadastrar(setor);
            }

            // Arrange
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor);
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
            bool resultadoEditar = comentarioRepositorio.Editar(comentario);
            // Assert
            Assert.True(resultadoEditar);

        }

    }
}
