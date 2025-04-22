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
        public Tarefa() { }


        //[JsonConstructor]
        //public Tarefa(string titulo, string status, string criador, string responsavel, string prazo, string descricao, string prioridade, string dataCriacao)
        //{

        //    Titulo = titulo;
        //    //Status = new StatusTarefa();
        //    //Criador = criador;
        //    //Responsavel = responsavel;
        //    //Prazo = prazo;
        //    //Descricao = descricao;
        //    //PrioridadeTarefa = prioridade;
        //    //DataCriacao = dataCriacao ?? DateAndTime.Now;


        //}


        [JsonConstructor]
        public Tarefa(string titulo, StatusTarefa status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, Prioridade PrioridadeTarefa, DateTime? dataCriacao = null)
        {


            Titulo = titulo;
            Status = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            Descricao = descricao;
            PrioridadeTarefa = PrioridadeTarefa;
            DataCriacao = dataCriacao ?? DateAndTime.Now;


        }

        public Tarefa(int id, string titulo, StatusTarefa status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, Prioridade PrioridadeTarefa, DateTime? dataCriacao = null)
        {
            Id = id;
            Titulo = titulo;
            Status = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateAndTime.Now;
            Descricao = descricao;
            PrioridadeTarefa = PrioridadeTarefa;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public StatusTarefa Status { get; set; }
        public Usuario Criador { get; set; }
        public Usuario Responsavel { get; set; }
        public List<Usuario> Membros { get; set; } = new List<Usuario>();
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
        public TimeSpan TempoTotal { get; set; } = TimeSpan.Zero;
        public List<Cronometro> Tempos { get; set; } = new();

        public List<Comentario> listaComentarios { get; set; } = new List<Comentario>();

        public Prioridade PrioridadeTarefa { get; set; }

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
