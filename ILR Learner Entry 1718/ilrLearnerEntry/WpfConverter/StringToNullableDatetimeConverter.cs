
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace ilrLearnerEntry.WpfConverter
{
	public class StringToNullableDatetimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			String sReturn = string.Empty;
			if (value != null)
			{
				DateTime dt;
				bool result = DateTime.TryParse(System.Convert.ToString(value), out dt);
				if (result)
				{
					sReturn = dt.ToString(culture);
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
				return (DateTime?)DateTime.Parse(s, culture);
			}
		}
	}
}

