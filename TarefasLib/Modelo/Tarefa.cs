using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasLibrary.Modelo
{
    public class Tarefa
    {
        public Tarefa(string titulo, Status status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, Prioridade prioridade, DateTime? dataCriacao = null)
        {

            Titulo = titulo;
            StatusTarefa = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateAndTime.Now;
            Descricao = descricao;
            PrioridadeTarefa = prioridade;
        }

        public Tarefa(int id, string titulo, Status status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, Prioridade prioridade, DateTime? dataCriacao = null)
        {
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
        public Tarefa() { }

        public int Id { get; set; }
        public string Titulo { get; set; }
       
        public Usuario Criador { get; set; }
        public Usuario Responsavel { get; set; }
        public List<Usuario> Membros { get; set; } = new List<Usuario>();
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
        public TimeSpan TempoTotal { get; set; } = TimeSpan.Zero;
        public List<Cronometro> Tempos { get; set; } = new();

        public List<Comentario> listaComentarios = new List<Comentario>();

        public Prioridade PrioridadeTarefa { get; set; }
        public Status StatusTarefa { get; set; }

        public enum Status
        {
            ToDo,
            Doing,
            Done
        }

        public enum Prioridade
        {
            Baixa,
            Alta,
            Urgente
        }

        public void Adicionar(Comentario comentario)
        {
            listaComentarios.Add(comentario);
        }

        public List<Comentario> ListarComentarios()
        {
            return listaComentarios;
        }

    }
}
