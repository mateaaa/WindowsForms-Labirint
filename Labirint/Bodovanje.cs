using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace Labirint
{
    class Bodovanje
    {
        static int bodovi = 50;

        static System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();

        static public int broj_bodova { get{ return bodovi; } }
        static public void skupljenPlijen() { bodovi += 3; }
        static public void brzinaKretanja(int brzina) { bodovi -= brzina; }
        static public void odstupanjeOdSredine() { bodovi -= 100; }
        static public void pomoc() { bodovi -= 2; }

    }
}
