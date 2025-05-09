using ApiRest.DTOs;

namespace BlazorTarefas.Servicos
{
    public class EmpresaServico
    {
        private List<EmpresaDTO> _empresa = new List<EmpresaDTO>();
        private readonly SetorServico _setor;
        private int _nextId = 1;

        public async Task<Boolean> Adicionar(CriarEmpresaDTO empresa)
        {
            _empresa.Add(new EmpresaDTO
            {
                Id = _nextId++,
                Nome = empresa.Nome,
                Cnpj = empresa.Cnpj,
                
            });
            return true;
        }

        public Boolean Atualizar(EmpresaDTO empresa)
        {
            var empresaExistente = _empresa.FirstOrDefault(t => t.Id == empresa.Id);
            if (empresaExistente != null)
            {
               empresaExistente.Nome = empresa.Nome;
                empresaExistente.Cnpj = empresa.Cnpj;
                return (true);
            }
            return (false);
        }

        public Task<List<EmpresaDTO>> BuscaTodos()
        {
            return Task.FromResult(_empresa);
        }

        public async Task<EmpresaDTO> BuscaPorId(int id) =>
            _empresa.FirstOrDefault(t => t.Id == id);

        public Task Remover(int id)
        {
            var empresa = _empresa.FirstOrDefault(t => t.Id == id);
            if (empresa != null) _empresa.Remove(empresa);
            return Task.CompletedTask;
        }
    }
}

