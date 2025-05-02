using ApiRest.DTOs;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.Servicos
{
    public class UsuarioServico
    {
        private List<UsuarioDTO> _usuario = new List<UsuarioDTO>();
        private int _nextId = 1;

        public Task Adicionar(CriarUsuarioDTO usuario)
        {
            _usuario.Add(new UsuarioDTO
            {
                Id = _nextId++,
                Nome = usuario.Nome,
                FuncaoUsuario = usuario.FuncaoUsuario,
                SetorUsuario = usuario.SetorUsuario
            });
            return Task.CompletedTask;
        }

        public Boolean Atualizar(UsuarioDTO usuario)
        {
            var usuarioExistente = _usuario.FirstOrDefault(t => t.Id == usuario.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Senha = usuario.Senha;
                usuarioExistente.FuncaoUsuario = usuario.FuncaoUsuario;
                usuarioExistente.SetorUsuario = usuario.SetorUsuario;
                return(true);
            }
            return (false);
        }

        public Task<List<UsuarioDTO>> BuscaTodos()
        {
            return Task.FromResult(_usuario);
        }

        public async Task<UsuarioDTO> BuscaPorId(int id) =>
            _usuario.FirstOrDefault(t => t.Id == id);

        public Task Remover(int id)
        {
            var usuario = _usuario.FirstOrDefault(t => t.Id == id);
            if (usuario != null) _usuario.Remove(usuario);
            return Task.CompletedTask;
        }

    }
}
