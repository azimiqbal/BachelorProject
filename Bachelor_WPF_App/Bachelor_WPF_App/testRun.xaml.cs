using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bachelor_WPF_App
{
    /// <summary>
    /// Interaction logic for testRun.xaml
    /// </summary>
    public partial class testRun : Page
    {
        public testRun()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox.Content = "Checked";
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox.Content = "Unchecked";
        }

        private void MrCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MrCheckBox.Content = "Checked";
        }
        private void MrCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MrCheckBox.Content = "Unchecked";
        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("RANA ASLAM");
        }
    }
}

