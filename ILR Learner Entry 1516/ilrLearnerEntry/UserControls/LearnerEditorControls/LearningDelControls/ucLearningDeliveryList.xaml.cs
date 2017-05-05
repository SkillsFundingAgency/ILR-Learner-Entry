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
    /// Interaction logic for ucLearningDeliveryList.xaml
    /// </summary>
    public partial class ucLearningDeliveryList : UserControl, INotifyPropertyChanged
    {
        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                LearningDeliveryItemControl.CurrentItem = e.AddedItems[0] as ILR.LearningDelivery;
                LearningDeliveryItemControl.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                LearningDeliveryItemControl.CurrentItem = null;
                LearningDeliveryItemControl.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #region Private Variables
        private Learner _learner;

        #endregion

        #region Constructor
        public ucLearningDeliveryList()
        {
            InitializeComponent();
            lv.Visibility = System.Windows.Visibility.Visible;
            LearningDeliveryItemControl.Visibility = System.Windows.Visibility.Collapsed;
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
                    OnPropertyChanged("CurrentItem");
                    LearningDeliveryItemsCV = CollectionViewSource.GetDefaultView(_learner.LearningDeliveryList);
                    LearningDeliveryItemsCV.Refresh();

                    if (_learner.LearningDeliveryList.Count > 0)
                    {
                        LearningDeliveryItemControl.Visibility = System.Windows.Visibility.Visible;
						if (LearningDeliveryItemsCV.CurrentItem !=null)
						{
							(LearningDeliveryItemsCV.CurrentItem as ILR.LearningDelivery).IsSelected = true;
						}
						
                    }
                    else
                    {
                        LearningDeliveryItemControl.CurrentItem = null;
                        LearningDeliveryItemControl.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    OnPropertyChanged("LearningDeliveryItemsCV");
                }
                else
                {
                    this.DataContext = null;
                    LearningDeliveryItemControl.Visibility = System.Windows.Visibility.Collapsed;
                }
				lv.UnselectAll();
            }
        }
        public ICollectionView LearningDeliveryItemsCV
        {
            get;
            private set;
        }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        private void AddNewLearningDelivery()
        {  
           LearningDelivery ld = _learner.CreateLearningDelivery();
           ld.IsSelected = true;
           LearningDeliveryItemsCV.MoveCurrentTo(ld);
            LearningDeliveryItemsCV.Refresh();
           OnPropertyChanged("LearningDeliveryItemsCV");
        }
        private void LearningDeliveryRemove_Click(object sender, RoutedEventArgs e)
        {
            RemoveLearningDeliveryRecord();
        }
        private void RemoveLearningDeliveryRecord()
        {
            if (LearningDeliveryItemsCV.CurrentItem != null)
            {
                ILR.LearningDelivery ld2Remove = LearningDeliveryItemsCV.CurrentItem as ILR.LearningDelivery;

                MessageBoxResult result = MessageBox.Show(String.Format("Are you sure you want to delete Learning Deliver Records ? {0}{0} Aim Seq : {1} {0} Aim Ref {2}", Environment.NewLine, ld2Remove.AimSeqNumber, ld2Remove.LearnAimRef)
                                                         , "Confirmation"
                                                         , MessageBoxButton.YesNo
                                                         , MessageBoxImage.Stop
                                                         , MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    if (ld2Remove != null)
                    {
                    	if (LearningDeliveryItemsCV.CurrentPosition == 0)
						{
							LearningDeliveryItemsCV.MoveCurrentToNext();
						}
						else if (_learner.LearningDeliveryList.Count()>0)
						{
							LearningDeliveryItemsCV.MoveCurrentToPrevious();
						}
					
						_learner.Delete(ld2Remove);					
                        LearningDelivery ldTmp = LearningDeliveryItemsCV.CurrentItem as LearningDelivery;
                        if (ldTmp != null)
                        {
                            ldTmp.IsSelected = true;
                        }
                    }
                }
            }
            LearningDeliveryItemsCV.Refresh();
            OnPropertyChanged("LearningDeliveryItemsCV");

        }

        private void LearningDeliveryAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNewLearningDelivery();
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
