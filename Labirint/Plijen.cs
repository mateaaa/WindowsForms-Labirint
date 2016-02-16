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
        public SolidBrush boja;

        public Rectangle pravokutnik;

        public int x, y, sirinaPlijena, visinaPlijena;

        
        public Plijen(int x, int y, Random r)
        {

            visinaPlijena = r.Next(2,20);
            sirinaPlijena = 10;

            this.x = x;
            this.y = y;

            boja = new SolidBrush(Color.Red);

            pravokutnik = new Rectangle(x,y, sirinaPlijena, visinaPlijena);
            
        }

        public void nacrtajPlijen(Graphics g)
        {
            g.FillRectangle(boja, pravokutnik);
        
        }
        public void promijeniMjesto()
        {
            pravokutnik = new Rectangle(y, x, sirinaPlijena, visinaPlijena);
          
        }


    }
}
