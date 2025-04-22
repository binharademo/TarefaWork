using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace Tests_Tarefas
{
    public class TesteListaUsuario
    {
        // TODO: Remover os parâmetros comentados do método de teste
        // TODO: Verificar a quantidade exata de usuários retornados (deve ser 1)
        // TODO: Testar caso em que não existem usuários no setor especificado
        [Fact]
        public void ListarUsuarioPorSetor(/*int id, string nome, string senha, string funcao, string setor*/)
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Marketing);
            Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Diretoria);

            servico.Criar(u01);
            servico.Criar(u02);
            servico.Criar(u03);


            //act
            var resultado = servico.ListarUsuarioPorSetor(Usuario.Setor.Ti);

            //assert
            Assert.NotEmpty(resultado);
            Assert.Equal(Usuario.Setor.Ti, resultado[0].SetorUsuario);
        }

        // TODO: Adicionar caso de teste para função que não tem nenhum usuário
        // TODO: Verificar se os usuários retornados são exatamente os esperados (não apenas a contagem)
        // TODO: Considerar usar SetUp/TearDown para inicializar o repositório e evitar duplicação de código
        [Theory]
        [InlineData(Usuario.Funcao.Analista, 1)]
        [InlineData(Usuario.Funcao.Marketing, 2)]
        [InlineData(Usuario.Funcao.Dev, 3)]
        public void ListarUsuarioPorFuncao(Usuario.Funcao funcao, int total)
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            servico.Criar(new Usuario("binhara", "123", Usuario.Funcao.Analista, Usuario.Setor.Ti));
            servico.Criar(new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));
            servico.Criar(new Usuario("binhara1", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti));
            servico.Criar(new Usuario("binhara3", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));
            servico.Criar(new Usuario("binhara5", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti));
            servico.Criar(new Usuario("binhara4", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));

            //act
            var resultado = servico.ListarUsuarioPorFuncao(funcao);

            //assert
            Assert.NotEmpty(resultado);
            Assert.Equal(total, resultado.Count);
            Assert.Empty(resultado.Where(u => u.FuncaoUsuario != funcao));
        }

    }

}




