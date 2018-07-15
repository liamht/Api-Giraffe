using System;
using System.Windows;

namespace APIGiraffe.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
