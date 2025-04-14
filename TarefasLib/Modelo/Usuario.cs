using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarefasLibrary.Modelo
{
    public class Usuario
    {
        public int Id { get; set; } = -1;

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        public string Senha { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }


        [JsonConstructor]
        public Usuario(string nome, string senha, string funcao, string setor)
        {
            Nome = nome;
            Senha = senha;
            Funcao = funcao;
            Setor = setor;
        }

        public Usuario(int id, string nome, string senha, string funcao, string setor)
        {
            Id = id;
            Nome = nome;
            Senha = senha;
            Funcao = funcao;
            Setor = setor;
        }
    }
}
