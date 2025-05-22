using MudBlazor;

namespace BlazorWebAssembly.Modelos
{
    public class TarefaStatus(int id, string nome, string icone, Color cor)
    {
        public int Id { get; set; } = id;
        public string Nome { get; set; } = nome;
        public Color Cor { get; set; } = cor;
        public string Icone { get; set; } = icone;
        
        public static List<TarefaStatus> Carregar() => [
            new(0, "Na Fila", Icons.Material.Filled.List, Color.Default),
            new(1, "Em Execução", Icons.Material.Filled.DirectionsRun, Color.Info),
            new(2, "Finalizados", Icons.Material.Filled.CheckCircle, Color.Success)
        ];
    }

}
