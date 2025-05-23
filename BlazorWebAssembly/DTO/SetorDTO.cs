namespace BlazorWebAssembly.DTO
{
    public class SetorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public int? EmpresaId {  get; set; }
    }
}
