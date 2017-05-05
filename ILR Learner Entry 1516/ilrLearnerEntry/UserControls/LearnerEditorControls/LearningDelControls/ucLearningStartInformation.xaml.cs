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
    public partial class ucLearningStartInformation : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables

        private const String CLASSNAME = "LearningDelivery";
        private ILR.Schema XmlSchema = new ILR.Schema();

        private ILR.LearningDelivery _learningDelivery;
        private const Int32 _maxHHSItem = 4;

        private string _priorlearnfundadj = string.Empty;
        private string _progtype = string.Empty;
        private string _fworkcode = string.Empty;
        private string _pwaycode = string.Empty;
        private string _otherfundadj = string.Empty;
        private string _addhours = string.Empty;
        private string _partnerukprn = string.Empty;


        #endregion

        #region Constructor
        public ucLearningStartInformation()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        private Boolean IsClearHSSSelection = false;
        public ILR.LearningDelivery CurrentItem
        {
            get { return _learningDelivery; }
            set
            {
                lv_HHS.SelectionChanged -= lv_HHS_SelectionChanged;
                this.DataContext = null;
                IsClearHSSSelection = true;
                _learningDelivery = null;
                _priorlearnfundadj = string.Empty;
                _progtype = string.Empty;
                _fworkcode = string.Empty;
                _pwaycode = string.Empty;
                _otherfundadj = string.Empty;
                _addhours = string.Empty;
                _partnerukprn = string.Empty;
                ClearAllHHSSelected();
                if (value != null)
                {
                    _learningDelivery = value;
                    this.DataContext = this;
                    _priorlearnfundadj = _learningDelivery.PriorLearnFundAdj.ToString();
                    _progtype = _learningDelivery.ProgType.ToString();
                    _fworkcode = _learningDelivery.FworkCode.ToString();
                    _pwaycode = _learningDelivery.PwayCode.ToString();
                    _otherfundadj = _learningDelivery.OtherFundAdj.ToString();
                    _addhours = _learningDelivery.AddHours.ToString();
                    _partnerukprn = _learningDelivery.PartnerUKPRN.ToString();
                    foreach (LearningDeliveryFAM HHSFam in _learningDelivery.HHS)
                    {
                        SetHHSAsSelected(HHSFam);
                    }
                    lv_HHS.SelectionChanged += lv_HHS_SelectionChanged;
                }
                OnPropertyChanged("AimTypeList");
                OnPropertyChanged("FundModelList");
                OnPropertyChanged("PriorLearnFundAdj");
                OnPropertyChanged("ProgType");
                OnPropertyChanged("FworkCode");
                OnPropertyChanged("PwayCode");
                OnPropertyChanged("OtherFundAdj");
                OnPropertyChanged("AddHours");
                OnPropertyChanged("PartnerUKPRN");
                OnPropertyChanged("ProgTypeList");
                OnPropertyChanged("HHSList");
                OnPropertyChanged("CurrentItem");
            }
        }
     

        public string PriorLearnFundAdj
        {
            get { return _priorlearnfundadj; }
            set
            {
                _priorlearnfundadj = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result) { CurrentItem.PriorLearnFundAdj = number; }
                else { CurrentItem.PriorLearnFundAdj = null; }
            }
        }
        public string ProgType
        {
            get { return _progtype; }
            set
            {
                _progtype = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result) { CurrentItem.ProgType = number; }
                else { CurrentItem.ProgType = null; }
            }
        }
        public string FworkCode
        {
            get { return _fworkcode; }
            set
            {
                _fworkcode = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result) { CurrentItem.FworkCode = number; }
                else { CurrentItem.FworkCode = null; }
            } 
        }
        public string PwayCode
        {
            get { return _pwaycode; }
            set
            {
                _pwaycode = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                { CurrentItem.PwayCode = number; }
                else            
                { CurrentItem.PwayCode = null; }
            }
        }
        public string OtherFundAdj
        {
            get { return _otherfundadj; }
            set
            {
                _otherfundadj = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                { CurrentItem.OtherFundAdj = number; }
                else
                { CurrentItem.OtherFundAdj = null; }
            }
        }
        public string AddHours
        {
            get { return _addhours; }
            set
            {
                _addhours = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                { CurrentItem.AddHours = number; }
                else
                { CurrentItem.AddHours = null; }
            }
        }
        public string PartnerUKPRN
        {
            get { return _partnerukprn; }
            set
            {
                _partnerukprn = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                { CurrentItem.PartnerUKPRN = number; }
                else
                { CurrentItem.PartnerUKPRN = null; }
            }
        }
        #region Datatables for Combo box values
        public DataTable AimTypeList { set; get; }
        public DataTable FundModelList { set; get; }
        public DataTable ProgTypeList { set; get; }
        private DataTable _dtHHS;
        public DataTable HHSList
        {
            get { return _dtHHS; }
            set
            {
                DataTable tmpDT = value;
                foreach (DataRow dr in tmpDT.Rows)
                {
                    if (String.IsNullOrWhiteSpace(dr["Code"].ToString()))
                    {
                        //Console.WriteLine(String.Format("Delete Code : [{0}] ", dr["Code"].ToString()));
                        dr.Delete();
                        break;
                    }
                }
                _dtHHS = tmpDT;
                _dtHHS.Columns.Add(new DataColumn("IsSelected", typeof(bool)));
            }
        }
        #endregion
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        private void lv_HHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView x in e.AddedItems)
            {
                LearningDeliveryFAM tmp = _learningDelivery.HHS.Where(f => f.LearnDelFAMType == "HHS" && f.LearnDelFAMCode == x["Code"].ToString()).FirstOrDefault();
                if (tmp == null)
                {
                    if (_learningDelivery.HHS.Count < _maxHHSItem)
                    {
                        _learningDelivery.AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, x["Code"].ToString());
                    }
                    else
                    {
                        MessageBox.Show(String.Format("   You may only select {0} items.", _maxHHSItem.ToString())
                                                               , "Max number of selectable items reached."
                                                               , MessageBoxButton.OK
                                                               , MessageBoxImage.Information
                                                               , MessageBoxResult.OK);
                        x["IsSelected"] = Convert.ToBoolean(false);
                        OnPropertyChanged("HHSList");
                    }
                }
            }
            foreach (DataRowView x in e.RemovedItems)
            {
                if (!IsClearHSSSelection)
                {
                    Console.WriteLine(string.Format("Remove {0}", x["Code"].ToString()));
                    foreach (LearningDeliveryFAM fam in _learningDelivery.HHS)
                    {
                        if (fam.LearnDelFAMCode.ToString() == x["Code"].ToString())
                        {
                            _learningDelivery.RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, x["Code"].ToString());
                            break;
                        }
                    }
                }
            }
        }
        private void SetHHSAsSelected(LearningDeliveryFAM Filter)
        {
            if (_dtHHS != null)
            {
                foreach (DataRow dr in _dtHHS.Rows)
                {
                    if (dr["Code"].ToString() == Filter.LearnDelFAMCode)
                    {
                        dr["IsSelected"] = Convert.ToBoolean(true);
                        break;
                    }
                }
            }
        }

        private void ClearAllHHSSelected()
        {
            IsClearHSSSelection = true;

            if (_dtHHS != null)
            {
                foreach (DataRow dr in _dtHHS.Rows)
                {
                    dr["IsSelected"] = Convert.ToBoolean(false);
                }
            }
            IsClearHSSSelection = false;

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
                        if (PriorLearnFundAdj != null && PriorLearnFundAdj.Length > 0)
                        {
                            sReturn += CheckPropertyLength(PriorLearnFundAdj, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(PriorLearnFundAdj, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "ProgType":
                        if (ProgType != null && ProgType.Length > 0)
                        {
                            sReturn += CheckPropertyLength(ProgType, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(ProgType, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "FworkCode":
                        if (FworkCode != null && FworkCode.Length > 0)
                        {
                            sReturn += CheckPropertyLength(FworkCode, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(FworkCode, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "PwayCode":
                        if (PwayCode != null && PwayCode.Length > 0)
                        {
                            sReturn += CheckPropertyLength(PwayCode, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(PwayCode, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "OtherFundAdj":
                        if (OtherFundAdj != null && OtherFundAdj.Length > 0)
                        {
                            sReturn += CheckPropertyLength(OtherFundAdj, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(OtherFundAdj, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "AddHours":
                        if (AddHours != null && AddHours.Length > 0)
                        {
                            sReturn += CheckPropertyLength(AddHours, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(AddHours, out number);
                            if (!result)
                            {
                                sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                            }
                        }
                        break;
                    case "PartnerUKPRN":
                        if (PartnerUKPRN != null && PartnerUKPRN.Length > 0)
                        {
                            sReturn += CheckPropertyLength(PartnerUKPRN, CLASSNAME, columnName);
                            int number;
                            bool result = Int32.TryParse(PartnerUKPRN, out number);
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
        #endregion

    }
}
