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
            // Arrange
            var usuario = new Usuario("Teste", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
            // Act
            bool resultado = usuarioRepositorio.Cadastrar(usuario);
            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void Editar_UsuarioEF()
        {
            // Arrange
            var usuario = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
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
            // Arrange
            var usuario = new Usuario("Gabriel90", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
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
            // Arrange
            var usuario = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            var usuarioRepositorio = new UsuarioRepositorio(connectionString);
            usuarioRepositorio.InicializarBancoDados();
            usuarioRepositorio.Cadastrar(usuario);
            // Act
            bool resultado = usuarioRepositorio.Remover(usuario);
            // Assert
            Assert.True(resultado);
        }
    }
}
