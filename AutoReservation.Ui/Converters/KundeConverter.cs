using System;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.Converters
{
    public class KundeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (KundeDto)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (KundeDto)value;
        }
    }
}
