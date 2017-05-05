using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
	/// <summary>
	/// Interaction logic for ucLearnerHEInformation.xaml
	/// </summary>
	public partial class ucLearnerHEInformation : UserControl, INotifyPropertyChanged
	{
		#region Private Variables
		private Learner _learner;
		#endregion

		#region Constructor
		public ucLearnerHEInformation()
		{
			InitializeComponent();
			this.DataContext = this;
		}
		#endregion

		#region Public Properties

		public Learner CurrentItem
		{
			get { return _learner; }
			set
			{
				if (value != null)
				{
					_learner = value;
					this.DataContext = this;
					OnPropertyChanged("HEFinCash");
					OnPropertyChanged("HEFinNearCash");
					OnPropertyChanged("HEFinCash");
					OnPropertyChanged("HEFinCash");
					OnPropertyChanged("CurrentItem");
				}
				else
				{
					this.DataContext = null;
				}
			}
		}

		// Change from Number to String here to stopp GI missing blank string !!
		public string UCASENum { get; set; }
		public DataTable TermTimeAccList { set; get; }

		public String HEFinCash
		{
			get
			{

				return ((this.CurrentItem==null)||(String.IsNullOrEmpty(this.CurrentItem.HEFinCash.ToString()))) ? String.Empty : this.CurrentItem.HEFinCash.ToString();			
			}
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					CurrentItem.HEFinCash = null;
				}
				else
				{
					CurrentItem.HEFinCash = int.Parse(value);
				}
				OnPropertyChanged("HEFinCash");
				OnPropertyChanged("CurrentItem");
			}
		}
		public String HEFinNearCash
		{
			get
			{

				return ((this.CurrentItem == null) || (String.IsNullOrEmpty(this.CurrentItem.HEFinNearCash.ToString()))) ? String.Empty : this.CurrentItem.HEFinNearCash.ToString(); 
			}
			set
			{
				CurrentItem.HEFinNearCash = int.Parse(value);
				OnPropertyChanged("HEFinNearCash");
				OnPropertyChanged("CurrentItem");
			}
		}
		#endregion

		#region Public Methods
		#endregion

		#region PRIVATE Methods
		#endregion


		#region INotifyPropertyChanged Members
		/// <summary>
		/// INotifyPropertyChanged requires a property called PropertyChanged.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Fires the event for the property when it changes.
		/// </summary>
		protected virtual void OnPropertyChanged(string propertyName)
		{
#if DEBUG
			VerifyPropertyName(propertyName);
#endif
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

		}

		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public void VerifyPropertyName(string propertyName)
		{
			// Verify that the property name matches a real,  
			// public, instance property on this object.
			if (TypeDescriptor.GetProperties(this)[propertyName] == null)
			{
				var msg = "Invalid property name: " + propertyName;

				if (this.ThrowOnInvalidPropertyName)
				{
					throw new Exception(msg);
				}
				else
				{
					Debug.Fail(msg);
				}
			}
		}

		protected bool ThrowOnInvalidPropertyName { get; set; }

		#endregion



	}
}
