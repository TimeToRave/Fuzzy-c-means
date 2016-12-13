using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_mean_interface
{
    class cmean
    {
        private int pointsCount, clustersCount;


        public double[,] Run(ref Point [] pointSet, ref Point [] centres, double m, bool flag)
        {
            pointsCount = pointSet.Length;
            clustersCount = 3;
                           
            double[,] uMatrix = new double[pointsCount, clustersCount];

            uMatrix =  GetuMatrix();

            double u = 0;

            double previousDecisionValue = 0;
            double currentDecisionValue = 1;

            for (int i = 0; i < 1000 && Math.Abs(previousDecisionValue - currentDecisionValue) > 0.0001; i++)
            {
                previousDecisionValue = currentDecisionValue;
                if (flag == false)
                {
                    centres = calcCentres(uMatrix, m, pointSet);
                }
                for(int iPoint = 0; iPoint < pointsCount; iPoint ++)
                {
                    for( int iCluster = 0; iCluster < clustersCount; iCluster++)
                    {
                        double distance = calcDistance(pointSet[iPoint], centres[iCluster]);
                        uMatrix[iPoint,iCluster] = Math.Pow(1 / distance, 2 / (m - 1));
                    }
                    uMatrix = normalizeUMatrixRow(uMatrix, iPoint);
                }


                double sum = 0;
                for (int iPoint = 0; iPoint < uMatrix.GetLength(0); iPoint++)
                {
                    for (int iCluster = 0; iCluster < clustersCount; iCluster++)
                    {
                        sum += uMatrix[iPoint, iCluster] * calcDistance(centres[iCluster], pointSet[iPoint]);
                    }
                }
                currentDecisionValue = sum;
           
                
            }


            return uMatrix;
        }

        Random rand = new Random(Guid.NewGuid().GetHashCode());
        public double[,] GetuMatrix()
        {
            double[,] uMatrix = new double[pointsCount, clustersCount];
            
            for (int i = 0; i < uMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < uMatrix.GetLength(1); j++)
                {
                    uMatrix[i, j] = Convert.ToDouble(rand.Next(100)) / 100;
                }
                uMatrix = normalizeUMatrixRow(uMatrix, i);
            }

            return uMatrix;

        }

        public Point [] calcCentres(double [,] uMatrix, double m, Point[] points)
        {

            Point[] centroids = new Point[clustersCount];

            for( int i = 0; i < clustersCount; i++)
            {
                double tempXa = 0;
                double tempXb = 0;
                double tempYa = 0;
                double tempYb = 0;

                for(int j = 0; j < pointsCount; j++)
                {
                    tempXa += Math.Pow(uMatrix[j, i], m);
                    tempXb += Math.Pow(uMatrix[j, i], m) * points[j].GetX();
                    tempYa += Math.Pow(uMatrix[j, i], m);
                    tempYb += Math.Pow(uMatrix[j, i], m) * points[j].GetY();
                }

                centroids[i] = new Point(tempXb / tempXa, tempYb / tempYa);
            }
            return centroids;
        }


        public double[,] normalizeUMatrixRow(double[,] uMatrix, int row)
        {
            double sum = 0;
            for (int i = 0; i < uMatrix.GetLength(1); i++)
            {
                sum += uMatrix[row, i];
            }

            for (int i = 0; i < uMatrix.GetLength(1); i++)
            {
                uMatrix[row, i] = uMatrix[row, i] / sum;
            }

            return uMatrix;
        }

        public double calcDistance (Point point1, Point point2)
        {
            double distance1 = Math.Pow((point1.GetX() - point2.GetX()), 2);
            double distance2 = Math.Pow((point1.GetY() - point2.GetY()), 2);
            return Math.Sqrt(distance1 + distance2);
        }
    }
}
