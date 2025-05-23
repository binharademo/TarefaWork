using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class SetorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public int EmpresaId { get; set; }
        public SetorDTO(Setor setor)
        {
            Id = setor.Id;
            Nome = setor.Nome;
            Status = setor.Status;
            EmpresaId = setor.EmpresaId;
        }

        public SetorDTO() { }
    }

    public class CriarSetorDTO
    {
        public string Nome { get; set; }
        public bool Status { get; set; }
        public int EmpresaId { get; set; }


    }

    public class AlterarSetorDTO
    {
        public string Nome { get; set; }

        public bool Status { get; set; }
    }
}
