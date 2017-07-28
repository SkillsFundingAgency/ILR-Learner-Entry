
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
    /// Interaction logic for ucApprenticeshipFinancialRecordItem.xaml
    /// </summary>
    public partial class ucFinancialDetailtem : UserControl, INotifyPropertyChanged
    {
        private const String CLASSNAME = "ApprenticeshipFinancialRecord";
        private ILR.Schema XmlSchema = new ILR.Schema();

        private ApprenticeshipFinancialRecord _item;
        private string _tbfincode = string.Empty;
        private string _tbfinamount = string.Empty;

        public ucFinancialDetailtem()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region Public Properties
        public ApprenticeshipFinancialRecord CurrentItem
        {
            get { return _item; }
            set
            {
                if (value != null)
                {
                    _item = value;
                    this.DataContext = this;
                }
                else
                {
                    this.DataContext = null;
                }
                OnPropertyChanged("AFinCodetList");
                OnPropertyChanged("AFinTypeList");
                OnPropertyChanged("AFinCode");
                OnPropertyChanged("AFinAmount");
                OnPropertyChanged("CurrentItem");
            }
        }
        public string AFinCode
        {
            get { return _tbfincode; }
            set
            {
                _tbfincode = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                {
                    CurrentItem.AFinCode = number;
                }
            }
        }
        public string AFinAmount
        {
            get { return _tbfinamount; }
            set
            {
                _tbfinamount = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                {
                    CurrentItem.AFinAmount = number;
                }
            }
        }
        public DataTable AFinTypeList { set; get; }
        public DataTable AFinCodetList { set; get; }
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

        #region IDataErrorInfo Members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                if (CurrentItem != null)
                {
                    switch (columnName)
                    {
                        case "AFinCode":
                            if (AFinCode != null && AFinCode.Length > 0)
                            {
                                sReturn += CheckPropertyLength(AFinCode, CLASSNAME, columnName);
                                int number;
                                bool result = Int32.TryParse(AFinCode, out number);
                                if (!result)
                                {
                                    sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                                }
                            }
                            break;
                        case "AFinAmount":
                            if (AFinAmount != null && AFinAmount.Length > 0)
                            {
                                sReturn += CheckPropertyLength(AFinAmount, CLASSNAME, columnName);
                                int number;
                                bool result = Int32.TryParse(AFinAmount, out number);
                                if (!result)
                                {
                                    sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                                }
                            }
                            break;
                        default:
                            break;
                    }
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
        #endregion


    }
}
