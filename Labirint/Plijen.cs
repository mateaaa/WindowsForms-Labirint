using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Labirint
{
    public class Plijen
    {
        public static List<Plijen> sviPlijenovi = new List<Plijen>();

        public bool skupljen = false;

        public bool paused = false;
        public Rectangle pravokutnik;

        private System.Timers.Timer pauseTimer = new System.Timers.Timer(5000); //5 sekundi
       
        public int x, y, sirinaPlijena = 25, visinaPlijena = 25;

        
        public Plijen(int xos, int yos, int sirina, int velicina )
        {
            
            this.x = xos;
            this.y = yos;

            pravokutnik = new Rectangle(x,y, sirinaPlijena, visinaPlijena);
           // pauseTimer.Elapsed -= pauseTimer_Elapsed;
           // pauseTimer.Elapsed+= pauseTimer_Elapsed;
            
        }
        /*
        void pauseTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            paused = false;
            pauseTimer.Enabled = false;
        }
        
        public void pauzirajTimer()
        {
           
            pauseTimer.Enabled = true;
        
        }
        public void promijeniMjesto(int[,] mazeCells, int mazeWidth, int mazeHeight)
        {
           

                for (int i = 0; i < mazeWidth; )
                {
                    for (int j = 0; j < mazeHeight; )
                    {
                        if (mazeCells[i / 25, j / 25] == 0)
                        {
                           this.pravokutnik = new Rectangle(i,j, sirinaPlijena, visinaPlijena);
                        }

                        j += 25 * 5;
                    }

                    i += 25 * 5;


                }


                
        }*/

       


    }
}
