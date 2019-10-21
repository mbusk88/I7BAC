using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using I7BAC.Command;
using I7BAC.Model;
using Prism.Commands;

namespace I7BAC.Viewmodel
{
    public class HentPatientViewmodel : INotifyPropertyChanged
    {
        private PatientListe _patientListe = new PatientListe();
        private RekvisitionListe _rekvisitionListe = new RekvisitionListe();
        private int _patientIndex;
        private int _rekvisionIndex;
        public PatientSelectionCommand PatientSelection { get; set; } = new PatientSelectionCommand();
        public RekvisionSelectionCommand RekvistionSelection { get; set; } = new RekvisionSelectionCommand();
        
        public void Init()
        {
            PatientSelection.PatientSelectionAction += PatientSelectionChanged;
            RekvistionSelection.RekvisitionSelectionAction += RekvistionSelectionChanged;
            
            GetPatientListe();
        }

        public PatientListe PatientListe
        {
            get => _patientListe;
            set
            {
                _patientListe = value;
                OnPropertyChanged();
            }
        }

        public RekvisitionListe RekvisitionListe
        {
            get => _rekvisitionListe;
            set
            {
                _rekvisitionListe = value;
                OnPropertyChanged();
            }
        }

        public int PatientIndex
        {
            get => _patientIndex;
            set
            {
                _patientIndex = value;
                OnPropertyChanged();
            }
        }

        public int RekvisionIndex
        {
            get => _rekvisionIndex;
            set
            {
                _rekvisionIndex = value;
                OnPropertyChanged();
            }
        }

        private void PatientSelectionChanged()
        {
            var selectedPatient = PatientListe[PatientIndex];

            if (selectedPatient != null)
            {
                RekvisitionListe = selectedPatient.RekvisitionListe;
            }
        }

        private void RekvistionSelectionChanged()
        {
            var selectedRekvisition = RekvisitionListe[RekvisionIndex];

            if (selectedRekvisition != null)
            {
                Aggregator.BroadCast(
                    PatientListe[PatientIndex].CPR,
                    selectedRekvisition.Rekvisitionsnr.ToString(),
                    selectedRekvisition.Dato,
                    selectedRekvisition.Billeder
                );
            }
        }

        private void GetPatientListe()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Patient>));
            TextReader reader = new StreamReader(@"C:\Users\Mikke\source\repos\I7BAC\I7BAC\bin\Debug\Patienter.xml");
            ObservableCollection<Patient> returnPatients = (ObservableCollection<Patient>)serializer.Deserialize(reader);
            reader.Close();

            foreach (var returnPatient in returnPatients)
            {
                PatientListe.Add(returnPatient);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}