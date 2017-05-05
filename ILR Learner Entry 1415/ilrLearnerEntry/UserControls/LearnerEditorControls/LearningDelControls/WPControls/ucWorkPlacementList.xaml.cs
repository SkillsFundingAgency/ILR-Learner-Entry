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


namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.WorkPlacementControls
{
	public partial class ucWorkPlacementList : UserControl, INotifyPropertyChanged
	{

		private LearningDelivery _learnerDelivery;

		#region Constructor
		public ucWorkPlacementList()
		{
			InitializeComponent();
		}
		#endregion

		#region Public Properties
		public LearningDelivery CurrentItem
		{
			get { return _learnerDelivery; }
			set
			{
				if (value != null)
				{
					_learnerDelivery = value;
					this.DataContext = this;
					WorkPlacementItemsCV = CollectionViewSource.GetDefaultView(_learnerDelivery.LearningDeliveryWorkPlacementList as List<LearningDeliveryWorkPlacement>);
					OnPropertyChanged("CurrentItem");
					if (_learnerDelivery.LearningDeliveryWorkPlacementList.Count > 0)
					{
						(WorkPlacementItemsCV.CurrentItem as ILR.LearningDeliveryWorkPlacement).IsSelected = true;
					}

					WorkPlacementItemsCV.MoveCurrentToFirst();
					WorkPlacementItemsCV.Refresh();
					OnPropertyChanged("CurrentItem");
				}
				else
				{
					this.DataContext = null;
				}
				OnPropertyChanged("CurrentItem");
				OnPropertyChanged("WorkPlacementItemsCV");
				ShouldShowListView();
			}
		}
		public ICollectionView WorkPlacementItemsCV
		{
			get;
			private set;
		}
		#endregion

		#region PRIVATE Methods
		private void ShouldShowListView()
		{
			if ((_learnerDelivery == null) || (_learnerDelivery.LearningDeliveryWorkPlacementList.Count == 0))
			{
				WorkPlacementItemControl.Visibility = System.Windows.Visibility.Collapsed;
			}
			else if (_learnerDelivery.LearningDeliveryWorkPlacementList.Count == 1)
			{
				WorkPlacementItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				WorkPlacementItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			lv.Visibility = System.Windows.Visibility.Visible;
		}

		private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				WorkPlacementItemControl.CurrentItem = e.AddedItems[0] as LearningDeliveryWorkPlacement;
			}
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			LearningDeliveryWorkPlacement tmp = _learnerDelivery.CreateLearningDeliveryWorkPlacement();
			WorkPlacementItemsCV.MoveCurrentTo(tmp);
			WorkPlacementItemsCV.Refresh();
			OnPropertyChanged("WorkPlacementItemsCV");
			ShouldShowListView();
		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{

			if (WorkPlacementItemsCV.CurrentItem != null)
			{
				LearningDeliveryWorkPlacement lr2Remove = WorkPlacementItemsCV.CurrentItem as LearningDeliveryWorkPlacement;
				if (lr2Remove != null)
				{
					_learnerDelivery.Delete(lr2Remove);
					if ((_learnerDelivery != null) && (_learnerDelivery.LearningDeliveryWorkPlacementList != null) && (_learnerDelivery.LearningDeliveryWorkPlacementList.Count>0))
					{

						if (!WorkPlacementItemsCV.MoveCurrentToPrevious())
						{
							WorkPlacementItemsCV.MoveCurrentToFirst();
						}
						if ((WorkPlacementItemsCV.CurrentItem != null) && (WorkPlacementItemsCV.CurrentItem != lr2Remove))
						{
							LearningDeliveryWorkPlacement lr = WorkPlacementItemsCV.CurrentItem as LearningDeliveryWorkPlacement;
							lr.IsSelected = true;

						}
						else
						{
							WorkPlacementItemsCV.MoveCurrentToNext();
							if (WorkPlacementItemsCV.CurrentItem != null)
							{
								LearningDeliveryWorkPlacement lr = WorkPlacementItemsCV.CurrentItem as LearningDeliveryWorkPlacement;
								lr.IsSelected = true;

							}
						}
					}
					WorkPlacementItemsCV.Refresh();
					OnPropertyChanged("WorkPlacementItemsCV");
					ShouldShowListView();
				}
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
