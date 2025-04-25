using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace TarefasLibrary.Repositorio
{

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }
    public class EmpresaRepositorio : IRepositorio<Empresa>
    {
        private List<Empresa> _empresas = new();
        private int contadorIDs = 0;

        public Empresa? BuscarPorId(int id)
        {
            return _empresas.FirstOrDefault(e => e.Id == id);
        }

        public bool Cadastrar(Empresa obj)
        {
            obj.Id = ++contadorIDs;
            _empresas.Add(obj);
            return true;
        }

        public bool Editar(Empresa obj)
        {
            var e = BuscarPorId(obj.Id);
            if (e is null || e.Id != obj.Id)
                return false;

            e.Cnpj = obj.Cnpj;
            e.Nome = obj.Nome;
            return true;
        }

        public List<Empresa> Listar()
        {
            return _empresas;
        }

        public bool Remover(Empresa obj)
        {
            return _empresas.Remove(obj);
        }
    }
}
