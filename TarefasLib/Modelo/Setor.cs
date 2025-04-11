using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasLibrary.Modelo
{
    public class Setor
    {
        public string Nome { get; set; }
        public bool Status { get; set; }
        public int Id { get; set; } = -1;

        public Setor(string nome)
        {
            Nome = nome;
            Status = true;
        }
        public Setor(int id, string nome, bool status)
        {
            Id = id;
            Nome = nome;
            Status = true;
        }
    }
}

