using System;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class UsuarioMemoriaRepositorio : IRepositorio<Usuario>
    {
        private static List<Usuario> ListaUsuarios = new List<Usuario>();

        public bool Cadastrar(Usuario usuario)
        {
            usuario.Id = GeraNovoId();
            ListaUsuarios.Add(usuario);
            return true;
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

        public List<Usuario> Listar()
        {
            return ListaUsuarios;
        }

        public bool Editar(Usuario obj)
        {
            var usuarioExistente = BuscarPorId(obj.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = obj.Nome;
                usuarioExistente.Senha = obj.Senha;
                usuarioExistente.Funcao = obj.Funcao;
                usuarioExistente.Setor = obj.Setor;
                return true;
            }
            return false;
        }

        public bool Remover(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
