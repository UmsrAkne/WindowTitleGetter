namespace WindowTitleGetter.Models
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return default(DateTime);
            }

            DateTime dateTime = (DateTime)value;
            return dateTime != default(DateTime) ? dateTime.ToString("MM/dd HH:mm") : "----- -----";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}