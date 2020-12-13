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
        // собственно список, пока пустой
        List<Particle> particles = new List<Particle>();
        Emitter emitter = new Emitter(); // добавили эмиттер

        public Form1()
        {
            InitializeComponent();

            // привязал изображение
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new TopEmitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
            };
        }

        //int counter = 0; // добавлю счетчик чтобы считать вызовы функции

        private void timer_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            //counter++; // увеличиваю значение счетчика каждый вызов
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black); // добавил очистку

                emitter.Render(g);
            }

            // обновить picDisplay
            picDisplay.Invalidate();

        }

       
    }
}
