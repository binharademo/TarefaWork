using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;

namespace TarefasLibrary.Negocio
{
    public class ComentarioServices
    {
        // TODO: Utilizar a interface ao invés da implementação concreta (DIP - Dependency Inversion Principle)
        private readonly IComentarioRepositorio _repository;
        public ComentarioServices(IComentarioRepositorio repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // TODO: Validar o parâmetro comentario antes de salvar (null check)
        public bool SalvarComentario(Comentario comentario)
        {
            // TODO: Adicionar validação de negócio antes de salvar
            return _repository.Cadastrar(comentario);
        }

        // TODO: Validar o id (deve ser maior que zero)
        public Comentario BuscarComentario(int id)
        {
            // TODO: Tratar caso de comentário não encontrado (null)
            return _repository.BuscarPorId(id);
        }

        // TODO: Retornar IReadOnlyCollection<Comentario> para evitar modificações externas da coleção
        public List<Comentario> ListarComentarios()
        {
            // TODO: Considerar implementar paginação para grandes volumes de dados
            return _repository.Listar();
        }

        // TODO: Retornar IReadOnlyCollection<Comentario> para evitar modificações externas da coleção
        public List<Comentario> ListarComentarios(int id)
        {
            // TODO: Considerar implementar paginação para grandes volumes de dados
            return _repository.BuscarPorTarefa(id);
        }

        public bool RemoverComentario(Comentario comentario)
        {
            // TODO: Adicionar validação de negócio antes de salvar
            return _repository.Remover(comentario);
        }
        public bool AlterarComentario(Comentario comentario)
        {
            // TODO: Adicionar validação de negócio antes de salvar
            return _repository.Editar(comentario);
        }

    }
}
