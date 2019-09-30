using I7BAC.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

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
            billedeListe = (BilledeListe) FindResource("BilledeListe");
 
            Aggregator.OnMessageTransmitted += OnMessageReceived;
        }

        private void ButtonHentPatient_OnClick(object sender, RoutedEventArgs e)
        {
            HentPatient hentPatient = new HentPatient();
          
            hentPatient.Show();
        }

        private void OnMessageReceived(string patientId, string rekvisitionsNr, string dato, BilledeListe billeder, string sum)
        {
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

            this.patientId.Text =  patientId;
            this.rekvisitionsnr.Text = rekvisitionsNr;
            this.dato.Text = dato;
            this.sum.Text = sum;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox) sender;
            var selectedBillede = (Billede) listBox.SelectedValue;

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