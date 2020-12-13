using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CousreProjectKolosov
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;

        public float GravitationX = 0;
        public float GravitationY = 1; // пусть гравитация будет силой один пиксель за такт, нам хватит

        public int ParticlesCount = 1000;

        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.Life -= 1; // уменьшаю здоровье

                // если здоровье кончилось
                if (particle.Life < 0)
                {
                    ResetParticle(particle);
                    
                }
                else
                {
                    // гравитация воздействует на вектор скорости, поэтому пересчитываем его
                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;
                    
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                }
            }

            // добавил генерацию частиц
            // генерирую не более 10 штук за тик
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < ParticlesCount) // пока частиц меньше 500 генерируем новые
                {
                    var particle = new ParticleColorful();
                    particle.setColor(Color.White, Color.FromArgb(0, Color.Black));
                    //particle.FromColor = Color.White;
                    //particle.ToColor = Color.FromArgb(0, Color.Black);

                    ResetParticle(particle); // добавили вызов ResetParticle

                    particles.Add(particle);
                }
                else
                {
                    break; // а если частиц уже 500 штук, то ничего не генерирую
                }
            }
        }

        public void Render(Graphics g)
        {

            g.DrawEllipse(
          new Pen(Color.Red, 5)
          , 10, 100, 50, 50
          );

            g.DrawEllipse(
          new Pen(Color.Orange, 5)
          , 80, 120, 50, 50
          );

            g.DrawEllipse(
          new Pen(Color.Yellow, 5)
          , 150, 140, 50, 50
          );

            g.DrawEllipse(
          new Pen(Color.Green, 5)
          , 220, 160, 50, 50
          );

            g.DrawEllipse(
         new Pen(Color.Aqua, 5)
         , 290, 140, 50, 50
         );

            g.DrawEllipse(
         new Pen(Color.Blue , 5)
         , 360, 120, 50, 50
         );

            g.DrawEllipse(
        new Pen(Color.DarkMagenta, 5)
        , 430, 100, 50, 50
        );





            int count = 0;
            // ну тут так и быть уж сам впишу...
            // это то же самое что на форме в методе Render
            foreach (var particle in particles)
            {
                count++;
               
                particle.Draw(g , 0);

                //float gX = 10 - particle.X;
                //float gY = 100 - particle.Y;

                //double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
                if (particle.X > 9 && particle.X < 61 && particle.Y > 100 ) // если частица оказалось внутри квадрата ПОФИКСИТЬ!
                {
                    particle.Draw(g , 1);
                   
                }

                if (particle.X > 79 && particle.X < 131 && particle.Y > 120) // если частица оказалось внутри квадрата ПОФИКСИТЬ!
                {
                    particle.Draw(g, 2);
                }

                if (particle.X > 149 && particle.X < 201 && particle.Y > 140) // если частица оказалось внутри квадрата ПОФИКСИТЬ!
                {
                    particle.Draw(g, 3);
                }

                if (particle.X > 219 && particle.X < 271 && particle.Y > 160) // если частица оказалось внутри квадрата ПОФИКСИТЬ!
                {
                    particle.Draw(g, 4);
                }

                if (particle.X > 289 && particle.X < 339 && particle.Y > 140) // если частица оказалось внутри квадрата ПОФИКСИТЬ!
                {
                    particle.Draw(g, 5);
                }

                if (particle.X > 359 && particle.X < 409 && particle.Y > 120) // если частица оказалось внутри квадрата ПОФИКСИТЬ!
                {
                    particle.Draw(g, 6);
                }

                if (particle.X > 429 && particle.X < 479 && particle.Y > 100) // если частица оказалось внутри квадрата ПОФИКСИТЬ!
                {
                    particle.Draw(g, 7);
                }






                //      g.DrawEllipse(
                //new Pen(Color.Red)
                //, 0, 0, 50, 50
                //);
                //var particl = new ParticleColorful();
                //particl.FromColor = Color.Red;
                //particl.ToColor = Color.FromArgb(0, Color.Red);


                //ResetParticle(particl); // добавили вызов ResetParticle

                ////particles.Add(particl);




            }


        }




        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 20 + Particle.rand.Next(100);
            particle.X = MousePositionX;
            particle.Y = MousePositionY;

            var direction = (double)Particle.rand.Next(360);
            var speed = 1 + Particle.rand.Next(10);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = 2 + Particle.rand.Next(10);

        }

    }

    public class TopEmitter : Emitter
    {
        public int Width;

        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle);

            // а теперь тут уже подкручиваем параметры движения
            particle.X = Particle.rand.Next(Width); // позиция X -- произвольная точка от 0 до Width
            particle.Y = 0;  // ноль -- это верх экрана 

            particle.SpeedY = 1; // падаем вниз по умолчанию
            particle.SpeedX = Particle.rand.Next(-2, 2); // разброс влево и вправа у частиц 
        }
    }
}
