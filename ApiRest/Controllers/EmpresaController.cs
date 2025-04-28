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
public class EmpresaController : ControllerBase
{
    private readonly ILogger<EmpresaController> _logger;
    private readonly EmpresaServico _empresa;

    public EmpresaController(ILogger<EmpresaController> logger, IRepositorio<Empresa> empresa)
    {
        _logger = logger;
        _empresa = new EmpresaServico(empresa);
    }

    [HttpGet]
    public ActionResult<IEnumerable<EmpresaDTO>> ObterTodas()
    {
        var empresas = _empresa.Listar();
        var empresasDTO = empresas.Select(u => new EmpresaDTO(u));

        return Ok(empresasDTO);
    }

    [HttpGet("{id}")]
    public ActionResult<EmpresaDTO> ObterPorId(int id)
    {
        var empresa = _empresa.BuscarPorId(id);
        if (empresa == null)
        {
            return NotFound();
        }

        return Ok(new EmpresaDTO(empresa));
    }

    [HttpPost]
    public ActionResult<EmpresaDTO> Criar(CriarEmpresaDTO empresaDTO)
    {
        var empresa = new Empresa(
            nome: empresaDTO.Nome,
            cnpj: empresaDTO.Cnpj
            );

        if (!_empresa.Cadastrar(empresa))
            return StatusCode(500);

        var empresaCriada = _empresa.BuscarPorId(empresa.Id);
        if (empresaCriada is null)
            return StatusCode(500);

        var empresaCriadaDTO = new EmpresaDTO(empresaCriada);

        return CreatedAtAction(
        nameof(ObterPorId),
        new
        {
            id = empresa.Id,
        },
        empresaCriadaDTO);

    }


    [HttpPut("{id}")]
    public ActionResult<EmpresaDTO> Atualizar(int id, AtualizarEmpresaDTO empresaDTO)
    {
        var empresaExistente = _empresa.BuscarPorId(id);
        if(empresaExistente == null) 
        {
            return NotFound();
        }

        // Atualizar os campos do usuário
        empresaExistente.Nome = empresaDTO.Nome;

        var empresaAtualizada = _empresa.Editar(empresaExistente);

        return Ok(empresaAtualizada);
    }

    [HttpDelete("{id}")]
    public ActionResult Remover(int id)
    {
        var empresa = _empresa.BuscarPorId(id);
        if (empresa == null)
        {
            return NotFound();
        }

        var resultado = _empresa.Remover(empresa);
        if (!resultado)
        {
            return StatusCode(500, "Erro ao remover a empresa");
        }

        return NoContent();
    }

}
