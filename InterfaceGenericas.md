# Interfaces Genéricas para Repositórios

Este documento apresenta uma implementação de interface genérica para repositórios, permitindo reutilização de código e padronização das operações de acesso a dados.

## Interface Genérica para Repositório

A interface genérica `IRepositorio<T>` define operações básicas de CRUD (Create, Read, Update, Delete) que podem ser aplicadas a qualquer tipo de entidade.

```csharp
using System;
using System.Collections.Generic;

namespace TarefasLibrary.Interface
{
    /// <summary>
    /// Interface genérica para repositórios de dados
    /// </summary>
    /// <typeparam name="T">Tipo da entidade gerenciada pelo repositório</typeparam>
    public interface IRepositorio<T> where T : class
    {
        /// <summary>
        /// Busca uma entidade pelo seu identificador
        /// </summary>
        /// <param name="id">Identificador da entidade</param>
        /// <returns>A entidade encontrada ou null se não existir</returns>
        T? BuscarPorId(int id);
        
        /// <summary>
        /// Lista todas as entidades do repositório
        /// </summary>
        /// <returns>Lista de entidades</returns>
        List<T> ListarTodos();
        
        /// <summary>
        /// Salva uma nova entidade no repositório
        /// </summary>
        /// <param name="entidade">Entidade a ser salva</param>
        /// <returns>True se a operação foi bem-sucedida, False caso contrário</returns>
        bool Salvar(T entidade);
        
        /// <summary>
        /// Atualiza uma entidade existente no repositório
        /// </summary>
        /// <param name="entidade">Entidade com as atualizações</param>
        /// <returns>True se a operação foi bem-sucedida, False caso contrário</returns>
        bool Atualizar(T entidade);
        
        /// <summary>
        /// Remove uma entidade do repositório
        /// </summary>
        /// <param name="id">Identificador da entidade a ser removida</param>
        /// <returns>True se a operação foi bem-sucedida, False caso contrário</returns>
        bool Remover(int id);
    }
}
```

## Implementação para Tarefas

Abaixo está uma implementação da interface genérica para o repositório de tarefas:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class TarefaRepositorio : IRepositorio<Tarefa>
    {
        private List<Tarefa> _tarefas = new List<Tarefa>();
        private int _proximoId = 1;
        
        public Tarefa? BuscarPorId(int id)
        {
            return _tarefas.FirstOrDefault(t => t.Id == id);
        }
        
        public List<Tarefa> ListarTodos()
        {
            return _tarefas;
        }
        
        public bool Salvar(Tarefa entidade)
        {
            if (entidade == null)
                return false;
                
            // Garante que a tarefa tenha um ID único
            entidade.Id = _proximoId++;
            _tarefas.Add(entidade);
            return true;
        }
        
        public bool Atualizar(Tarefa entidade)
        {
            if (entidade == null)
                return false;
                
            var tarefaExistente = BuscarPorId(entidade.Id);
            if (tarefaExistente == null)
                return false;
                
            // Remove a tarefa antiga e adiciona a atualizada
            _tarefas.Remove(tarefaExistente);
            _tarefas.Add(entidade);
            return true;
        }
        
        public bool Remover(int id)
        {
            var tarefa = BuscarPorId(id);
            if (tarefa == null)
                return false;
                
            return _tarefas.Remove(tarefa);
        }
        
        // Métodos específicos para Tarefa que não estão na interface genérica
        public List<Tarefa> ListarPorUsuario(int usuarioId)
        {
            return _tarefas.Where(t => t.Responsavel.Id == usuarioId || t.Criador.Id == usuarioId).ToList();
        }
        
        public bool AtualizarStatus(int id, string novoStatus)
        {
            var tarefa = BuscarPorId(id);
            if (tarefa == null)
                return false;
                
            tarefa.Status = novoStatus;
            return true;
        }
    }
}
```

## Implementação para Usuários

Abaixo está uma implementação da interface genérica para o repositório de usuários:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class UsuarioRepositorio : IRepositorio<Usuario>
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private int _proximoId = 1;
        
        public Usuario? BuscarPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }
        
        public List<Usuario> ListarTodos()
        {
            return _usuarios;
        }
        
        public bool Salvar(Usuario entidade)
        {
            if (entidade == null)
                return false;
                
            // Garante que o usuário tenha um ID único
            entidade.Id = _proximoId++;
            _usuarios.Add(entidade);
            return true;
        }
        
        public bool Atualizar(Usuario entidade)
        {
            if (entidade == null)
                return false;
                
            var usuarioExistente = BuscarPorId(entidade.Id);
            if (usuarioExistente == null)
                return false;
                
            // Remove o usuário antigo e adiciona o atualizado
            _usuarios.Remove(usuarioExistente);
            _usuarios.Add(entidade);
            return true;
        }
        
        public bool Remover(int id)
        {
            var usuario = BuscarPorId(id);
            if (usuario == null)
                return false;
                
            return _usuarios.Remove(usuario);
        }
        
        // Métodos específicos para Usuário que não estão na interface genérica
        public Usuario? BuscarPorNome(string nome)
        {
            return _usuarios.FirstOrDefault(u => u.Nome == nome);
        }
        
        public bool Editar(int id, string nome, string senha, string funcao, string setor)
        {
            var usuario = BuscarPorId(id);
            if (usuario == null)
                return false;
                
            usuario.Nome = nome;
            usuario.Senha = senha;
            usuario.Funcao = funcao;
            usuario.Setor = setor;
            return true;
        }
    }
}
```

