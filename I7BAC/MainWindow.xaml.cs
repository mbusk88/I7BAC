using I7BAC.dto;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using I7BAC.MachineLearning;
using System.Threading.Tasks;

namespace I7BAC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Billede b;
        private Image image;
        private BilledeListe billedeListe;
        public MainWindow()
        {
            InitializeComponent();
            billedeListe = (BilledeListe)FindResource("BilledeListe");

            Aggregator.OnMessageTransmitted += OnMessageReceived;
            Aggregator.OnPythonResultTransmitted += OnPythonResultReceived;
        }

        private void OnPythonResultReceived(string result)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.sum.Text = result;
            });
        }

        private void ButtonHentPatient_OnClick(object sender, RoutedEventArgs e)
        {
            HentPatient hentPatient = new HentPatient();

            hentPatient.Show();
        }

        private void OnMessageReceived(string patientId, string rekvisitionsNr, string dato, BilledeListe billeder)
        {
            this.sum.Text = "Behandler...";
            Task.Run(() => PythonCaller.CallScript(billeder[0].URL));


            image = ImageBiopsi;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(billeder[0].URL);
            bi.EndInit();
            image.Source = bi;

            if (billedeListe.Any())
            {
                billedeListe.Clear();
            }

            foreach (var billede in billeder)
            {
                billedeListe.Add(billede);
            }

            this.patientId.Text = patientId;
            this.rekvisitionsnr.Text = rekvisitionsNr;
            this.dato.Text = dato;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;
            var selectedBillede = (Billede)listBox.SelectedValue;

            this.sum.Text = "Behandler...";
            Task.Run(() => PythonCaller.CallScript(selectedBillede.URL));

            if (selectedBillede != null)
            {
                image = ImageBiopsi;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(selectedBillede.URL);
                bi.EndInit();
                image.Source = bi;
            }
        }
    }
}