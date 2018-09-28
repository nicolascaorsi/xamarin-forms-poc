using System;
using Company.Modules.Message.Database;
using Xamarin.Forms;

namespace Company.Modules.Message.UI.Converters
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Database.Message message)
                return message.ReadAt.HasValue ? Color.Gray : Color.Green;
            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }
}
