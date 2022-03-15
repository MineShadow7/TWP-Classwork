using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1_2
{
    class Poloska
    {
        // int x, y, w, h;
        Rectangle rect;
        Color col;


        public Poloska(int x, int y, int w, int h) 
        {
            rect = new Rectangle(x, y, w, h);
            col = Color.Aqua;
        }





        //Задать цвет

        public void SetColor(Color c)
        {
            col = c;
        }

        // Рисуем!!
        public void Draw(Graphics g) 
        {
            g.FillRectangle(new SolidBrush(col), rect);
            g.DrawRectangle(Pens.Black, rect);
        }

        public bool Inside(int x, int y)
        {
            if (x < rect.X || x > rect.Right)
                return false;
            if (y < rect.Top || y > rect.Bottom)
                return false;
            return true;
        }
        public bool Intersect(Poloska p)
        {
            return rect.IntersectsWith(p.rect);
        }
    }
}
