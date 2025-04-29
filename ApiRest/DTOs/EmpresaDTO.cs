using TarefasLibrary.Modelo;

namespace ApiRest.DTOs
{
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        public EmpresaDTO(Empresa empresa)
        {
            Id = empresa.Id;
            Nome = empresa.Nome;
            Cnpj = empresa.Cnpj;
        }
    }

    public class CriarEmpresaDTO
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }

    public class AtualizarEmpresaDTO
    {
        public string Nome { get; set; }
    }
}
