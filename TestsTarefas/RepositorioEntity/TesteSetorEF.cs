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
            // Arrange  
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var empresa = empresaRepositorio.Listar().First();
            
            if (empresa == null)
            {
                empresa = new Empresa("empresa teste", "777777");
                empresaRepositorio.Cadastrar(empresa);
            }

            var setor1 = new Setor("financeiro", empresa);
            var setor2 = new Setor("ti", empresa);
            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();

            // Act  
            var setor1Buscado = setorRepositorio.Cadastrar(setor1);
            var setor2Buscado = setorRepositorio.Cadastrar(setor2);

            // Assert  
            Assert.True(setor1Buscado);
            Assert.True(setor2Buscado);
        }

        [Fact]
        public void Editar_setorEF()
        {
            // arrange
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var empresa = empresaRepositorio.Listar().First();

            if (empresa == null)
            {
                empresa = new Empresa("empresa teste", "777777");
                empresaRepositorio.Cadastrar(empresa);
            }

            var setor = new Setor("setor teste123", empresa);
            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();
            var setorBuscado = setorRepositorio.Cadastrar(setor);

            // act

            setor.Nome = "setor novo";
            var setorEditado = setorRepositorio.Editar(setor);

            // assert

            Assert.True(setorEditado);
        }

        [Fact]
        public void Excluir_setorEF()
        {
            // Arrange
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var empresa1 = empresaRepositorio.Listar().First();
            if (empresa1 == null)
            {
                empresa1 = new Empresa("empresa1 teste", "777777");
                empresaRepositorio.Cadastrar(empresa1);
            }
            var empresa2 = empresaRepositorio.Listar().First(e => e.Id != empresa1.Id);
            if (empresa2 == null)
            {
                empresa2 = new Empresa("odeio vinicius ratzke", "777");
                empresaRepositorio.Cadastrar(empresa2);
            }

            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();

            var setor = new Setor("ti", empresa1);
            var setor2 = new Setor("morte a vinicius", empresa2);

            var criaSetor1 = setorRepositorio.Cadastrar(setor);
            var criaSetor2 = setorRepositorio.Cadastrar(setor2);

            // Act
            var resultadoExclusao = setorRepositorio.Remover(setor);

            // Assert
            Assert.True(resultadoExclusao);
        }
    }
}
