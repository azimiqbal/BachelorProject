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

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for rolloverAngle.xaml
    /// </summary>
    public partial class rolloverAngle : Page
    {
        public rolloverAngle()
        {
            InitializeComponent();
        }


        private void rolloverAngle_click(object sender, RoutedEventArgs e)
        {
            // Read variables from UI
            double p = double.Parse(textBoxP.Text);
            double y = double.Parse(textBoxY.Text);
            double z = double.Parse(textBoxZ.Text);
            double h = double.Parse(textBoxH.Text);
            double alpha = double.Parse(textBoxAlpha.Text)*(Math.PI/180);

            // Calculate rolloverAngle
            double rolloverAngle = calculate_rolloverAngle(p, y, z, h, alpha);
            // Show results in UI
            textBoxRolloverAngle.Text = rolloverAngle.ToString("0.000");
        }


        private double calculate_rolloverAngle(double p, double y, double z, double h, double alpha)
        {
            // Calculate the different steps
            //step 1
            double y_power = p - y;
            double z_power = z - h;
            //step 2
            double r = Math.Sqrt(Math.Pow(z_power,2) + Math.Pow(y_power, 2));
            //step 3
            double C = (2*r)*Math.Sin(alpha/2);
            //step 4
            double yellow = Math.Acos(z_power/r);
            //step 5
            double alpha2 = Math.Sin(20 * (Math.PI / 180));
            double lightgreen2 = Math.Sqrt(2) * alpha2;
            double lightgreen1 = lightgreen2 / 0.49;
            double lightgreen = Math.Asin(lightgreen1);

            //step 6
            double orange = (lightgreen - yellow);

            //step 7
            double delta_y = C * Math.Sin(orange);

            //step 8
            double a = C * Math.Cos(orange);

            //final step
            double rolloverAngle = Math.Atan((p-(y+delta_y))/(z-a))*(180 / Math.PI);
            // Return result
            return rolloverAngle;
        }
    }
}
