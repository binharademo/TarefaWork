using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StatusTarefa
{
    // Definindo o enum Status dentro da classe
    public enum Status
    {
        ToDo,
        Doing,
        Done
    }

    // Atributo do tipo Status
    public Status status;

    // Construtor para definir o status inicial
    public StatusTarefa(Status status)
    {
        this.status = status;
    }

    // Método para obter o status
    public Status getStatus()
    {
        return status;
    }

    // Método para atualizar o status
    public void setStatus(Status status)
    {
        this.status = status;
    }
}

