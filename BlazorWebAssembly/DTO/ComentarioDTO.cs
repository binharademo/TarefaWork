namespace BlazorWebAssembly.DTO
{
    public class ComentarioDTO
    {
        public int Id { get; set; }
        public string descricao { get; set; }
        public int UsuarioId { get; set; }
        public DateTime dataCriacao {  get; set; }
    }
}
