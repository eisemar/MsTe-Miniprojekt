using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Ui.Factory
{
    public class RemoteDataAccessCreator : Creator
    {
        public override Common.Interfaces.IAutoReservationService CreateInstance()
        {
            ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            return channelFactory.CreateChannel();
        }
    }
}
