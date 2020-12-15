using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CousreProjectKolosov
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter; 

        public Form1()
        {
            InitializeComponent();
            
            comboBox2.SelectedIndex = 0;
            
            colorDialog1.FullOpen = true;
            colorDialog2.FullOpen = true;

            emitter = new Emitter
            {
                SpeedMin = 1,
                SpeedMax = 20,
                X = 0,
                Y = 0,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
            };

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            colorDialog1.Color = Color.Gold;
            colorDialog2.Color = Color.Red;
            colorDialog3.Color = Color.Black;
            colorDialog4.Color = Color.Black;

            button1.BackColor = colorDialog1.Color;
            button2.BackColor = colorDialog2.Color;
            button4.BackColor = colorDialog3.Color;
            button5.BackColor = colorDialog4.Color;

            lblDirection.Text = $"{tbDirection.Value}°";
            label2.Text = $"{tbSpread.Value} у.е";
            label5.Text = $"{tbFrequency.Value} у.е";



            this.BackColor = Color.Black;

            emitters.Add(this.emitter); 


        }

      

        private void timer_Tick(object sender, EventArgs e)
        {
            label8.Text = emitter.particles.Count.ToString();

            emitter.UpdateState(); // каждый тик обновляем систему



            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(colorDialog3.Color);
                
                emitter.Render(g); // рендерим систему
            }

            picDisplay.Invalidate();
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value; 
            lblDirection.Visible = true;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void tbSpread_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbSpread.Value;
            label2.Visible = true;
            label2.Text = $"{tbSpread.Value} у.е";
        }

        private void tbFrequency_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = tbFrequency.Value;
            label5.Visible = true;
            label5.Text = $"{tbFrequency.Value} у.е";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            emitter.ColorFrom = colorDialog1.Color;
            button1.BackColor = colorDialog1.Color;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (colorDialog2.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            emitter.ColorTo = colorDialog2.Color;
            button2.BackColor = colorDialog2.Color;

        }

        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    emitter = new Emitter
                    {
                        Direction = 0,
                        SpeedMin = 1,
                        SpeedMax = 20,
                        X = 0,
                        Y = 0,
                        ColorFrom = Color.Gold,
                        ColorTo = Color.FromArgb(0, Color.Red),
                    };
                    break;

                case 1:
                    emitter = new Emitter
                    {
                        Direction = 90,
                        SpeedMin = 1,
                        SpeedMax = 30,
                        Spreading = 30,
                        ColorFrom = Color.Gold,
                        ColorTo = Color.FromArgb(0, Color.Red),
                        X = picDisplay.Width / 2,
                        Y = picDisplay.Height,
                    };
                    break;
                case 2:
                    emitter = new TopEmitter
                    {
                        Width = picDisplay.Width,
                        GravitationY = 0.25f
                    };
                    button1.BackColor = emitter.ColorFrom;
                    button2.BackColor = Color.Black;
                    break;
                case 3:
                    this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
                    {
                        Direction = 0,
                        Spreading = 10,
                        SpeedMin = 1,
                        SpeedMax = 30,
                        ColorFrom = Color.Gold,
                        ColorTo = Color.FromArgb(0, Color.Red),
                        ParticlesPerTick = 10,
                        X = picDisplay.Width / 2,
                        Y = picDisplay.Height / 2,
                    };
                    break;
               
                case 4:
                    emitter = new Emitter
                    {
                        Direction = 90,
                        SpeedMin = 1,
                        SpeedMax = 20,
                        Spreading = 30,
                        ColorFrom = Color.Gold,
                        ColorTo = Color.FromArgb(0, Color.Red),

                    };
                    break;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            button4.BackColor = colorDialog3.Color;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog4.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            button5.BackColor = colorDialog4.Color;
            this.BackColor = colorDialog4.Color; 

        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                emitter.impactPoints.Add(new IImpactPoint
                {
                    X = e.X,
                    Y = e.Y,
                    Radius = 50
                });
            }
            if (e.Button == MouseButtons.Right)
            {
                int xMouse = e.X;
                int yMouse = e.Y;

                foreach (IImpactPoint point in emitter.impactPoints)
                {
                    
                    float gX = point.X - xMouse;
                    float gY = point.Y - yMouse;

                    double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до мышки
                    if (r < point.Radius / 2)
                    {
                        emitter.impactPoints.Remove(point);

                        break;
                    }
                   
                }
            }
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (comboBox2.SelectedIndex == 4)
            {
                emitter.X = e.X;
                emitter.Y = e.Y;
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнил: Колосов Вячеслав Германович \n Группа: ИСТб 19-1 \n Вариант: №6 \n Версия: v1 \n Дата: 15.12.2020");
            
        }
    }
}

