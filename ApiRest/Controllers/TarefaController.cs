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
public class TarefaController : ControllerBase
{
    private readonly ILogger<TarefaController> _logger;
    private readonly TarefaServico _tarefa;
    private readonly UsuarioServico _usuario;

    public TarefaController(ILogger<TarefaController> logger, ITarefaRepositorio tarefa, IUsuarioRepositorio usuario)
    {
        _logger = logger;
        _tarefa = new TarefaServico(tarefa);
        _usuario = new UsuarioServico(usuario);

    }

    [HttpGet]
    public ActionResult<IEnumerable<TarefaDTO>> ObterTodas()
    {
        var tarefas = _tarefa.ListarTodas();
        var tarefasDTO = tarefas.Select(t => new TarefaDTO(t));

        return Ok(tarefasDTO);
    }

    [HttpGet("{id}")]
    public ActionResult<TarefaDTO> ObterPorId(int id)
    {
        var tarefa = _tarefa.BuscarPorId(id);
        if (tarefa == null)
        {
            return NotFound();
        }

        return Ok(new TarefaDTO(tarefa));
    }

    [HttpPost]
    public ActionResult<TarefaBasicoDTO> Criar(CriarTarefaDTO tarefaDTO)
    {
        var criador = _usuario.Buscar(tarefaDTO.CriadorId);
        if (criador == null)
        {
            return BadRequest("Criador não encontrado");
        }

        var responsavel = _usuario.Buscar(tarefaDTO.ResponsavelId);
        if (responsavel == null)
        {
            return BadRequest("Responsável não encontrado");
        }

        var tarefa = new Tarefa(
            titulo: tarefaDTO.Titulo,
            status: tarefaDTO.Status,
            criador: tarefaDTO.CriadorId,
            responsavel: tarefaDTO.ResponsavelId,
            prazo: tarefaDTO.Prazo,
            descricao: tarefaDTO.Descricao,
            prioridade: tarefaDTO.PrioridadeTarefa
        );

        if (!_tarefa.Salvar(tarefa))
            return StatusCode(500);

        var tarefaCriada = _tarefa.BuscarPorId(tarefa.Id);
        if (tarefaCriada is null)
            return StatusCode(500);

        var tarefaCriadaDTO = new TarefaBasicoDTO(tarefaCriada);

        return CreatedAtAction(nameof(ObterPorId), new { id = tarefaCriadaDTO.Id }, tarefaCriadaDTO);
    }

    [HttpPut("{id}")]
    public ActionResult<TarefaBasicoDTO> Atualizar(int id, AtualizarTarefaDTO tarefaDTO)
    {
        var tarefaExistente = _tarefa.BuscarPorId(id);
        if (tarefaExistente == null)
        {
            return NotFound();
        }

        var responsavel = _usuario.Buscar(tarefaDTO.ResponsavelId);
        if (responsavel == null)
        {
            return BadRequest("Responsável não encontrado");
        }

        // Atualizar os campos da tarefa
        tarefaExistente.Titulo = tarefaDTO.Titulo;
        tarefaExistente.StatusTarefa = tarefaDTO.Status;
        tarefaExistente.Responsavel = responsavel;
        tarefaExistente.Prazo = tarefaDTO.Prazo;
        tarefaExistente.Descricao = tarefaDTO.Descricao;
        tarefaExistente.PrioridadeTarefa = tarefaDTO.PrioridadeTarefa;


        if (!_tarefa.Atualizar(tarefaExistente, tarefaDTO.Status, tarefaDTO.Descricao, tarefaDTO.Prazo, tarefaDTO.Titulo,tarefaDTO.PrioridadeTarefa))
            return StatusCode(500);

        var tarefaAtualizada = _tarefa.BuscarPorId(tarefaExistente.Id);
        if (tarefaAtualizada is null)
            return StatusCode(500);

        return Ok(new TarefaBasicoDTO(tarefaAtualizada));
    }

    [HttpPut("{id}/membro/{membro}")]
    public ActionResult<TarefaBasicoDTO> AdicionarMembro(int id, int membro)
    {

        var tarefaExistente = _tarefa.BuscarPorId(id);
        if (tarefaExistente == null)
        {
            return NotFound();
        }

        var novoMembro = _usuario.Buscar(membro);
        if (novoMembro == null)
        {
            return BadRequest("Responsável não encontrado");
        }

        _tarefa.MarcarMembro(tarefaExistente, novoMembro);

        var tarefaAtualizada = _tarefa.BuscarPorId(id);
        if (tarefaAtualizada is null)
            return StatusCode(500);

        return Ok(new TarefaBasicoDTO(tarefaAtualizada));
    }

    [HttpPut("{id}/status/{status}")]
    public ActionResult<TarefaBasicoDTO> AtualizarStatus(int id, Tarefa.Status status)
    {

        var tarefaExistente = _tarefa.BuscarPorId(id);
        if (tarefaExistente == null)
        {
            return NotFound();
        }

        try
        {
            _tarefa.Atualizar(tarefaExistente, (Tarefa.Status)status);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

        var tarefaAtualizada = _tarefa.BuscarPorId(id);
        if (tarefaAtualizada is null)
            return StatusCode(500);

        return Ok(new TarefaBasicoDTO(tarefaAtualizada));
    }

}
