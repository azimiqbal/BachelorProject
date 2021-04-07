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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a three-dimensional array, first value is row, seccond is column and third is nr of blocks
            string[,,] Arr3d = new string[3, 2, 1];
            //initializing array
            Arr3d[0, 0, 0] = "X1 ";
            Arr3d[1, 0, 0] = "Y1 ";
            Arr3d[2, 0, 0] = "Z1 ";

            Arr3d[0, 1, 0] = "X2 ";
            Arr3d[1, 1, 0] = "Y2 ";
            Arr3d[2, 1, 0] = "Z2 ";

            // Loop over each dimension's length.
            for (int i = 0; i < Arr3d.GetLength(2); i++)
            {
                for (int y = 0; y < Arr3d.GetLength(1); y++)
                {
                    for (int x = 0; x < Arr3d.GetLength(0); x++)

                    {
                        Console.Write(Arr3d[x, y, i]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
