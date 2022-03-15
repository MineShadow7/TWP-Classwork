using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Это классная работа под названием ЛАБ 3.


namespace Lab1_2
{
    public partial class Form1 : Form
    {

        Poloska p;
        List<Poloska> ps;

        Random random;
        const int WPolos = 200;
        const int HPolos = 50;


        public Form1()
        {
            InitializeComponent();

            ps = new List<Poloska>();
            random = new Random();

            CreatePolos(10);
        }



        public void CreatePolos(int n)
        {
            for (int i = 0; i < n; i++)
            {
                int x = random.Next(this.Width - WPolos);
                int y = random.Next(this.Height - HPolos - 100);
                p = new Poloska(x, y, WPolos, HPolos);

                int r = random.Next(256);
                int g = random.Next(256);
                int b = random.Next(256);

                Color c = Color.FromArgb(r, g, b);
                p.SetColor(c);
                ps.Add(p);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            for(int i = 0; i < ps.Count; i++)
            {
                ps[i].Draw(g);
            }



        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = ps.Count - 1; i >= 0; i--)
            {
                if (ps[i].Inside(e.X, e.Y))
                {
                    bool ok = true;
                    for(int j = i + 1; j < ps.Count; j++)
                    {
                        if (ps[i].Intersect(ps[j]))
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok) 
                    {
                        ps.RemoveAt(i);
                        Invalidate();
                        break;
                    }

                }
            }
            CreatePolos(1);
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CreatePolos(1);
            Invalidate();
        }
    }
}
