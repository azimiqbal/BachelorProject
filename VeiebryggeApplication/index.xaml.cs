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
using System.Diagnostics;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Data;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfDocument = PdfSharp.Pdf.PdfDocument;
using PdfPage = PdfSharp.Pdf.PdfPage;


namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for index.xaml
    /// </summary>
    public partial class index : Page
    {
        public index()
        {
            InitializeComponent();
        }

        private void BtnMenu1(object sender, RoutedEventArgs e)
        {
            Main.Content = new RunTest();
        }
        private void BtnMenu2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Tests();
        }
    }
}