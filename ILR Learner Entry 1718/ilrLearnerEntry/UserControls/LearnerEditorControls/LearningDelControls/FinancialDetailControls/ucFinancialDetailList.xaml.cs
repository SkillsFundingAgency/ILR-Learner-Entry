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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.FinancialDetailControls
{
	/// <summary>
	/// Interaction logic for ucTrailblazerList.xaml
	/// </summary>
	public partial class ucFinancialDetailList : UserControl, INotifyPropertyChanged
	{
		#region Private Variables
		private ILR.LearningDelivery _learningDelivery;
		private List<ILR.ApprenticeshipFinancialRecord> _apprenticeshipFinancialRecordList = new List<ILR.ApprenticeshipFinancialRecord>(0);
		#endregion

		public ucFinancialDetailList()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		#region Public Properties

		public LearningDelivery CurrentItem
		{
			get { return _learningDelivery; }
			set
			{
				_learningDelivery = value;
				if ((_learningDelivery == null) || (_learningDelivery.ApprenticeshipFinancialRecordList==null))
				{
					_apprenticeshipFinancialRecordList = null;
				}
				else
				{
					_apprenticeshipFinancialRecordList = GetTrailblazerList(_learningDelivery.ApprenticeshipFinancialRecordList);
					ApprenticeshipFinancialItemsCV = CollectionViewSource.GetDefaultView(_apprenticeshipFinancialRecordList as List<ILR.ApprenticeshipFinancialRecord>);
					//ApprenticeshipFinancialItemsCV.MoveCurrentToFirst();

					if (_apprenticeshipFinancialRecordList.Count > 0)
					{
						(ApprenticeshipFinancialItemsCV.CurrentItem as ILR.ApprenticeshipFinancialRecord).IsSelected = true;
					}
					ApprenticeshipFinancialItemsCV.Refresh();
				}
				OnPropertyChanged("CurrentItem");
				OnPropertyChanged("ApprenticeshipFinancialItemsCV");
				ShouldShowListView();
			}
		}
		public ICollectionView ApprenticeshipFinancialItemsCV
        {
			get;
			private set;
		}

        public DataTable FinTypeList { set; get; }
        public DataTable FinCodetList_TNP { set; get; }
        public DataTable FinCodetList_PMR { set; get; }
        #endregion

        #region PRIVATE Methods
        private List<ILR.ApprenticeshipFinancialRecord> GetTrailblazerList(List<ILR.ApprenticeshipFinancialRecord> inputlist)
		{
			return inputlist.ToList();
		}
		private void ShouldShowListView()
		{
			if ((_learningDelivery == null) || (_learningDelivery.ApprenticeshipFinancialRecordList.Count == 0))
			{
                LDFinancialDetailControl.Visibility = System.Windows.Visibility.Collapsed;
			}
			else if (_learningDelivery.ApprenticeshipFinancialRecordList.Count>0)
			{
                LDFinancialDetailControl.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
                LDFinancialDetailControl.Visibility = System.Windows.Visibility.Visible;
			}
			lv.Visibility = System.Windows.Visibility.Visible;
		}

		private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
                LDFinancialDetailControl.CurrentItem = e.AddedItems[0] as ApprenticeshipFinancialRecord;
			}
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{			   
			ApprenticeshipFinancialRecord tmp = _learningDelivery.CreateApprenticeshipFinancialRecord();
			_apprenticeshipFinancialRecordList = _learningDelivery.ApprenticeshipFinancialRecordList;
			ApprenticeshipFinancialItemsCV = CollectionViewSource.GetDefaultView(_learningDelivery.ApprenticeshipFinancialRecordList as List<ILR.ApprenticeshipFinancialRecord>);
			ApprenticeshipFinancialItemsCV.MoveCurrentTo(tmp);
			ApprenticeshipFinancialItemsCV.Refresh();
			OnPropertyChanged("ApprenticeshipFinancialItemsCV");
			ShouldShowListView();
		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			if (ApprenticeshipFinancialItemsCV.CurrentItem != null)
			{
				ILR.ApprenticeshipFinancialRecord les2Remove = ApprenticeshipFinancialItemsCV.CurrentItem as ILR.ApprenticeshipFinancialRecord;

				if (les2Remove != null)
				{
					_learningDelivery.Delete(les2Remove);
					_apprenticeshipFinancialRecordList.Remove(les2Remove);

					if (!ApprenticeshipFinancialItemsCV.MoveCurrentToPrevious())
					{
						ApprenticeshipFinancialItemsCV.MoveCurrentToFirst();
						ApprenticeshipFinancialItemsCV.Refresh();
						OnPropertyChanged("ApprenticeshipFinancialItemsCV");
					}
					if ((ApprenticeshipFinancialItemsCV.CurrentItem != null) && (ApprenticeshipFinancialItemsCV.CurrentItem != les2Remove))
					{
						ILR.ApprenticeshipFinancialRecord f = ApprenticeshipFinancialItemsCV.CurrentItem as ILR.ApprenticeshipFinancialRecord;
						f.IsSelected = true;

					}
					else
					{
						ApprenticeshipFinancialItemsCV.MoveCurrentToNext();
						if (ApprenticeshipFinancialItemsCV.CurrentItem != null)
						{
							ILR.ApprenticeshipFinancialRecord f = ApprenticeshipFinancialItemsCV.CurrentItem as ILR.ApprenticeshipFinancialRecord;
							f.IsSelected = true;

						}
					}
				}
				ApprenticeshipFinancialItemsCV.Refresh();
			}
			OnPropertyChanged("ApprenticeshipFinancialItemsCV");
			ShouldShowListView();

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
