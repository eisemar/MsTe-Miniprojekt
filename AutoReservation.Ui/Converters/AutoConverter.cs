using System;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.Converters
{
    public class AutoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (AutoDto)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (AutoDto)value;
        }
    }
}
