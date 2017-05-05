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
    /// Interaction logic for ucLearningStartInformation.xaml
    /// </summary>
    public partial class ucLearningStartInformation : UserControl, INotifyPropertyChanged
    {


        #region Private Variables
        private ILR.LearningDelivery _learningDelivery;

        #endregion

        #region Constructor
        public ucLearningStartInformation()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties

        public ILR.LearningDelivery CurrentItem
        {
            get { return _learningDelivery; }
            set
            {
                if (value != null)
                {
                    _learningDelivery = value;
                    this.DataContext = this;
                }
                else
                {
                    this.DataContext = null;
                }
                OnPropertyChanged("AimTypeList");
                OnPropertyChanged("FundModelList");
                OnPropertyChanged("PriorLearnFundAdj");
                OnPropertyChanged("ProgType");
                OnPropertyChanged("FworkCode");
                OnPropertyChanged("PwayCode");
                OnPropertyChanged("OtherFundAdj");
                OnPropertyChanged("ProgTypeList");
                OnPropertyChanged("CurrentItem");
            }
        }

        public DataTable AimTypeList { set; get; }
        public DataTable FundModelList { set; get; }
        public DataTable ProgTypeList { set; get; }

        public string PriorLearnFundAdj
        {
            get
            {
                if (CurrentItem == null || CurrentItem.PriorLearnFundAdj == null)
                { return string.Empty; }
                else
                { return CurrentItem.PriorLearnFundAdj.ToString(); }
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.PriorLearnFundAdj = null;
                }
                else
                {
                    try
                    {
                        int number = Int32.Parse(value);
                        CurrentItem.PriorLearnFundAdj = number;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show(String.Format("Value Entered is not a number : {0}", value)
                                                          , "Not a Number"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show(String.Format("Value to big : {0}", value)
                                                          , "Overflow"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                }
                OnPropertyChanged("PriorLearnFundAdj");
            }
        }
        public string ProgType
        {
            get
            {
                if (CurrentItem == null || CurrentItem.ProgType == null)
                { return string.Empty; }
                else
                { return CurrentItem.ProgType.ToString(); }
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.ProgType = null;
                }
                else
                {
                    try
                    {
                        int number = Int32.Parse(value);
                        CurrentItem.ProgType = number;
                    }
                    catch (FormatException)
                    {
                         MessageBox.Show(String.Format("Value Entered is not a number : {0}", value)
                                                          , "Not a Number"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                    catch (OverflowException)
                    {
                         MessageBox.Show(String.Format("Value to big : {0}", value)
                                                          , "Overflow"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                }
                OnPropertyChanged("ProgType");
            }
        }
        public string FworkCode
        {
            get
            {
                if (CurrentItem == null || CurrentItem.FworkCode == null)
                { return string.Empty; }
                else
                { return CurrentItem.FworkCode.ToString(); }
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.FworkCode = null;
                }
                else
                {
                    try
                    {
                        int number = Int32.Parse(value);
                        CurrentItem.FworkCode = number;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show(String.Format("Value Entered is not a number : {0}", value)
                                                          , "Not a Number"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                    catch (OverflowException)
                    {
                       MessageBox.Show(String.Format("Value to big : {0}", value)
                                                          , "Overflow"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                }
                OnPropertyChanged("FworkCode");
            }
        }
        public string PwayCode
        {
            get
            {
                if (CurrentItem == null || CurrentItem.PwayCode == null)
                { return string.Empty; }
                else
                { return CurrentItem.PwayCode.ToString(); }
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.PwayCode = null;
                }
                else
                {
                    try
                    {
                        int number = Int32.Parse(value);
                        CurrentItem.PwayCode = number;
                    }
                    catch (FormatException)
                    {
                         MessageBox.Show(String.Format("Value Entered is not a number : {0}", value)
                                                          , "Not a Number"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show(String.Format("Value to big : {0}", value)
                                                          , "Overflow"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                }
                OnPropertyChanged("PwayCode");
            }
        }
        public string OtherFundAdj
        {
            get
            {
                if (CurrentItem == null || CurrentItem.OtherFundAdj == null)
                { return string.Empty; }
                else
                { return CurrentItem.OtherFundAdj.ToString(); }
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.FworkCode = null;
                }
                else
                {
                    try
                    {
                        int number = Int32.Parse(value);
                        CurrentItem.OtherFundAdj = number;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show(String.Format("Value Entered is not a number : {0}", value)
                                                          , "Not a Number"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show(String.Format("Value to big : {0}", value)
                                                          , "Overflow"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Question);
                    }
                }
                OnPropertyChanged("OtherFundAdj");
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
