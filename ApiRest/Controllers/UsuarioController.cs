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
         _usuario.Criar(usuario);

        return CreatedAtAction(nameof(BuscaPorId), new { id = usuario.Id }, usuario);
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
            bool editado = _usuario.Editar(id, usuario.Nome, usuario.Senha, usuario.Funcao, usuario.Setor);

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
        var retorno = _usuario.Buscar(id);
        if (retorno is null)
            return NotFound();

        return Ok(retorno);
    }

    [HttpGet]
    public async Task<IActionResult>Listar()
    {
        var retorno = _usuario.ListarUsuario();
        if (retorno is null)
            return NotFound();

        return Ok(retorno);
    }

}
