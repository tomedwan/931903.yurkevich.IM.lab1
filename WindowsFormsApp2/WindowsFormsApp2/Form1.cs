using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double g = 9.81;
        const double C = 0.15;
        const double rho = 1.29;
        double height, angle, speed, S, m;


        double cosa, sina, beta, k;

        double t, x, y, vx, vy, maxh;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            height = (double)edHeight.Value;
            angle = (double)edAngle.Value;
            speed = (double)edSpeed.Value;
            S = (double)edSize.Value;
            m = (double)edWeight.Value;

            cosa = Math.Cos(angle * Math.PI / 180);
            sina = Math.Sin(angle * Math.PI / 180);

            beta = 0.5 * C * S * rho;
            k = beta / m;
            t = 0;
            x = 0;
            y = maxh = height;
            vx = speed * cosa;
            vy = speed * sina;

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double dt = (double)edStep.Value; //модел время
            double vx_old = vx;
            double vy_old = vy;
            double root = Math.Sqrt(vx * vx + vy * vy);
            double vend, xend;
            t = t + dt;
            vx = vx_old - k * vx_old * root * dt;
            vy = vy_old - (g + k * vy_old * root) * dt;
            xend = x;
            x = x + vx * dt;
            y = y + vy * dt;

            if (y > maxh) maxh = y;

            chart1.Series[0].Points.AddXY(x, y);
            if (y <= 0)
            {
                timer1.Stop();
                vend = Math.Sqrt(speed * speed - 2 * speed * sina * g * t + g * g * t * t);
                distance.Text = Convert.ToString(xend);
                maxheight.Text = Convert.ToString(maxh);
                epSpeed.Text = Convert.ToString(vend);
            }
        }
    }
}
