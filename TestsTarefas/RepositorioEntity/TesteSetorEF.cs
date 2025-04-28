using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio.Entity;

namespace Tests_Tarefas.RepositorioEntity
{
    public class TesteSetorEF
    {
         private const string connectionString = "Data Source=TestTarefas.db";

        [Fact]
        public void Cadastro_SetorEF()
        {
            // arrange
            var setor = new Setor("setor teste123");
            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();

            //act
            var setorBuscado = setorRepositorio.Cadastrar(setor);

            //assert
            Assert.True(setorBuscado);

        }
        [Fact]
        public void Editar_setorEF() {
            
            // arrange
            var setor = new Setor("setor teste123");
            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();
            var setorBuscado = setorRepositorio.Cadastrar(setor);

            // act

            setor.Nome = "setor alterado";
            var setorEditado = setorRepositorio.Editar(setor);

            // assert

            Assert.True(setorEditado);
        }

        [Fact]
        public void Excluir_setorEF() {
            // arrange
            var setor = new Setor("setor teste123");
            var setor2 = new Setor("nao excluido");
            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();
            var setorExcluido = setorRepositorio.Cadastrar(setor);
            var setorNaoExcluido = setorRepositorio.Cadastrar(setor2);

            //act

            var excluirSetor = setorRepositorio.Remover(setor);

            //assert
            Assert.True(excluirSetor);
        }

    }
}
