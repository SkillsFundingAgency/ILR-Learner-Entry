
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ilrLearnerEntry.WpfConverter
{
	public class FamTypeVisabilityConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{

			System.Windows.Visibility v = System.Windows.Visibility.Visible;
			if ((value != null) && (!String.IsNullOrEmpty(value.ToString())))
			{
				if (parameter == null)
				{
					parameter = "XYZ";
				}

				// What are we showing
				switch (parameter.ToString().ToUpper())
				{
					case "TYPE":
						v = Process_Visability_Type(value.ToString());
						break;
					case "CODE":
						v = Process_Visability_Code(value.ToString());
						break;
					case "FROM":
						v = Process_Visability_From(value.ToString());
						break;
					case "TO":
						v = Process_Visability_To(value.ToString());
						break;
					default:
						v = System.Windows.Visibility.Visible;
						break;
				}

			}
			return v;

		}

		private System.Windows.Visibility Process_Visability_Code(String FamType)
		{
			System.Windows.Visibility v = System.Windows.Visibility.Visible;

			switch (FamType.ToString().ToUpper())
			{
				case "ALB":
					v = System.Windows.Visibility.Visible;
					break;
				case "LSF":
				case "HEM":
				case "LDM":
					v = System.Windows.Visibility.Collapsed;
					break;
				default:
					v = System.Windows.Visibility.Visible;
					break;
			}
			return v;
		}
		private System.Windows.Visibility Process_Visability_Type(String FamType)
		{
			System.Windows.Visibility v = System.Windows.Visibility.Visible;

			if (FamType == null)
			{
				switch (FamType.ToString().ToUpper())
				{
					case "ALB":
					case "LSF":
						v = System.Windows.Visibility.Visible;
						break;
					case "HEM":
					case "LDM":
						v = System.Windows.Visibility.Collapsed;
						break;
					default:
						v = System.Windows.Visibility.Visible;
						break;
				}
			}
			return v;
		}
		private System.Windows.Visibility Process_Visability_From(String FamType)
		{
			System.Windows.Visibility v = System.Windows.Visibility.Visible;

			if (FamType == null)
			{
				switch (FamType.ToString().ToUpper())
				{
					case "ALB":
					case "LSF":
						v = System.Windows.Visibility.Visible;
						break;
					case "HEM":
					case "LDM":
						v = System.Windows.Visibility.Collapsed;
						break;
					default:
						v = System.Windows.Visibility.Visible;
						break;
				}
			}
			return v;
		}
		private System.Windows.Visibility Process_Visability_To(String FamType)
		{
			System.Windows.Visibility v = System.Windows.Visibility.Visible;

			if (FamType == null)
			{
				switch (FamType.ToString().ToUpper())
				{
					case "ALB":
					case "LSF":
						v = System.Windows.Visibility.Visible;
						break;
					case "HEM":
					case "LDM":
						v = System.Windows.Visibility.Collapsed;
						break;
					default:
						v = System.Windows.Visibility.Visible;
						break;
				}
			}
			return v;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{

			return false;
		}

	}
}

