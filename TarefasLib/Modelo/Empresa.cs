namespace TarefasLibrary.Modelo
{
    public class Empresa {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public List<Setor> Setores { get; set; } = new List<Setor>();

        public Empresa(string nome, string cnpj)
        {
            Cnpj = cnpj;
            Nome = nome;
        }
        public Empresa(int id, string nome, string cnpj)
        {
            Id = id;
            Cnpj = cnpj;
            Nome = nome;
        }
    }
}
