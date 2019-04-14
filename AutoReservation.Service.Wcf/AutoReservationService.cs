using AutoReservation.BusinessLayer;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private AutoReservationBusinessComponent businessComponent;

        public AutoReservationService()
        {
            businessComponent = new AutoReservationBusinessComponent();
        }

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }

        #region Auto
        public void AddAuto(Common.DataTransferObjects.AutoDto autoDto)
        {
            businessComponent.AddAuto(DtoConverter.ConvertToEntity(autoDto));
        }

        public System.Collections.Generic.IList<Common.DataTransferObjects.AutoDto> GetAutos()
        {
            return businessComponent.GetAutos().ConvertToDtos();
        }

        public Common.DataTransferObjects.AutoDto GetAutoById(int id)
        {
            return businessComponent.GetAutoById(id).ConvertToDto();
        }

        public void UpdateAuto(Common.DataTransferObjects.AutoDto originalDto, Common.DataTransferObjects.AutoDto modifiedDto)
        {
            try
            {
                businessComponent.UpdateAuto(DtoConverter.ConvertToEntity(originalDto), DtoConverter.ConvertToEntity(modifiedDto));
            }
            catch (LocalOptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<Common.DataTransferObjects.AutoDto>(originalDto, e.Message);
            }
        }

        public void DeleteAuto(Common.DataTransferObjects.AutoDto autoDto)
        {
            businessComponent.DeleteAuto(DtoConverter.ConvertToEntity(autoDto));
        }
        #endregion

        #region Kunde
        public void AddKunde(Common.DataTransferObjects.KundeDto kundeDto)
        {
            businessComponent.AddKunde(DtoConverter.ConvertToEntity(kundeDto));
        }

        public System.Collections.Generic.IList<Common.DataTransferObjects.KundeDto> GetKunden()
        {
            return businessComponent.GetKunden().ConvertToDtos();
        }

        public Common.DataTransferObjects.KundeDto GetKundeById(int id)
        {
            return businessComponent.GetKundeById(id).ConvertToDto();
        }

        public void UpdateKunde(Common.DataTransferObjects.KundeDto originalDto, Common.DataTransferObjects.KundeDto modifiedDto)
        {
            try
            { 
                businessComponent.UpdateKunde(DtoConverter.ConvertToEntity(originalDto), DtoConverter.ConvertToEntity(modifiedDto));
            }
            catch (LocalOptimisticConcurrencyException<Kunde> e)
            {
                throw new FaultException<Common.DataTransferObjects.KundeDto>(originalDto, e.Message);
            }
            
        }

        public void DeleteKunde(Common.DataTransferObjects.KundeDto kundeDto)
        {
            businessComponent.DeleteKunde(DtoConverter.ConvertToEntity(kundeDto));
        }
        #endregion

        #region Reservation
        public void AddReservation(Common.DataTransferObjects.ReservationDto reservationDto)
        {
            businessComponent.AddReservation(DtoConverter.ConvertToEntity(reservationDto));
        }

        public System.Collections.Generic.IList<Common.DataTransferObjects.ReservationDto> GetReservationen()
        {
            return businessComponent.GetReservationen().ConvertToDtos();
        }

        public Common.DataTransferObjects.ReservationDto GetReservationById(int id)
        {
            return businessComponent.GetReservationById(id).ConvertToDto();
        }

        public void UpdateReservation(Common.DataTransferObjects.ReservationDto originalDto, Common.DataTransferObjects.ReservationDto modifiedDto)
        {
            try
            {
                businessComponent.UpdateReservation(DtoConverter.ConvertToEntity(originalDto), DtoConverter.ConvertToEntity(modifiedDto));
            }
            catch (LocalOptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<Common.DataTransferObjects.ReservationDto>(originalDto, e.Message);
            }
        }

        public void DeleteReservation(Common.DataTransferObjects.ReservationDto reservationDto)
        {
            businessComponent.DeleteReservation(DtoConverter.ConvertToEntity(reservationDto));
        }
        #endregion

    }
}