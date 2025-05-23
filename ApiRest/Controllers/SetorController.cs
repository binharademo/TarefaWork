using ApiRest.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Negocio;
using TarefasLibrary.Repositorio;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class SetorController : ControllerBase
{
    private readonly ILogger<SetorController> _logger;
    private readonly SetorServico _setor;
    private readonly EmpresaServico _empresa;

    public SetorController(ILogger<SetorController> logger, IRepositorio<Setor> setor, IRepositorio<Empresa> empresa)
    {
        _logger = logger;
        _setor = new SetorServico(setor);
        _empresa = new EmpresaServico(empresa);
    }

    [HttpGet]
    public ActionResult<IEnumerable<SetorDTO>> ObterTodos()
    {
        var setores = _setor.Listar();
        var setoresDTO = setores.Select(s => new SetorDTO(s));

        return Ok(setoresDTO);
    }

    [HttpGet("{id}")]
    public ActionResult<SetorDTO> ObterPorId(int id)
    {
        var setor = _setor.BuscarPorId(id);
        if (setor == null)
        {
            return NotFound();
        }

        return Ok(new SetorDTO(setor));
    }

    [HttpPost]
    public ActionResult<SetorDTO> Criar(CriarSetorDTO setorDTO)
    {
        // Primeiro, verificamos se a empresa existe
        var empresa = _empresa.BuscarPorId(setorDTO.EmpresaId);
        if (empresa == null)
        {
            return BadRequest("Empresa não encontrada");
        }

        var setor = new Setor
        {
            Nome = setorDTO.Nome,
            Status = setorDTO.Status,
            EmpresaId = setorDTO.EmpresaId
        };

        if (!_setor.Cadastrar(setor))
            return StatusCode(500);

        var setorCriado = _setor.BuscarPorId(setor.Id);
        if (setorCriado is null)
            return StatusCode(500);

        var setorCriadoDTO = new SetorDTO(setorCriado);

        return CreatedAtAction(
        nameof(ObterPorId),
        new
        {
            id = setor.Id,
        },
        setorCriadoDTO);
    }

    [HttpPut("{id}")]
    public ActionResult<SetorDTO> Atualizar(int id, AlterarSetorDTO setorDTO)
    {
        var setorExistente = _setor.BuscarPorId(id);
        if (setorExistente == null)
        {
            return NotFound();
        }

        // Atualizar os campos do setor
        setorExistente.Nome = setorDTO.Nome;
        setorExistente.Status = setorDTO.Status;

        if (!_setor.Editar(setorExistente))
        {
            return StatusCode(500, "Erro ao atualizar o setor");
        }

        var setorAtualizado = _setor.BuscarPorId(id);
        return Ok(new SetorDTO(setorAtualizado));
    }

    [HttpDelete("{id}")]
    public ActionResult Remover(int id)
    {
        var setor = _setor.BuscarPorId(id);
        if (setor == null)
        {
            return NotFound();
        }

        var resultado = _setor.Remover(setor);
        if (!resultado)
        {
            return StatusCode(500, "Erro ao remover o setor");
        }

        return NoContent();
    }
}