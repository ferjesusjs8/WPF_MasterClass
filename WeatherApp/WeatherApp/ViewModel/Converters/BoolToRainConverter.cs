using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.ViewModel.Converters
{
    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRaining = (bool)value;

            if (isRaining)
                return "Currently Raining";

            return "Currently Not Raining";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("Currently Raining"))
                return true;

            return false;
        }
    }
}
