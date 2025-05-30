﻿using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface ITarefaRepositorio
    {
        Tarefa? BuscarPorID(int id);
        List<Tarefa> ListarTodas();
        bool Salvar(Tarefa tarefa);
        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus, string novadescricao, DateTime novoprazo, string novotitulo, Tarefa.Prioridade novaprioridade, int responsavelId);
        public bool Atualizar(Tarefa tarefa, Tarefa.Status novostatus);
        public List<Tarefa> ListarPorUsuario(int id);
        public bool MarcarMembro(Tarefa tarefa, Usuario membro);
        List<Tarefa> Buscar(FiltroTarefa filtro);
    }
}