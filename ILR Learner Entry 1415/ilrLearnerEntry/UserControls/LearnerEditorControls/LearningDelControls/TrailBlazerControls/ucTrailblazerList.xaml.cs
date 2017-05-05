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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.TrailBlazerControls
{
	/// <summary>
	/// Interaction logic for ucTrailblazerList.xaml
	/// </summary>
	public partial class ucTrailblazerList : UserControl, INotifyPropertyChanged
	{
		#region Private Variables
		private ILR.LearningDelivery _learningDelivery;
		private List<ILR.ApprenticeshipTrailblazerFinancialDetails> _TrailBlazerList = new List<ILR.ApprenticeshipTrailblazerFinancialDetails>(0);

		#endregion

		public ucTrailblazerList()
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
				if ((_learningDelivery == null) || (_learningDelivery.ApprenticeshipTrailblazerFinancialDetailsList==null))
				{
					_TrailBlazerList = null;
				}
				else
				{
					_TrailBlazerList = GetTrailblazerList(_learningDelivery.ApprenticeshipTrailblazerFinancialDetailsList);
					TrailblazerItemsCV = CollectionViewSource.GetDefaultView(_TrailBlazerList as List<ILR.ApprenticeshipTrailblazerFinancialDetails>);
					TrailblazerItemsCV.MoveCurrentToFirst();

					if (_TrailBlazerList.Count > 0)
					{
						(TrailblazerItemsCV.CurrentItem as ILR.ApprenticeshipTrailblazerFinancialDetails).IsSelected = true;
					}
					TrailblazerItemsCV.Refresh();
				}
				OnPropertyChanged("CurrentItem");
				OnPropertyChanged("TrailblazerItemsCV");
				ShouldShowListView();
			}
		}
		public ICollectionView TrailblazerItemsCV
		{
			get;
			private set;
		}
		#endregion

		#region PRIVATE Methods
		private List<ILR.ApprenticeshipTrailblazerFinancialDetails> GetTrailblazerList(List<ILR.ApprenticeshipTrailblazerFinancialDetails> inputlist)
		{
			return inputlist.ToList();
		}
		private void ShouldShowListView()
		{
			if ((_learningDelivery == null) || (_learningDelivery.ApprenticeshipTrailblazerFinancialDetailsList.Count == 0))
			{
				TrlblazItemControl.Visibility = System.Windows.Visibility.Collapsed;
			}
			else if (_learningDelivery.ApprenticeshipTrailblazerFinancialDetailsList.Count>0)
			{
				TrlblazItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				TrlblazItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			lv.Visibility = System.Windows.Visibility.Visible;
		}

		private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				TrlblazItemControl.CurrentItem = e.AddedItems[0] as ApprenticeshipTrailblazerFinancialDetails;
			}
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			   
			ApprenticeshipTrailblazerFinancialDetails tmp = _learningDelivery.CreateApprenticeshipTrailblazerFinancialDetails();

			_TrailBlazerList = _learningDelivery.ApprenticeshipTrailblazerFinancialDetailsList;
			TrailblazerItemsCV = CollectionViewSource.GetDefaultView(_learningDelivery.ApprenticeshipTrailblazerFinancialDetailsList as List<ILR.ApprenticeshipTrailblazerFinancialDetails>);
			TrailblazerItemsCV.MoveCurrentTo(tmp);
			TrailblazerItemsCV.Refresh();
			OnPropertyChanged("TrailblazerItemsCV");
			ShouldShowListView();
		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			if (TrailblazerItemsCV.CurrentItem != null)
			{
				ILR.ApprenticeshipTrailblazerFinancialDetails les2Remove = TrailblazerItemsCV.CurrentItem as ILR.ApprenticeshipTrailblazerFinancialDetails;

				if (les2Remove != null)
				{
					_learningDelivery.Delete(les2Remove);
					_TrailBlazerList.Remove(les2Remove);


					if (!TrailblazerItemsCV.MoveCurrentToPrevious())
					{
						TrailblazerItemsCV.MoveCurrentToFirst();
						TrailblazerItemsCV.Refresh();
						OnPropertyChanged("TrailblazerItemsCV");
					}
					if ((TrailblazerItemsCV.CurrentItem != null) && (TrailblazerItemsCV.CurrentItem != les2Remove))
					{
						ILR.ApprenticeshipTrailblazerFinancialDetails f = TrailblazerItemsCV.CurrentItem as ILR.ApprenticeshipTrailblazerFinancialDetails;
						f.IsSelected = true;

					}
					else
					{
						TrailblazerItemsCV.MoveCurrentToNext();
						if (TrailblazerItemsCV.CurrentItem != null)
						{
							ILR.ApprenticeshipTrailblazerFinancialDetails f = TrailblazerItemsCV.CurrentItem as ILR.ApprenticeshipTrailblazerFinancialDetails;
							f.IsSelected = true;

						}
					}
				}
				TrailblazerItemsCV.Refresh();
			}
			OnPropertyChanged("TrailblazerItemsCV");
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
