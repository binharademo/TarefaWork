using TarefasLibrary;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;


namespace Tests_Tarefas
{
    public class UsuarioTeste
    {

        // TODO: Verificar se o usuário foi realmente persistido no repositório
        // TODO: Testar criação de usuário com dados inválidos (nome vazio, senha fraca, etc.)
        // TODO: Verificar se o ID do usuário é atribuído corretamente
        [Fact]
        public void CriarUsuario()
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            //act
            Usuario usuario01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            servico.Criar(usuario01);


            //assert
            Assert.NotNull(usuario01);

        }


        [Fact]
        public void ListarUsuario(/*int id, string nome, string senha, string funcao, string setor*/)
        {
            //arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);

            servico.Criar(u01);
            servico.Criar(u02);
            servico.Criar(u03);


            //act
            var resultado = servico.ListarUsuario();

            //assert
            Assert.NotEmpty(resultado);
           //Assert.Equal( resultado.Count);
        }





        // TODO: Este método não tem a anotação [Fact] ou [Theory], portanto não é executado como teste
        // TODO: Os usuários são criados mas não são salvos no repositório
        // TODO: O teste tenta buscar um usuário por ID sem ter salvo nenhum usuário com esse ID
        public void SalvarEBuscar(int id, string nome, string senha, Usuario.Funcao funcao, Usuario.Setor setor)
        {
            // arrange
            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio());

            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);

            //act
            var result = servico.Buscar(id);

            //assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(nome, result.Nome);
            Assert.Equal(senha, result.Senha);
            Assert.Equal(Usuario.Funcao.Dev, result.FuncaoUsuario);
            Assert.Equal(setor, result.SetorUsuario);
        }

        // TODO: Verificar se os usuários retornados são exatamente os mesmos que foram criados
        // TODO: Testar o caso de listar quando não há usuários cadastrados
        // TODO: Considerar usar SetUp/TearDown para inicializar o repositório e evitar duplicação de código
        [Fact]
        public void ListarUsuarios()
        {
            //arrange
            Usuario u01 = new Usuario("binhara", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u02 = new Usuario("binhara1", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);
            Usuario u03 = new Usuario("binhara2", "123", Usuario.Funcao.Dev, Usuario.Setor.Ti);


            UsuarioServico servico = new UsuarioServico(new UsuarioMemoriaRepositorio()); // configuracao
            servico.Criar(u01);
            servico.Criar(u02);
            servico.Criar(u03);

            //act
            var resultado = servico.ListarUsuario();

            //assert
            Assert.NotEmpty(resultado);
            Assert.Equal(3, resultado.Count);
        }
    }

}




