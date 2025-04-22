// TODO: Adicionar using statements necessários para evitar dependências implícitas

namespace TarefasLibrary.Modelo
{
    public class Comentario
    {
        public int Id { get; set; }
        // TODO: Adicionar validação para garantir que Descricao não seja nula ou vazia
        public string Descricao { get; set; }

        // TODO: Considerar tornar DataCriacao somente leitura (readonly) após inicialização
        public DateTime DataCriacao { get; set; }


        // TODO: Validar parâmetros de entrada para evitar valores inválidos (SRP - Single Responsibility Principle)
        public Comentario(string descricao, DateTime dataCriacao)
        {
            // TODO: Implementar validação de parâmetros (descricao não deve ser nula ou vazia)
            Descricao = descricao;
            DataCriacao = dataCriacao;
        }

        // TODO: Este construtor cria um objeto incompleto, considerar remover ou adicionar validação
        public Comentario(int id)
        {
            Id = id;
        }

        // TODO: Construtor vazio pode criar objetos em estado inválido, considerar remover ou adicionar inicialização padrão
        public Comentario() { }

        public static List<Comentario> ListaComentarios = new List<Comentario>();
        private static int contadorIDs = 0;

        // TODO: Remover lógica de persistência da classe de modelo (SRP - Single Responsibility Principle)
        // TODO: Esta responsabilidade deve ser movida para um repositório (Separation of Concerns)
        public void SalvarComentario()
        {
            Id = ++contadorIDs;
            ListaComentarios.Add(this);
        }

        // TODO: Remover lógica de busca da classe de modelo (SRP - Single Responsibility Principle)
        // TODO: Esta responsabilidade deve ser movida para um repositório (Separation of Concerns)
        // TODO: Considerar usar LINQ para simplificar a busca (FirstOrDefault)
        public bool BuscarComentario()
        {
            foreach (var comentario in ListaComentarios)
            {
                if (comentario.Id == Id)
                {
                    Id = comentario.Id;
                    Descricao = comentario.Descricao;
                    DataCriacao = comentario.DataCriacao;
                    return true;
                }

            }
            return false;
        }

    }
}