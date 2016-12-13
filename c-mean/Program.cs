using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_mean
{
    class Program
    {
        static void Main(string[] args)
        {

            
            cmean algorithm = new cmean();
            Point[] pointSet = new Point[20];
            for (int i = 0; i < 20; i++)
            {
                pointSet[i] = new Point(1, 100);
            }
            double m = 1;
            double [,] uMatrix = algorithm.Run(pointSet, m);
            m = 1;
        }

    }
}
