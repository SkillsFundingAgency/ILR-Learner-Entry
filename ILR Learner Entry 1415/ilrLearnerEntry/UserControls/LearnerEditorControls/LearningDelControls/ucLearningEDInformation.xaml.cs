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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
	/// <summary>
	/// Interaction logic for ucLearningEDInformation.xaml
	/// </summary>
	public partial class ucLearningEDInformation : UserControl, INotifyPropertyChanged
	{

		#region Private Variables
		private ILR.LearningDelivery _learningDelivery;
		#endregion

		#region Constructor
		public ucLearningEDInformation()
		{
			InitializeComponent();
			this.DataContext = this;
		}
		#endregion

		#region Public Properties
		public ILR.LearningDeliveryHE HeItem
		{
			get { return ((_learningDelivery == null)||(_learningDelivery.LearningDeliveryHE == null)) ? null : _learningDelivery.LearningDeliveryHE; }
			set
			{
				_learningDelivery.LearningDeliveryHE = value;
				OnPropertyChanged("CurrentItem");
				OnPropertyChanged("HeItem");
			}

		}
		public ILR.LearningDelivery CurrentItem
		{
			get { return _learningDelivery; }
			set
			{
				_learningDelivery = value;
				if (_learningDelivery != null)
				{
					if (_learningDelivery.LearningDeliveryHE == null)
					{
						_learningDelivery.CreateLearningDeliveryHE();
					}
				}
				OnPropertyChanged("SOC2000List");
				OnPropertyChanged("EconomicList");
				OnPropertyChanged("ELQList");
				OnPropertyChanged("TYPEYRList");
				OnPropertyChanged("MSTUFEEList");
				OnPropertyChanged("SPECFEEList");
				OnPropertyChanged("MODESTUDList");
				OnPropertyChanged("FUNDLEVList");
				OnPropertyChanged("FUNDCOMPList");
				OnPropertyChanged("QUALENT3List");
				OnPropertyChanged("CurrentItem");
				OnPropertyChanged("HeItem");
				OnPropertyChanged("HEM3");
				OnPropertyChanged("HEM2");
				OnPropertyChanged("HEM1");
			}
		}

		

		public Boolean HEM1
		{
			get { return FindHemFAM(1); }
			set
			{
				SetHemValue(1, value);
				OnPropertyChanged("HEM1");
			}
		}
		public Boolean HEM2
		{
			get { return FindHemFAM(3); }
			set
			{
				SetHemValue(3, value);
				OnPropertyChanged("HEM2");
			}
		}
		public Boolean HEM3
		{
			get { return FindHemFAM(5); }
			set
			{
				SetHemValue(5, value);
				OnPropertyChanged("HEM3");
			}
		}
		private void SetHemValue(Int32 index, bool value)
		{
			bool Found = false;
			if (FindHemFAM(index))
			{
				List<LearningDeliveryFAM> TmpFamList = _learningDelivery.HEM;

				foreach (LearningDeliveryFAM fam in TmpFamList)
				{
					if ((!string.IsNullOrEmpty(fam.LearnDelFAMCode)) && (fam.LearnDelFAMCode == index.ToString()))
					{
						if (value)
						{
							fam.LearnDelFAMCode = index.ToString();
						}
						else
						{
							TmpFamList.Remove(fam);
						}
						Found = true;
						break;
					}
				}
				_learningDelivery.HEM = TmpFamList;
			}

			// Didnot Find a record so Add 1
			if (!Found)
			{
				_learningDelivery.AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM, index.ToString());
			}
		}
		private Boolean FindHemFAM(Int32 index)
		{
			Boolean IsFound = false;
			if ((_learningDelivery == null) || (_learningDelivery.HEM == null))
			{
				IsFound = false;
			}
			else
			{
				foreach (LearningDeliveryFAM fam in _learningDelivery.HEM)
				{
					if ((!string.IsNullOrEmpty(fam.LearnDelFAMCode)) && (fam.LearnDelFAMCode == index.ToString()))
					{
						IsFound = true;
						break;
					}
				}
			}
			return IsFound;
		}

		public DataTable SOC2000List { set; get; }
		public DataTable EconomicList { set; get; }
		public DataTable ELQList { set; get; }
		public DataTable TYPEYRList { set; get; }
		public DataTable MSTUFEEList { set; get; }
		public DataTable SPECFEEList { set; get; }
		public DataTable MODESTUDList { set; get; }
		public DataTable FUNDLEVList { set; get; }
		public DataTable FUNDCOMPList { set; get; }

		public DataTable QUALENT3List { set; get; }


		

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