## Implementação de Serviço Genérico

Além dos repositórios, também podemos criar um serviço genérico que utilize a interface de repositório:

```csharp
using System;
using System.Collections.Generic;
using TarefasLibrary.Interface;

namespace TarefasLibrary.Negocio
{
    public class ServicoGenerico<T> where T : class
    {
        protected readonly IRepositorio<T> _repositorio;
        
        public ServicoGenerico(IRepositorio<T> repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }
        
        public virtual T? BuscarPorId(int id)
        {
            return _repositorio.BuscarPorId(id);
        }
        
        public virtual List<T> ListarTodos()
        {
            return _repositorio.ListarTodos();
        }
        
        public virtual bool Salvar(T entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade));
                
            return _repositorio.Salvar(entidade);
        }
        
        public virtual bool Atualizar(T entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade));
                
            return _repositorio.Atualizar(entidade);
        }
        
        public virtual bool Remover(int id)
        {
            return _repositorio.Remover(id);
        }
    }
}
```

## Testes Unitários para Repositório Genérico

Abaixo está um exemplo de teste unitário para o repositório genérico de tarefas:

```csharp
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;

namespace TestsTarefas
{
    [TestClass]
    public class TesteRepositorioGenerico
    {
        [TestMethod]
        public void TesteSalvarTarefa()
        {
            // Arrange
            var repositorio = new TarefaRepositorio();
            var criador = new Usuario("João", "senha123", "Gerente", "TI");
            var responsavel = new Usuario("Maria", "senha456", "Desenvolvedora", "TI");
            var tarefa = new Tarefa("Implementar Interface Genérica", "Pendente", criador, responsavel, 
                                    DateTime.Now.AddDays(7), "Criar uma interface genérica para repositórios");
            
            // Act
            var resultado = repositorio.Salvar(tarefa);
            
            // Assert
            Assert.IsTrue(resultado);
            Assert.AreEqual(1, tarefa.Id); // Verifica se o ID foi atribuído corretamente
        }
        
        [TestMethod]
        public void TesteBuscarTarefaPorId()
        {
            // Arrange
            var repositorio = new TarefaRepositorio();
            var criador = new Usuario("João", "senha123", "Gerente", "TI");
            var responsavel = new Usuario("Maria", "senha456", "Desenvolvedora", "TI");
            var tarefa = new Tarefa("Implementar Interface Genérica", "Pendente", criador, responsavel, 
                                    DateTime.Now.AddDays(7), "Criar uma interface genérica para repositórios");
            repositorio.Salvar(tarefa);
            
            // Act
            var tarefaEncontrada = repositorio.BuscarPorId(tarefa.Id);
            
            // Assert
            Assert.IsNotNull(tarefaEncontrada);
            Assert.AreEqual(tarefa.Titulo, tarefaEncontrada.Titulo);
        }
        
        [TestMethod]
        public void TesteAtualizarTarefa()
        {
            // Arrange
            var repositorio = new TarefaRepositorio();
            var criador = new Usuario("João", "senha123", "Gerente", "TI");
            var responsavel = new Usuario("Maria", "senha456", "Desenvolvedora", "TI");
            var tarefa = new Tarefa("Implementar Interface Genérica", "Pendente", criador, responsavel, 
                                    DateTime.Now.AddDays(7), "Criar uma interface genérica para repositórios");
            repositorio.Salvar(tarefa);
            
            // Modificando a tarefa
            tarefa.Status = "Em Andamento";
            tarefa.Descricao = "Descrição atualizada";
            
            // Act
            var resultado = repositorio.Atualizar(tarefa);
            var tarefaAtualizada = repositorio.BuscarPorId(tarefa.Id);
            
            // Assert
            Assert.IsTrue(resultado);
            Assert.IsNotNull(tarefaAtualizada);
            Assert.AreEqual("Em Andamento", tarefaAtualizada.Status);
            Assert.AreEqual("Descrição atualizada", tarefaAtualizada.Descricao);
        }
        
        [TestMethod]
        public void TesteRemoverTarefa()
        {
            // Arrange
            var repositorio = new TarefaRepositorio();
            var criador = new Usuario("João", "senha123", "Gerente", "TI");
            var responsavel = new Usuario("Maria", "senha456", "Desenvolvedora", "TI");
            var tarefa = new Tarefa("Implementar Interface Genérica", "Pendente", criador, responsavel, 
                                    DateTime.Now.AddDays(7), "Criar uma interface genérica para repositórios");
            repositorio.Salvar(tarefa);
            
            // Act
            var resultado = repositorio.Remover(tarefa.Id);
            var tarefaRemovida = repositorio.BuscarPorId(tarefa.Id);
            
            // Assert
            Assert.IsTrue(resultado);
            Assert.IsNull(tarefaRemovida);
        }
        
        [TestMethod]
        public void TesteListarTodos()
        {
            // Arrange
            var repositorio = new TarefaRepositorio();
            var criador = new Usuario("João", "senha123", "Gerente", "TI");
            var responsavel = new Usuario("Maria", "senha456", "Desenvolvedora", "TI");
            
            var tarefa1 = new Tarefa("Tarefa 1", "Pendente", criador, responsavel, 
                                     DateTime.Now.AddDays(7), "Descrição 1");
            var tarefa2 = new Tarefa("Tarefa 2", "Em Andamento", criador, responsavel, 
                                     DateTime.Now.AddDays(14), "Descrição 2");
            
            repositorio.Salvar(tarefa1);
            repositorio.Salvar(tarefa2);
            
            // Act
            var tarefas = repositorio.ListarTodos();
            
            // Assert
            Assert.AreEqual(2, tarefas.Count);
            Assert.IsTrue(tarefas.Any(t => t.Titulo == "Tarefa 1"));
            Assert.IsTrue(tarefas.Any(t => t.Titulo == "Tarefa 2"));
        }
        
        [TestMethod]
        public void TesteListarPorUsuario()
        {
            // Arrange
            var repositorio = new TarefaRepositorio();
            var criador = new Usuario(1, "João", "senha123", "Gerente", "TI");
            var responsavel1 = new Usuario(2, "Maria", "senha456", "Desenvolvedora", "TI");
            var responsavel2 = new Usuario(3, "Pedro", "senha789", "Analista", "TI");
            
            var tarefa1 = new Tarefa("Tarefa 1", "Pendente", criador, responsavel1, 
                                     DateTime.Now.AddDays(7), "Descrição 1");
            var tarefa2 = new Tarefa("Tarefa 2", "Em Andamento", criador, responsavel2, 
                                     DateTime.Now.AddDays(14), "Descrição 2");
            
            repositorio.Salvar(tarefa1);
            repositorio.Salvar(tarefa2);
            
            // Act
            var tarefasResponsavel1 = repositorio.ListarPorUsuario(responsavel1.Id);
            
            // Assert
            Assert.AreEqual(1, tarefasResponsavel1.Count);
            Assert.AreEqual("Tarefa 1", tarefasResponsavel1[0].Titulo);
        }
    }
}
```

