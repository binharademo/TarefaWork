using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{
    // TODO: Implementar interface IRepository
    public class ComentarioRepository : IComentarioRepositorio
    {
        private readonly string _connectionString;

        public ComentarioRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Comentario> ListaComentario = new List<Comentario>();
        private int contadorIDs = 1;

        public Comentario? BuscarPorId(int id)
        {
            // TODO: Considerar lançar exceção ou retornar um resultado mais descritivo quando não encontrar o comentário
            return this.ListaComentario.FirstOrDefault(c => c.Id == id);
        }

        public bool Cadastrar(Comentario comentario)
        {
            // TODO: Implementar transações ou mecanismo de persistência mais robusto
            comentario.Id = ++this.contadorIDs;
            this.ListaComentario.Add(comentario);
            return true;
        }

        public List<Comentario> Listar()
        {
            // TODO: Considerar implementar paginação para grandes volumes de dados
            // TODO: Retornar uma cópia da lista para evitar modificações externas
            return this.ListaComentario;
        }

        public bool Editar(Comentario obj)
        {
            var e = BuscarPorId(obj.Id);
            if (e is null || e.Id != obj.Id)
                return false;

            e.Descricao = obj.Descricao;
            return true;
        }

        public bool Remover(Comentario obj)
        {
            return ListaComentario.Remove(obj);
        }

        public List<Comentario> BuscarPorTarefa(int id)
        {
            return this.ListaComentario.Where(c => c.TarefaId == id).ToList();
        }
    }
}
