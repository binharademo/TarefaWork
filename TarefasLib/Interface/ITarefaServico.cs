﻿using TarefasLibrary.Modelo;

namespace TarefasLibrary.Interface
{
    public interface ITarefaServico
    {
        bool Salvar(Tarefa tarefa);
        public Tarefa? BuscarPorId(int id);
        public bool Atualizar(Tarefa tarefa, StatusTarefa novostatus, string novadescricao, DateTime novoprazo);
        public List<Tarefa> ListarTodas();
        public List<Tarefa> ListarPorUsuario(int id);

    }
}