# Exemplos de Problemas e Soluções

Este documento contém exemplos detalhados de problemas encontrados no código e suas respectivas soluções, seguindo os princípios SOLID e boas práticas de código limpo.

## Problemas Críticos

### 1. Violação do Princípio de Inversão de Dependência (DIP)

#### Problema:
Em `ApiRest/Controllers/TarefaController.cs`, os serviços são instanciados dentro do construtor:

```csharp
public TarefaController(ILogger<TarefaController> logger, ITarefaRepositorio tarefa, IUsuarioRepositorio<Usuario> usuario)
{
    _logger = logger;
    _tarefa = new TarefaServico(tarefa);  // Instanciação direta = acoplamento forte
    _usuario = new UsuarioServico(usuario);  // Instanciação direta = acoplamento forte
}
```

#### Solução:
1. Criar interfaces para os serviços:

```csharp
// TarefasLib/Interface/ITarefaServico.cs
public interface ITarefaServico
{
    bool Salvar(Tarefa tarefa);
    Tarefa? BuscarPorId(int id);
    // ... outros métodos
}

// TarefasLib/Interface/IUsuarioServico.cs
public interface IUsuarioServico
{
    Usuario? Buscar(int id);
    // ... outros métodos
}
```

2. Registrar os serviços no contêiner de DI em `Program.cs`:

```csharp
// ApiRest/Program.cs
builder.Services.AddSingleton<ITarefaRepositorio, TarefaMemoriaRepositorio>();
builder.Services.AddSingleton<IUsuarioRepositorio<Usuario>, UsuarioMemoriaRepositorio>();
builder.Services.AddSingleton<ITarefaServico, TarefaServico>();  // Registro do serviço
builder.Services.AddSingleton<IUsuarioServico, UsuarioServico>();  // Registro do serviço
```

3. Injetar as interfaces no controlador:

```csharp
public TarefaController(
    ILogger<TarefaController> logger, 
    ITarefaServico tarefaServico,  // Injeção da interface
    IUsuarioServico usuarioServico)  // Injeção da interface
{
    _logger = logger;
    _tarefa = tarefaServico;  // Uso da dependência injetada
    _usuario = usuarioServico;  // Uso da dependência injetada
}
```

### 2. Violações do Princípio da Responsabilidade Única (SRP)

#### Problema:
A classe `TarefaServico` em `TarefasLib/Negocio/TarefaServico.cs` lida tanto com o gerenciamento de tarefas quanto com o rastreamento de tempo:

```csharp
public class TarefaServico : ITarefaServico, ICronometroServico<Tarefa>
{
    // Métodos de gerenciamento de tarefas
    public bool Salvar(Tarefa tarefa) { ... }
    public Tarefa? BuscarPorId(int id) { ... }
    
    // Métodos de cronômetro (deveriam estar em outra classe)
    public TimeSpan PausaCronometro(Tarefa tarefa) { ... }
    public bool IniciaCronometro(Tarefa tarefa) { ... }
}
```

#### Solução:
Dividir em duas classes separadas:

```csharp
// TarefasLib/Negocio/TarefaServico.cs
public class TarefaServico : ITarefaServico
{
    private readonly ITarefaRepositorio _repositorio;
    
    public TarefaServico(ITarefaRepositorio repositorio)
    {
        _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
    }
    
    public bool Salvar(Tarefa tarefa) { ... }
    public Tarefa? BuscarPorId(int id) { ... }
    // ... outros métodos relacionados a tarefas
}

// TarefasLib/Negocio/CronometroServico.cs
public class CronometroServico : ICronometroServico<Tarefa>
{
    private readonly ITarefaRepositorio _repositorio;
    
    public CronometroServico(ITarefaRepositorio repositorio)
    {
        _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
    }
    
    public TimeSpan PausaCronometro(Tarefa tarefa) { ... }
    public bool IniciaCronometro(Tarefa tarefa) { ... }
    // ... outros métodos relacionados a cronômetro
}
```

### 3. Falta de Validação de Entrada

#### Problema:
Métodos sem validação adequada de parâmetros:

```csharp
// TarefaServico.cs - Sem validação de parâmetros
public bool Salvar(Tarefa tarefa) {
    return _repositorio.Salvar(tarefa);  // Pode causar NullReferenceException
}

// TarefaMemoriaRepositorio.cs - Sem verificação de nulo
public bool Salvar(Tarefa tarefa) {
    tarefa.Id = GeraNovoId();  // Pode causar NullReferenceException
    _tarefas.Add(tarefa);
    return true;
}
```

