using ApiRest.DTOs;
using TarefasLibrary.Modelo;

namespace BlazorTarefas.ModelosTeste
{
    public class TarefaTeste
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Concluida { get; set; } = false;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public string Prioridade { get; set; } = "Baixa";
        public string Status { get; set; } = "TODO";
        public UsuarioDTO Criador { get; set; }
        public UsuarioDTO Responsavel { get; set; }

    }
}
