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
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();

            // Garante que a empresa e o setor existam
            var empresa = empresaRepositorio.Listar().FirstOrDefault();
            if (empresa == null)
            {
                empresa = new Empresa("Empresa Gabriel", "9999999999999");
                empresaRepositorio.Cadastrar(empresa);
                empresa = empresaRepositorio.Listar().First(); // garante ID preenchido
            }

            var setor = setorRepositorio.Listar().FirstOrDefault(s => s.EmpresaId == empresa.Id);
            if (setor == null)
            {
                setor = new Setor("Setor Gabriel", empresa);
                setorRepositorio.Cadastrar(setor);
                setor = setorRepositorio.Listar().First(s => s.EmpresaId == empresa.Id);
            }

            // Cria os objetos com o setor.Id real
            var tarefa = new Tarefa("Teste", Tarefa.Status.ToDo,
                new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor.Id),
                new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor.Id),
                DateTime.Now.AddDays(5), "Descricao", Tarefa.Prioridade.Alta);

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor.Id);
            var resultadoUsuario = usuarioRepositorio.Cadastrar(usuario);

            // Salva a tarefa
            tarefaRepositorio.Salvar(tarefa);

            // Cria e salva os comentários
            var comentario = new Comentario("Comentario de teste", DateTime.Now, tarefa.Id, usuario.Id);
            var comentario2 = new Comentario("Comentario de teste2", DateTime.Now, tarefa.Id, usuario.Id);
            var resultado1 = comentarioRepositorio.Cadastrar(comentario);
            var resultado2 = comentarioRepositorio.Cadastrar(comentario2);

            // Assert
            Assert.True(resultadoUsuario);
            Assert.True(resultado1);
            Assert.True(resultado2);
        }


        [Fact]
        public void ListarComentarios()
        {
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();

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

            // Criação da tarefa
            var tarefa = new Tarefa("Testebuscar", Tarefa.Status.ToDo,
               new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor.Id),
               new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor.Id),
               DateTime.Now.AddDays(5), "Descricao",
               Tarefa.Prioridade.Alta);

            // Criação do usuário
            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor.Id);
            
            usuarioRepositorio.Cadastrar(usuario);
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
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();

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

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor.Id);
           
            bool resultado = usuarioRepositorio.Cadastrar(usuario);

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
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            var tarefaRepositorio = new TarefaRepositorio(connectionString);
            var comentarioRepositorio = new ComentarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();
            tarefaRepositorio.InicializarBancoDados();
            comentarioRepositorio.InicializarBancoDados();

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

            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor.Id);
            bool resultado = usuarioRepositorio.Cadastrar(usuario);

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
