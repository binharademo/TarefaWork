using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarefasLibrary.Modelo
{
    public class Tarefa
    {
        // TODO: Validar parâmetros de entrada para evitar valores inválidos (SRP - Single Responsibility Principle)
        public Tarefa(string titulo, Tarefa.Status status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, Tarefa.Prioridade prioridade, DateTime? dataCriacao = null)
        {
            // TODO: Considerar usar Guard Clauses para validar os parâmetros
            Titulo = titulo;
            StatusTarefa = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateAndTime.Now;
            Descricao = descricao;
            PrioridadeTarefa = prioridade;
        }

        // TODO: Considerar usar o padrão Factory para criar instâncias de Tarefa (criação centralizada)
        public Tarefa(int id, string titulo, Tarefa.Status status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, Tarefa.Prioridade prioridade, DateTime? dataCriacao = null)
        {
            // TODO: Reutilizar código do construtor anterior para evitar duplicação (DRY - Don't Repeat Yourself)
            Id = id;
            Titulo = titulo;
            StatusTarefa = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateAndTime.Now;
            Descricao = descricao;
            PrioridadeTarefa = prioridade;
        }

        public Tarefa(string titulo, Tarefa.Status status, int criador, int responsavel, DateTime prazo, string descricao, Tarefa.Prioridade prioridade, DateTime? dataCriacao = null)
        {
            // TODO: Reutilizar código do construtor anterior para evitar duplicação (DRY - Don't Repeat Yourself)

            Titulo = titulo;
            StatusTarefa = status;
            CriadorId = criador;
            ResponsavelId = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateAndTime.Now;
            Descricao = descricao;
            PrioridadeTarefa = prioridade;
        }
        public Tarefa() { }

        // TODO: Considerar tornar as propriedades imutáveis (init-only) para garantir integridade dos dados
        public int Id { get; set; }
        // TODO: Adicionar validação para garantir que Titulo não seja nulo ou vazio
        public string Titulo { get; set; }
       
        public Usuario Criador { get; set; }
        public Usuario Responsavel { get; set; }
        public List<Usuario> Membros { get; set; } = new List<Usuario>();
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
        public TimeSpan TempoTotal { get; set; } = TimeSpan.Zero;
        // TODO: Encapsular a lista de Tempos para controlar acesso e modificações (Encapsulation)
        public List<Cronometro> Tempos { get; set; } = new();

        // TODO: Transformar em propriedade privada com getter público para encapsulamento adequado
        // TODO: Seguir convenção de nomenclatura (_listaComentarios ou usar PascalCase para propriedades públicas)
        public List<Comentario> listaComentarios = new List<Comentario>();

        public Prioridade PrioridadeTarefa { get; set; }
        public Status StatusTarefa { get; set; }

        public int CriadorId { get; set; }
        public int ResponsavelId { get; set; }

        public enum Status
        {
            ToDo,
            Doing,
            Done
        }

        public enum Prioridade
        {
            Baixa,
            Normal,
            Alta,
            Urgente
        }

        // TODO: Validar se o comentário não é nulo antes de adicionar
        public void Adicionar(Comentario comentario)
        {
            // TODO: Implementar validação de parâmetro
            listaComentarios.Add(comentario);
        }

        // TODO: Retornar IReadOnlyCollection<Comentario> para evitar modificações externas da coleção
        public List<Comentario> ListarComentarios()
        {
            // TODO: Considerar retornar uma cópia da lista para evitar modificações externas
            return listaComentarios;
        }

    }
}
