﻿using TarefasLibrary;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Negocio;
using TarefasLibrary.Modelo;

namespace Tests_Tarefas
{
    public class TesteEditarUsuario
    {
        [Fact]
        public void EditarUsuarioManual()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioParaEditar = servico.Buscar(usuario.Id);
            usuarioParaEditar.Nome = "binhara_editado";
            usuarioParaEditar.Senha = "456";
            usuarioParaEditar.FuncaoUsuario = Usuario.Funcao.Dev;
            usuarioParaEditar.SetorUsuario = Usuario.Setor.Ti;

            // Assert
            var usuarioEditado = servico.Buscar(usuario.Id);
            Assert.NotNull(usuarioEditado);
            Assert.Equal("binhara_editado", usuarioEditado.Nome);
            Assert.Equal("456", usuarioEditado.Senha);
            Assert.Equal(Usuario.Funcao.Dev, usuarioEditado.FuncaoUsuario);
            Assert.Equal(Usuario.Setor.Ti, usuarioEditado.SetorUsuario);
        }

        [Fact]
        public void EditarNomeUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, "marcelo",usuario.Senha, usuario.FuncaoUsuario, usuario.SetorUsuario);
            

            // Assert
            
            Assert.True(usuarioEditado);
            Assert.Equal("marcelo", usuario.Nome);
        }

        [Fact]
        public void EditarSenhaUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id,usuario.Nome,"NovaSenha", usuario.FuncaoUsuario, usuario.SetorUsuario);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal("NovaSenha", usuario.Senha);
        }


        [Fact]
        public void EditarFuncaoUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha, Usuario.Funcao.Analista, usuario.SetorUsuario);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal(Usuario.Funcao.Analista, usuario.FuncaoUsuario);
        }


        [Fact]
        public void EditarSetorUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id, usuario.Nome, usuario.Senha,usuario.FuncaoUsuario,Usuario.Setor.Marketing);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal(Usuario.Funcao.Marketing, usuario.FuncaoUsuario);
        }


        [Fact]
        public void EditarUsuario()
        {
            // Arrange
            var repositorio = new UsuarioMemoriaRepositorio();
            var servico = new UsuarioServico(repositorio);


            Usuario usuario = new Usuario("binhara", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti);
            servico.Criar(usuario);

            // Act
            var usuarioEditado = servico.Editar(usuario.Id,"Alessandro Binhara", "SenhaNova", Usuario.Funcao.Analista, Usuario.Setor.Marketing);


            // Assert

            Assert.True(usuarioEditado);
            Assert.Equal("Alessandro Binhara", usuario.Nome);
            Assert.Equal("SenhaNova", usuario.Senha);
            Assert.Equal(Usuario.Funcao.Analista, usuario.FuncaoUsuario);
            Assert.Equal(Usuario.Setor.Marketing, usuario.SetorUsuario);
        }


    }
}
