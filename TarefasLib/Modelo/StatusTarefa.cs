using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StatusTarefa
{
    public enum Status
    {
        ToDo,
        Doing,
        Done
    }
    public Status status;

    public StatusTarefa(Status status)
    {
        this.status = status;
    }

    
    public Status getStatus()
    {
        return status;
    }

    public void setStatus(Status status)
    {
        this.status = status;
    }
}

