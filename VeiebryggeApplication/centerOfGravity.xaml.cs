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
using System.IO;

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for centerOfGravity.xaml
    /// </summary>
    public partial class centerOfGravity : Page
    {
        public centerOfGravity()
        {
            InitializeComponent();
        }

        //constant
        const double g = 9.81f;

        //for x-axis
        private void Button_ClickX(object sender, RoutedEventArgs e)
        {
            // Read variables from UI
            double x_A = double.Parse(textBoxXA.Text);
            double m_A = double.Parse(textBoxMA.Text);
            double x_B = double.Parse(textBoxXB.Text);
            double m_B = double.Parse(textBoxMB.Text);
            double x_C = double.Parse(textBoxXC.Text);
            double m_C = double.Parse(textBoxMC.Text);
            double m_Vehicle = double.Parse(textBoxMVehicle.Text);

            // Calculate x_Vehicle
            double x_Vehicle = calculate_X_Vehicle(x_A, m_A, x_B, m_B, x_C, m_C, m_Vehicle);
            // Show results in UI
            textBoxXVehicle.Text = x_Vehicle.ToString("0.000");
        }


        //for y-axis
        private void Button_ClickY(object sender, RoutedEventArgs e)
        {
            // Read variables from UI
            double y_A = double.Parse(textBoxYA.Text);
            double m_A = double.Parse(textBoxMA.Text);
            double y_B = double.Parse(textBoxYB.Text);
            double m_B = double.Parse(textBoxMB.Text);
            double m_Vehicle = double.Parse(textBoxMVehicle.Text);
            // Calculate y_Vehicle
            double y_Vehicle = calculate_Y_Vehicle(y_A, m_A, y_B, m_B, m_Vehicle);
            // Show results in UI
            textBoxYVehicle.Text = y_Vehicle.ToString("0.000");
        }

        //for z-axis
        private void Button_ClickZ(object sender, RoutedEventArgs e)
        {
            // Read variables from UI
            double y_A = double.Parse(textBoxYA.Text);
            double m_A = double.Parse(textBoxMA.Text);
            double y_B = double.Parse(textBoxYB.Text);
            double m_B = double.Parse(textBoxMB.Text);
            double alpha = double.Parse(textBoxAlpha.Text) * (Math.PI/180);
            double m_Vehicle = double.Parse(textBoxMVehicle.Text);
            double y_Vehiclee = double.Parse(textBoxYVehicle_value.Text);
            // Calculate z_Vehicle
            double z_Vehicle = calculate_Z_Vehicle(y_A, m_A, y_B, m_B, alpha, m_Vehicle, y_Vehiclee);
            // Show results in UI
            textBoxZVehicle.Text = z_Vehicle.ToString("0.000");
        }


        //for x-axis
        private double calculate_X_Vehicle(double x_A, double m_A, double x_B, double m_B, double x_C, double m_C, double m_Vehicle)
        {
            // Calculate numerator term by term
            double term1 = m_A * g * x_A;
            double term2 = m_B * g * x_B;
            double term3 = m_C * g * x_C;
            double numerator = term1 + term2 + term3;
            // Calculate denominator
            double denominator = m_Vehicle * g;
            // Check denominator
            if (denominator == 0)
                return double.NaN;
            // Calculate result by dividing the above numerator by denominator
            double x_Vehicle = numerator / denominator;
            // Return result
            return x_Vehicle;
        }


        //for y-axis
        private double calculate_Y_Vehicle(double y_A, double m_A, double y_B, double m_B, double m_Vehicle)
        {
            // Calculate numerator term by term
            double term1 = m_A * g * y_A;
            double term2 = m_B * g * y_B;
            double numerator = term1 + term2;
            // Calculate denominator
            double denominator = m_Vehicle * g;
            // Check denominator
            if (denominator == 0)
                return double.NaN;
            // Calculate result by dividing the above numerator by denominator
            double y_Vehicle = numerator / denominator;
            // Return result
            return y_Vehicle;
        }

        //for z-axis
        private double calculate_Z_Vehicle(double y_A, double m_A, double y_B, double m_B, double alpha, double m_Vehicle, double y_Vehiclee)
        {
            // Calculate numerator term by term
            double term1 = m_A * g * y_A;
            double term2 = m_B * g * y_B;
            double term3 = m_Vehicle * g * Math.Cos(alpha) * y_Vehiclee;
            double numerator = term1 + term2 - (term3);
            // Calculate denominator
            double denominator = m_Vehicle * g * Math.Sin(alpha);
            // Check denominator
            if (denominator == 0)
                return double.NaN;
            // Calculate result by dividing the above numerator by denominator
            double z_Vehicle = numerator / denominator;
            // Return result
            return z_Vehicle;
        }
    }
}
