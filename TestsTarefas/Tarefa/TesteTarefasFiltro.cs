﻿using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace Tests_Tarefas.TarefaFiltro
{
    public class TesteTarefasFiltro
    {
        [Theory]
        [InlineData("titulo", 2)]
        [InlineData("5", 2)]
        [InlineData("i t", 1)]
        [InlineData("zero", 0)]
        public void FiltrarPorNome(string titulo, int totalResultados)
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);
            Tarefa.Status status = Tarefa.Status.ToDo;
            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);

            tarefa.Salvar(new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("titulo dois", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("t i t u l o 3", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("4 567", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));

            //act
            var filtro = new FiltroTarefa();
            filtro.Nome = titulo;
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(totalResultados, resultado.Count);
            Assert.Empty(resultado.Where(t => !t.Titulo.Contains(titulo)));

        }

        [Theory]
        [InlineData(Tarefa.Prioridade.Alta, 3)]
        [InlineData(Tarefa.Prioridade.Baixa, 2)]
        [InlineData(Tarefa.Prioridade.Urgente, 0)]
        public void FiltrarPorPrioridade(Tarefa.Prioridade prioridade, int totalResultados)
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);
            Tarefa.Status status = Tarefa.Status.ToDo;
            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);

            tarefa.Salvar(new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("titulo dois", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("t i t u l o 3", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("4 567", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));

            //act
            var filtro = new FiltroTarefa();
            filtro.Prioridade = prioridade;
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(totalResultados, resultado.Count);

            Assert.Empty(resultado.Where(t => t.PrioridadeTarefa != prioridade));
        }


        [Theory]
        [InlineData(Tarefa.Status.Done, 3)]
        [InlineData(Tarefa.Status.ToDo, 2)]
        [InlineData(Tarefa.Status.Doing, 0)]
        public void FiltrarPorStatus(Tarefa.Status statusTarefa, int totalResultados)
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);

            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);

            tarefa.Salvar(new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("titulo dois", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("t i t u l o 3", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("4 567", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));

            //act
            var filtro = new FiltroTarefa();
            filtro.Status = statusTarefa;
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(totalResultados, resultado.Count);
            Assert.Empty(resultado.Where(t => t.StatusTarefa != statusTarefa));
        }


        [Theory]
        [InlineData(10, 20, 3)]
        [InlineData(1, 10, 2)]
        [InlineData(25, 30, 0)]
        public void FiltrarPorDataCriacao(int inicio, int fim, int totalResultados)
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);

            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 3, 31);

            tarefa.Salvar(new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, new DateTime(2025, 3, 05)));
            tarefa.Salvar(new Tarefa("titulo dois", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, new DateTime(2025, 3, 10)));
            tarefa.Salvar(new Tarefa("t i t u l o 3", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, new DateTime(2025, 3, 12)));
            tarefa.Salvar(new Tarefa("4 567", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, new DateTime(2025, 3, 20)));
            tarefa.Salvar(new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, new DateTime(2025, 3, 24)));

            //act
            var filtro = new FiltroTarefa();
            filtro.Inicio = new DateTime(2025, 3, inicio);
            filtro.Fim = new DateTime(2025, 3, fim);
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(totalResultados, resultado.Count);
            Assert.Empty(resultado.Where(t => t.DataCriacao < filtro.Inicio));
            Assert.Empty(resultado.Where(t => t.DataCriacao > filtro.Fim));
        }


        [Fact]
        public void FiltrarPorCriador()
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);

            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);

            tarefa.Salvar(new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("titulo dois", Tarefa.Status.Done, criador, criador, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("t i t u l o 3", Tarefa.Status.ToDo, responsavel, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("4 567", Tarefa.Status.ToDo, responsavel, criador, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));

            //act
            var filtro = new FiltroTarefa();
            filtro.Criador = criador.Id;
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(3, resultado.Count);
            Assert.Empty(resultado.Where(t => t.Criador.Id != criador.Id));
        }

        [Fact]
        public void FiltrarPorResponsavel()
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);

            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);

            tarefa.Salvar(new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("titulo dois", Tarefa.Status.Done, criador, criador, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("t i t u l o 3", Tarefa.Status.ToDo, responsavel, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("4 567", Tarefa.Status.ToDo, responsavel, criador, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));

            //act
            var filtro = new FiltroTarefa();
            filtro.Responsavel = responsavel.Id;
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(3, resultado.Count);
            Assert.Empty(resultado.Where(t => t.Responsavel.Id != responsavel.Id));
        }


        [Fact]
        public void FiltrarPorMembro()
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);

            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);
            Tarefa t01;

            t01 = new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao);
            tarefa.Salvar(t01);
            tarefa.MarcarMembro(t01, responsavel);
            t01 = new Tarefa("titulo dois", Tarefa.Status.Done, criador, criador, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao);
            tarefa.Salvar(t01);
            tarefa.MarcarMembro(t01, responsavel);
            t01 = new Tarefa("t i t u l o 3", Tarefa.Status.ToDo, responsavel, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao);
            tarefa.Salvar(t01);
            tarefa.MarcarMembro(t01, criador);
            t01 = new Tarefa("4 567", Tarefa.Status.ToDo, responsavel, criador, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao);
            tarefa.Salvar(t01);
            tarefa.MarcarMembro(t01, criador);
            t01 = new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao);
            tarefa.Salvar(t01);
            tarefa.MarcarMembro(t01, responsavel);

            //act
            var filtro = new FiltroTarefa();
            filtro.Membro = responsavel.Id;
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(3, resultado.Count);
            Assert.Equal(3, resultado.Count(t => t.Membros.Exists(m => m.Id == responsavel.Id)));
        }


        [Theory]
        [InlineData(Tarefa.Prioridade.Baixa, Tarefa.Status.ToDo, 2)]
        [InlineData(Tarefa.Prioridade.Baixa, Tarefa.Status.Done, 0)]
        [InlineData(Tarefa.Prioridade.Alta, null, 3)]
        [InlineData(null, Tarefa.Status.ToDo, 3)]
        [InlineData(null, null, 5)]
        public void FiltrarPrioridadeEStatus(Tarefa.Prioridade? prioridade, Tarefa.Status? statusTarefa, int totalResultados)
        {
            //Arrange
            var setor = new Setor("Setor Teste", 1);
            TarefaServico tarefa = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, setor);
            usuario.Criar(responsavel);

            DateTime prazo = new DateTime(2025, 12, 31);
            DateTime dataCriacao = new DateTime(2025, 12, 31);

            tarefa.Salvar(new Tarefa("titulo001", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("titulo dois", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));
            tarefa.Salvar(new Tarefa("t i t u l o 3", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("4 567", Tarefa.Status.ToDo, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Baixa, dataCriacao));
            tarefa.Salvar(new Tarefa("%%%%% 5", Tarefa.Status.Done, criador, responsavel, prazo, "Estudar C# para ser um bom programador", Tarefa.Prioridade.Alta, dataCriacao));

            //act
            var filtro = new FiltroTarefa();
            filtro.Status = statusTarefa;
            filtro.Prioridade = prioridade;
            List<Tarefa> resultado = tarefa.Busca(filtro);

            //assert
            Assert.Equal(totalResultados, resultado.Count);
            Assert.Empty(resultado.Where(t => (prioridade is not null && t.PrioridadeTarefa != prioridade) && (statusTarefa is not null && Tarefa.Status.Done != statusTarefa)));
        }

    }
}
