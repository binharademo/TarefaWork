using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;

namespace TarefasLibrary.Negocio
{
    public class ComentarioServices
    {
        // TODO: Utilizar a interface ao invés da implementação concreta (DIP - Dependency Inversion Principle)
        private readonly ComentarioRepository? _repository;
        public ComentarioServices(ComentarioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // TODO: Validar o parâmetro comentario antes de salvar (null check)
        public void SalvarComentario(Comentario comentario)
        {
            // TODO: Adicionar validação de negócio antes de salvar
            _repository.SalvarComentario(comentario);
        }

        // TODO: Validar o id (deve ser maior que zero)
        public Comentario BuscarComentario(int id)
        {
            // TODO: Tratar caso de comentário não encontrado (null)
            return _repository.BuscarComentario(id);
        }

        // TODO: Retornar IReadOnlyCollection<Comentario> para evitar modificações externas da coleção
        public List<Comentario> ListarComentarios()
        {
            // TODO: Considerar implementar paginação para grandes volumes de dados
            return _repository.ListarComentarios();
        }

    }
}
