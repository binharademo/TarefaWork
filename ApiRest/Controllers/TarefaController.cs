using ApiRest.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

    public TarefaController(ILogger<TarefaController> logger/*, UsuarioServico usuario*/)
    {
        _logger = logger;
        _tarefa = new TarefaServico(new TarefaMemoriaRepositorio());//usuario;
        _usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());//usuario;

    }

    [HttpGet]
    public ActionResult<IEnumerable<TarefaDTO>> ObterTodas()
    {
        var tarefas = _tarefa.ListarTodas();
        var tarefasDTO = tarefas.Select(t => new TarefaDTO
        {
            Id = t.Id,
            Titulo = t.Titulo,
            Status = t.Status,
            Criador = new UsuarioDTO
            {
                Id = t.Criador.Id,
                Nome = t.Criador.Nome,
                FuncaoUsuario = t.Criador.FuncaoUsuario,
                SetorUsuario = t.Criador.SetorUsuario
            },
            Responsavel = new UsuarioDTO
            {
                Id = t.Responsavel.Id,
                Nome = t.Responsavel.Nome,
                FuncaoUsuario = t.Responsavel.FuncaoUsuario,
                SetorUsuario = t.Responsavel.SetorUsuario
            },
            Membros = t.Membros.Select(m => new UsuarioDTO
            {
                Id = m.Id,
                Nome = m.Nome,
                FuncaoUsuario = m.FuncaoUsuario,
                SetorUsuario = m.SetorUsuario

            }).ToList(),
            Descricao = t.Descricao,
            DataCriacao = t.DataCriacao,
            Prazo = t.Prazo,
            TempoTotal = t.TempoTotal,
            PrioridadeTarefa = t.PrioridadeTarefa
        });

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

        var tarefaDTO = new TarefaDTO
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Status = tarefa.Status,
            Criador = new UsuarioDTO
            {
                Id = tarefa.Criador.Id,
                Nome = tarefa.Criador.Nome,
                FuncaoUsuario = tarefa.Criador.FuncaoUsuario,
                SetorUsuario = tarefa.Criador.SetorUsuario
            },
            Responsavel = new UsuarioDTO
            {
                Id = tarefa.Responsavel.Id,
                Nome = tarefa.Responsavel.Nome,
                FuncaoUsuario = tarefa.Responsavel.FuncaoUsuario,
                SetorUsuario = tarefa.Responsavel.SetorUsuario
            },
            Membros = tarefa.Membros.Select(m => new UsuarioDTO
            {
                Id = m.Id,
                Nome = m.Nome,
                FuncaoUsuario = m.FuncaoUsuario,
                SetorUsuario = m.SetorUsuario
            }).ToList(),
            Descricao = tarefa.Descricao,
            DataCriacao = tarefa.DataCriacao,
            Prazo = tarefa.Prazo,
            TempoTotal = tarefa.TempoTotal,
            PrioridadeTarefa = tarefa.PrioridadeTarefa
        };

        return Ok(tarefaDTO);
    }

    [HttpPost]
    public ActionResult<TarefaDTO> Criar(CriarTarefaDTO tarefaDTO)
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
            criador: criador,
            responsavel: responsavel,
            prazo: tarefaDTO.Prazo,
            descricao: tarefaDTO.Descricao,
            prioridade: tarefaDTO.PrioridadeTarefa
        );

        if (!_tarefa.Salvar(tarefa))
            return StatusCode(500);

        var tarefaCriada = _tarefa.BuscarPorId(tarefa.Id);
        if (tarefaCriada is null)
            return StatusCode(500);

        var tarefaCriadaDTO = new TarefaDTO
        {
            Id = tarefaCriada.Id,
            Titulo = tarefaCriada.Titulo,
            Status = tarefaCriada.Status,
            Criador = new UsuarioDTO
            {
                Id = tarefaCriada.Criador.Id,
                Nome = tarefaCriada.Criador.Nome,

            },
            Responsavel = new UsuarioDTO
            {
                Id = tarefaCriada.Responsavel.Id,
                Nome = tarefaCriada.Responsavel.Nome,

            },
            Membros = tarefaCriada.Membros.Select(m => new UsuarioDTO
            {
                Id = m.Id,
                Nome = m.Nome,

            }).ToList(),
            Descricao = tarefaCriada.Descricao,
            DataCriacao = tarefaCriada.DataCriacao,
            Prazo = tarefaCriada.Prazo,
            TempoTotal = tarefaCriada.TempoTotal,
            PrioridadeTarefa = tarefaCriada.PrioridadeTarefa
        };

        return CreatedAtAction(nameof(ObterPorId), new { id = tarefaCriadaDTO.Id }, tarefaCriadaDTO);
    }

    [HttpPut("{id}")]
    public ActionResult<TarefaDTO> Atualizar(int id, AtualizarTarefaDTO tarefaDTO)
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
        tarefaExistente.Status = tarefaDTO.Status;
        tarefaExistente.Responsavel = responsavel;
        tarefaExistente.Prazo = tarefaDTO.Prazo;
        tarefaExistente.Descricao = tarefaDTO.Descricao;
        tarefaExistente.PrioridadeTarefa = tarefaDTO.PrioridadeTarefa;


        if (!_tarefa.Atualizar(tarefaExistente, tarefaDTO.Status, tarefaDTO.Descricao, tarefaDTO.Prazo))
            return StatusCode(500);

        var tarefaAtualizada = _tarefa.BuscarPorId(tarefaExistente.Id);
        if (tarefaAtualizada is null)
            return StatusCode(500);

        var tarefaAtualizadaDTO = new TarefaDTO
        {
            Id = tarefaAtualizada.Id,
            Titulo = tarefaAtualizada.Titulo,
            Status = tarefaAtualizada.Status,
            Criador = new UsuarioDTO
            {
                Id = tarefaAtualizada.Criador.Id,
                Nome = tarefaAtualizada.Criador.Nome,
                FuncaoUsuario = tarefaAtualizada.Criador.FuncaoUsuario,
                SetorUsuario = tarefaAtualizada.Criador.SetorUsuario
            },
            Responsavel = new UsuarioDTO
            {
                Id = tarefaAtualizada.Responsavel.Id,
                Nome = tarefaAtualizada.Responsavel.Nome,
                FuncaoUsuario = tarefaAtualizada.Responsavel.FuncaoUsuario,
                SetorUsuario = tarefaAtualizada.Responsavel.SetorUsuario

            },
            Membros = tarefaAtualizada.Membros.Select(m => new UsuarioDTO
            {
                Id = m.Id,
                Nome = m.Nome,
                
            }).ToList(),
            Descricao = tarefaAtualizada.Descricao,
            DataCriacao = tarefaAtualizada.DataCriacao,
            Prazo = tarefaAtualizada.Prazo,
            TempoTotal = tarefaAtualizada.TempoTotal,
            PrioridadeTarefa = tarefaAtualizada.PrioridadeTarefa
        };

        return Ok(tarefaAtualizadaDTO);
    }

}
