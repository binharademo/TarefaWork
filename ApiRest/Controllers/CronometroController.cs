using ApiRest.DTOs;
using Microsoft.AspNetCore.Mvc;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;

namespace ApiRest.Controllers;

[ApiController]
[Route("Tarefa/{id}")]
public class CronometroController : ControllerBase
{
    private readonly ILogger<TarefaController> _logger;
    private readonly TarefaServico _tarefa;

    public CronometroController(ILogger<TarefaController> logger, ITarefaRepositorio tarefa)
    {
        _logger = logger;
        _tarefa = new TarefaServico(tarefa);
    }

    [HttpPut("play")]
    public ActionResult<TarefaBasicoDTO> IniciarCronometro(int id)
    {

        var tarefaExistente = _tarefa.BuscarPorId(id);
        if (tarefaExistente == null)
        {
            return NotFound();
        }

        _tarefa.IniciaCronometro(tarefaExistente);

        var tarefaAtualizada = _tarefa.BuscarPorId(id);
        if (tarefaAtualizada is null)
            return StatusCode(500);

        return Ok(new TarefaBasicoDTO(tarefaAtualizada));
    }

    [HttpPut("pause")]
    public ActionResult<TarefaBasicoDTO> PausarCronometro(int id)
    {

        var tarefaExistente = _tarefa.BuscarPorId(id);
        if (tarefaExistente == null)
        {
            return NotFound();
        }

        _tarefa.PausaCronometro(tarefaExistente);

        var tarefaAtualizada = _tarefa.BuscarPorId(id);
        if (tarefaAtualizada is null)
            return StatusCode(500);

        return Ok(new TarefaBasicoDTO(tarefaAtualizada));
    }

}