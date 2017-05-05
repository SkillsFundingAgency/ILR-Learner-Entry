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

namespace ilrLearnerEntry.UserControls.EmploymentStatus
{
    /// <summary>
    /// Interaction logic for ucEmploymentStatusList.xaml
    /// </summary>
    public partial class ucEmploymentStatusList : UserControl, INotifyPropertyChanged
    {
        private Learner _learner;

        #region Constructor
        public ucEmploymentStatusList()
        {
            InitializeComponent();
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
                }
                else
                {
                    this.DataContext = null;
                }
                EmploymentStatusItemsCV = CollectionViewSource.GetDefaultView(_learner.LearnerEmploymentStatusList as List<LearnerEmploymentStatus>);
				if (_learner.LearnerEmploymentStatusList.Count > 0)
				{
					(EmploymentStatusItemsCV.CurrentItem as ILR.LearnerEmploymentStatus).IsSelected = true;
				}
                EmploymentStatusItemsCV.MoveCurrentToFirst();
                EmploymentStatusItemsCV.Refresh();
                OnPropertyChanged("CurrentItem");
                OnPropertyChanged("EmploymentStatusItemsCV");
                ShouldShowListView();
            }
        }
        public ICollectionView EmploymentStatusItemsCV
        {
            get;
            private set;
        }
        #endregion

        #region PRIVATE Methods
        private void ShouldShowListView()
        {
            if (_learner.LearnerEmploymentStatusList.Count == 0)
            {
                EmpStausItemControl.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (_learner.LearnerEmploymentStatusList.Count == 1)
            {
                EmpStausItemControl.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                EmpStausItemControl.Visibility = System.Windows.Visibility.Visible;
            }
            lv.Visibility = System.Windows.Visibility.Visible;
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                EmpStausItemControl.CurrentItem = e.AddedItems[0] as LearnerEmploymentStatus;
            }
        }

        private void  Add_Click(object sender, RoutedEventArgs e)
        {
            LearnerEmploymentStatus tmp = _learner.CreateLearnerEmploymentStatus();
            EmploymentStatusItemsCV.Refresh();
            OnPropertyChanged("EmploymentStatusItemsCV");
            EmploymentStatusItemsCV.MoveCurrentTo(tmp);
            ShouldShowListView();
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (EmploymentStatusItemsCV.CurrentItem != null)
            {
                ILR.LearnerEmploymentStatus les2Remove = EmploymentStatusItemsCV.CurrentItem as ILR.LearnerEmploymentStatus;
                if (les2Remove != null)
                {
                    _learner.Delete(les2Remove);
                    EmploymentStatusItemsCV.MoveCurrentToPrevious();
                    LearnerEmploymentStatus ldTmp = EmploymentStatusItemsCV.CurrentItem as LearnerEmploymentStatus;
                    if (ldTmp != null)
                    {
                        ldTmp.IsSelected = true;
                    }
                }
            }
            EmploymentStatusItemsCV.Refresh();
            OnPropertyChanged("EmploymentStatusItemsCV");
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
