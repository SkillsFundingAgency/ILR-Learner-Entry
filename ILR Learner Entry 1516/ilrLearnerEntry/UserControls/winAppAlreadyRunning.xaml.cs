using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ilrLearnerEntry.UserControls
{
	/// <summary>
	/// Interaction logic for winAppAlreadyRunning.xaml
	/// </summary>
	public partial class winAppAlreadyRunning : Window
	{
		public winAppAlreadyRunning()
		{
			InitializeComponent();
			this.txtApplicationName.Text = App.ApplicationName;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
