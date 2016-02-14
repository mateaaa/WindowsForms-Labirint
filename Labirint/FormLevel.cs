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
    public partial class FormLevel : Form
    {
        public FormLevel()
        {
            InitializeComponent();
        }

        int mazeWidth;
        int mazeHeight;

        private void FormLevel_Load(object sender, EventArgs e)
        {
            mazeWidth = 500;
            mazeHeight = 500;
            ClientSize = new Size(mazeWidth, mazeHeight);
            int rWidth = 25;
            int rHeight = 25;

            //generiraj labirint
            GenForm labirint = new GenForm();
            int[,] mazeCells = new int[mazeWidth / rWidth, mazeHeight / rHeight];
            mazeCells = labirint.generateMaze(mazeWidth, mazeHeight, rWidth, rHeight);

            //nacrtaj labirint
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width,
                        pictureBox1.Height);
            }
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {

                for (int i = 0; i < mazeWidth;)
                {
                    for (int j = 0; j < mazeHeight;)
                    {
                        if (mazeCells[i / rWidth, j / rHeight] == 0)
                        {
                            Rectangle rect = new Rectangle(i, j, rWidth, rHeight);
                            g.FillRectangle(Brushes.BlueViolet, rect);
                        }
                        j += rHeight;
                    }

                    i += rWidth;
                }
            }
            pictureBox1.Invalidate();

        }
    }
}