## Benefícios da Interface Genérica

1. **Reutilização de código**: A mesma interface pode ser usada para diferentes tipos de entidades.
2. **Padronização**: Todas as implementações de repositório seguem o mesmo padrão.
3. **Flexibilidade**: É possível adicionar métodos específicos nas implementações concretas.
4. **Testabilidade**: Facilita a criação de testes unitários e mocks.
5. **Manutenibilidade**: Reduz a duplicação de código e facilita manutenções futuras.

## Explicação Teórica sobre Tipos Genéricos em C#

### O que são Tipos Genéricos?

Os tipos genéricos foram introduzidos no C# 2.0 e são uma poderosa funcionalidade que permite criar classes, interfaces, métodos e delegados que adiam a especificação de um ou mais tipos até que a classe ou método seja declarado e instanciado pelo código cliente. Usando um parâmetro de tipo genérico T, você pode escrever uma única classe que pode ser usada com diferentes tipos, preservando a segurança de tipos.

### Benefícios dos Tipos Genéricos

1. **Segurança de Tipo**: Erros de tipo são detectados em tempo de compilação, não em tempo de execução.
2. **Reutilização de Código**: Elimina a necessidade de criar múltiplas versões da mesma lógica para diferentes tipos.
3. **Desempenho**: Evita conversões de tipo (boxing/unboxing) que ocorrem com coleções não genéricas.
4. **Legibilidade**: Torna o código mais claro e expressivo.

