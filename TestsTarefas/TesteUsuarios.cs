using TarefasLibrary;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;


namespace Tests_Tarefas
{
    public class UsuarioTeste
    {

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
            Assert.Equal(3, resultado.Count);
        }





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




