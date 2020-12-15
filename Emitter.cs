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
        public List<Particle> particles = new List<Particle>();

        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();

        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 0; // разброс частиц относительно Direction
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 30; // начальная максимальная скорость движения частицы
        public int RadiusMin = 2; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 100; // максимальное время жизни частицы

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public int ParticlesCount = 500; //макс кол-во заспауненых частиц
        public int ParticlesPerTick = 10; // кол-во частиц в секунду

        public float GravitationX = 0;
        public float GravitationY = 1; // пусть гравитация будет силой один пиксель за такт, нам хватит

        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.FromColor = ColorFrom;
            particle.ToColor = ColorTo;

            return particle;
        }

        //метод обновления состояния
        public void UpdateState()
        {
            int particlesToCreate = ParticlesPerTick; 

            foreach (var particle in particles)
            {
              
                if (particle.Life <= 0)
                {
                    if (particlesToCreate > 0)
                    {
                        particlesToCreate -= 1; 
                        ResetParticle(particle);
                    }
                }
                else
                {
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;

                    particle.Life -= 1;
                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }
                   
                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;
                }
            }

            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }


        }

        // функция рендеринга
        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }

            foreach (var point in impactPoints) 
            {    
                point.Render(g); 
            }
        }

        public virtual void ResetParticle(Particle particle)
        {
            var partColor = particle as ParticleColorful;
            partColor.FromColor = ColorFrom;
            partColor.ToColor = ColorTo;

            particle.Life = Particle.rand.Next(LifeMin, LifeMax);

            particle.X = X ;
            particle.Y = Y ;

            var direction = Direction
                 + (double)Particle.rand.Next(Spreading)
                 - Spreading / 2;

            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);
        }

    }

    public class TopEmitter : Emitter //эмиттер "снега"
    {
        public int Width; 

        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle); 

            particle.X = Particle.rand.Next(Width); 
            particle.Y = 0; 

            particle.SpeedY = 1; 
            particle.SpeedX = Particle.rand.Next(-2, 2); 
        }
    }

}
