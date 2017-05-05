
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ilrLearnerEntry.WpfConverter
{
    public class StringIsEnabledConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = false;
            if (value != null)
            {
                if (!String.IsNullOrEmpty(value.ToString()))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }

    }
}

