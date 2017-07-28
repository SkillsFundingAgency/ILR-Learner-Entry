
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ilrLearnerEntry.WpfConverter
{
    public class IsCompleteToBackgroundColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;

            String sReturn = String.Empty;
            Boolean valueToTry = System.Convert.ToBoolean(value);
            switch (valueToTry)
            {
                case true:
                    sReturn = "#FFBDE891";
                    break;
                case false:
                    sReturn = "#FFE89191";
                    break;
                default:
                    Console.WriteLine(String.Format("valueToTry : {0}", valueToTry.ToString()));
                    sReturn = String.Empty;
                    break;
            }
            return sReturn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}

