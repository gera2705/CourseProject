using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CousreProjectKolosov
{
    public  class IImpactPoint
    {
        public float X; // ну точка же, вот и две координаты
        public float Y;
        public int Radius;
        public int count = 0;

        public void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY);
            if (r + particle.Radius < Radius / 2)
            {
                count++;
                particle.Life = 0;

            }
        }

   
        public void Render(Graphics g)
        {
            g.DrawEllipse(
                    new Pen(Color.Red , 3),
                    X - Radius / 2,
                    Y - Radius / 2,
                    Radius,
                    Radius

                );

            var stringFormat = new StringFormat(); 
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center; 

            g.DrawString(
             $"{count}", 
             new Font("Verdana", 10), 
             new SolidBrush(Color.Blue), 
             X, 
             Y,
             stringFormat
         );
        }

    }



}
