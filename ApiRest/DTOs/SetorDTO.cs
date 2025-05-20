using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class SetorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public EmpresaDTO Empresa { get; set; }
        public SetorDTO(Setor setor)
        {
            Id = setor.Id;
            Nome = setor.Nome;
            Status = setor.Status;
            Empresa = new EmpresaDTO
            {
                Id = setor.Empresa.Id,
                Nome = setor.Empresa.Nome,
                Cnpj = setor.Empresa.Cnpj
            };
        }

        public SetorDTO() { }

    }

    public class CriarSetorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public int EmpresaId { get; set; }


    }

    public class AlterarSetorDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Status { get; set; }

        public int EmpresaId { get; set; }

    }
}
