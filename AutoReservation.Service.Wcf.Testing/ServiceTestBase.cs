using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void AutosTest()
        {
            var autos = Target.GetAutos();
            Assert.AreEqual(3, autos.Count);
        }

        [TestMethod]
        public void KundenTest()
        {
            var kunden = Target.GetKunden();
            Assert.AreEqual(4, kunden.Count);
        }

        [TestMethod]
        public void ReservationenTest()
        {
            var reservationen = Target.GetReservationen();
            Assert.AreEqual(3, reservationen.Count);
        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            var auto = Target.GetAutoById(1);
            Assert.AreEqual("Fiat Punto", auto.Marke);
            Assert.AreEqual(AutoKlasse.Standard, auto.AutoKlasse);
            Assert.AreEqual(50, auto.Tagestarif);
            Assert.AreEqual(0, auto.Basistarif);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var kunde = Target.GetKundeById(1);
            Assert.AreEqual("Nass", kunde.Nachname);
            Assert.AreEqual("Anna", kunde.Vorname);
            Assert.AreEqual(Convert.ToDateTime("1961-05-05 00:00:00"), kunde.Geburtsdatum);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            var reservation = Target.GetReservationById(1);
            Assert.AreEqual(1, reservation.ReservationNr);
            Assert.AreEqual(1, reservation.Auto.Id);
            Assert.AreEqual(1, reservation.Kunde.Id);
            Assert.AreEqual(Convert.ToDateTime("2020-01-10 00:00:00"), reservation.Von);
            Assert.AreEqual(Convert.ToDateTime("2020-01-20 00:00:00"), reservation.Bis);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            var reservation = Target.GetReservationById(4);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            var newAuto = new AutoDto() {
                Id = 4,
                Marke = "BMW",
                AutoKlasse = AutoKlasse.Luxusklasse,
                Tagestarif = 100,
                Basistarif = 50
            };
            Target.AddAuto(newAuto);

            Assert.AreEqual(4, Target.GetAutos().Count);

            var auto = Target.GetAutoById(4);
            Assert.AreEqual("BMW", auto.Marke);
            Assert.AreEqual(AutoKlasse.Luxusklasse, auto.AutoKlasse);
            Assert.AreEqual(100, auto.Tagestarif);
            Assert.AreEqual(50, auto.Basistarif);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            var newKunde = new KundeDto()
            {
                Id = 5,
                Nachname = "Muster",
                Vorname = "Max",
                Geburtsdatum = Convert.ToDateTime("2000-01-01 00:00:00")
            };
            Target.AddKunde(newKunde);

            Assert.AreEqual(5, Target.GetKunden().Count);

            var kunde = Target.GetKundeById(5);
            Assert.AreEqual("Muster", kunde.Nachname);
            Assert.AreEqual("Max", kunde.Vorname);
            Assert.AreEqual(Convert.ToDateTime("2000-01-01 00:00:00"), kunde.Geburtsdatum);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            var newReservation = new ReservationDto()
            {
                ReservationNr = 4,
                Auto = Target.GetAutoById(3),
                Kunde = Target.GetKundeById(4),
                Von = new DateTime(2020,1,20,0,0,0),
                Bis = new DateTime(2020,1,30,0,0,0)
            };
            Target.AddReservation(newReservation);

            Assert.AreEqual(4, Target.GetReservationen().Count);

            var reservation = Target.GetReservationById(4);
            Assert.AreEqual(4, reservation.ReservationNr);
            Assert.AreEqual("Audi S6", reservation.Auto.Marke);
            Assert.AreEqual(AutoKlasse.Luxusklasse, reservation.Auto.AutoKlasse);
            Assert.AreEqual(180, reservation.Auto.Tagestarif);
            Assert.AreEqual(50, reservation.Auto.Basistarif);
            Assert.AreEqual("Zufall", reservation.Kunde.Nachname);
            Assert.AreEqual("Rainer", reservation.Kunde.Vorname);
            Assert.AreEqual(Convert.ToDateTime("1944-11-11 00:00:00"), reservation.Kunde.Geburtsdatum);
            Assert.AreEqual(Convert.ToDateTime("2020-01-20 00:00:00"), reservation.Von);
            Assert.AreEqual(Convert.ToDateTime("2020-01-30 00:00:00"), reservation.Bis);

        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            var original = Target.GetAutoById(1);
            var modified = (AutoDto)original.Clone();
            modified.Tagestarif = 75;
            Target.UpdateAuto(original, modified);

            var auto = Target.GetAutoById(1);
            Assert.AreEqual(75, auto.Tagestarif);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var original = Target.GetKundeById(1);
            var modified = (KundeDto)original.Clone();
            modified.Vorname = "Hans";
            Target.UpdateKunde(original, modified);

            var auto = Target.GetKundeById(1);
            Assert.AreEqual("Hans", auto.Vorname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var original = Target.GetReservationById(3);
            var modified = (ReservationDto)original.Clone();
            modified.Bis = new DateTime(2020, 2, 10, 0, 0, 0);
            Target.UpdateReservation(original, modified);

            var reservation = Target.GetReservationById(3);
            Assert.AreEqual(Convert.ToDateTime("2020-02-10 00:00:00"), reservation.Bis);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>), "No Fault Exception was thrown")]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            var original = Target.GetAutoById(1);
            var modified1 = (AutoDto)original.Clone();
            var modified2 = (AutoDto)original.Clone();
            modified1.Tagestarif = 75;
            modified2.Tagestarif = 100;
            Target.UpdateAuto(original, modified1);
            Target.UpdateAuto(original, modified2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<KundeDto>), "No Fault Exception was thrown")]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            var original = Target.GetKundeById(1);
            var modified1 = (KundeDto)original.Clone();
            var modified2 = (KundeDto)original.Clone();
            modified1.Vorname = "Hans";
            modified2.Vorname = "Barak";

            Target.UpdateKunde(original, modified1);
            Target.UpdateKunde(original, modified2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ReservationDto>), "No Fault Exception was thrown")]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            var original = Target.GetReservationById(3);
            var modified1 = (ReservationDto)original.Clone();
            var modified2 = (ReservationDto)original.Clone();
            modified1.Bis = new DateTime(2020, 2, 10, 0, 0, 0);
            modified2.Bis = new DateTime(2022, 2, 22, 0, 0, 0);
            Target.UpdateReservation(original, modified1);
            Target.UpdateReservation(original, modified2);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            int oldCount = Target.GetKunden().Count;
            var kunde = Target.GetKundeById(1);
            Target.DeleteKunde(kunde);
            Assert.AreEqual(oldCount - 1, Target.GetKunden().Count);
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            int oldCount = Target.GetAutos().Count;
            var auto = Target.GetAutoById(1);
            Target.DeleteAuto(auto);
            Assert.AreEqual(oldCount - 1, Target.GetAutos().Count);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            int oldCount = Target.GetReservationen().Count;
            var reservation = Target.GetReservationById(1);
            Target.DeleteReservation(reservation);
            Assert.AreEqual(oldCount - 1, Target.GetReservationen().Count);
        }
    }
}
