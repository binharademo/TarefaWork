using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary;
using TarefasLibrary.Repositorio;

namespace TarefasLibrary.Negocio
{
    public class ComentarioServices
    {
        private readonly ComentarioRepository? _repository;
        public ComentarioServices(ComentarioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void SalvarComentario(Comentario comentario)
        {
            _repository.SalvarComentario(comentario);
        }

        public Comentario BuscarComentario(int id)
        {
            return _repository.BuscarComentario(id);
        }

        public List<Comentario> ListarComentarios()
        {
            return _repository.ListarComentarios();
        }

    }
}
