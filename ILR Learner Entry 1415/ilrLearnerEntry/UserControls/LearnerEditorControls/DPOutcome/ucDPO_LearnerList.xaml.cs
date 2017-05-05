using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.DPOutcomeControls
{    /// <summary>
     /// Interaction logic for ucDPO_LearnerList.xaml
     /// </summary>
    public partial class ucDPO_LearnerList : UserControl, INotifyPropertyChanged
	{

		#region Private Variables
		private String _learnerfilterString = string.Empty;
		#endregion

		#region Constructor
		public ucDPO_LearnerList()
		{
			InitializeComponent();

			LearnerDPDetailGrid.Visibility = System.Windows.Visibility.Hidden;
			SetupLookups();

		}
		#endregion
		#region Public Properties
		public ICollectionView LearnerDPList
		{
			get;
			private set;
		}
		private bool LearnerFilter(object item)
		{
			LearnerDestinationandProgression learner = item as LearnerDestinationandProgression;
			if (!string.IsNullOrEmpty(_learnerfilterString))
			{
				bool bReturn = false;
				if ((learner.ULN != null) && (learner.ULN.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
				if ((learner.LearnRefNumber != null) && (learner.LearnRefNumber.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
				return bReturn;
			}
			else
			{
				return true;
			}
		}
		public string LearnerFilterString
		{
			get { return _learnerfilterString; }
			set
			{
				_learnerfilterString = value;
				OnPropertyChanged("LearnerFilterString");
				if (LearnerDPList != null)
				{
					LearnerDPList.Refresh();
					OnPropertyChanged("LearnerDPList");
				}
			}
		}

		#endregion

		#region Private Methods
		private void SetupListData()
		{
			LearnerDPList = null;
			if (App.ILRMessage.LearnerDestinationandProgressionCount > 0)
			{
				LearnerDPList = CollectionViewSource.GetDefaultView(App.ILRMessage.LearnerDestinationandProgressionList);
				this.DataContext = this;
				LearnerDPList.Filter = LearnerFilter;
				LearnerDPList.CurrentChanged += LearnerDPList_CurrentChanged;
				if (App.ILRMessage.LearnerDestinationandProgressionList.Count > 0)
				{
					(LearnerDPList.CurrentItem as ILR.LearnerDestinationandProgression).IsSelected = true;
				}
				LearnerDPList.Refresh();
				OnPropertyChanged("LearnerDPList");

			}
			else
			{
				this.DataContext = null;
				if (LearnerDPList != null)
				{ 
					LearnerDPList.CurrentChanged -= LearnerDPList_CurrentChanged; 
				}
				OnPropertyChanged("LearnerDPList");
				LearnerDPList = null;

			}
			ShowOrHideChildControls();
		}
		private void ShowOrHideChildControls()
		{
			if (App.ILRMessage.LearnerDestinationandProgressionCount > 0)
			{
				LearnerDPDetailGrid.Visibility = System.Windows.Visibility.Visible;
				SetSubControl(LearnerDPList.CurrentItem as LearnerDestinationandProgression);
			}
			else
			{
				LearnerDPDetailGrid.Visibility = System.Windows.Visibility.Hidden;
			}

		}
		private void LearnerDPList_CurrentChanged(object sender, EventArgs e)
		{
			SetSubControl(LearnerDPList.CurrentItem as LearnerDestinationandProgression);
		}

		private void SetSubControl(LearnerDestinationandProgression learnerDp)
		{
			if (LearnerDPList.CurrentItem != null)
			{
				LearnerDPDetailGrid.Visibility = System.Windows.Visibility.Visible;
				LearnerOutcomeListControl.CurrentItem = learnerDp;
			}
			else
			{
				LearnerOutcomeListControl.CurrentItem = null;
				LearnerDPDetailGrid.Visibility = System.Windows.Visibility.Hidden;
			}
		}

		private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
		private void AddLearner_Click(object sender, RoutedEventArgs e)
		{
			LearnerDestinationandProgression NewLr = App.ILRMessage.CreateLearnerDestinationandProgression();
			if (App.ILRMessage.LearnerDestinationandProgressionCount == 1)
			{
				SetupListData();
			}
			else
			{
				ShowOrHideChildControls();
			}
			NewLr.IsSelected = true;
			if (LearnerDPList.CurrentItem != NewLr)
			{
				LearnerDPList.MoveCurrentTo(NewLr);
			}
			LearnerDPList.Refresh();
			OnPropertyChanged("LearnerDPList");
		}
		private void RemoveLearner_Click(object sender, RoutedEventArgs e)
		{
			if (LearnerDPList.CurrentItem != null)
			{
				LearnerDestinationandProgression ldp2Remove = LearnerDPList.CurrentItem as LearnerDestinationandProgression;
				if (ldp2Remove != null)
				{
					MessageBoxResult result = MessageBox.Show(String.Format("Are you sure you want to delete {0} Learner Destinationand Progression Record {0}{0} Learner Ref - {2} {0} ULN : {1}", Environment.NewLine, ldp2Remove.LearnRefNumber, ldp2Remove.ULN)
															, "Confirmation"
															, MessageBoxButton.YesNo
															, MessageBoxImage.Question
															, MessageBoxResult.No);
					if (result == MessageBoxResult.Yes)
					{
						App.ILRMessage.Delete(ldp2Remove); 
						if (!LearnerDPList.IsEmpty)
						{

							if (!LearnerDPList.MoveCurrentToPrevious())
							{
								LearnerDPList.MoveCurrentToFirst();
								LearnerDPList.Refresh();
								OnPropertyChanged("LearnerDPList");
							}
							if ((LearnerDPList.CurrentItem != null) && (LearnerDPList.CurrentItem != ldp2Remove))
							{
								LearnerDestinationandProgression lr = LearnerDPList.CurrentItem as LearnerDestinationandProgression;
								lr.IsSelected = true;

							}
							else
							{
								LearnerDPList.MoveCurrentToNext();
								if (LearnerDPList.CurrentItem != null)
								{
									LearnerDestinationandProgression lr = LearnerDPList.CurrentItem as LearnerDestinationandProgression;
									lr.IsSelected = true;

								}
							}

						}
						LearnerDPList.Refresh();
						OnPropertyChanged("LearnerDPList");
					}
				}
			}
		}
		private void SetupLookups()
		{
			ILR.Lookup lp = new ILR.Lookup();
			LearnerOutcomeListControl.OutcomeDetailControl.OutcomeTypeList = lp.GetLookup("OutType");
		}
		#endregion

		public void UpdateChildControlAsNewDataLoaded()
		{
			SetupListData();
			DataItemListBox.UnselectAll();
		}

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

		private void SaveLearner_Click(object sender, RoutedEventArgs e)
		{
			App.ILRMessage.Save();
		}

	}
}
