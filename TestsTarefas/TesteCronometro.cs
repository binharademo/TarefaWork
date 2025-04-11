using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;

namespace Tests_Tarefas
{
    public class TesteCronometro
    {
        [Fact]
        public void NovoCronometro()
        {
            var c = new Cronometro();

            Assert.True(c.EmAndamento());
        }

        [Fact]
        public void stopCronometro()
        {
            var c = new Cronometro();
            Thread.Sleep(100);
            c.Stop();

            Assert.False(c.EmAndamento());
            Assert.True(c.Total.Milliseconds >= 100); 
        }
    }
}
