using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    interface IUsuarioServico
    {
        public Usuario? Buscar(int id);
        public Usuario Criar(Usuario usuario);
        public List<Usuario> ListarUsuario();
        public bool Editar(int id, string nome, string senha, Usuario.Funcao funcao, Setor setorusuario);
        //public bool Remover(int id);
    }
}