### Sintaxe Básica

```csharp
// Declaração de uma classe genérica
public class MinhaClasse<T>
{
    private T _valor;
    
    public MinhaClasse(T valor)
    {
        _valor = valor;
    }
    
    public T ObterValor()
    {
        return _valor;
    }
}

// Uso da classe genérica
var instanciaString = new MinhaClasse<string>("Olá");
var instanciaInt = new MinhaClasse<int>(42);
```

### Restrições de Tipo (Constraints)

Você pode aplicar restrições ao parâmetro de tipo T para limitar os tipos que podem ser usados:

```csharp
// T deve ser uma classe (referência)
public class MinhaClasse<T> where T : class
{
    // Implementação
}

// T deve ser uma struct (valor)
public class MinhaClasse<T> where T : struct
{
    // Implementação
}

// T deve ter um construtor sem parâmetros
public class MinhaClasse<T> where T : new()
{
    // Implementação
}

// T deve implementar uma interface
public class MinhaClasse<T> where T : IMinhaInterface
{
    // Implementação
}

// T deve herdar de uma classe base
public class MinhaClasse<T> where T : ClasseBase
{
    // Implementação
}

// Múltiplas restrições
public class MinhaClasse<T> where T : ClasseBase, IMinhaInterface, new()
{
    // Implementação
}
```

### Métodos Genéricos

Além de classes e interfaces, você pode criar métodos genéricos:

```csharp
public T EncontrarMaior<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

// Uso
int maiorInt = EncontrarMaior<int>(5, 10);
string maiorString = EncontrarMaior<string>("abc", "xyz");
```

### Tipos Genéricos Múltiplos

Você pode usar mais de um parâmetro de tipo:

```csharp
public class Dicionario<TChave, TValor>
{
    // Implementação
}

// Uso
var dicionario = new Dicionario<int, string>();
```

### Covariância e Contravariância

A partir do C# 4.0, os tipos genéricos suportam covariância e contravariância:

- **Covariância (out)**: Permite usar um tipo mais derivado do que o especificado.
- **Contravariância (in)**: Permite usar um tipo menos derivado do que o especificado.

```csharp
// Covariância
IEnumerable<string> strings = new List<string>();
IEnumerable<object> objetos = strings; // Válido com covariância

// Contravariância
Action<object> actionObject = obj => Console.WriteLine(obj);
Action<string> actionString = actionObject; // Válido com contravariância
```

### Aplicação em Repositórios Genéricos

No contexto de repositórios, os tipos genéricos são especialmente úteis porque:

1. **Abstração de Entidades**: Permitem criar uma única interface de repositório que funciona com qualquer entidade.
2. **Consistência de API**: Garantem que todos os repositórios tenham os mesmos métodos básicos.
3. **Extensibilidade**: Facilitam a adição de novos tipos de entidades sem duplicar código.

Por exemplo, nossa interface `IRepositorio<T>` pode ser usada com qualquer classe de modelo:

```csharp
// Repositório para Tarefas
IRepositorio<Tarefa> tarefaRepo = new TarefaRepositorio();

// Repositório para Usuários
IRepositorio<Usuario> usuarioRepo = new UsuarioRepositorio();

// Repositório para Comentários
IRepositorio<Comentario> comentarioRepo = new ComentarioRepositorio();
```

Todos esses repositórios compartilham a mesma interface e comportamento básico, mas podem ser estendidos com métodos específicos para cada tipo de entidade.

## Como Usar

Para usar a interface genérica em um novo tipo de entidade, basta criar uma nova classe que implemente `IRepositorio<T>` para o tipo desejado:

```csharp
public class ComentarioRepositorio : IRepositorio<Comentario>
{
    // Implementação dos métodos da interface
}
```

E para usar o repositório em um serviço:

```csharp
public class TarefaServico
{
    private readonly IRepositorio<Tarefa> _repositorio;
    
    public TarefaServico(IRepositorio<Tarefa> repositorio)
    {
        _repositorio = repositorio;
    }
    
    // Métodos do serviço que utilizam o repositório
}
```

Ou usar diretamente o serviço genérico:

```csharp
var repositorio = new TarefaRepositorio();
var servico = new ServicoGenerico<Tarefa>(repositorio);

// Usando o serviço
var tarefa = servico.BuscarPorId(1);
