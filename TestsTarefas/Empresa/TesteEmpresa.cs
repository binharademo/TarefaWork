using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace Tests_Tarefas.teste_Empresa
{
    public class TesteEmpresa
    {

        [Fact]
        public void Cadastrar_Empresa()
        {
            var servico = new EmpresaServico(new EmpresaMemoriaRepositorio());

            Empresa empresa = new Empresa("nome", "CNPJ");
            var resultado = servico.Cadastrar(empresa);

            Assert.True(resultado);
        }

        [Fact]
        public void CadastrarEListarEmpresa()
        {
            var servico = new EmpresaServico(new EmpresaMemoriaRepositorio());

            Empresa empresa = new Empresa("nome", "CNPJ");
            var resultado = servico.Cadastrar(empresa);

            var empresas = servico.Listar();

            Assert.True(resultado);
            Assert.NotEmpty(empresas);
            Assert.Contains(empresa, empresas);
        }

        [Fact]
        public void EditarEmpresa()
        {
            var servico = new EmpresaServico(new EmpresaMemoriaRepositorio());

            Empresa empresa = new Empresa("nome", "CNPJ");
            servico.Cadastrar(empresa);

            var resultado = servico.Editar(empresa.Id, "NovoNome", "novo CNPJ");

            Assert.True(resultado);
            Assert.Equal("NovoNome", empresa.Nome);
        }

        [Fact]
        public void RemoverEmpresa()
        {
            var servico = new EmpresaServico(new EmpresaMemoriaRepositorio());

            Empresa empresa = new Empresa("nome", "CNPJ");
            servico.Cadastrar(empresa);

            var resultadoRemocao = servico.Remover(empresa);
            Assert.True(resultadoRemocao);
        }
    }
}
