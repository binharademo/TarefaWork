using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class SetorDTO
    {
        public int Id;
        public string Nome;
        public bool Status;
        public int EmpresaId;
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
        public int Id;
        public string Nome;
        public bool Status;
    }

    public class AlterarSetorDTO
    {
        public int Id;
        public string Nome;
        public bool Status;
    }
}
