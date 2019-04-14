using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent : IAutoReservationGateway
    {

        public AutoReservationBusinessComponent()
        {
            
        }


        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }

        #region CRUD Auto

        public void AddAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Add(auto);
                context.SaveChanges();
            }
        }

        public IList<Auto> GetAutos()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var autos = from a in context.Autos select a;
                return autos.ToList();
            }
        }

        public Auto GetAutoById(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var auto = from a in context.Autos where a.Id == id select a;
                return auto.FirstOrDefault();

            }
        }
        
        public void UpdateAuto(Auto original, Auto modified)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Autos.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Attach(auto);
                context.Autos.Remove(auto);
                context.SaveChanges();
            }
        }

        #endregion


        #region CRUD Kunde

        public void AddKunde(Kunde kunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
        }

        public IList<Kunde> GetKunden()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var kunden = from k in context.Kunden select k;
                return kunden.ToList();
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var kunde = from k in context.Kunden where k.Id == id select k;
                return kunde.FirstOrDefault(k => k != null);
                
            }
        }

        public void UpdateKunde(Kunde original, Kunde modified)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Kunden.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Kunden.Attach(kunde);
                context.Kunden.Remove(kunde);
                context.SaveChanges();
            }
        }

        #endregion


        #region CRUD Reservation

        public void AddReservation(Reservation reservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
        }

        public IList<Reservation> GetReservationen()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var reservationen = from r in context.Reservationen.Include(x => x.Auto).Include(x => x.Kunde) select r;
                return reservationen.ToList();
            }
        }

        public Reservation GetReservationById(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var reservation = from r in context.Reservationen.Include(x => x.Auto).Include(x => x.Kunde) where r.ReservationNr == id select r;
                return reservation.FirstOrDefault(r => r != null);

            }
        }

        public void UpdateReservation(Reservation original, Reservation modified)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Reservationen.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Reservationen.Attach(reservation);
                context.Reservationen.Remove(reservation);
                context.SaveChanges();
            }
        }

        #endregion

    }
}