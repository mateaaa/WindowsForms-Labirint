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
        private int x;
        private int y;

        bool useM;
        bool dragging;
        Point mouseDownPoint;

        Graphics lab;

        public FormLevel(bool useMouse)
        {
            useM = useMouse;
            InitializeComponent();

            x = 0;
            y = 0;
            dragging = false;
        }

        int mazeWidth;
        int mazeHeight;
        int[,] mazeCells;
        int mazeX=0;
        int mazeY=0;

        private void FormLevel_Load(object sender, EventArgs e)
        {
            mazeWidth = 500;
            mazeHeight = 500;
            ClientSize = new Size(mazeWidth, mazeHeight);
            int rWidth = 25;
            int rHeight = 25;

            //generiraj labirint
            GenForm labirint = new GenForm();
            mazeCells = new int[mazeWidth / rWidth, mazeHeight / rHeight];
            mazeCells = labirint.generateMaze(mazeWidth, mazeHeight, rWidth, rHeight);

            //nacrtaj labirint
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width,
                        pictureBox1.Height);
            }

            lab = Graphics.FromImage(pictureBox1.Image);
                
            for (int i = 0; i < mazeWidth;)
            {
                for (int j = 0; j < mazeHeight;)
                {
                    if (mazeCells[i / rWidth, j / rHeight] == 0)
                    {
                        Rectangle rect = new Rectangle(i, j, rWidth, rHeight);
                        lab.FillRectangle(Brushes.BlueViolet, rect);
                    }
                    j += rHeight;
                }

                i += rWidth;
            }

            pictureBox1.Invalidate();

        }

        public void drawPoint(object sender, EventArgs e) {
            Rectangle rect = new Rectangle(x, y, 25, 25);
            lab.FillRectangle(Brushes.Green, rect);
            mazeCells[0, 0] = 2;
            pictureBox1.Invalidate();            
        }

        public void makeRect()
        {
            Rectangle rect = new Rectangle(x, y, 25, 25);
            lab.FillRectangle(Brushes.Ivory, rect);
        }

        private void FormLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (useM == false)
            {
                if (e.KeyCode == Keys.Left && mazeX - 1>=0)
                {
                    if (mazeCells[mazeX - 1, mazeY] == 2)
                    {
                        makeRect();
                        x -= 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeX--;
                    }
                    else if (mazeCells[mazeX - 1, mazeY] == 1)
                    {
                        x -= 25;
                        mazeX--;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                else if (e.KeyCode == Keys.Right && mazeX + 1<=19)
                {
                    if (mazeCells[mazeX + 1, mazeY] == 2)
                    {
                        makeRect();
                        x += 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeX++;
                    }
                    else if (mazeCells[mazeX + 1, mazeY] == 1)
                    {
                        x += 25;
                        mazeX++;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                else if (e.KeyCode == Keys.Up && mazeY - 1>=0)
                {
                    if (mazeCells[mazeX, mazeY - 1] == 2)
                    {
                        makeRect();
                        y -= 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeY--;
                    }
                    else if (mazeCells[mazeX, mazeY - 1] == 1)
                    {
                        y -= 25;
                        mazeY--;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                else if (e.KeyCode == Keys.Down && mazeY + 1<=19)
                {
                    if (mazeCells[mazeX, mazeY + 1] == 2)
                    {
                        makeRect();
                        y += 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeY++;
                    }
                    else if (mazeCells[mazeX, mazeY + 1] == 1)
                    {
                        y += 25;
                        mazeY++;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }                
            }
        }

        private void FormLevel_MouseDown(object sender, MouseEventArgs e)
        {
            if (useM == true)
            {
                dragging = true;
                mouseDownPoint = new Point(e.X, e.Y);
            }
        }

        private void FormLevel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                if (mouseDownPoint.X - e.X <= x + 20 && e.X < x && mazeX - 1 >= 0)
                {
                    if (mazeCells[mazeX - 1, mazeY] == 2)
                    {
                        makeRect();
                        x -= 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeX--;
                    }
                    else if (mazeCells[mazeX - 1, mazeY] == 1)
                    {
                        x -= 25;
                        mazeX--;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                else if (e.X - mouseDownPoint.X <= x + 20 && e.X > x + 25 && mazeX + 1 <= 19)
                {
                    if (mazeCells[mazeX + 1, mazeY] == 2)
                    {
                        makeRect();
                        x += 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeX++;
                    }
                    if (mazeCells[mazeX + 1, mazeY] == 1)
                    {
                        x += 25;
                        mazeX++;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                else if (mouseDownPoint.Y - e.Y <= y + 20 && e.Y < y && mazeY - 1 >= 0)
                {
                    if (mazeCells[mazeX, mazeY - 1] == 2)
                    {
                        makeRect();
                        y -= 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeY--;
                    }
                    if (mazeCells[mazeX, mazeY - 1] == 1)
                    {
                        y -= 25;
                        mazeY--;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                else if (e.Y - mouseDownPoint.Y >= y + 20 && e.Y > y + 25 && mazeY + 1 <= 19)
                {
                    if (mazeCells[mazeX, mazeY + 1] == 2)
                    {
                        makeRect();
                        y += 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeY++;
                    }
                    if (mazeCells[mazeX, mazeY + 1] == 1)
                    {
                        y += 25;
                        mazeY++;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
            }                
        }

        private void FormLevel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

    }
}
