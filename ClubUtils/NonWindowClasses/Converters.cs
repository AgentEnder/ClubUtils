using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class BooleanToHiddenVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility rv = Visibility.Visible;
            try
            {
                var x = bool.Parse(value.ToString());
                if (x)
                {
                    rv = Visibility.Visible;
                }
                else
                {
                    rv = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
            }
            return rv;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

    }

    public class CalendarEventConverter : IValueConverter
    {
        static Dictionary<DateTime, string> dict = new Dictionary<DateTime, string>();
        public static void SetDates(List<DateTime> dates)
        {
            dict.Clear();
            foreach (var date in dates)
            {
                dict.Add(date, "Event");
            }
        }
        public CalendarEventConverter()
        {
            dict = new Dictionary<DateTime, string>();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text;
            if (!dict.TryGetValue((DateTime)value, out text)) text = null;
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}