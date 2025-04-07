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
        public Tarefa(string titulo, string status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, DateTime? dataCriacao = null)
        {
            Titulo = titulo;
            Status = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateAndTime.Now;
            Descricao = descricao;
        }

        public Tarefa(int id, string titulo, string status, Usuario criador, Usuario responsavel, DateTime prazo, string descricao, DateTime? dataCriacao = null)
        {
            Id = id;
            Titulo = titulo;
            Status = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao ?? DateAndTime.Now;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Status { get; set; }
        public Usuario Criador { get; set; }
        public Usuario Responsavel { get; set; } 
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
       
        public List<Comentario> listaComentarios = new List<Comentario>();

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
