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
            int level;
            if (radioButton1.Checked)
                level = 1;
            else if (radioButton2.Checked)
                level = 2;
            else //radioButton3.Checked
                level = 3;

            bool useMouse = radioButton5.Checked ? true : false;
            bool useMouse_free = radioButton7.Checked ? true : false;
            FormLevel L1 = new FormLevel(useMouse, useMouse_free, level);
            L1.Show();
        }
    }
}
