using System;
using System.Net.Mime;
using I7BAC;
using I7BAC.Model;
using I7BAC.Viewmodel;
using NUnit.Framework;

namespace I7BAC_Test
{
    [TestFixture]
    public class MainWindowViewmodelTest
    {
        public MainWindowViewModel _sut;


        [SetUp]
        public void Setup()
        {
            _sut = new MainWindowViewModel();
            _sut.Init();
        }

        [Test]
        public void PatientSelection_WhenPatientSelected_CorrectRekvisationsListLoaded()
        {
            var patientId = "12354";
            var rekvisationNr = "09234i2";
            var dato = "1 juni";
            var billedList = new BilledeListe();

            Aggregator.BroadCast(patientId, rekvisationNr, dato, billedList);

            Assert.AreEqual(patientId, _sut.PatientId);
            Assert.AreEqual(rekvisationNr, _sut.Rekvisitionsnr);
            Assert.AreEqual(dato, dato);
            Assert.AreEqual(billedList, _sut.BilledeListe);
        }
    }
}
