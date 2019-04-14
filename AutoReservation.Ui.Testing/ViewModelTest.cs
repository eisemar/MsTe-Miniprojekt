using AutoReservation.TestEnvironment;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Ui.Properties;
using System.Windows.Input;

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void AutosLoadTest()
        {
            AutoViewModel autoViewModel = new AutoViewModel();
            Assert.IsTrue(autoViewModel.LoadCommand.CanExecute(null));

            autoViewModel.LoadCommand.Execute(null);
            Assert.AreEqual<int>(3, autoViewModel.Autos.Count);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            KundeViewModel kundeViewModel = new KundeViewModel();
            Assert.IsTrue(kundeViewModel.LoadCommand.CanExecute(null));

            kundeViewModel.LoadCommand.Execute(null);
            Assert.AreEqual<int>(4, kundeViewModel.Kunden.Count);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel();
            Assert.IsTrue(reservationViewModel.LoadCommand.CanExecute(null));

            reservationViewModel.LoadCommand.Execute(null);
            Assert.AreEqual<int>(3, reservationViewModel.Reservationen.Count);
        }
    }
}