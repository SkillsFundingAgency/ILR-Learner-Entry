
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ilrLearnerEntry.WpfConverter
{
    public class IsCompleteVisabilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }
            else
            {
                if (value is bool?)
                {
                    bool? flag2 = (bool?)value;
                    flag = (flag2.HasValue && flag2.Value);
                }
            }

            //if false is passed as a converter parameter then reverse the value of input value  
            if (parameter != null)
            {
                bool par = true;
                if ((bool.TryParse(parameter.ToString(), out par)) && (!par)) flag = !flag;
            }

            return flag ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is System.Windows.Visibility)
            {
                return (System.Windows.Visibility)value == System.Windows.Visibility.Visible;
            }
            return false;
        }

    }
}

