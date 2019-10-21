using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using I7BAC.MachineLearning;
using System.Threading.Tasks;
using I7BAC.Model;
using I7BAC.Viewmodel;

namespace I7BAC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = (MainWindowViewModel)FindResource("MainWindowViewModel");
            mainWindowViewModel.Init();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainWindowViewModel.ImageChanged.Execute(sender);
        }
    }
}