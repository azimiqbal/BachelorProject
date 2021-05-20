using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Microsoft.Win32;
using Emgu.CV.Util;

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for image_processing.xaml
    /// </summary>
    public partial class image_processing : Page
    {

        public image_processing()
        {
            InitializeComponent();

        }


        private static Image<Bgr, byte> detectCirclee(Image<Bgr, byte> image)
        {
            Image<Gray, byte> imageGrey = image.Convert<Gray, byte>();

            double cannyThreshold = 180.0;
            double circleAccumulatorThreshold = 120;
            CircleF[] circles = CvInvoke.HoughCircles(imageGrey, HoughModes.Gradient,
            2.0, 20.0, cannyThreshold,
            circleAccumulatorThreshold, 5);

            //canny and edge detection
            double cannyThresholdLinking = 120.0;
            UMat cannyEdges = new UMat();
            CvInvoke.Canny(imageGrey, cannyEdges, cannyThreshold, cannyThresholdLinking);

            LineSegment2D[] lines = CvInvoke.HoughLinesP(cannyEdges, 1, Math.PI / 45.0,
            20, 30, 10);
            Image<Bgr, byte> circleImage = image.CopyBlank();
            foreach (CircleF circle in circles)
                circleImage.Draw(circle, new Bgr(255, 0, 0), 5);

            return circleImage;
        }


        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openPic = new Microsoft.Win32.OpenFileDialog();
            if (openPic.ShowDialog() == true)
            {
                Image<Bgr, byte> gambar = new Image<Bgr, byte>(openPic.FileName);
                //originalImage.Source = Emgu.CV.WPF.BitmapSourceConvert.ToBitmapSource(gambar);
       
                Image<Bgr, byte> bunder = detectCirclee(gambar);
                //lingkaranImage.Source = Emgu.CV.WPF.BitmapSourceConvert.ToBitmapSource(bunder);



            }

        }

    }
}
