namespace Tarefas_Library
{
    public class UsuarioMemoriaRepositorio : IUsuarioRepositorio
    {
        private List<Usuario> ListaUsuarios = new List<Usuario>();

        public bool Cadastrar(Usuario usuario)
        {
            usuario.Id = GeraNovoId();
            ListaUsuarios.Add(usuario);
            return true;
        }

        public List<Usuario> ListarUsuario()
        {
            return ListaUsuarios;
        }

        public Usuario? BuscarPorId(int id)
        {
            foreach (var usuario in ListaUsuarios)
            {
                if (usuario.Id == id)
                    return usuario;
            }
            return null;
        }

        private int GeraNovoId()
        {
            if (ListaUsuarios.Count == 0) return 1;

            return ListaUsuarios.Max(u => u.Id) + 1;
        }
    }
}
