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
        public int ukloni = 1;

        private System.Timers.Timer pauseTimer = new System.Timers.Timer(5000); //5 sekundi
       
        public int x, y, sirinaPlijena = 25, visinaPlijena = 25;

        
        public Plijen(int xos, int yos, int sirina, int velicina )
        {
            
            this.x = xos;
            this.y = yos;

            pravokutnik = new Rectangle(x,y, sirinaPlijena, visinaPlijena);
           
        }       
    }
}
