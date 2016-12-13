using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_mean
{
    class Point
    {
        private double x;
        private double y;

        public Point()
        {
            this.x = 0;
            this.y = 0;
        }

        public void RandomPoint(int down, int up)
        {
            Random rand = new Random();

            this.x = Convert.ToDouble(rand.Next(down, up));
            this.y = Convert.ToDouble(rand.Next(down, up));

        }

        public Point(double _x, double _y)
        {
            this.x = _x;
            this.y = _y;
        }

        public double GetX()
        {
            return this.x;
        }

        public void SetX(double _x)
        {
            this.x = _x;
        }

        public double GetY()
        {
            return this.y;
        }

        public void Sety(double _y)
        {
            this.y = _y;
        }
        public void PrintPoint()
        {
            Console.WriteLine(this.x + "   " + this.y);
        }
    }
}
