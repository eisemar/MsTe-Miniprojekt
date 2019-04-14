using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        [OperationContract]
        void AddAuto(AutoDto autoDto);

        [OperationContract]
        IList<AutoDto> GetAutos();
        
        [OperationContract]
        AutoDto GetAutoById(int id);
        
        [OperationContract]
        [FaultContract(typeof(AutoDto))]
        void UpdateAuto(AutoDto originalDto, AutoDto modifiedDto);
        
        [OperationContract]
        void DeleteAuto(AutoDto autoDto);



        [OperationContract]
        void AddKunde(KundeDto kundeDto);
        
        [OperationContract]
        IList<KundeDto> GetKunden();

        [OperationContract]
        KundeDto GetKundeById(int id);
        
        [OperationContract]
        [FaultContract(typeof(KundeDto))]
        void UpdateKunde(KundeDto originalDto, KundeDto modifiedDto);

        [OperationContract]
        void DeleteKunde(KundeDto kundeDto);



        [OperationContract]
        void AddReservation(ReservationDto reservationDto);
        
        [OperationContract]
        IList<ReservationDto> GetReservationen();
        
        [OperationContract]
        ReservationDto GetReservationById(int id);
        
        [OperationContract]
        [FaultContract(typeof(ReservationDto))]
        void UpdateReservation(ReservationDto originalDto, ReservationDto modifiedDto);
        
        [OperationContract]
        void DeleteReservation(ReservationDto reservationDto);
    }
}
