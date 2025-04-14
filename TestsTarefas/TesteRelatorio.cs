using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace Tests_Tarefas
{
    public class TesteRelatorio
    {
        [Fact]
        public void GeraRelatorio() { 
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa01);

            //act
            tarefaServico.IniciaCronometro(tarefa01);
            Thread.Sleep(100);
            tarefaServico.PausaCronometro(tarefa01);

            Tarefa tarefa02 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa02);

            tarefaServico.IniciaCronometro(tarefa02);
            Thread.Sleep(100);
            tarefaServico.PausaCronometro(tarefa02);

            var resultado = tarefaServico.ListarPorUsuario(criador.Id);
            var resultado2 = tarefaServico.ListarPorUsuario(criador.Id);
            
            //assert
            Assert.Equal(tarefa01.TempoTotal, resultado[0].TempoTotal);
            Assert.Equal(tarefa02.TempoTotal, resultado[1].TempoTotal);
            Assert.Equal(tarefa01.Responsavel, resultado[0].Responsavel);
            Assert.Equal(tarefa02.Responsavel, resultado[1].Responsavel);

        }

        [Fact]
        public void GeraRelatorioComTarefaNaoExistente()
        {
            TarefaServico tarefaServico = new TarefaServico(new TarefaMemoriaRepositorio());
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
            Usuario criador = new Usuario("Gabriel", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(criador);
            Usuario responsavel = new Usuario("Vinicius", "123456", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(responsavel);
            StatusTarefa tarefa = new StatusTarefa(StatusTarefa.Status.ToDo);

            Tarefa tarefa01 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);
            tarefaServico.Salvar(tarefa01);

            //act
            tarefaServico.IniciaCronometro(tarefa01);
            Thread.Sleep(100);
            tarefaServico.PausaCronometro(tarefa01);

            Tarefa tarefa02 = new Tarefa("titulo", tarefa, criador, responsavel, new DateTime(2025, 05, 20), "descricao", Tarefa.Prioridade.Alta);

            var resultado = tarefaServico.ListarPorUsuario(criador.Id);

            //assert
            Assert.Equal(tarefa01.TempoTotal, resultado[0].TempoTotal);
            Assert.Equal(tarefa01.Responsavel, resultado[0].Responsavel);

            Assert.NotEqual(2, resultado.Count);


        }
    }
}
