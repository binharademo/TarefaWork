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
            var empresa1 = empresaRepositorio.Listar().FirstOrDefault();
            if (empresa1 == null)
            {
                empresa1 = new Empresa("empresa1 teste", "777777");
                empresaRepositorio.Cadastrar(empresa1);
                empresa1 = empresaRepositorio.Listar().First(e => e.Cnpj == "777777");
            }

            var empresa2 = empresaRepositorio.Listar().FirstOrDefault(e => e.Id != empresa1.Id);
            if (empresa2 == null)
            {
                empresa2 = new Empresa("empresa2 teste", "888888");
                empresaRepositorio.Cadastrar(empresa2);
                empresa2 = empresaRepositorio.Listar().First(e => e.Cnpj == "888888");
            }

            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();

            var setor = new Setor("TI", empresa1);
            var setor2 = new Setor("Backup", empresa2);

            setorRepositorio.Cadastrar(setor);
            setorRepositorio.Cadastrar(setor2);

            // Act
            var resultadoExclusao = setorRepositorio.Remover(setor);

            // Assert
            Assert.True(resultadoExclusao);
        }


        [Fact]
        public void Listar_setorEF()
        {
            // Arrange
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var empresa = empresaRepositorio.Listar().First();
            if (empresa == null)
            {
                empresa = new Empresa("empresa teste", "777777");
                empresaRepositorio.Cadastrar(empresa);
            }
            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();
            var setor1 = new Setor("financeiro", empresa);
            var setor2 = new Setor("ti", empresa);
            setorRepositorio.Cadastrar(setor1);
            setorRepositorio.Cadastrar(setor2);
            // Act
            var setores = setorRepositorio.Listar();
            // Assert
            Assert.NotEmpty(setores);
            Assert.Contains(setores, s => s.Nome == "financeiro");
            Assert.Contains(setores, s => s.Nome == "ti");
        }

        [Fact]
        public void Excluir_setorNaoExistenteEF()
        {
            // Arrange
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var empresa1 = empresaRepositorio.Listar().First();
            if (empresa1 == null)
            {
                empresa1 = new Empresa("empresa1 teste", "777777");
                empresaRepositorio.Cadastrar(empresa1);
            }

            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();

            var setor = new Setor("TesteSetorQueNaoExiste", empresa1);


            // Act
            var resultadoExclusao = setorRepositorio.Remover(setor);

            // Assert
            Assert.False(resultadoExclusao);
        }

        [Fact]
        public void Editar_setorNaoExistenteEF()
        {
            // Arrange
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var empresa1 = empresaRepositorio.Listar().First();
            if (empresa1 == null)
            {
                empresa1 = new Empresa("empresa1 teste", "777777");
                empresaRepositorio.Cadastrar(empresa1);
            }

            var setorRepositorio = new SetorRepositorio(connectionString);
            setorRepositorio.InicializarBancoDados();

            var setor = new Setor("TesteSetorQueNaoExiste", empresa1);


            // Act
            var resultadoEdicao = setorRepositorio.Editar(setor);

            // Assert
            Assert.False(resultadoEdicao);
        }

        [Fact]
        public void ListarPorId_setorEF()
        {
            // Arrange
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            var setorRepositorio = new SetorRepositorio(connectionString);
            var empresa = empresaRepositorio.Listar().First();

            if (empresa == null)
            {
                empresa = new Empresa("empresa teste", "777777");
                empresaRepositorio.Cadastrar(empresa);
            }
            var setor = setorRepositorio.Listar().FirstOrDefault(s => s.EmpresaId == empresa.Id);
            if (setor == null)
            {
                setor = new Setor("financeiro", empresa);
                setorRepositorio.Cadastrar(setor);
            }
            // Act
            var setorBuscado = setorRepositorio.BuscarPorId(setor.Id);
            // Assert
            Assert.NotNull(setorBuscado);
        }
    }
}
