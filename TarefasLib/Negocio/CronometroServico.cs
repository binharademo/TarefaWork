

using System.Diagnostics;

namespace TestsTarefas
{
    public class CronometroServico //: ICronometroServico
    {
        //private ICronometroRepositorio _repositorio;

        //public CronometroServico(ICronometroRepositorio repositorio)
        //{
        //    _repositorio = repositorio;
        //}

        //public void Iniciar()
        //{
        //    var cronometro = _repositorio.ObterCronometro();
        //    cronometro.EmExecucao = true;
        //    cronometro.TempoInicio = DateTime.Now;
        //    cronometro.TempoAcumulado = TimeSpan.Zero;
        //    _repositorio.Salvar(cronometro);
        //}

        //public CronometroServico ObterCronometro()
        //{
        //    throw new NotImplementedException();
        //}

        //public TimeSpan ObterTempoDecorrido()
        //{
        //    var cronometro = _repositorio.ObterCronometro();
        //    return cronometro.TempoAcumulado;
        //}



        //public void Pausar()
        //{
        //    var cronometro = _repositorio.ObterCronometro();
        //    if(cronometro.EmExecucao && cronometro.TempoInicio.HasValue)
        //    {
        //        TimeSpan tempoDecorrido = DateTime.Now - cronometro.TempoInicio.Value;
        //        cronometro.TempoAcumulado += tempoDecorrido;
        //        cronometro.EmExecucao = false;
        //        cronometro.TempoInicio = null;
        //        _repositorio.Salvar(cronometro) ;
        //    }
        //}
    }
}