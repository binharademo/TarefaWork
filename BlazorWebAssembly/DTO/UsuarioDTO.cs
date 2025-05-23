namespace BlazorWebAssembly.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public int? FuncaoUsuario { get; set; }
        public int? SetorUsuarioId { get; set; }
        public string SetorNome { get; set; }
    }
}
