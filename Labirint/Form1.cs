using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labirint
{
    public partial class Form1 : Form
    {
        public Form1()
        {


            InitializeComponent();
         

        }
         private void MoveToStart()
        {
         
       }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) //Level 1
            {
                bool useMouse = radioButton5.Checked ? true : false;
                bool useMouse_free = radioButton7.Checked ? true : false;
                FormLevel L1 = new FormLevel(useMouse, useMouse_free);
                L1.Show();
            }
        }
    }
}
