using AutoReservation.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.BusinessLayer
{
    interface IAutoReservationGateway
    {
        void AddAuto(Auto auto);
        IList<Auto> GetAutos();
        Auto GetAutoById(int id);
        void UpdateAuto(Auto original, Auto modified);
        void DeleteAuto(Auto auto);
        

        void AddKunde(Kunde kunde);
        IList<Kunde> GetKunden();
        Kunde GetKundeById(int id);
        void UpdateKunde(Kunde original, Kunde modified);
        void DeleteKunde(Kunde kunde);
        
        
        void AddReservation(Reservation reservation);    
        IList<Reservation> GetReservationen();
        Reservation GetReservationById(int id);
        void UpdateReservation(Reservation original, Reservation modified);
        void DeleteReservation(Reservation reservation);
    }
}
