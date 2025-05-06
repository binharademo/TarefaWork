using ApiRest.DTOs;

namespace BlazorTarefas.Servicos
{
    public class SetorServico
    {
        private List<SetorDTO> _setor = new List<SetorDTO>();
        private int _nextId = 1;

        public async Task<Boolean> Adicionar(CriarSetorDTO setor)
        {
            _setor.Add(new SetorDTO
            {
                Id = _nextId++,
                Nome = setor.Nome,
                Status = setor.Status,
                EmpresaId = 1
            });
            return true;
        }

        public Boolean Atualizar(SetorDTO setor)
        {
            var setorExistente = _setor.FirstOrDefault(t => t.Id == setor.Id);
            if (setorExistente != null)
            {
                setorExistente.Nome = setor.Nome;
                setorExistente.Status = setor.Status;
                return (true);
            }
            return (false);
        }

        public Task<List<SetorDTO>> BuscaTodos()
        {
            return Task.FromResult(_setor);
        }

        public async Task<SetorDTO> BuscaPorId(int id) =>
            _setor.FirstOrDefault(t => t.Id == id);

        public Task Remover(int id)
        {
            var setor = _setor.FirstOrDefault(t => t.Id == id);
            if (setor != null) _setor.Remove(setor);
            return Task.CompletedTask;
        }
    }
}

