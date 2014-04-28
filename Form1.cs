using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        int cw, ch;
        bool left = true, right = true;//for making either left or right mouse buttons to work at a given time
        Box[] boxarr = new Box[9];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Instructions:\n\n1)In this game it is assumed that one player plays with left mouse button and the other plays with right mouse button\n\n2)One player has to wait for the another player to finish the turn before playing his'\n\n3)If you find any bugs or even better way to do a task in the game ,rather than making fun out of me, make necessary changes and please do mail me back at venugupta4u@gmail.com");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Something();

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0, y = 0;
            Box b = null;
            Point p = e.Location;
            if (p.X > 0 && p.X < cw / 3)
                x = 0;
            else if (p.X > cw / 3 && p.X < 2 * cw / 3)
                x = cw / 3;
            else if (p.X > 2 * cw / 3 && p.X < cw)
                x = 2 * cw / 3;
            if (p.Y > 0 && p.Y < ch / 3)
                y = 0;
            else if (p.Y > ch / 3 && p.Y < 2 * ch / 3)
                y = ch / 3;
            else if (p.Y > 2 * ch / 3 && p.Y < ch)
                y = 2 * ch / 3;
            for (int i = 0; i < 9; i++)
            {
                b = boxarr[i];
                if (b.x == x && b.y == y)
                    break;
            }
            if (e.Button == MouseButtons.Left && left == true && b.fig == Figure.Nothing)
            {
                b.fig = Figure.Circle;
                right = true;
                left = false;
                Invalidate();
            }
            if (e.Button == MouseButtons.Right && right == true && b.fig == Figure.Nothing)
            {
                b.fig = Figure.Rectangle;
                left = true;
                right = false;
                Invalidate();
            }
            Strikeoff();
        }
        public void Strikeoff()
        {
            int[,] win = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            int cnt = 0;

            
            for (int i = 0; (i < 8); i++)
            {
                if (boxarr[win[i, 0]].fig == Figure.Circle && boxarr[win[i, 1]].fig == Figure.Circle && boxarr[win[i, 2]].fig == Figure.Circle)
                {
                    MessageBox.Show("Player Left wins");
                    SetInitialState();
                    return;
                }
                else if (boxarr[win[i, 0]].fig == Figure.Rectangle && boxarr[win[i, 1]].fig == Figure.Rectangle && boxarr[win[i, 2]].fig == Figure.Rectangle)
                {
                    MessageBox.Show("Player Right wins");
                    SetInitialState();
                    return;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (boxarr[i].fig == Figure.Circle || boxarr[i].fig == Figure.Rectangle)
                    cnt++;
            }
            if (cnt == 9)
            {
                MessageBox.Show("Game is a Draw");
                SetInitialState();
                return;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            Graphics g = this.CreateGraphics();
            cw = this.ClientSize.Width;
            ch = this.ClientSize.Height;
            boxarr[0] = new Box(0, 0);
            boxarr[1] = new Box(cw / 3, 0);
            boxarr[2] = new Box(2 * cw / 3, 0);
            boxarr[3] = new Box(0, ch / 3);
            boxarr[4] = new Box(cw / 3, ch / 3);
            boxarr[5] = new Box(2 * cw / 3, ch / 3);
            boxarr[6] = new Box(0, 2 * ch / 3);
            boxarr[7] = new Box(cw / 3, 2 * ch / 3);
            boxarr[8] = new Box(2 * cw / 3, 2 * ch / 3);
        }
        public void Something()
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black);
            g.DrawLine(p, cw / 3, 0, cw / 3, ch);
            g.DrawLine(p, 2 * cw / 3, 0, 2 * cw / 3, ch);
            g.DrawLine(p, 0, ch / 3, cw, ch / 3);
            g.DrawLine(p, 0, 2 * ch / 3, cw, 2 * ch / 3);
            for (int i = 0; i < 9; i++)
            {
                if (boxarr[i] != null)
                {
                    if (boxarr[i].fig == Figure.Circle)
                    {
                        p.Color = Color.Red;
                        g.DrawEllipse(p, boxarr[i].x, boxarr[i].y, cw / 3 - 10, ch / 3 - 10);
                    }
                    else if (boxarr[i].fig == Figure.Rectangle)
                    {
                        p.Color = Color.RoyalBlue;
                        g.DrawRectangle(p, boxarr[i].x, boxarr[i].y, cw / 3 - 10, ch / 3 - 10);
                    }
                }

            }
        }
        public void SetInitialState()
        {
            btnStart.Visible = true;
            btnStart.Text = "Play Again";
            for (int i = 0; i < 9; i++)
                boxarr[i] = null;
            cw = 0;
            ch = 0;
            left = true;
            right = true;
            Invalidate();
        }
        enum Figure
        {
            Nothing,
            Circle,
            Rectangle
        }
        class Box
        {
            public int x, y;
            public Figure fig;
            public Box()
            {
            }
            public Box(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}


