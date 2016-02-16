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
        public Rectangle pravokutnik;
        public int ukloni = 1;

        public int x, y, sirinaPlijena = 25, visinaPlijena = 25;

        
        public Plijen(int xos, int yos, int sirina, int velicina )
        {
            
            this.x = xos;
            this.y = yos;

            pravokutnik = new Rectangle(x,y, sirinaPlijena, visinaPlijena);
         
            
        }


        public void promijeniMjesto(int[,] mazeCells, int mazeWidth, int mazeHeight)
        {
           

                for (int i = 0; i < mazeWidth; )
                {
                    for (int j = 0; j < mazeHeight; )
                    {
                        if (mazeCells[i / 25, j / 25] == 1)
                        {
                           this.pravokutnik = new Rectangle(i,j, sirinaPlijena, visinaPlijena);
                        }

                        j += 25 * 5;
                    }

                    i += 25 * 5;


                }


                
        }


    }
}
