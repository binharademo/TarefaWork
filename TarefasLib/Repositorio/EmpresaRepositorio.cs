using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;

namespace TarefasLibrary.Repositorio
{
    public class Empresa {

        public int id { get; set; }
        public string Cnpj { get; set; }
    }
    public class EmpresaRepositorio : IRepositorio<Empresa>
    {
        public Empresa? BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Cadastrar(Empresa obj)
        {
            throw new NotImplementedException();
        }

        public bool Editar(Empresa obj)
        {
            throw new NotImplementedException();
        }

        public List<Empresa> Listar()
        {
            throw new NotImplementedException();
        }

        public bool Remover(Empresa obj)
        {
            throw new NotImplementedException();
        }
    }
}
