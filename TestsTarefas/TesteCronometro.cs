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
        // TODO: Melhorar o nome do teste para indicar claramente o comportamento esperado (ex: NovoCronometro_DeveIniciarEmAndamento)
        // TODO: Adicionar verificações adicionais para outras propriedades iniciais do cronômetro
        [Fact]
        public void NovoCronometro()
        {
            var c = new Cronometro();

            Assert.True(c.EmAndamento());
        }

        // TODO: Seguir convenção de nomenclatura PascalCase para nomes de métodos (StopCronometro em vez de stopCronometro)
        // TODO: Evitar uso de Thread.Sleep em testes, pois torna os testes mais lentos e potencialmente instáveis
        // TODO: Implementar um mock para o tempo para tornar o teste mais determinístico
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
