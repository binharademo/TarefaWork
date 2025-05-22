using TarefasLibrary;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;


namespace Tests_Tarefas
{
    public class UsuarioTeste
    {
        public class UsuarioServicoTests
        {
            private readonly UsuarioServico _servico;

            public UsuarioServicoTests()
            {
                var repositorio = new UsuarioMemoriaRepositorio();
                var setor = new Setor("Setor Teste", 1);
                _servico = new UsuarioServico(repositorio);

                _servico.Criar(new Usuario(1, "binhara", "123", Usuario.Funcao.Dev, setor));
                _servico.Criar(new Usuario(2, "binhara2", "123", Usuario.Funcao.Dev, setor));
                _servico.Criar(new Usuario(3, "binhara3", "123", Usuario.Funcao.Dev, setor));

            }

            // TODO: Verificar se o usuário foi realmente persistido no repositório
            // TODO: Testar criação de usuário com dados inválidos (nome vazio, senha fraca, etc.)
            // TODO: Verificar se o ID do usuário é atribuído corretamente
            [Fact]
            public void CriarUsuario()
            {
                //arrange

                UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
                var setor = new Setor("Setor Teste", 1);

                //act
                Usuario usuario01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, setor);
                servico.Criar(usuario01);
                servico.Buscar(usuario01.Id);

                Usuario usuario02 = new Usuario("", "123", Usuario.Funcao.Dev, setor); // não está validando o nome vazio, cria o objeto sem nome
                servico.Criar(usuario02);
                servico.Buscar(usuario02.Id);


                //assert
                Assert.NotNull(usuario01);
                Assert.NotNull(usuario02);

            }


            [Fact]
            public void ListarUsuario()
            {

                //act / arrange
                var resultado = _servico.ListarUsuario();

                //assert
                Assert.NotEmpty(resultado);
                Assert.Equal(3, resultado.Count);
            }





            // TODO: Este método não tem a anotação [Fact] ou [Theory], portanto não é executado como teste
            // TODO: Os usuários são criados mas não são salvos no repositório
            // TODO: O teste tenta buscar um usuário por ID sem ter salvo nenhum usuário com esse ID
            [Fact]
            public void SalvarEBuscar_DeveSalvarERecuperarUsuarioCorretamente()
            {
                // arrange
                var servico = new UsuarioServico(new UsuarioMemoriaRepositorio());
                var setor = new Setor("Setor Teste", 1);

                var usuario = new Usuario("binhara", "123", Usuario.Funcao.Dev, setor);
                servico.Criar(usuario);

                // act
                var result = servico.Buscar(usuario.Id);

                // assert
                Assert.NotNull(result);
                Assert.Equal(usuario.Id, result.Id);
                Assert.Equal(usuario.Nome, result.Nome);
                Assert.Equal(usuario.Senha, result.Senha);
                Assert.Equal(usuario.FuncaoUsuario, result.FuncaoUsuario);
                Assert.Equal(usuario.SetorUsuario, result.SetorUsuario);
            }


            // TODO: Verificar se os usuários retornados são exatamente os mesmos que foram criados
            // TODO: Testar o caso de listar quando não há usuários cadastrados
            [Fact]
            public void ListarUsuarios()
            {
         

                //act
                var resultado = _servico.ListarUsuario();

                //assert
                Assert.NotEmpty(resultado);
                Assert.Equal(3, resultado.Count);
            }

        }

    }
}




