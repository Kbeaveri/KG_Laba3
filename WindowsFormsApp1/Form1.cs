using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Graphics g;
        float W;
        float H;
        float x_center;
        float y_center;
        float dx;
        float dy;
        int x1 = 0;
        int y1 = 0;
        int x2 = 5;
        int y2 = 3;
        int x_centering;
        int y_centering;
        int offsetX = 0;
        bool flag = false;
        bool invertingX = false;
        bool invertingY = false;
        bool invertingXY = false;
        bool startBrzd = false;
        bool startPic = false;
        Pen pen = new Pen(Color.Black);
        Pen linePen = new Pen(Color.Red,2);
        Pen linePen2 = new Pen(Color.Green, 2);
        Pen pointLine = new Pen(Color.Blue,2);
        Pen Rec = new Pen(Color.Black);
        Brush Br = Brushes.Gray;
        List<Point> points = new List<Point>();
        List<Point> pic = new List<Point>();
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            W = this.pictureBox1.Width;
            H = this.pictureBox1.Height;
            x_center = W / 2;
            y_center = H / 2;
            dx = W / 20;
            dy = H / 20;
        }
        private void DrawAxis()
        {
            g.Clear(Color.White);
            Pen Axis = new Pen(Color.Black,3);
            g.DrawLine(Axis, x_center, 0, x_center, H);
            g.DrawLine(Axis, 0, y_center, W, y_center);
            Font Fon = new Font("Arial", 9, FontStyle.Regular);
            Brush Br = Brushes.Black;
            g.DrawString("X", Fon, Br, W - 15, y_center + 10);
            g.DrawString("Y", Fon, Br, x_center - 20, 10);
            for (int i = -10; i < 10; i++)
            {
                g.DrawString(i.ToString(), Fon, Br, x_center - 15, y_center + dy * i);
                g.DrawString(i.ToString(), Fon, Br, x_center + dx * i - 10, y_center + 10);
            }
            for (int i = 0;i < 20; i++)
            {
                g.DrawLine(pen,1,1*i* dy,W,1*i* dy);
                g.DrawLine(pen, 1 * i * dx, 1, 1 * i * dx, H);
            }
            DrawLine(x1, y1, x2, y2,linePen);
        }
        float X(int x)
        {
            return x_center + x * dx;
        }
        float Y(int y)
        {
            return y_center + y * -dy;
        }
        private void drawPoint(float x,float y)
        {
            g.DrawEllipse(pointLine,x_center + x * dx, y_center + y * -dy, 3, 3);
        }

        private bool checkX()   
        {
            return true;
        }
        void DrawLine(float x1,float y1,float x2,float y2,Pen pen)
        {
            g.DrawLine(pen,x_center + x1 * dx,y_center + y1 * -dy,x_center + x2 * dx,y_center + y2 * -dy);
        }
        void centering()
        {
            x_centering = -x1;
            y_centering = -y1;
            x1 = 0;
            y1 = 0;
            x2 = x2 + x_centering;
            y2 = y2 + y_centering;
            DrawAxis();
            DrawLine(x1, y1, x2, y2, linePen);
        }
        void centering2()
        {
            for (int i = 0;i < pic.Count; i++)
            {
                pic[i] = new Point(pic[i].X - x_centering, pic[i].Y - y_centering);
            }
        }
        private void invertY()
        {
            y2 = -y2;
        }
        private void invertY(int a)
        {
            y2 = -y2;
            for (int i = 0; i < pic.Count; i++)
            {
                pic[i]= new Point(pic[i].X, -pic[i].Y+ 1);
            }
        }
        private void invertX()
        {
            x2 = -x2;
        }
        private void invertX(int a)
        {
            x2 = -x2;
            for (int i = 0; i < pic.Count; i++)
            {
                pic[i] = new Point(-pic[i].X - 1, pic[i].Y);
            }
        }
        private void BRZN()
        {
            int x = x1, y = y1;
            int delX = x2 - x1;
            int delY = y2 - y1;
            int e = 2 * delY - delX;
            for (int i = 0;i < delX; i++)
            {
                drawPoint(x, y);
                points.Add(new Point(x,y));
                while (e >= 0)
                {
                    y += 1;
                    e = e - 2 * delX;
                }
                x += 1;
                e = e + 2 * delY;
            }
            drawPoint(x2, y2);
            points.Add(new Point(x2, y2));
        }
        private void invertXY()
        {
            
            int tmp;
            tmp = x2;
            x2 = y2;
            y2 = tmp;
        }
        private void invertXY(int a)
        {
            int tmp;
            tmp = x2;
            x2 = y2;
            y2 = tmp;
            for (int i = 0; i < pic.Count; i++)
            {
                pic[i] = new Point(pic[i].Y, pic[i].X);
            }
        }
        private void drawPic() 
        {
            pic.Add(new Point(points[0].X, points[0].Y + 1));
            for (int i = 1;i < points.Count-1; i++)
            {
                if (points[i].Y < points[i + 1].Y)
                {
                    pic.Add(new Point(points[i].X, points[i].Y + 1));
                }
                else
                {
                    pic.Add(new Point(points[i].X, points[i].Y));
                }
            }
        }
        private void drawRec()
        {
            for (int i = 0;i < pic.Count; i++)
            {
                g.FillRectangle(Br, X(pic[i].X), Y(pic[i].Y), 27, 27);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                DrawAxis();
                flag = true;
                return;
            }
            if (x1 != 0 || y1 != 0)
            {
                centering();
                return;
            }
            if (x2 < 0)
            {
                invertX();
                invertingX = true;
                DrawAxis();
                return;
            }
            if (y2 < 0)
            {
                invertY();
                invertingY = true;
                DrawAxis();
                return;
            }
            if (x2 < y2)
            {
                invertXY();
                invertingXY = true;
                DrawAxis();
                return;
            }
            if (startBrzd == false)
            {
                BRZN();
                startBrzd = true;
                return;
            }
            if (startPic == false)
            {
                drawPic();
                drawRec();
                startPic = true;
                return;
            }
            if (invertingXY)
            {
                invertXY(1);
            }
            if (invertingY)
            {
                invertY(1);
            }
            if (invertingX)
            {
                invertX(1);
            }
            centering2();
            DrawAxis();
            drawRec();

            //for (int i = 0; i < points.Count-1; i++)
            //{
            //    DrawLine(points[i].X, points[i].Y, points[i+1].X, points[i+1].Y,linePen2);
            //}

        }
    }
}
