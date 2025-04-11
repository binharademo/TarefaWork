using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace TestsTarefas
{
    public class TesteSetores
    {
        [Fact]
        public void Cadastrar_Setor()
        {
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);

            Setor setor = new Setor("Comercial");
            var resultado = servico.Cadastrar(setor);

            Assert.True(resultado);
        }

        [Fact]
        public void CadastrarEListarSetor()
        {
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);

            Setor setor = new Setor("Comercial");
            var resultado = servico.Cadastrar(setor);

            var listaSetores = servico.Listar();

            Assert.True(resultado);
            Assert.NotEmpty(listaSetores);
            Assert.Contains(setor, listaSetores);
        }

        [Fact]
        public void EditarSetor()
        {
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);

            Setor setor = new Setor("Comercial");
            servico.Cadastrar(setor);

            var resultadoEdicao = servico.Editar(setor.Id, "Financeiro", true);

            Assert.True(resultadoEdicao);
            Assert.Equal("Financeiro", setor.Nome);

        }

        [Fact]
        public void RemoverSetor()
        {
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);
            Setor setor = new Setor("Comercial");
            servico.Cadastrar(setor);
            var resultadoRemocao = servico.Remover(setor);
            Assert.True(resultadoRemocao);
        }

    }
}
