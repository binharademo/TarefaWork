using ApiRest.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly UsuarioServico _usuario;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio usuario)
    {
        _logger = logger;
        _usuario = new UsuarioServico(usuario);
    }

    [HttpGet]
    public ActionResult<IEnumerable<UsuarioDTO>> ObterTodos()
    {
        var usuarios = _usuario.ListarUsuario();
        var usuariosDTO = usuarios.Select(u => new UsuarioDTO
        {
            Id = u.Id,
            Nome = u.Nome,
            FuncaoUsuario = u.FuncaoUsuario,
            SetorUsuarioId = u.SetorUsuarioId,
            SetorNome = u.SetorUsuario?.Nome 
        });

        return Ok(usuariosDTO);
    }

    [HttpGet("{id}")]
    public ActionResult<UsuarioDTO> ObterPorId(int id)
    {
        var usuario = _usuario.Buscar(id);
        if (usuario == null)
        {
            return NotFound();
        }

        var usuarioDTO = new UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            FuncaoUsuario = usuario.FuncaoUsuario,
            SetorUsuarioId = usuario.SetorUsuarioId,
            SetorNome = usuario.SetorUsuario?.Nome
        };

        return Ok(usuarioDTO);
    }

    [HttpPost]
    public ActionResult<UsuarioDTO> Criar(CriarUsuarioDTO usuarioDTO)
    {
        var usuario = new Usuario
        {
            Nome = usuarioDTO.Nome,
            Senha = usuarioDTO.Senha,
            FuncaoUsuario = usuarioDTO.FuncaoUsuario,
            SetorUsuarioId = usuarioDTO.SetorUsuarioId
        };

        var usuarioCriado = _usuario.Criar(usuario);

        if (usuarioCriado == null)
        {
            return BadRequest("Não foi possível criar o usuário.");
        }

        var usuarioCriadoDTO = new UsuarioDTO
        {
            Id = usuarioCriado.Id,
            Nome = usuarioCriado.Nome,
            FuncaoUsuario = usuarioCriado.FuncaoUsuario,
            SetorUsuarioId = usuarioCriado.SetorUsuarioId,
            SetorNome = usuarioCriado.SetorUsuario?.Nome 
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = usuarioCriadoDTO.Id }, usuarioCriadoDTO);
    }


    [HttpPut("{id}")]
    public ActionResult<UsuarioDTO> Atualizar(int id, AtualizarUsuarioDTO usuarioDTO)
    {
        var usuarioExistente = _usuario.Buscar(id); 
        if (usuarioExistente == null)
            return NotFound();

        usuarioExistente.Nome = usuarioDTO.Nome;
        usuarioExistente.Senha = usuarioDTO.Senha;
        usuarioExistente.FuncaoUsuario = usuarioDTO.FuncaoUsuario;
        usuarioExistente.SetorUsuarioId = usuarioDTO.SetorUsuarioId;

        var sucesso = _usuario.Editar(usuarioExistente);
        if (!sucesso)
            return BadRequest("Não foi possível atualizar o usuário.");

        var usuarioAtualizado = _usuario.Buscar(id); 
        var usuarioAtualizadoDTO = new UsuarioDTO
        {
            Id = usuarioAtualizado.Id,
            Nome = usuarioAtualizado.Nome,
            FuncaoUsuario = usuarioAtualizado.FuncaoUsuario,
            SetorUsuarioId = usuarioAtualizado.SetorUsuarioId,
            SetorNome = usuarioAtualizado.SetorUsuario?.Nome
        };

        return Ok(usuarioAtualizadoDTO);
    }


    //[HttpDelete("{id}")]
    //public ActionResult Remover(int id)
    //{
    //    var usuario = _usuario.Buscar(id);
    //    if (usuario == null)
    //    {
    //        return NotFound();
    //    }

    //    var resultado = _usuario.Remover(usuario);
    //    if (!resultado)
    //    {
    //        return StatusCode(500, "Erro ao remover o usuário");
    //    }

    //    return NoContent();
    //}

}
