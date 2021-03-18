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
using System.IO.Ports;

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for read_from_arduino.xaml
    /// </summary>
    public partial class read_from_arduino : Page
    {
        public read_from_arduino()
        {
            InitializeComponent();

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            SerialPort sp = new SerialPort();
            sp.BaudRate = 9600;
            sp.PortName = "COM4";
            sp.Open();
            while (true)
            {
                string data_rx = sp.ReadLine();
                Console.WriteLine(data_rx);
            }
        }
    }

}




