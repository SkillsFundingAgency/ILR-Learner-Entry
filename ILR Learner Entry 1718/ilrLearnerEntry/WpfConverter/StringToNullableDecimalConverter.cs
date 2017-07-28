
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace ilrLearnerEntry.WpfConverter
{
	public class StringToNullableDecimalConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			String sReturn = string.Empty;
			if (value != null)
			{
				decimal number;
				bool result = decimal.TryParse(System.Convert.ToString(value), out number);
				if (result)
				{
					sReturn = number.ToString(culture);
				}
			}
			return sReturn;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string s = (string)value;
			if (String.IsNullOrEmpty(s))
				return null;
			else
			{
				return (decimal?)decimal.Parse(s, culture);
			}
		}
	}
}

