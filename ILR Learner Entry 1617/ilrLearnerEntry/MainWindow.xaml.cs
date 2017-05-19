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

namespace ilrLearnerEntry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region routed commands
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            App.Log("MainWindow"," Constructor", "Start");
            HomeScreenControl.OnNewFileImported += HomeScreenControl_OnNewFileImported;
            HomeScreenControl.OnUkprnUpdated += HomeScreenControl_OnUkprnUpdated;
            this.DataContext = this;
            OnPropertyChanged("WindowTitle");
            if (App.ILRMessage.LearnerCount > 0 || App.ILRMessage.LearnerDestinationandProgressionCount > 0)
            {
                App.Log("MainWindow", " Constructor", "Update Child Controls");
                UpdateChildControlAsNewDataLoaded();
            }
            else
            {
                HomeScreenControl.RefreshData();
            }
            App.Log("MainWindow", " Constructor", "End");
        }
        #endregion

        #region Public Properties
        public string WindowTitle
        {
            get
            {
                if (App.ILRMessage.LearningProvider.UKPRN != null)
                {
                    return String.Format("{0} - UKPRN : {1}", App.ApplicationName, App.ILRMessage.LearningProvider.UKPRN.ToString());				
                }
                else
                {
                    return App.ApplicationName;
                }
            }
        }
        #endregion

        #region Public Methods
        public void UpdateChildControlAsNewDataLoaded()
        {
            LearnersControl.UpdateChildControlAsNewDataLoaded();
            DPOutcomeControl.UpdateChildControlAsNewDataLoaded();
            HomeScreenControl.RefreshData();
        }
        #endregion

        #region PRIVATE Methods
        private void HomeScreenControl_OnNewFileImported(object sender, RoutedEventArgs e)
        {
            UpdateChildControlAsNewDataLoaded();
        }
        public void HomeScreenControl_OnUkprnUpdated(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("WindowTitle");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (App.ILRMessage != null)
            {
                App.ILRMessage.Save();
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
