
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
    /// <summary>
    /// Interaction logic for ucWorkPlacementItem.xaml
    /// </summary>
    public partial class ucWorkPlacementItem : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        private const String CLASSNAME = "LearningDeliveryWorkPlacement";
        private ILR.Schema XmlSchema = new ILR.Schema();

        private LearningDeliveryWorkPlacement _learningDeliveryWorkPlacement;
        private string _workplaceempid = string.Empty;
        private string _workPlaceHours = string.Empty;

        #region Constructor
        public ucWorkPlacementItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Public Properties
        public LearningDeliveryWorkPlacement CurrentItem
        {
            get { return _learningDeliveryWorkPlacement; }
            set
            {
                if (value != null)
                {
                    _learningDeliveryWorkPlacement = value;
                    WorkPlaceEmpId = _learningDeliveryWorkPlacement.WorkPlaceEmpId.ToString();
                    WorkPlaceHours = _learningDeliveryWorkPlacement.WorkPlaceHours;
                    this.DataContext = this;
                    OnPropertyChanged("CurrentItem");
                    OnPropertyChanged("WorkPlaceEmpId");
                    OnPropertyChanged("WorkPlaceHours");
                    OnPropertyChanged("WorkplacementCodeList");
                    _learningDeliveryWorkPlacement.Refresh();
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }
        public string WorkPlaceEmpId
        {
            get { return _workplaceempid; }
            set
            {
                _workplaceempid = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                {
                    CurrentItem.WorkPlaceEmpId = number;
                }
                _learningDeliveryWorkPlacement.Refresh();
            }
        }
        public string WorkPlaceHours
        {
            get { return _workPlaceHours; }
            set
            {
                _workPlaceHours = value;
                //int number;
                //bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                //if (result)
                //{
                    CurrentItem.WorkPlaceHours = value;
                //}

                _learningDeliveryWorkPlacement.Refresh();
            }
        }
        public DataTable WorkplacementCodeList { set; get; }

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
                        case "WorkPlaceEmpId":
                            if (WorkPlaceEmpId != null && WorkPlaceEmpId.Length > 0)
                            {
                                sReturn += CheckPropertyLength(WorkPlaceEmpId, CLASSNAME, columnName);
                                int number;
                                bool result = Int32.TryParse(WorkPlaceEmpId, out number);
                                if (!result)
                                {
                                    sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                                }
                            }
                            break;
                        case "WorkPlaceHours":
                            if (WorkPlaceHours != null && WorkPlaceHours.Length > 0)
                            {
                                sReturn += CheckPropertyLength(WorkPlaceHours, CLASSNAME, columnName);
                                int number;
                                bool result = Int32.TryParse(WorkPlaceHours, out number);
                                if (!result)
                                {
                                    sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                                }
                            }
                            else if (WorkPlaceHours == null || WorkPlaceHours.ToString().Length == 0)
                                return "Work Place Hours is required\r\n";
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
