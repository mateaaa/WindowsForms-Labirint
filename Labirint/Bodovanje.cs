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
        static int bodovi = 0;

        static System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();

        static public int broj_bodova { get{ return bodovi; } }
        static public void skupljenPlijen() { bodovi += 3; }
        static public void brzinaKretanja(Timer brzina) { bodovi -= 2; }
        static public void odstupanjeOdSredine() { bodovi -= 1; }
        static public void pomoc() { bodovi -= 2; }

    }
}
