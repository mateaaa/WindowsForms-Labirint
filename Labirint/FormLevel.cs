using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace Labirint
{
    public partial class FormLevel : Form
    {             
        private int x;
        private int y;

        static System.Windows.Forms.Timer MyTimer1 = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer level3Timer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer timer;
        int time;

        bool useM;
        bool useM_free;
        int mazeLevel;
        private Point mouseStart;
        private Point mouseEnd;
        Pen pen = new Pen(Color.FromArgb(255, 0, 255, 0), 5);

        bool dragging;
        bool paint = true; 

        Point mouseDownPoint;
        int end = 0;

        Graphics lab;
        Graphics nacrtanPlijen;

        public FormLevel(bool useMouse, bool useMouse_free, int level)
        {
            useM = useMouse;
            useM_free = useMouse_free;
            mazeLevel = level;

            InitializeComponent();

            x = 0;
            y = 0;
            dragging = false;
        }

        static int mazeWidth;
        static int mazeHeight;
        int[,] mazeCells;
        int mazeX=0;
        int mazeY=0;
        int finishX;
        int finishY;
        int rWidth = 25;
        int rHeight = 25;

        private void FormLevel_Load(object sender,EventArgs e)
        {
            mazeWidth = 500;
            mazeHeight = 500;
            ClientSize = new Size(mazeWidth, mazeHeight);

            //generiraj labirint
            GenForm labirint = new GenForm();
            mazeCells = new int[mazeWidth / rWidth, mazeHeight / rHeight];
            mazeCells = labirint.generateMaze(mazeWidth, mazeHeight, rWidth, rHeight);

            
            //nacrtaj labirint
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            }

            lab = Graphics.FromImage(pictureBox1.Image);
                
            for (int i = 0; i < mazeWidth;)
            {
                for (int j = 0; j < mazeHeight;)
                {
                    if (mazeCells[i / rWidth, j / rHeight] == 0)
                    {
                        Rectangle rect = new Rectangle(i , j , rWidth, rHeight);
                        lab.FillRectangle(Brushes.BlueViolet, rect);
                    }
                    
                    j += rHeight;
                }

                i += rWidth;
            }


            //nacrtaj kraj
            finishX = mazeWidth - 2 * rWidth;
            finishY = mazeHeight - 2 * rHeight;
            Label finish = new Label();
            finish.Location = new Point(finishX, finishY);
            finish.Visible = true;
            finish.Text = "Finish";
            finish.Parent = pictureBox1;
            finish.Height = rHeight;
            finish.Width = 2 * rWidth;

            if (mazeLevel == 2 || mazeLevel == 3)
            {
                //stvori(generiraj) plijenove

                for (int k = 0; k < 20; k++)
                {
                    for (int i = 100; i < mazeWidth;)
                    {
                        for (int j = 100; j < mazeHeight;)
                        {
                            if (mazeCells[i / rWidth, j / rHeight] == 1)
                            {
                                Plijen.sviPlijenovi.Add(new Plijen(i, j, 25, 25));
                                mazeCells[i / rWidth, j / rHeight] = 3;
                            }

                            j += rHeight * 5;
                        }

                        i += rWidth * 5;


                    }
                }


                MyTimer1.Interval = 20000;
                MyTimer1.Tick += new EventHandler(timer2_Tick);
                MyTimer1.Enabled = true;

                MyTimer1.Start();
                int bodoviTick = 0;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                bodoviTick++;
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
            }

            if (mazeLevel == 3)
            {
                level3Timer.Interval = 50000;
                level3Timer.Tick += new EventHandler(level3Timer_Tick);
                level3Timer.Enabled = true;
                level3Timer.Start();
            }

            
            //timer za bodovanje
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Start();

            time = 0;
            timer.Tick += Timer_Tick;
            
            pictureBox1.Invalidate();
             
        }

        private void level3Timer_Tick(object sender, EventArgs e)
        {
            level3Timer.Stop();
            DialogResult r = MessageBox.Show("Igra je gotova! Nisi završio u zadanom vremenu.");
            Close();
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
        
        private void zatvori()
        {
            end = 1;
            Bodovanje.brzinaKretanja(time);
            DialogResult result = MessageBox.Show("Bravo! Završio si labirint." +
                             " bodovi: " + Bodovanje.broj_bodova.ToString());

            if (result == DialogResult.OK)
            {
                Close();
            }
        }

        public void Timer_Tick(object sender, EventArgs e)
        {            
            if (end==1)
            {
                timer.Stop();
            }
            else
                time++;
        }

        //kretanje pomocu tipkovnice
        private void FormLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (useM == false && useM_free == false)
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
                    else if (mazeCells[mazeX - 1, mazeY] == 1 || mazeCells[mazeX - 1, mazeY] == 3)
                    {
                        x -= 25;
                        if (mazeCells[mazeX - 1, mazeY] == 3) Bodovanje.skupljenPlijen();
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
                    else if (mazeCells[mazeX + 1, mazeY] == 1 || mazeCells[mazeX + 1, mazeY] == 3)
                    {
                        x += 25;
                        if (mazeCells[mazeX + 1, mazeY] == 3) Bodovanje.skupljenPlijen();
                        mazeX++;
                        if (mazeX == 18 && mazeY == 18)
                        {
                            zatvori();
                        }
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
                    else if (mazeCells[mazeX, mazeY - 1] == 1 || mazeCells[mazeX, mazeY - 1] == 3)
                    {
                        y -= 25;
                        if (mazeCells[mazeX, mazeY - 1] == 3) Bodovanje.skupljenPlijen();
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
                    else if (mazeCells[mazeX, mazeY + 1] == 1 || mazeCells[mazeX, mazeY + 1] == 3)
                    {
                        y += 25;
                        if (mazeCells[mazeX, mazeY + 1] == 3) Bodovanje.skupljenPlijen();
                        mazeY++;
                        if (mazeX == 18 && mazeY == 18)
                        {
                            zatvori();
                        }
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                              
            }
            
        }


        private void FormLevel_MouseDown(object sender, MouseEventArgs e)
        {
            if (useM == true || useM_free == true)
            {
                dragging = true;

                mouseDownPoint = new Point(e.X, e.Y);

                if (useM_free == true)
                {
                    mouseStart = mouseEnd = mouseDownPoint;
                }
            }
        }

        private void FormLevel_MouseMove(object sender, MouseEventArgs e)
        {
            if (useM == true)
            {
                simpleMouseMove(e);
            }
            else {
                freehandMouseMove(e);
            }
        }
        //pomocna funkcija za slobodno kretanje
        private void again(int i)
        {
            dragging = false;
            int pomocni_x = 0, pomocni_y;
            for (int k = 0; k < 20; k++)
            {
                pomocni_y = 0;
                for (int l = 0; l < 20; l++)
                {
                    if (mazeCells[k, l] == i && (k != 0 || l != 0))
                    {
                        Rectangle rect = new Rectangle(pomocni_x, pomocni_y, 25, 25);
                        lab.FillRectangle(Brushes.Ivory, rect);
                    }
                    pomocni_y += rHeight;
                }
                pomocni_x += rWidth;
            }
        }
        //kretanje pomocu misa - slobodna linija
        private void freehandMouseMove(MouseEventArgs e)
        {
            int gotovo=0;
            int i, j;
            
            int boxXstart=0, boxXend=0, boxYstart, boxYend;
            if (dragging && mouseDownPoint.X>0 && mouseDownPoint.X<rWidth && 
                mouseDownPoint.Y>0 && mouseDownPoint.Y<rHeight)
            {
                for (i = 0; i < 20; i++)
                {
                        boxYstart = 0;
                        boxYend = 0;
                        boxXstart = boxXend;
                        boxXend += rWidth;                        
                        for (j = 0; j < 20; j++)
                        {
                            if (gotovo == 0)
                            {
                                boxYstart = boxYend;
                                boxYend += rHeight;
                                if (e.X >= finishX && e.Y >= finishY)
                                {
                                    gotovo = 1;
                                    zatvori();
                                }                            
                                else if (mazeCells[i, j] == 1 || mazeCells[i, j] == 3 && boxXstart < e.X &&
                                    boxXend > e.X && boxYstart < e.Y && boxYend > e.Y)
                                {
                                    if (mazeCells[i, j] == 3)
                                    {
                                        Bodovanje.skupljenPlijen();
                                        mazeCells[i, j] = 1;
                                        Rectangle rect = new Rectangle(boxXstart, boxYstart, 25, 25);
                                        lab.FillRectangle(Brushes.Ivory, rect);
                                    }
                                    mouseStart = mouseEnd;
                                    mouseEnd = new Point(e.X, e.Y);

                                    lab.DrawLine(pen, mouseStart, mouseEnd);

                                    this.Invalidate();
                                }
                                else if (mazeCells[i, j] == 0 && boxXstart <= e.X &&
                                boxXend >= e.X && boxYstart <= e.Y && boxYend >= e.Y)
                                {
                                    again(1);
                                }
                            }                            
                        }
                        
                }
                
            }  
            else if(dragging == false)
            {
                again(1);
            }          
        }
        //kretanje pomocu misa
        private void simpleMouseMove(MouseEventArgs e)
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
                    else if (mazeCells[mazeX - 1, mazeY] == 1 || mazeCells[mazeX - 1, mazeY] == 3)
                    {
                        x -= 25;
                        if (mazeCells[mazeX - 1, mazeY] == 3) Bodovanje.skupljenPlijen();
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
                    else if (mazeCells[mazeX + 1, mazeY] == 1 || mazeCells[mazeX + 1, mazeY] == 3)
                    {
                        x += 25;
                        if (mazeCells[mazeX + 1, mazeY] == 3) Bodovanje.skupljenPlijen();
                        mazeX++;
                        if (e.X >= finishX && e.Y >= finishY)
                        {
                            zatvori();
                        }
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
                    else if (mazeCells[mazeX, mazeY - 1] == 1 || mazeCells[mazeX, mazeY - 1] == 3)
                    {
                        y -= 25;
                        if (mazeCells[mazeX, mazeY - 1] == 3) Bodovanje.skupljenPlijen();
                        mazeY--;
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
                else if (e.Y - mouseDownPoint.Y <= y + 20 && e.Y >= y + 25 && mazeY + 1 <= 19)
                {
                    if (mazeCells[mazeX, mazeY + 1] == 2)
                    {
                        makeRect();
                        y += 25;
                        mazeCells[mazeX, mazeY] = 1;
                        mazeY++;
                    }
                    else if (mazeCells[mazeX, mazeY + 1] == 1 || mazeCells[mazeX, mazeY + 1] == 3)
                    {
                        y += 25;
                        if (mazeCells[mazeX, mazeY + 1] == 3) Bodovanje.skupljenPlijen();
                        mazeY++;
                        if (e.X >= finishX && e.Y >= finishY)
                        {
                            zatvori();
                        }
                        mazeCells[mazeX, mazeY] = 2;
                    }
                }
            }                
        }

        private void FormLevel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }


        private void FormLevel_Paint(object sender, PaintEventArgs e)
        {            
            if (paint)
            {

                Random r = new Random();

                Color Color1 = Color.Crimson;
                Color Color2 = Color.Yellow;
                Color Color3 = Color.Blue;

                Brush Brush1 = new SolidBrush(Color1);
                Brush Brush2 = new SolidBrush(Color2);
                Brush Brush3 = new SolidBrush(Color3);
                List<Brush> brushes = new List<Brush>();
                brushes.Add(Brush1);
                brushes.Add(Brush2);
                brushes.Add(Brush3);

                foreach (Plijen p in Plijen.sviPlijenovi)
                {
                    nacrtanPlijen = Graphics.FromImage(pictureBox1.Image);

                    int b = r.Next(0, 3);

                    nacrtanPlijen.FillRectangle(brushes[b], p.pravokutnik);
                }

            }
            pictureBox1.Invalidate();             
       }

        
        private void timer2_Tick(object sender, EventArgs e)
        {
            
            MyTimer1.Stop();
            paint = false;

            if (paint == false)
            {
                again(3);
                Plijen.sviPlijenovi.Clear();
            }
            
            this.Invalidate();
        }        
    }
}
