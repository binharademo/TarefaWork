using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace Tests_Tarefas
{
    public class TesteListaUsuario
    {
        public class UsuarioServicoTests
        {
            private readonly UsuarioServico _servico;

            public UsuarioServicoTests()
            {
                var repositorio = new UsuarioMemoriaRepositorio();
                _servico = new UsuarioServico(repositorio);

                _servico.Criar(new Usuario("binhara", "123", Usuario.Funcao.Analista, Usuario.Setor.Ti));
                _servico.Criar(new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));
                _servico.Criar(new Usuario("binhara1", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti));
                _servico.Criar(new Usuario("binhara3", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));
                _servico.Criar(new Usuario("binhara5", "123", Usuario.Funcao.Marketing, Usuario.Setor.Ti));
                _servico.Criar(new Usuario("binhara4", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti));
            }
            [Fact]
            public void ListarUsuarioPorSetor_DeveRetornarSomenteUsuariosDoSetor()
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
                Assert.Single(resultado);
                Assert.Equal(Usuario.Setor.Ti, resultado[0].SetorUsuario);
            }

            [Fact]
            public void ListarUsuarioPorSetor_SemUsuariosNoSetor_DeveRetornarListaVazia()
            {
                // Arrange
                var servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

                var u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Marketing);
                var u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Diretoria);

                servico.Criar(u01);
                servico.Criar(u02);

                // Act
                var resultado = servico.ListarUsuarioPorSetor(Usuario.Setor.Ti);

                // Assert
                Assert.NotNull(resultado);
                Assert.Empty(resultado);
            }

            // TODO: Adicionar caso de teste para função que não tem nenhum usuário
            // TODO: Verificar se os usuários retornados são exatamente os esperados (não apenas a contagem)
            [Theory]
            [InlineData(Usuario.Funcao.Analista, 1)]
            [InlineData(Usuario.Funcao.Marketing, 2)]
            [InlineData(Usuario.Funcao.Dev, 3)]
            public void ListarUsuarioPorFuncao_DeveRetornarUsuariosComAFuncaoCorreta(Usuario.Funcao funcao, int total)
            {
                // act
                var resultado = _servico.ListarUsuarioPorFuncao(funcao);

                // assert
                Assert.NotEmpty(resultado);
                Assert.Equal(total, resultado.Count);
                Assert.Empty(resultado.Where(u => u.FuncaoUsuario != funcao));
            }

        }

    }
}




