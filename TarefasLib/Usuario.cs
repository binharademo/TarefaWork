using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarefas_Library
{
    public class Usuario
    {
        public int Id { get; set; } = -1;
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }

        public Usuario(string nome, string senha, string funcao, string setor)
        {
            this.Nome = nome;
            this.Senha = senha;
            this.Funcao = funcao;
            this.Setor = setor;
        }

        public Usuario(int id, string nome, string senha, string funcao, string setor)
        {
            this.Id = id;
            this.Nome = nome;
            this.Senha = senha;
            this.Funcao = funcao;
            this.Setor = setor;
        }
    }
}
