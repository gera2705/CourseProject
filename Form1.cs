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
        Emitter emitter; // добавим поле для эмиттера

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            
            colorDialog1.FullOpen = true;
            colorDialog2.FullOpen = true;

          

            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            // привязал изображение
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            // гравитон
            //emitter.impactPoints.Add(new GravityPoint
            //{
            //    X = (float)(picDisplay.Width * 0.25),
            //    Y = picDisplay.Height / 2
            //});

            //emitter.impactPoints.Add(new GravityPoint
            //{
            //    X = picDisplay.Width / 2,
            //    Y = picDisplay.Height / 2
            //});

            //// снова гравитон
            //emitter.impactPoints.Add(new GravityPoint
            //{
            //    X = (float)(picDisplay.Width * 0.75),
            //    Y = picDisplay.Height / 2
            //});

            //this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            //{
            //    Direction = 0,
            //    Spreading = 10,
            //    SpeedMin = 10,
            //    SpeedMax = 10,
            //    ColorFrom = Color.Gold,
            //    ColorTo = Color.FromArgb(0, Color.Red),
            //    ParticlesPerTick = 10,
            //    X = picDisplay.Width / 2,
            //    Y = picDisplay.Height / 2,
            //};





            emitters.Add(this.emitter); // все равно добавляю в список emitters, чтобы он рендерился и обновлялся


        }

      

        private void timer_Tick(object sender, EventArgs e)
        {
            label8.Text = emitter.particles.Count.ToString();

            emitter.UpdateState(); // каждый тик обновляем систему

            using (var g = Graphics.FromImage(picDisplay.Image))
            {

                if (comboBox1.SelectedIndex == 0)
                {
                    g.Clear(Color.Black);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    g.Clear(Color.White);
                }

                emitter.Render(g); // рендерим систему
            }

            picDisplay.Invalidate();
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value; // направлению эмиттера присваиваем значение ползунка 
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void tbSpread_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbSpread.Value;
        }

        private void tbFrequency_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = tbFrequency.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            emitter.ColorFrom = colorDialog1.Color;
           // button1.BackColor = colorDialog1.Color;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (colorDialog2.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            emitter.ColorTo = colorDialog2.Color;
            //button2.BackColor = colorDialog2.Color;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
                    {
                        Direction = 0,
                        Spreading = 10,
                        SpeedMin = 10,
                        SpeedMax = 10,
                        ColorFrom = Color.Gold,
                        ColorTo = Color.FromArgb(0, Color.Red),
                        ParticlesPerTick = 10,
                        X = picDisplay.Width / 2,
                        Y = picDisplay.Height / 2,
                    };
                    break;
                case 1:
                    emitter = new Emitter
                    {
                        Direction = 90,
                        SpeedMin = 10,
                        SpeedMax = 20,
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

                    //emitter.impactPoints.Add(new GravityPoint
                    //{
                    //    X = (float)(picDisplay.Width * 0.25),
                    //    Y = picDisplay.Height / 2
                    //});

                    //emitter.impactPoints.Add(new GravityPoint
                    //{
                    //    X = picDisplay.Width / 2,
                    //    Y = picDisplay.Height / 2
                    //});

                    //// снова гравитон
                    //emitter.impactPoints.Add(new GravityPoint
                    //{
                    //    X = (float)(picDisplay.Width * 0.75),
                    //    Y = picDisplay.Height / 2
                    //});

                    break;
            }
        }
    }
}

