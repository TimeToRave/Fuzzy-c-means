using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace c_mean_interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Center1XTB.IsEnabled = false;
            Center2XTB.IsEnabled = false;
            Center3XTB.IsEnabled = false;
            Center1YTB.IsEnabled = false;
            Center2YTB.IsEnabled = false;
            Center3YTB.IsEnabled = false;
            label1.Opacity = 0;
            label2.Opacity = 0;
            label3.Opacity = 0;
            label4.Opacity = 0;
            label5.Opacity = 0;
            Center1XTB.Opacity = 20;
            Center2XTB.Opacity = 20;
            Center3XTB.Opacity = 20;
            Center1YTB.Opacity = 20;
            Center2YTB.Opacity = 20;
            Center3YTB.Opacity = 20;


        }
        Point[] pointSet;

        private void GeneratePointsButton_Click(object sender, RoutedEventArgs e)
        {
            pointSet = new Point[Convert.ToInt32(PointsCountTB.Text)];
            for (int i = 0; i < Convert.ToInt32(PointsCountTB.Text); i++)
            {
                pointSet[i] = new Point();

                pointSet[i].RandomPoint(1, 255);
            }

            WriteableBitmap wb = new WriteableBitmap(300,300, 96, 96, PixelFormats.Bgra32, null);

            Int32Rect rect = new Int32Rect(0, 0, 300, 300);

            byte[] pixels = new byte[300 * 300 * wb.Format.BitsPerPixel / 8];

            for (int i = 0; i < pointSet.Length; i++)
            {
                
                int alpha = 255;
                int red = 0;
                int green = 0;
                int blue = 0;

                int x = Convert.ToInt32(pointSet[i].GetX());
                int y = Convert.ToInt32(pointSet[i].GetY());

                int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;

                pixels[pixelOffset - 4] = (byte)blue;
                pixels[pixelOffset - 3] = (byte)green;
                pixels[pixelOffset - 2] = (byte)red;
                pixels[pixelOffset - 1] = (byte)alpha;

                pixels[pixelOffset + 4] = (byte)blue;
                pixels[pixelOffset + 5] = (byte)green;
                pixels[pixelOffset + 6] = (byte)red;
                pixels[pixelOffset + 7] = (byte)alpha;

                pixelOffset = (x - 300 + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;

                pixels[pixelOffset - 4] = (byte)blue;
                pixels[pixelOffset - 3] = (byte)green;
                pixels[pixelOffset - 2] = (byte)red;
                pixels[pixelOffset - 1] = (byte)alpha;

                pixels[pixelOffset + 4] = (byte)blue;
                pixels[pixelOffset + 5] = (byte)green;
                pixels[pixelOffset + 6] = (byte)red;
                pixels[pixelOffset + 7] = (byte)alpha;

                pixelOffset = (x + 300 + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;

                pixels[pixelOffset - 4] = (byte)blue;
                pixels[pixelOffset - 3] = (byte)green;
                pixels[pixelOffset - 2] = (byte)red;
                pixels[pixelOffset - 1] = (byte)alpha;

                pixels[pixelOffset + 4] = (byte)blue;
                pixels[pixelOffset + 5] = (byte)green;
                pixels[pixelOffset + 6] = (byte)red;
                pixels[pixelOffset + 7] = (byte)alpha;


                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                wb.WritePixels(rect, pixels, stride, 0);
            }

            ClustersImage.Source = wb;

        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            cmean algorithm = new cmean();
            double m = 1.5;
            m = Convert.ToDouble(setMTB.Text);
            Point[] centres = new Point[3];
            if (CentresCheckBox.IsChecked == true)
            {
                centres[0] = new Point(Convert.ToInt32(Convert.ToDouble(Center1XTB.Text)), Convert.ToInt32(Convert.ToDouble(Center1YTB.Text)));
                centres[1] = new Point(Convert.ToInt32(Convert.ToDouble(Center2XTB.Text)), Convert.ToInt32(Convert.ToDouble(Center2YTB.Text)));
                centres[2] = new Point(Convert.ToInt32(Convert.ToDouble(Center3XTB.Text)), Convert.ToInt32(Convert.ToDouble(Center3YTB.Text)));
            }
            else
            {
                
                centres[0] = new Point();
                centres[0].RandomPoint(1, 255);
                centres[1] = new Point();
                centres[1].RandomPoint(1, 255);
                centres[2] = new Point();
                centres[2].RandomPoint(1, 255);
            }

            bool flag;
            if (calcCentresCB.IsChecked == true)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            double[,] uMatrix = algorithm.Run( ref pointSet, ref centres, m, flag);

            Center1XTB.Text = centres[0].GetX().ToString();
            Center1YTB.Text = centres[0].GetY().ToString();
            Center2XTB.Text = centres[1].GetX().ToString();
            Center2YTB.Text = centres[1].GetY().ToString();
            Center3XTB.Text = centres[2].GetX().ToString();
            Center3YTB.Text = centres[2].GetY().ToString();



            WriteableBitmap wb = new WriteableBitmap(300, 300, 96, 96, PixelFormats.Bgra32, null);

           
            Int32Rect rect = new Int32Rect(0, 0, 300, 300);

            byte[] pixels = new byte[300 * 300 * wb.Format.BitsPerPixel / 8];

            for (int i = 0; i < pointSet.Length; i++)
            {

                int alpha = 255;
                int red = 0;
                int green = 0;
                int blue = 0;

                int x = Convert.ToInt32(pointSet[i].GetX());
                int y = Convert.ToInt32(pointSet[i].GetY());

                if(uMatrix[i,0] >= uMatrix[i,1] && uMatrix[i, 0] >= uMatrix[i, 2])
                {
                    red = 255;
                }
                if (uMatrix[i, 1] >= uMatrix[i, 0] && uMatrix[i, 1] >= uMatrix[i, 2])
                {
                    blue = 255;
                }
                if (uMatrix[i, 2] >= uMatrix[i, 0] && uMatrix[i, 2] >= uMatrix[i, 1])
                {
                    green = 255;
                }
                int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;

                pixels[pixelOffset - 4] = (byte)blue;
                pixels[pixelOffset - 3] = (byte)green;
                pixels[pixelOffset - 2] = (byte)red;
                pixels[pixelOffset - 1] = (byte)alpha;

                pixels[pixelOffset + 4] = (byte)blue;
                pixels[pixelOffset + 5] = (byte)green;
                pixels[pixelOffset + 6] = (byte)red;
                pixels[pixelOffset + 7] = (byte)alpha;

                pixelOffset = (x - 300 + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;

                pixels[pixelOffset - 4] = (byte)blue;
                pixels[pixelOffset - 3] = (byte)green;
                pixels[pixelOffset - 2] = (byte)red;
                pixels[pixelOffset - 1] = (byte)alpha;

                pixels[pixelOffset + 4] = (byte)blue;
                pixels[pixelOffset + 5] = (byte)green;
                pixels[pixelOffset + 6] = (byte)red;
                pixels[pixelOffset + 7] = (byte)alpha;

                pixelOffset = (x + 300 + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                pixels[pixelOffset] = (byte)blue;
                pixels[pixelOffset + 1] = (byte)green;
                pixels[pixelOffset + 2] = (byte)red;
                pixels[pixelOffset + 3] = (byte)alpha;

                pixels[pixelOffset - 4] = (byte)blue;
                pixels[pixelOffset - 3] = (byte)green;
                pixels[pixelOffset - 2] = (byte)red;
                pixels[pixelOffset - 1] = (byte)alpha;

                pixels[pixelOffset + 4] = (byte)blue;
                pixels[pixelOffset + 5] = (byte)green;
                pixels[pixelOffset + 6] = (byte)red;
                pixels[pixelOffset + 7] = (byte)alpha;


                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                wb.WritePixels(rect, pixels, stride, 0);
            }

            ClustersImage.Source = wb;


        }

        private void CentresCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(CentresCheckBox.IsChecked == true)
            {
                Center1XTB.IsEnabled = true;
                Center2XTB.IsEnabled = true;
                Center3XTB.IsEnabled = true;
                Center1YTB.IsEnabled = true;
                Center2YTB.IsEnabled = true;
                Center3YTB.IsEnabled = true;
                Center1XTB.Opacity = 100;
                Center2XTB.Opacity = 100;
                Center3XTB.Opacity = 100;
                Center1YTB.Opacity = 100;
                Center2YTB.Opacity = 100;
                Center3YTB.Opacity = 100;
                label1.Opacity = 100;
                label2.Opacity = 100;
                label3.Opacity = 100;
                label4.Opacity = 100;
                label5.Opacity = 100;

            }
            else
            {
                Center1XTB.IsEnabled = false;
                Center2XTB.IsEnabled = false;
                Center3XTB.IsEnabled = false;
                Center1YTB.IsEnabled = false;
                Center2YTB.IsEnabled = false;
                Center3YTB.IsEnabled = false;
                label1.Opacity = 0;
                label2.Opacity = 0;
                label3.Opacity = 0;
                label4.Opacity = 0;
                label5.Opacity = 0;
                Center1XTB.Opacity = 20;
                Center2XTB.Opacity = 20;
                Center3XTB.Opacity = 20;
                Center1YTB.Opacity = 20;
                Center2YTB.Opacity = 20;
                Center3YTB.Opacity = 20;
            }

        }
    }
}
