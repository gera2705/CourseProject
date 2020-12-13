﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CousreProjectKolosov
{
    public class Particle
    {
        public int Radius; // радуис частицы
        public float X; // X координата положения частицы в пространстве
        public float Y; // Y координата положения частицы в пространстве

        public float SpeedX; // скорость перемещения по оси X
        public float SpeedY; // скорость перемещения по оси Y

        public float Life; // запас здоровья частицы

        // добавили генератор случайных чисел
        public static Random rand = new Random();

        // конструктор по умолчанию будет создавать кастомную частицу
        public Particle()
        {
            // генерируем произвольное направление и скорость
            var direction = (double)rand.Next(360);
            var speed = 1 + rand.Next(10);

            // рассчитываем вектор скорости
            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            // а это не трогаем
            Radius = 2 + rand.Next(10);
            Life = 20 + rand.Next(100);
        }
        

        public virtual void Draw(Graphics g , int colNum)
        {
            // рассчитываем коэффициент прозрачности по шкале от 0 до 1.0
            float k = Math.Min(1f, Life / 100);
            // рассчитываем значение альфа канала в шкале от 0 до 255
            // по аналогии с RGB, он используется для задания прозрачности
            int alpha = (int)(k * 255);

            // создаем цвет из уже существующего, но привязываем к нему еще и значение альфа канала


            
                var color = Color.FromArgb(alpha, Color.Black);
                var b = new SolidBrush(color);
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
                b.Dispose();
           

            // нарисовали залитый кружок радиусом Radius с центром в X, Y
            //g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            // удалили кисть из памяти, вообще сборщик мусора рано или поздно это сам сделает
            // но документация рекомендует делать это самому
            //b.Dispose();
        }

        
    }

    public class ParticleColorful : Particle
    {
        // два новых поля под цвет начальный и конечный
        public Color FromColor;
        public Color ToColor;


        public void setColor(Color FromColor , Color ToColor)
        {
            this.FromColor = FromColor;
            this.ToColor = ToColor;

        }

        // для смеси цветов
        public static Color MixColor(Color color1, Color color2, float k)
        {
            return Color.FromArgb(
                (int)(color2.A * k + color1.A * (1 - k)),
                (int)(color2.R * k + color1.R * (1 - k)),
                (int)(color2.G * k + color1.G * (1 - k)),
                (int)(color2.B * k + color1.B * (1 - k))
            );
        }

        // ну и отрисовку перепишем
        public override void Draw(Graphics g , int Colnum)
        {
            float k = Math.Min(1f, Life / 100);
            var color = MixColor(ToColor, FromColor, k); 
            switch (Colnum)
            {
                case 1: 
                    color = MixColor(Color.Red, Color.Red, k);
                    break;
                case 2:
                    color = MixColor(Color.Orange, Color.Orange, k);
                    break;
                case 3:
                    color = MixColor(Color.Yellow, Color.Yellow, k);
                    break;
                case 4:
                    color = MixColor(Color.Green, Color.Green, k);
                    break;
                case 5:
                    color = MixColor(Color.Aqua, Color.Aqua, k);
                    break;
                case 6:
                    color = MixColor(Color.Blue, Color.Blue, k);
                    break;
                case 7:
                    color = MixColor(Color.DarkMagenta, Color.DarkMagenta, k);
                    break;

            }
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
            //так как k уменьшается от 1 до 0, то порядок цветов обратный
            //if (Colnum == 1)
            //{
            //    var color = MixColor(Color.Red, Color.Red, k);
            //    var b = new SolidBrush(color);
            //    g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            //    b.Dispose();
            //}
            //else
            //{
            //    var color = MixColor(ToColor, FromColor, k);
            //    var b = new SolidBrush(color);

            //    g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            //    b.Dispose();
            //}



        }
    }
}