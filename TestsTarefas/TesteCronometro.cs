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
        public void NovoCronometro_DeveIniciarComPropriedadesCorretas()
        {
            // Arrange & Act
            var c = new Cronometro();

            // Assert
            Assert.True(c.EmAndamento());
            Assert.Equal(TimeSpan.Zero, c.Total);
            Assert.True(c.Inicio <= DateTime.Now);
        }



        // TODO: Implementar um mock para o tempo para tornar o teste mais determinístico
        [Fact]
        public void StopCronometro_DeveInterromperCronometro()
        {
            // Arrange
            var c = new Cronometro();

            // Act
            var tempoInicial = c.Inicio;
            c.Stop();

            // Assert
            Assert.False(c.EmAndamento());
            Assert.True(c.Total >= TimeSpan.Zero);
        }
    }
}

//FALTA
// 1 - Implementar um mock para o tempo para tornar o teste mais determinístico
