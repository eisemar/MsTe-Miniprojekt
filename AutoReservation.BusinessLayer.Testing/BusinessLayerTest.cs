using AutoReservation.Dal;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void UpdateAutoTest()
        {
            Auto original = Target.GetAutoById(1);
            Auto modified = Target.GetAutoById(1);

            modified.Marke = "BMW 5er";
            target.UpdateAuto(original, modified);

            Auto newOriginal = Target.GetAutoById(1);
            Assert.AreEqual<String>(newOriginal.Marke, "BMW 5er");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Kunde original = Target.GetKundeById(1);
            Kunde modified = Target.GetKundeById(1);
            modified.Vorname = "Hans";
            modified.Nachname = "Muster";
            modified.Geburtsdatum = new DateTime(1970,1,1,0,0,0);
            target.UpdateKunde(original, modified);

            Kunde newOriginal = Target.GetKundeById(1);
            Assert.AreEqual(newOriginal.Vorname, "Hans");
            Assert.AreEqual(newOriginal.Nachname, "Muster");
            Assert.AreEqual(newOriginal.Geburtsdatum, new DateTime(1970, 1, 1, 0, 0, 0));
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation original = Target.GetReservationById(1);
            Reservation modified = Target.GetReservationById(1);
            modified.Von = new DateTime(2020, 1, 20);
            modified.Bis = new DateTime(2020, 1, 30);
            target.UpdateReservation(original, modified);

            Reservation newOriginal = Target.GetReservationById(1);
            Assert.AreEqual(newOriginal.Von, new DateTime(2020, 1, 20));
            Assert.AreEqual(newOriginal.Bis, new DateTime(2020, 1, 30));
        }

    }
}
