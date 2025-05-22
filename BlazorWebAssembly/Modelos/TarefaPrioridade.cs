using MudBlazor;

namespace BlazorWebAssembly.Modelos
{
    public class TarefaPrioridade(int id, string nome, string icone, Color cor)
    {
        public int Id { get; set; } = id;
        public string Nome { get; set; } = nome;
        public Color Cor { get; set; } = cor;
        public string Icone { get; set; } = icone;
        
        public static List<TarefaPrioridade> Carregar() => [
            new(0, "Baixa", Icons.Material.Filled.KeyboardArrowDown, Color.Success),
            new(1, "Média", Icons.Material.Filled.KeyboardArrowRight, Color.Info),
            new(2, "Alta", Icons.Material.Filled.KeyboardArrowUp, Color.Warning),
            new(3, "Urgente", Icons.Material.Filled.KeyboardDoubleArrowUp, Color.Error)
        ];
    }

}
