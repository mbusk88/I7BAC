using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using I7BAC.Command;
using I7BAC.MachineLearning;
using Prism.Mvvm;
using Prism.Commands;
using I7BAC.Model;

namespace I7BAC.Viewmodel
{
    public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {
        private Patient _patient = new Patient();
        private HentPatient _hentPatientWindow = new HentPatient();
        private Rekvisition _rekvisition = new Rekvisition();
        private Billede _billede = new Billede();
        private BilledeListe _billedeListe = new BilledeListe();
        private string _sum;
        private Image _image = new Image();
        private int _selectedImageIndex;
        public HentPatientCommand HentPatient { get; set; } = new HentPatientCommand();
        public ImageChangedCommand ImageChanged { get; set; } = new ImageChangedCommand();

        public void Init()
        {
            HentPatient.HentPatientAction += OpenHentPatientWindow;
            ImageChanged.ImageChangedAction += ImageSelectionChange;

            _rekvisition.Rekvisitionsnr = null;

            Aggregator.OnMessageTransmitted += OnMessageReceived;
            Aggregator.OnPythonResultTransmitted += OnPythonResultReceived;
        }

        public string PatientId
        {
            get => _patient.CPR;
            set
            {
                _patient.CPR = value;
                OnPropertyChanged();
            }
        }

        public string Rekvisitionsnr
        {
            get => _rekvisition.Rekvisitionsnr.ToString();
            set
            {
                _rekvisition.Rekvisitionsnr = new Guid(value);
                OnPropertyChanged();
            }
        }

        public string Dato
        {
            get => _rekvisition.Dato;
            set
            {
                _rekvisition.Dato = value;
                OnPropertyChanged();
            }
        }

        public string Kategori
        {
            get => _billede.Kategori;
            set
            {
                _billede.Kategori = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ImageSource
        {
            get => _image.Source;
            set
            {
                _image.Source = value;
                OnPropertyChanged();
            }
        }

        public BilledeListe BilledeListe
        {
            get => _billedeListe;
            set
            {
                _billedeListe = value;
                OnPropertyChanged();
            }
        }

        public string Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged();
            }
        }

        public int SelectedImageIndex
        {
            get => _selectedImageIndex;
            set
            {
                _selectedImageIndex = value;
                OnPropertyChanged();
            }
        }

        private void OpenHentPatientWindow()
        {
            _hentPatientWindow.Show();
        }

        private void ImageSelectionChange()
        {
            try
            {
                var selectedBillede = _billedeListe[SelectedImageIndex];

                if (selectedBillede != null)
                {
                    Sum = "Behandler...";
                    Task.Run(() => PythonCaller.CallScript(selectedBillede.URL));
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(selectedBillede.URL);
                    bi.EndInit();
                    ImageSource = bi;
                }
            }
            catch (Exception e)
            {
            }
        }

        private void OnMessageReceived(string patientId, string rekvisitionsNr, string dato, BilledeListe billeder)
        {
            _hentPatientWindow.Hide();

            Sum = "Behandler...";
            Task.Run(() => PythonCaller.CallScript(billeder[0].URL));

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(billeder[0].URL);
            bi.EndInit();
            ImageSource = bi;

            if (BilledeListe.Any())
            {
                BilledeListe.Clear();
            }

            foreach (var billede in billeder)
            {
                BilledeListe.Add(billede);
            }

            PatientId = patientId;
            Rekvisitionsnr = rekvisitionsNr;
            Dato = dato;
        }

        private void OnPythonResultReceived(string result)
        {
            Sum = result;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
