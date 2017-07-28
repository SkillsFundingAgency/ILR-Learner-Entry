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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.LdFramControls
{
	/// <summary>
	/// Interaction logic for LearnerSupportFundingList.xaml
	/// </summary>
	public partial class ucLdFamList : UserControl, INotifyPropertyChanged
	{
		private LearningDelivery _learnerDelivery;
		private List<LearningDeliveryFAM> _FAMLList = new List<LearningDeliveryFAM>(0);
		private LearningDeliveryFAM.MultiOccurrenceFAMs _famType;
		private String _title = String.Empty;

		#region Constructor
		public ucLdFamList()
		{
			InitializeComponent();
			MaxItems = 3;
			this.DataContext = this;

		}
		#endregion

		#region Public Properties

		public LearningDelivery CurrentItem
		{
			get { return _learnerDelivery; }
			set
			{
				_learnerDelivery = value;
				_FAMLList = GetLSFList(_learnerDelivery.LearningDeliveryFAMList);
				FamItemsCV = CollectionViewSource.GetDefaultView(_FAMLList as List<LearningDeliveryFAM>);
				FamItemsCV.MoveCurrentToFirst();
				if (_FAMLList.Count > 0)
				{
					(FamItemsCV.CurrentItem as ILR.LearningDeliveryFAM).IsSelected = true;
				}
				FamItemsCV.Refresh();
				OnPropertyChanged("CurrentItem");
				OnPropertyChanged("FamItemsCV");
				OnPropertyChanged("UserControlTitle"); ;
				ShouldShowListView();
			}
		}

		public LearningDeliveryFAM.MultiOccurrenceFAMs FamType
		{
			get { return _famType; }
			set { _famType = value; }
		}

		public ICollectionView FamItemsCV
		{
			get;
			private set;
		}
		public String UserControlTitle { get { return _title; } set { _title = value; OnPropertyChanged("UserControlTitle"); } }
		public Int32 MaxItems { get; set; }

		public Visibility IsTypeVisable
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				switch (FamType)
				{
					case LearningDeliveryFAM.MultiOccurrenceFAMs.ACT:
                    case LearningDeliveryFAM.MultiOccurrenceFAMs.LSF:
                        v = Visibility.Collapsed;
						break;
					default:
						v = Visibility.Collapsed;
						break;
				}
				return v;
			}
		}
		public Visibility IsCodeVisable
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				switch (FamType)
				{
					case LearningDeliveryFAM.MultiOccurrenceFAMs.ALB:
					case LearningDeliveryFAM.MultiOccurrenceFAMs.HEM:
					case LearningDeliveryFAM.MultiOccurrenceFAMs.LDM:
                    case LearningDeliveryFAM.MultiOccurrenceFAMs.ACT:
                    case LearningDeliveryFAM.MultiOccurrenceFAMs.LSF:
						v = Visibility.Visible;
						break;
					default:
						v = Visibility.Collapsed;
						break;
				}
				return v;
			}
		}
		public Visibility IsFromVisable
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				switch (FamType)
				{
					case LearningDeliveryFAM.MultiOccurrenceFAMs.ALB:
					case LearningDeliveryFAM.MultiOccurrenceFAMs.HEM:
					case LearningDeliveryFAM.MultiOccurrenceFAMs.LDM:
                    case LearningDeliveryFAM.MultiOccurrenceFAMs.ACT:
                    case LearningDeliveryFAM.MultiOccurrenceFAMs.LSF:
						v = Visibility.Visible;
						break;
					default:
						v = Visibility.Collapsed;
						break;
				}
				return v;
			}
		}
		public Visibility IsVisable
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				switch (FamType)
				{
					case LearningDeliveryFAM.MultiOccurrenceFAMs.ALB:
					case LearningDeliveryFAM.MultiOccurrenceFAMs.HEM:
					case LearningDeliveryFAM.MultiOccurrenceFAMs.LDM:
                    case LearningDeliveryFAM.MultiOccurrenceFAMs.ACT:
                    case LearningDeliveryFAM.MultiOccurrenceFAMs.LSF:
						v = Visibility.Visible;
						break;
					default:
						v = Visibility.Collapsed;
						break;
				}
				return v;
			}
		}

		#endregion

		#region PRIVATE Methods
		private List<LearningDeliveryFAM> GetLSFList(List<LearningDeliveryFAM> inputlist)
		{
			return inputlist.FindAll(x => x.LearnDelFAMType == _famType.ToString()); ;
		}
		private void ShouldShowListView()
		{
			if (GetLSFList(_learnerDelivery.LearningDeliveryFAMList).Count() == 0)
			{
				ItemControl.Visibility = System.Windows.Visibility.Collapsed;
			}
			else if (GetLSFList(_learnerDelivery.LearningDeliveryFAMList).Count() == 1)
			{
				ItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				ItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			lv.Visibility = System.Windows.Visibility.Visible;
		}

		private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				ItemControl.CurrentItem = e.AddedItems[0] as LearningDeliveryFAM;
			}
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			if (_FAMLList.Count < MaxItems)
			{
				LearningDeliveryFAM tmp = _learnerDelivery.CreateLearningDeliveryFAM();
				switch (FamType.ToString().ToUpper())
				{
					case "ALB":
					case "LSF":
                    case "ACT":
                        tmp.LearnDelFAMType = FamType.ToString().ToUpper();
						break;
					default:
						break;
				}
				switch (FamType.ToString().ToUpper())
				{
					case "ALB":
                    case "LSF":
                    case "ACT":
                        tmp.LearnDelFAMCode = "1";
						break;
					default:
						break;
				}
				
				_FAMLList = GetLSFList(_learnerDelivery.LearningDeliveryFAMList);
				FamItemsCV = CollectionViewSource.GetDefaultView(_FAMLList as List<LearningDeliveryFAM>);
				tmp.IsSelected = true;
				FamItemsCV.MoveCurrentTo(tmp);
				FamItemsCV.Refresh();
				OnPropertyChanged("FamItemsCV");
				ShouldShowListView();
			}else
			{
				MessageBox.Show(String.Format("You must remove and item fron the {1} list first.{0}{0} You have {2} items in the list at this time.", Environment.NewLine, UserControlTitle, _FAMLList.Count())
															, "To many itams already."
															, MessageBoxButton.OK
															, MessageBoxImage.Information
															, MessageBoxResult.OK);
			}
		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			if (FamItemsCV.CurrentItem != null)
			{
				LearningDeliveryFAM fam2Remove = FamItemsCV.CurrentItem as LearningDeliveryFAM;
				if (fam2Remove != null)
				{
					_learnerDelivery.Delete(fam2Remove);
					_FAMLList.Remove(fam2Remove);
					if (!FamItemsCV.IsEmpty)
					{
						if (!FamItemsCV.MoveCurrentToPrevious())
						{
							FamItemsCV.MoveCurrentToFirst();
							FamItemsCV.Refresh();
							OnPropertyChanged("FamItemsCV");
						}
						if ((FamItemsCV.CurrentItem != null) && (FamItemsCV.CurrentItem != fam2Remove))
						{
							LearningDeliveryFAM f = FamItemsCV.CurrentItem as LearningDeliveryFAM;
							f.IsSelected = true;
						}
						else
						{
							FamItemsCV.MoveCurrentToNext();
							if (FamItemsCV.CurrentItem != null)
							{
								LearningDeliveryFAM f = FamItemsCV.CurrentItem as LearningDeliveryFAM;
								f.IsSelected = true;

							}
						}
					}
					FamItemsCV.Refresh();
				}
				OnPropertyChanged("FamItemsCV");
				ShouldShowListView();
			}
		}

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
