using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

    public UsuarioController(ILogger<UsuarioController> logger/*, UsuarioServico usuario*/)
    {
        _logger = logger;
        _usuario = new UsuarioServico(new UsuarioMemoriaRepositorio());//usuario;
    }

    [HttpPost]
    public async Task<IActionResult> CriaUsuario([FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Tentativa de criar novo usuário: {Nome}", usuario.Nome);
        
            _usuario.Criar(usuario);
        
            _logger.LogInformation("Usuário criado com sucesso. ID: {Id}", usuario.Id);
        
            return CreatedAtAction(nameof(BuscaPorId), new { id = usuario.Id }, usuario);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Argumento inválido ao criar usuário: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Operação inválida ao criar usuário: {Message}", ex.Message);
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Erro inesperado ao criar usuário: {Message}", ex.Message);
            return StatusCode(500, "Ocorreu um erro interno ao processar sua requisição");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] Usuario usuario)
    {
        // Validação básica dos dados recebidos
        if (usuario == null)
        {
            return BadRequest("Dados do usuário não foram informados.");
        }
        if (id != usuario.Id)
        {
            return BadRequest("Id não encontrado.");
        }

        try
        {
            // Chama o serviço para editar o usuário
            bool editado = _usuario.Editar(id, usuario.Nome, usuario.Senha, usuario.FuncaoUsuario, usuario.SetorUsuario);

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
            var retorno = _usuario.Buscar(id);

            if (retorno is null)
            return NotFound("Não foi possível encontrar o ID");

            return Ok(retorno);
        }
        catch (Exception ex) {
            return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        _logger.LogInformation("Iniciando listagem de usuários");

        try
        {
            var retorno = _usuario.ListarUsuario(); 
            if (retorno == null || !retorno.Any())
            {
                _logger.LogWarning("Nenhum usuário encontrado para listagem");
                return NotFound("Nenhum usuário cadastrado no sistema");
            }

            _logger.LogInformation("Listagem concluída. Total de usuários: {QuantidadeUsuarios}", retorno.Count());
            return Ok(retorno);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao listar usuários. Erro: {ErrorMessage}", ex.Message);
            return StatusCode(500, "Ocorreu um erro interno ao processar a listagem de usuários");
        }
    }

}
