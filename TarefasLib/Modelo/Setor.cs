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
        public int Id { get; set; }

        //public Empresa Empresa { get; set; }
        public int EmpresaId { get; set; }

        public Setor(string nome, Empresa empresa)
        {
            Nome = nome;
            Status = true;
            EmpresaId = empresa.Id;
        }
        public Setor(string nome, int empresa)
        {
            Nome = nome;
            Status = true;
            EmpresaId = empresa;
        }
        public Setor(int id, string nome, bool status)
        {
            Id = id;
            Nome = nome;
            Status = true;
        }
    }
}

