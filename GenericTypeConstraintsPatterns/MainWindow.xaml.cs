using GenericTypeConstraintsPatterns.Entity;
using GenericTypeConstraintsPatterns.ViewModel;
using System.Windows;

namespace GenericTypeConstraintsPatterns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        MainViewModel viewmodel = new();
       

        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}