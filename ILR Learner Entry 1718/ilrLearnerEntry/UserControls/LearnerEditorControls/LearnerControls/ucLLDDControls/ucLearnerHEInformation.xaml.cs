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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucLearnerHEInformation.xaml
    /// </summary>
    public partial class ucLearnerHEInformation : UserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private const String CLASSNAME = "Learner";
        private ILR.Schema XmlSchema = new ILR.Schema();
        private Learner _learner;
        private String _hefincash = string.Empty;
        private String _hefinnearcash = string.Empty;
        private String _hefinother = string.Empty;
        private String _hefinaccommodationdiscounts = string.Empty;
        #endregion

        #region Constructor
        public ucLearnerHEInformation()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Public Properties
        public Learner CurrentItem
        {
            get { return _learner; }
            set
            {
                _hefincash = string.Empty;
                _hefinnearcash = string.Empty;
                _hefinother = string.Empty;
                _hefinaccommodationdiscounts = string.Empty;
                if (value != null)
                {
                    _learner = value;
                    _hefincash = value.HEFinCash.ToString();
                    _hefinnearcash = value.HEFinNearCash.ToString();
                    _hefinother = value.HEFinOther.ToString();
                    _hefinaccommodationdiscounts = value.HEFinAccommodationDiscounts.ToString();
                    this.DataContext = this;
                    OnPropertyChanged("HEFinCash");
                    OnPropertyChanged("HEFinNearCash");
                    OnPropertyChanged("HEFinAccommodationDiscounts");
                    OnPropertyChanged("HEFinOther");
                    OnPropertyChanged("CurrentItem");
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }
        public String HEFinCash
        {
            get { return _hefincash; }
            set
            {
                _hefincash = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinCash != null) CurrentItem.HEFinCash = null;
                }
                else
                {
                    int number;
                    bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                    if (result) { CurrentItem.HEFinCash = number; }
                }
                OnPropertyChanged("HEFinCash");
            }
        }
        public String HEFinNearCash
        {
            get { return _hefinnearcash; }
            set
            {
                _hefinnearcash = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinNearCash!=null) CurrentItem.HEFinNearCash = null;
                }
                else
                {
                    int number;
                    bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                    if (result) { CurrentItem.HEFinNearCash = number; }
                }
                OnPropertyChanged("HEFinNearCash");
            }
        }
        public String HEFinAccommodationDiscounts
        {
            get { return _hefinaccommodationdiscounts; }
            set
            {
                _hefinaccommodationdiscounts = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinAccommodationDiscounts != null) CurrentItem.HEFinAccommodationDiscounts = null;
                }
                else
                {
                    int number;
                    bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                    if (result) { CurrentItem.HEFinAccommodationDiscounts = number; }
                }
                OnPropertyChanged("HEFinAccommodationDiscounts");
            }
        }
        public String HEFinOther
        {
            get { return _hefinother; }
            set
            {
                _hefinother = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinOther != null) CurrentItem.HEFinOther = null;
                }
                else
                {
                    int number;
                    bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                    if (result) { CurrentItem.HEFinOther = number; }
                }
                OnPropertyChanged("HEFinOther");
            }
        }
        // Change from Number to String here to stopp GI missing blank string !!
        //public string UCASENum { get; set; }
        public DataTable TermTimeAccList { set; get; }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            OnPropertyChanged("CurrentItem");
            CurrentItem.RefreshData();
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

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                switch (columnName)
                {
                    case "HEFinCash":
                        if (HEFinCash != null && HEFinCash.Length > 0)
                        {
                            sReturn += CheckPropertyLength(HEFinCash, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(HEFinCash, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "HEFinNearCash":
                        if (HEFinNearCash != null && HEFinNearCash.Length > 0)
                        {
                            sReturn += CheckPropertyLength(HEFinNearCash, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(HEFinNearCash, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "HEFinOther":
                        if (HEFinOther != null && HEFinNearCash.Length > 0)
                        {
                            sReturn += CheckPropertyLength(HEFinOther, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(HEFinOther, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "HEFinAccommodationDiscounts":
                        if (HEFinAccommodationDiscounts != null && HEFinNearCash.Length > 0)
                        {
                            sReturn += CheckPropertyLength(HEFinAccommodationDiscounts, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(HEFinAccommodationDiscounts, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    default:
                        break;
                }
                return sReturn;
            }
        }
        public int GetItemSize(string ItemName)
        {
            return XmlSchema.GetMaxLength(ItemName);
        }
        public string CheckPropertyLength(object itemValue, string ClassName, string ItemName)
        {
            String ItemFullName = String.Format("{0}.{1}", ClassName, ItemName);
            int ItemSize = GetItemSize(ItemFullName);
            if (itemValue != null && itemValue.ToString().Length > ItemSize)
            {
                return String.Format("exceeds maximum length ({0} characters). Current length : {1}\r\n", ItemSize, itemValue.ToString().Length);
            }
            return null;
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

    }
}
