
using ApiRest.DTOs;
using TarefasLibrary.Modelo;

namespace GerenciadorUsuarios.Services;

public class UsuarioServico
{
    private readonly List<UsuarioDTO> _usuarios = new();
    private int _nextId = 1;

    public UsuarioServico()
    {
        // Adicionar algumas usuarios iniciais para teste
        AddUsuariosIniciais();
    }

    private void AddUsuariosIniciais()
    {
        // Usuario 1
        _usuarios.Add(new UsuarioDTO
        {
            Id = _nextId++,
            Nome = "Fernando",
            FuncaoUsuario = Usuario.Funcao.Analista,
            SetorUsuario = Usuario.Setor.Ti 
        });

        // Usuario 2
        _usuarios.Add(new UsuarioDTO
        {
            Id = _nextId++,
            Nome = "Rodrigo",
            FuncaoUsuario = Usuario.Funcao.Analista,
            SetorUsuario = Usuario.Setor.Ti
        });

        // Usuario 3
        _usuarios.Add(new UsuarioDTO
        {
            Id = _nextId++,
            Nome = "Vinicius",
            FuncaoUsuario = Usuario.Funcao.Analista,
            SetorUsuario = Usuario.Setor.Ti
        });

        // Usuario 4
        _usuarios.Add(new UsuarioDTO
        {
            Id = _nextId++,
            Nome = "Guilherme",
            FuncaoUsuario = Usuario.Funcao.Analista,
            SetorUsuario = Usuario.Setor.Ti
        });

        // Usuario 5
        _usuarios.Add(new UsuarioDTO
        {
            Id = _nextId++,
            Nome = "Gabriel",
            FuncaoUsuario = Usuario.Funcao.Analista,
            SetorUsuario = Usuario.Setor.Ti
        });
    }

    public Task<List<UsuarioDTO>> GetUsuariosAsync() => Task.FromResult(_usuarios);

    public Task<UsuarioDTO?> GetUsuarioByIdAsync(int id) =>
        Task.FromResult(_usuarios.FirstOrDefault(t => t.Id == id));

    public Task AddUsuarioAsync(UsuarioDTO usuario)
    {
        usuario.Id = _nextId++;
        _usuarios.Add(usuario);
        return Task.CompletedTask;
    }

    public Task UpdateUsuarioAsync(UsuarioDTO usuario)
    {
        var existente = _usuarios.FirstOrDefault(t => t.Id == usuario.Id);
        if (existente != null)
        {
            existente.Id = usuario.Id;
            existente.Nome = usuario.Nome;
            existente.FuncaoUsuario = usuario.FuncaoUsuario;
            existente.SetorUsuario = usuario.SetorUsuario;
        }
        return Task.CompletedTask;
    }

    public Task DeleteUsuarioAsync(int id)
    {
        var usuario = _usuarios.FirstOrDefault(t => t.Id == id);
        if (usuario != null) _usuarios.Remove(usuario);
        return Task.CompletedTask;
    }
}
