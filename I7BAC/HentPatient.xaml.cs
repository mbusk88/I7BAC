using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using I7BAC.Model;
using I7BAC.Viewmodel;

namespace I7BAC
{
    /// <summary>
    /// Interaction logic for HentPatient.xaml
    /// </summary>
    public partial class HentPatient : Window
    {
        private HentPatientViewmodel _hentPatientViewmodel;

        public HentPatient()
        {
            InitializeComponent();

            _hentPatientViewmodel = (HentPatientViewmodel) FindResource("HentPatientViewmodel");
            _hentPatientViewmodel.Init();
        }

        private void BtnAnnuller_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Til at indlæse data i xml fil (log) første gang
        //private void SaveModels()
        //{
        //    var patientListe = new PatientListe();
        //    var rekvisationListe = new RekvisitionListe();
        //    var billeder = new BilledeListe();

        //    billeder.Add(new Billede { URL = "", Filnavn = "" });
        //    rekvisationListe.Add(new Rekvisition
        //    { Billeder = billeder, Dato = DateTime.Now.ToString(), Rekvisitionsnr = Guid.NewGuid() });
        //    patientListe.Add(new Patient { CPR = "080674-2492", RekvisitionListe = rekvisationListe, Navn = "Mette Frederiksen" });

        //    XmlSerializer serializer = new XmlSerializer(typeof(PatientListe));
        //    TextWriter writer = new StreamWriter("Patienter.xml");
        //    serializer.Serialize(writer, patientListe);
        //    writer.Close();
        //}
        private void ListViewPatienter_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _hentPatientViewmodel.PatientSelection.Execute(sender);
        }

        private void ListViewRekvisitioner_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _hentPatientViewmodel.RekvistionSelection.Execute(sender);
        }
    }
}