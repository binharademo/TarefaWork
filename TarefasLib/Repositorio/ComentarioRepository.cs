using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    public class ComentarioRepository
    {

        // TODO: Validar o parâmetro comentario antes de salvar (null check)
        public void SalvarComentario(Comentario comentario)
        {
            // TODO: Implementar transações ou mecanismo de persistência mais robusto
            comentario.Id = ++ListaComentarios.contadorIDs;
            ListaComentarios.ListaComentario.Add(comentario);
        }

        // TODO: Validar o id (deve ser maior que zero)
        public Comentario BuscarComentario(int id)
        {
            // TODO: Considerar lançar exceção ou retornar um resultado mais descritivo quando não encontrar o comentário
            return ListaComentarios.ListaComentario.FirstOrDefault(c => c.Id == id);
        }

        // TODO: Retornar IReadOnlyCollection<Comentario> para evitar modificações externas da coleção
        public List<Comentario> ListarComentarios()
        {
            // TODO: Considerar implementar paginação para grandes volumes de dados
            // TODO: Retornar uma cópia da lista para evitar modificações externas
            return ListaComentarios.ListaComentario;
        }

    }
}
