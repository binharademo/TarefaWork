using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio.Entity;

namespace Tests_Tarefas.RepositorioEntity
{
    public class TesteEmpresaEF
    {
        private const string connectionString = "Data Source=TestTarefas.db";

        [Fact]
        public void Cadastro_EmpresaEF()
        {
            var empresa = new Empresa("empresa vinicius", "12345678901234");
            var empresaRepositorio = new EmpresaRepositorio(connectionString);
            empresaRepositorio.InicializarBancoDados();
           

            // Act
            var empresaBuscada = empresaRepositorio.Cadastrar(empresa);

            // Assert
            Assert.True(empresaBuscada);

        }
    }
}