#### Solução:
Adicionar validação de entrada em todos os métodos:

```csharp
// TarefaServico.cs - Com validação de parâmetros
public bool Salvar(Tarefa tarefa) {
    if (tarefa == null)
        throw new ArgumentNullException(nameof(tarefa));
        
    if (string.IsNullOrWhiteSpace(tarefa.Titulo))
        throw new ArgumentException("O título da tarefa é obrigatório", nameof(tarefa));
        
    return _repositorio.Salvar(tarefa);
}

// TarefaMemoriaRepositorio.cs - Com verificação de nulo
public bool Salvar(Tarefa tarefa) {
    if (tarefa == null)
        throw new ArgumentNullException(nameof(tarefa));
        
    tarefa.Id = GeraNovoId();
    _tarefas.Add(tarefa);
    return true;
}
```

### 4. Exposição Direta de Entidades na Camada de API

#### Problema:
DTOs são criados a partir de entidades, mas não abstraem completamente o modelo de domínio:

```csharp
// ApiRest/Controllers/TarefaController.cs
[HttpGet]
public ActionResult<IEnumerable<TarefaDTO>> ObterTodas()
{
    var tarefas = _tarefa.ListarTodas();
    var tarefasDTO = tarefas.Select(t => new TarefaDTO(t));  // Exposição direta da entidade para o DTO
    return Ok(tarefasDTO);
}

// Manipulação direta da entidade no controlador
tarefaExistente.Titulo = tarefaDTO.Titulo;
tarefaExistente.StatusTarefa = tarefaDTO.Status;
```

#### Solução:
Implementar mapeamento adequado entre entidades e DTOs:

```csharp
// Adicionar uma classe de mapeamento
public static class TarefaMapper
{
    public static TarefaDTO ToDTO(this Tarefa tarefa)
    {
        if (tarefa == null) return null;
        
        return new TarefaDTO
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            // ... outras propriedades
        };
    }
    
    public static Tarefa ToEntity(this TarefaDTO dto)
    {
        if (dto == null) return null;
        
        return new Tarefa
        {
            Id = dto.Id,
            Titulo = dto.Titulo,
            // ... outras propriedades
        };
    }
    
    public static void UpdateEntityFromDTO(this Tarefa entity, TarefaDTO dto)
    {
        if (entity == null || dto == null) return;
        
        entity.Titulo = dto.Titulo;
        entity.StatusTarefa = dto.Status;
        // ... outras propriedades
    }
}

// Uso no controlador
[HttpGet]
public ActionResult<IEnumerable<TarefaDTO>> ObterTodas()
{
    var tarefas = _tarefa.ListarTodas();
    var tarefasDTO = tarefas.Select(t => t.ToDTO());
    return Ok(tarefasDTO);
}

// Atualização da entidade
var tarefaExistente = _tarefa.BuscarPorId(id);
if (tarefaExistente == null) return NotFound();

tarefaExistente.UpdateEntityFromDTO(tarefaDTO);
```

## Problemas de Alta Prioridade

### 5. Tratamento Inconsistente de Erros

#### Problema:
O tratamento de erros é inconsistente entre os controladores:

```csharp
// Às vezes retorna apenas o código de status
if (!_tarefa.Salvar(tarefa))
    return StatusCode(500);

// Às vezes inclui uma mensagem de erro
return StatusCode(500, e.Message);

// Às vezes usa NotFound sem mensagem
return NotFound();
```

#### Solução:
Implementar uma estratégia consistente de tratamento de erros com middleware:

1. Criar uma classe de resposta de erro padronizada:

```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    
    public static ApiResponse<T> Ok(T data)
    {
        return new ApiResponse<T> { Success = true, Data = data, StatusCode = 200 };
    }
    
    public static ApiResponse<T> Error(string message, int statusCode = 500)
    {
        return new ApiResponse<T> { Success = false, Message = message, StatusCode = statusCode };
    }
}
```

2. Criar um middleware de tratamento de exceções:

```csharp
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new ApiResponse<object>
        {
            Success = false,
            Message = exception.Message,
            StatusCode = context.Response.StatusCode
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
```

3. Registrar o middleware em Program.cs:

```csharp
app.UseMiddleware<ErrorHandlingMiddleware>();
```

