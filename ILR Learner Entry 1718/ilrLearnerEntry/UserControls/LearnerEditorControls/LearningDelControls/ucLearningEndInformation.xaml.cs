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
    /// Interaction logic for ucLearningEndInformation.xaml
    /// </summary>
    public partial class ucLearningEndInformation : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables
        private const String CLASSNAME = "LearningDelivery";
        private ILR.Schema XmlSchema = new ILR.Schema();
        private ILR.LearningDelivery _learningDelivery;        
        #endregion

        #region Constructor
        public ucLearningEndInformation()
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
                dtLearnActEndDate.SelectedDateChanged -= dtLearnActEndDate_SelectedDateChanged;
                dtAchDate.SelectedDateChanged -= dtAchDate_SelectedDateChanged;
                _learningDelivery = value;
                if (value != null)
                {
                    this.DataContext = this;
                    Refresh();
                    dtLearnActEndDate.SelectedDateChanged += dtLearnActEndDate_SelectedDateChanged;
                    dtAchDate.SelectedDateChanged += dtAchDate_SelectedDateChanged;

                }
                else
                {
                    this.DataContext = null;
                }
            }
        }
        public DateTime? LearnActEndDate
        {
            get { return _learningDelivery == null ? null : _learningDelivery.LearnActEndDate; }
            set
            {
                DateTime? dtRetunr = null;
                if (value != null)
                {
                    DateTime dt;
                    bool result = DateTime.TryParse(System.Convert.ToString(value), out dt);
                    if (result && (dt != null))
                    {
                        dtRetunr = dt;
                    }
                    else
                    {
                        dtRetunr = null;
                    }
                }
                if (_learningDelivery!= null && _learningDelivery.LearnActEndDate != dtRetunr)
                {
                    _learningDelivery.LearnActEndDate = dtRetunr;
                }
                OnPropertyChanged("LearnActEndDate");
                OnPropertyChanged("LearnActEndDateDisplayDate");
            }
        }
        public string LearnActEndDateDisplayDate
        {
            get { return LearnActEndDate==null ? string.Empty : Convert.ToDateTime(LearnActEndDate).ToString("dd-MM-yyyy"); }
        }
        public DateTime? AchDate
        {
            get { return  _learningDelivery == null ? null : _learningDelivery.AchDate; ; }
            set
            {
                DateTime? dtRetunr = null;
                if (value != null)
                {
                    DateTime dt;
                    bool result = DateTime.TryParse(System.Convert.ToString(value), out dt);
                    if (result && (dt != null))
                    {
                        dtRetunr = dt;
                    }
                    else
                    {
                        dtRetunr = null;
                    }
                }
                if (_learningDelivery.AchDate != dtRetunr)
                {
                    _learningDelivery.AchDate = dtRetunr;
                }
                OnPropertyChanged("AchDate");
                OnPropertyChanged("AchDateDisplayDate");                
            }
        }
        public string AchDateDisplayDate
        {
            get { return AchDate == null ? string.Empty : Convert.ToDateTime(AchDate).ToString("dd-MM-yyyy"); }
        }
        public DataTable CompStatusList { set; get; }
        public DataTable OutcomeList { set; get; }
        public DataTable WithdrawReasonList { set; get; }
        public DataTable EmpOutcomeList { set; get; }
        #endregion

        #region Public Methods       
        #endregion

        #region PRIVATE Methods 	
        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            //CurrentItem.RefreshData();
            Refresh();
        }
        private void Refresh()
        {
            //OnPropertyChanged("OutcomeList");
            OnPropertyChanged("WithdrawReasonList");
            OnPropertyChanged("CurrentItem");
            OnPropertyChanged("CompStatusList");
            OnPropertyChanged("LearnActEndDate");
            OnPropertyChanged("LearnActEndDateDisplayDate");
            OnPropertyChanged("AchDate");
            OnPropertyChanged("AchDateDisplayDate");
        }
        private void dtLearnActEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (DateTime SelectedDataTimeValue in e.AddedItems)
                {
                    LearnActEndDate = SelectedDataTimeValue;
                    break;
                }
            }
            else
            {
                foreach (DateTime SelectedDataTimeValue in e.RemovedItems)
                {
                    LearnActEndDate = null;
                    break;
                }
            }        
        }
        private void dtAchDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (DateTime SelectedDataTimeValue in e.AddedItems)
                {
                    AchDate = SelectedDataTimeValue;
                    break;
                }
            }
            else
            {
                foreach (DateTime SelectedDataTimeValue in e.RemovedItems)
                {
                    AchDate = null;
                    break;
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
                switch (columnName)
                {
                    case "PriorLearnFundAdj":
                        break;
                    case "AchDate":
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
        #endregion

    }
}
