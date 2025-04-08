using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TestsTarefas;

namespace Tests_Tarefas
{
    public class TesteRelogio
    {

        [Fact]
        public void CriaRelogio()
        {
            // arrange
            var r = new Relogio();

            //act
            r.Start();
            r.Pause();

            r.Start();
            r.Pause();

            //assert
            Assert.NotEmpty(r.Tempos);
            Assert.Equal(2, r.Tempos.Count);
        }

        [Fact]
        public void TentaStartarSemPause()
        {
            // arrange
            var r = new Relogio();

            //act
            r.Start();
            bool result = r.Start();

            //assert
            Assert.NotEmpty(r.Tempos);
            Assert.False(result);
        }

        [Fact]
        public void TentaPausar2X()
        {
            // arrange
            var r = new Relogio();
            r.Start();
            r.Pause();

            //act
            bool result = r.Pause();

            //assert
            Assert.NotEmpty(r.Tempos);
            Assert.False(result);
        }

        [Fact]
        public void CalculaTempo()
        {
            // arrange
            var r = new Relogio();
            var dt = new DateTime(2025,04,06,10,20,00);
            r.Start();
            Thread.Sleep(100);
            r.Pause();
            r.Start();
            Thread.Sleep(100);
            r.Pause();

            //act
            TimeSpan result = r.Finaliza();

            //assert
            Assert.True(result.Milliseconds >= 200); 
        }

        [Fact]
        public void CalculaTempoSemPausa()
        {
            // arrange
            var r = new Relogio();
            var dt = new DateTime(2025, 04, 06, 10, 20, 00);
            r.Start();
            Thread.Sleep(100);
            r.Pause();
            r.Start();
            Thread.Sleep(100);

            //act
            TimeSpan result = r.Finaliza();

            //assert
            Assert.True(result.Milliseconds >= 200);
        }
    }
}
