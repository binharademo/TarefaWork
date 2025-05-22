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
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        public string Senha { get; set; }
        public Funcao FuncaoUsuario { get; set; }
        public Setor SetorUsuario { get; set; }
        public int SetorUsuarioId { get; set; } 
        public List<Tarefa> TarefasDono { get; set; } = new List<Tarefa>();
        public List<Tarefa> TarefasResponsavel { get; set; } = new List<Tarefa>();
        public List<Tarefa> TarefasMembro { get; set; } = new List<Tarefa>();

        public List<Comentario> listaComentarios = new List<Comentario>();

        [JsonConstructor]
        public Usuario(string nome, string senha, Funcao funcao, Setor setor)
        {          
            Nome = nome;
            Senha = senha;
            FuncaoUsuario = funcao;
            SetorUsuario = setor;
            SetorUsuarioId = setor?.Id ?? 0;

        }

        public Usuario(string nome, string senha, Funcao funcao, int setorusuarioid)
        {
            Nome = nome;
            Senha = senha;
            FuncaoUsuario = funcao;
            SetorUsuarioId = setorusuarioid;

        }


        public Usuario() { }

        public Usuario(int id, string nome, string senha, Funcao funcao, Setor setor)
        {
            Id = id;
            Nome = nome;
            Senha = senha;
            FuncaoUsuario = funcao;
            SetorUsuario = setor;
            SetorUsuarioId = setor?.Id ?? 0;
        }

        public enum Funcao
        {
            Dev,
            Analista,
            Marketing
        }

  
    }
}
