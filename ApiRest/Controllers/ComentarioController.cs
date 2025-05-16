using ApiRest.DTOs;
using Microsoft.AspNetCore.Mvc;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;

namespace ApiRest.Controllers;

[ApiController]
[Route("Tarefa/{id}/[controller]")]
public class ComentarioController : ControllerBase
{
    private readonly ILogger<TarefaController> _logger;
    private readonly TarefaServico _tarefa;
    private readonly ComentarioServices _comentario;
    private readonly UsuarioServico _usuario;

    public ComentarioController(ILogger<TarefaController> logger, ITarefaRepositorio tarefa, IComentarioRepositorio comentario, IUsuarioRepositorio usuario)
    {
        _logger = logger;
        _tarefa = new TarefaServico(tarefa);
        _comentario = new ComentarioServices(comentario);
        _usuario = new UsuarioServico(usuario);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ComentarioDTO>> ObterTodas(int id)
    {
        var tarefaExistente = _tarefa.BuscarPorId(id);
        if (tarefaExistente == null)
        {
            return NotFound();
        }

        var comentarios = _comentario.ListarComentarios(id).Select(c => new ComentarioDTO(c));

        return Ok(comentarios);
    }

    [HttpGet("{comentarioId}")]
    public ActionResult<ComentarioDTO> ObterPorId(int comentarioId)
    {
        var comentario = _comentario.BuscarComentario(comentarioId);
        if (comentario == null)
        {
            return NotFound();
        }

        return Ok(new ComentarioDTO(comentario));
    }

    [HttpPost]
    public ActionResult<ComentarioDTO> Criar(int id, ComentarioCriarDTO comentarioDTO)
    {

        var comentario = new Comentario(
            descricao: comentarioDTO.Descricao,
            dataCriacao: comentarioDTO.DataCriacao,
            tarefaId: id,
            usuarioId: comentarioDTO.UsuarioId
        );

        if (!_comentario.SalvarComentario(comentario))
            return StatusCode(500);

        var comentarioCriado = _comentario.BuscarComentario(comentario.Id);
        if (comentarioCriado is null)
            return StatusCode(500);

        var comentarioCriadoDTO = new ComentarioDTO(comentarioCriado);

        return CreatedAtAction(
        nameof(ObterPorId),
        new
        {
            id = id,  
            comentarioId = comentarioCriadoDTO.Id  
        },
        comentarioCriadoDTO);
    }


    [HttpDelete("{comentarioId}")]
    public ActionResult Remover(int comentarioId)
    {
        var comentario = _comentario.BuscarComentario(comentarioId);
        if (comentario == null)
        {
            return NotFound();
        }

        var resultado = _comentario.RemoverComentario(comentario);
        if (!resultado)
        {
            return StatusCode(500, "Erro ao remover o comentario");
        }

        return NoContent();
    }

    [HttpPut("{comentarioId}")]
    public ActionResult<ComentarioDTO>  Atualizar(int id, int comentarioId, ComentarioAtualizarDTO comentarioDTO)
    {
        var comentarioExistente = _comentario.BuscarComentario(comentarioId);
        if (comentarioExistente == null)
        {
            return NotFound();
        }


        // Atualizar os campos da tarefa
        comentarioExistente.Descricao = comentarioDTO.Descricao;


        if (!_comentario.AlterarComentario(comentarioExistente))
            return StatusCode(500);

        var comentarioAtualizado = _comentario.BuscarComentario(comentarioExistente.Id);
        if (comentarioAtualizado is null)
            return StatusCode(500);

        return Ok(new ComentarioDTO(comentarioAtualizado));
    }

}