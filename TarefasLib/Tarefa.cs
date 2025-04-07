using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarefas_Library
{
    public class Tarefa
    {
        public Tarefa(int id, string titulo, string status, string criador, string responsavel, DateTime prazo, string descricao, DateTime? dataCriacao = null)
        {
            Id = id;
            Titulo = titulo;
            Status = status;
            Criador = criador;
            Responsavel = responsavel;
            Prazo = prazo;
            DataCriacao = dataCriacao?? DateAndTime.Now;
            Descricao = descricao;
        }

        public Tarefa(int Id)
        {
            this.Id = Id;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Status { get; set; }
        public string Criador { get; set; }
        public string Responsavel { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }

        private static List<Tarefa> _tarefas = new List<Tarefa>();
        private int v;

        public void Salvar()
        {
            _tarefas.Add(this);
            return ;
        }

        public List<Tarefa> ListarTodas()
        {
            return _tarefas;
        }

        public bool Buscar()
        {
            foreach (var tarefa in _tarefas)
            {
                if (tarefa.Id == this.Id)
                {
                    Id = tarefa.Id;
                    Titulo = tarefa.Titulo;
                    Status = tarefa.Status;
                    Criador = tarefa.Criador;
                    Responsavel = tarefa.Responsavel;
                    Prazo = tarefa.Prazo;
                    Descricao = tarefa.Descricao;
                    DataCriacao = tarefa.DataCriacao;
                    return true;
                }
                else
                {
                    Id = 0;
                    Titulo = "";
                    Status = "";
                    Criador = "";
                    Responsavel = "";
                    Prazo = DateTime.Now;
                    Descricao = "";
                    DataCriacao = DateTime.Now;
                    return false;
                }
            }
            return false;
        }
    }
}
