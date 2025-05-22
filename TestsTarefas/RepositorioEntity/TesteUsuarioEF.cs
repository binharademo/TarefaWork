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
    public class TesteUsuarioEF
    {
        private const string connectionString = "Data Source=TestTarefas.db";

        [Fact]
        public void Cadastro_UsuarioEF()
        {
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new TarefasLibrary.Repositorio.Entity.SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();

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
            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, setor.Id);
            // Act
            bool resultado = usuarioRepositorio.Cadastrar(usuario);
            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void Editar_UsuarioEF()
        {
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new TarefasLibrary.Repositorio.Entity.SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();

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
            var usuario = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor.Id);
            usuarioRepositorio.Cadastrar(usuario);

            // Act
            usuario.Nome = "Gabriel Editado";
            bool resultado = usuarioRepositorio.Editar(usuario);
            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void BuscarPorId_UsuarioEF()
        {
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new TarefasLibrary.Repositorio.Entity.SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
     
            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();
         
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
            var usuario = new Usuario("Gabriel90", "123456", Usuario.Funcao.Dev, setor.Id);
         
            usuarioRepositorio.Cadastrar(usuario);
            // Act
            var resultado = usuarioRepositorio.BuscarPorId(usuario.Id);
            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuario.Nome, resultado.Nome);
        }

        [Fact]
        public void Deletar_UsuarioEF()
        {
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new TarefasLibrary.Repositorio.Entity.SetorRepositorio(connectionString);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);

            empresaRepositorio.InicializarBancoDados();
            setorRepositorio.InicializarBancoDados();
            usuarioRepositorio.InicializarBancoDados();

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
            var usuario = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor.Id);
            usuarioRepositorio.Cadastrar(usuario);
            // Act
            bool resultado = usuarioRepositorio.Remover(usuario);
            // Assert
            Assert.True(resultado);
        }
    }
}