4. Uso consistente nos controladores:

```csharp
[HttpGet("{id}")]
public ActionResult<ApiResponse<TarefaDTO>> ObterPorId(int id)
{
    var tarefa = _tarefa.BuscarPorId(id);
    if (tarefa == null)
        return NotFound(ApiResponse<TarefaDTO>.Error("Tarefa não encontrada", 404));

    return Ok(ApiResponse<TarefaDTO>.Ok(tarefa.ToDTO()));
}
```

### 6. Problemas de Implementação do Repositório

#### Problema:
Repositórios de memória retornam referências diretas para coleções internas:

```csharp
// Permite modificação externa do estado interno
public List<Tarefa> ListarTodas()
{
    return _tarefas;
}

public List<Usuario> Listar()
{
    return ListaUsuarios;
}
```

#### Solução:
Retornar cópias ou coleções somente leitura:

```csharp
// Retorna uma cópia da lista
public List<Tarefa> ListarTodas()
{
    return _tarefas.ToList();
}

// Retorna uma coleção somente leitura
public IReadOnlyCollection<Usuario> Listar()
{
    return ListaUsuarios.AsReadOnly();
}
```

### 7. Uso Inadequado de Tipos Anuláveis

#### Problema:
Uso inconsistente de tipos de referência anuláveis:

```csharp
// Uso correto de tipo anulável
public Tarefa? BuscarPorId(int id) { ... }

// Sem verificação adequada de nulo após retornar tipo anulável
var tarefaExistente = _tarefa.BuscarPorId(id);
tarefaExistente.Titulo = tarefaDTO.Titulo; // Possível NullReferenceException
```

#### Solução:
Usar consistentemente tipos de referência anuláveis com verificações adequadas:

```csharp
// Declaração correta de tipo anulável
public Tarefa? BuscarPorId(int id) { ... }

// Verificação adequada de nulo
var tarefaExistente = _tarefa.BuscarPorId(id);
if (tarefaExistente == null)
{
    return NotFound();
}

// Agora é seguro acessar as propriedades
tarefaExistente.Titulo = tarefaDTO.Titulo;
```

### 8. Falta de Suporte a Transações

#### Problema:
Não há tratamento de transações para operações que modificam várias entidades:

```csharp
// Múltiplas operações sem transação
tarefaExistente.Titulo = tarefaDTO.Titulo;
tarefaExistente.StatusTarefa = tarefaDTO.Status;
tarefaExistente.Responsavel = responsavel;
// ... mais modificações ...

if (!_tarefa.Atualizar(tarefaExistente, tarefaDTO.Status, tarefaDTO.Descricao, tarefaDTO.Prazo))
    return StatusCode(500);
```

#### Solução:
Implementar o padrão Unit of Work:

1. Criar a interface Unit of Work:

```csharp
public interface IUnitOfWork : IDisposable
{
    ITarefaRepositorio Tarefas { get; }
    IUsuarioRepositorio<Usuario> Usuarios { get; }
    IComentarioRepositorio Comentarios { get; }
    
    Task<int> CommitAsync();
    void Rollback();
}
```

2. Implementar a classe Unit of Work:

```csharp
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private bool _disposed = false;
    
    public ITarefaRepositorio Tarefas { get; private set; }
    public IUsuarioRepositorio<Usuario> Usuarios { get; private set; }
    public IComentarioRepositorio Comentarios { get; private set; }
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Tarefas = new TarefaRepositorio(_context);
        Usuarios = new UsuarioRepositorio(_context);
        Comentarios = new ComentarioRepositorio(_context);
    }
    
    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Rollback()
    {
        // Descartar todas as alterações rastreadas
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
    
    // Implementação do IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
```

3. Uso no serviço:

```csharp
public class TarefaServico : ITarefaServico
{
    private readonly IUnitOfWork _unitOfWork;
    
    public TarefaServico(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> AtualizarAsync(Tarefa tarefa, Tarefa.Status novoStatus, string novaDescricao, DateTime novoPrazo)
    {
        if (tarefa == null)
            throw new ArgumentNullException(nameof(tarefa));
            
        try
        {
            tarefa.StatusTarefa = novoStatus;
            tarefa.Descricao = novaDescricao;
            tarefa.Prazo = novoPrazo;
            
            await _unitOfWork.CommitAsync();
            return true;
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            return false;
        }
    }
}
```
