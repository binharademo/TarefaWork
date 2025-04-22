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

    public TarefaController(ILogger<TarefaController> logger/*, UsuarioServico usuario*/)
    {
        _logger = logger;
        _tarefa = new TarefaServico(new TarefaMemoriaRepositorio());//usuario;
    }

    [HttpPost]
    public async Task<IActionResult> SalvarTarefa([FromBody] Tarefa tarefa)
    {
        var result = ModelState.IsValid; 
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido na requisição SalvarTarefa");
                return BadRequest(ModelState);
            }

            _tarefa.Salvar(tarefa);
            _logger.LogInformation($"Tarefa criada com ID: {tarefa.Id}");

            return CreatedAtAction(nameof(BuscaPorId), new { id = tarefa.Id }, tarefa);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro de argumento ao salvar tarefa");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao salvar tarefa");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua requisição");
        }
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] Tarefa tarefa)
    {
        // Validação básica dos dados recebidos
        if (tarefa == null)
        {
            return BadRequest("Dados do usuário não foram informados.");
        }
        if (id != tarefa.Id)
        {
            return BadRequest("Id não encontrado.");
        }

        try
        {
            
            bool editado = _tarefa.Atualizar(tarefa, tarefa.Status, tarefa.Descricao, tarefa.Prazo);

            if (!editado)
            {
                return NotFound("Usuário não encontrado ou não foi possível editar.");
            }

            // Retorna NoContent indicando que a alteração foi feita com sucesso
            return NoContent();
        }

        catch (Exception ex)
        {
            // Opcional: log da exceção
            return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscaPorId(int id)
    {
        try
        {
            var retorno = _tarefa.BuscarPorId(id);

            if (retorno is null)
                return NotFound("Não foi possível encontrar o ID da tarefa");

            return Ok(retorno);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var retorno = _tarefa.ListarTodas();
            if (retorno is null)
                return NotFound("Nenhuma tarefa listada");

            return Ok(retorno);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
        }
    }

}
