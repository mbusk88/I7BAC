using I7BAC.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace I7BAC
{
    /// <summary>
    /// Interaction logic for HentPatient.xaml
    /// </summary>
    public partial class HentPatient : Window
    {
        private PatientListe patientListe;
        private RekvisitionListe _rekvisitionListe;
        private Patient selectedPatient;
        private string sum = "";

        private MainWindow mainWindow { get; set; }

        public HentPatient()
        {
            InitializeComponent();

            patientListe = (PatientListe) FindResource("PatientListe");
            _rekvisitionListe = (RekvisitionListe) FindResource("RekvisitionListe");

            GetPatientListe();
            //SaveModels();
            
        }

        // Henter liste over patienter fra xml fil (log)
        private void GetPatientListe()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Patient>));
            TextReader reader = new StreamReader("Patienter.xml");
            ObservableCollection<Patient> returnPatients = (ObservableCollection<Patient>)serializer.Deserialize(reader);
            reader.Close();

            foreach (var returnPatient in returnPatients)
            {
                patientListe.Add(returnPatient);
            }
        }

        private void GetRekvisitionListe()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Rekvisition>));
            TextReader reader = new StreamReader("Patienter.xml");
            ObservableCollection<Rekvisition> returnRekvisitions = (ObservableCollection<Rekvisition>)serializer.Deserialize(reader);
            reader.Close();

            foreach (var returnPatient in returnRekvisitions)
            {
                _rekvisitionListe.Add(returnPatient);
            }
        }

        // Til at indlæse data i xml fil (log) første gang
        private void SaveModels()
        {
            var patientListe = new PatientListe();
            var rekvisationListe = new RekvisitionListe();
            var billeder = new BilledeListe();

            billeder.Add(new Billede { URL = "", Filnavn = "" });
            rekvisationListe.Add(new Rekvisition
            { Billeder = billeder, Dato = DateTime.Now, Rekvisitionsnr = Guid.NewGuid() });
            patientListe.Add(new Patient { CPR = "080674-2492", RekvisitionListe = rekvisationListe, Navn = "Mette Frederiksen" });

            XmlSerializer serializer = new XmlSerializer(typeof(PatientListe));
            TextWriter writer = new StreamWriter("Patienter.xml");
            serializer.Serialize(writer, patientListe);
            writer.Close();
        }
        
        private void BtnAnnuller_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewPatienter_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedPatient = (Patient) ListViewPatienter.SelectedItem;
            if (selectedPatient != null)
            {
                ListViewRekvisitioner.ItemsSource = selectedPatient.RekvisitionListe;
            }
        }

        private void ListViewRekvisitioner_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Rekvisition selectedRekvisition = (Rekvisition) ListViewRekvisitioner.SelectedItem;

            if (selectedRekvisition != null)
            {
                pythonScript();

                Aggregator.BroadCast(
                    selectedPatient.CPR,
                    selectedRekvisition.Rekvisitionsnr.ToString(),
                    selectedRekvisition.Dato.ToString(),
                    selectedRekvisition.Billeder,
                    sum
                );

                this.Close();
            }
        }

        private void pythonScript()
        {
            // full path of python interpreter
            string python = @"C:\Users\Mikke\Anaconda3\python.exe";

            // python app to call
            string myPythonApp = @"C:\Users\Mikke\I7BAC\algorithmGUItest.py";



            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments  
            // 1st argument is pointer to itself, 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = myPythonApp;



            Process myProcess = new Process();
            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start process 
            myProcess.Start();



            // Read the standard output of the app we called.  
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();

            // wait exit signal from the app we called 
            myProcess.WaitForExit();

            // close the process 
            myProcess.Close();



            // write the output we got from python app 
            //Console.WriteLine("Value received from script: " + myString);

            
            if (myString != "")
            {
                sum = myString;
            }
        }
    }
}
