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

    public class TesteSetores
    {
        // TODO: Verificar se o setor foi realmente cadastrado no repositório, não apenas o retorno do método
        // TODO: Testar cadastro de setor com nome vazio ou inválido
        // TODO: Testar tentativa de cadastrar setor com nome duplicado
        [Fact]
        public void Cadastrar_Setor()
        {
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);
            var empresa = new Empresa("empresa vinicius", "12345678901234");

            Setor setor = new Setor("Comercial"/*, empresa*/);
            var resultado = servico.Cadastrar(setor);

            Assert.True(resultado);
        }

        [Fact]
        public void CadastrarEListarSetor()
        {
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);
            var empresa = new Empresa("empresa vinicius", "12345678901234");

            Setor setor = new Setor("Comercial"/*, empresa*/);
            var resultado = servico.Cadastrar(setor);

            var listaSetores = servico.Listar();

            Assert.True(resultado);
            Assert.NotEmpty(listaSetores);
            Assert.Contains(setor, listaSetores);
        }

        // TODO: Testar edição de setor inexistente (deve retornar false)
        // TODO: Verificar se a edição persiste no repositório
        // TODO: Testar edição com nome inválido ou duplicado
        [Fact]
        public void EditarSetor()
        {
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);
            var empresa = new Empresa("empresa vinicius", "12345678901234");

            Setor setor = new Setor("Comercial"/*, empresa*/);
            servico.Cadastrar(setor);

            var resultadoEdicao = servico.Editar(setor.Id, "Financeiro", true);

            Assert.True(resultadoEdicao);
            Assert.Equal("Financeiro", setor.Nome);

        }

        // TODO: Verificar se o setor foi realmente removido do repositório (listar e confirmar que não está presente)
        // TODO: Testar remoção de setor inexistente
        // TODO: Testar remoção de setor que está sendo utilizado por usuários (deve falhar ou tratar adequadamente)
        [Fact]
        public void RemoverSetor()
        {
            //arrange
            var repositorio = new SetorRepositorio();
            var servico = new SetorServico(repositorio);
            var empresa = new Empresa("empresa vinicius", "12345678901234");
            Setor setor = new Setor("Comercial"/*, empresa*/);
            
            //act
            servico.Cadastrar(setor);
            var resultadoRemocao = servico.Remover(setor);

            //assert
            Assert.True(resultadoRemocao);
        }

    }
}
