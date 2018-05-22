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
using System.Windows.Threading;

using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucLearnerFAMs.xaml
    /// </summary>
    public partial class ucLearnerFAMs : UserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private const String CLASSNAME = "Learner";
        private ILR.Schema XmlSchema = new ILR.Schema();
        private Learner _learner;
        private DataTable _dt;
        private const Int32 _maxNLMItem = 2;

        private string _planlearnhours = string.Empty;
        private string _planeephours = string.Empty;
        #endregion

        #region Constructor
        public ucLearnerFAMs()
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
                _planlearnhours = string.Empty;
                _planeephours = string.Empty;

                OnPropertyChanged("PlanLearnHours");
                OnPropertyChanged("PlanEEPHours");
                if (value != null)
                {
                    _learner = value;
                    _planlearnhours = value.PlanLearnHours.ToString();
                    _planeephours = value.PlanEEPHours.ToString();
                    this.DataContext = this;
                    lv_NLM.SelectionChanged -= lv_NLM_SelectionChanged;
                    OnPropertyChanged("FMEList");
                    OnPropertyChanged("PriorAttainmentList");
                    OnPropertyChanged("MGAList");
                    OnPropertyChanged("EGAList");
                    OnPropertyChanged("PlanLearnHours");
                    OnPropertyChanged("PlanEEPHours");
                    ClearAllNLMSelected();
                    OnPropertyChanged("NLMList");
                    foreach (int? nlmCode in _learner.NLM)
                    {
                        SetNLMAsSelected(nlmCode.ToString());
                    }
                    OnPropertyChanged("NLMList");
                    OnPropertyChanged("CurrentItem");
                    lv_NLM.SelectionChanged += lv_NLM_SelectionChanged;

                }
                else
                {
                    this.DataContext = null;
                    lv_NLM.SelectionChanged -= lv_NLM_SelectionChanged;
                    ClearAllNLMSelected();
                }
            }
        }
        public string PlanLearnHours
        {
            get { return _planlearnhours; }
            set
            {
                _planlearnhours = value;
                int number;
                bool result = Int32.TryParse(Convert.ToString(value), out number);
                if (result)
                {
                    CurrentItem.PlanLearnHours = number;
                }
                else if (string.IsNullOrEmpty(value))
                {
                    CurrentItem.PlanLearnHours = null;
                }
            }
        }

        public string PlanEEPHours
        {
            get { return _planeephours; }
            set
            {
                _planeephours = value;
                int number;
                bool result = Int32.TryParse(Convert.ToString(value), out number);
                if (result)
                {
                    CurrentItem.PlanEEPHours = number;
                }
                else if (string.IsNullOrEmpty(value))
                {
                    CurrentItem.PlanEEPHours = null;
                }
            }
        }


        public DataTable PriorAttainmentList { set; get; }
        public DataTable MGAList { set; get; }
        public DataTable EGAList { set; get; }
        public DataTable FMEList { set; get; }

        public DataTable NLMList
        {
            get { return _dt; }
            set
            {
                DataTable tmpDT = value;
                foreach (DataRow dr in tmpDT.Rows)
                {
                    if (String.IsNullOrWhiteSpace(dr["Code"].ToString()))
                    {
                        dr.Delete();
                        break;
                    }
                }

                _dt = tmpDT;
                _dt.Columns.Add(new DataColumn("IsSelected", typeof(bool)));
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        private void lv_NLM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView x in e.AddedItems)
            {
                LearnerFAM tmp = _learner.LearnerFAMList.Where(f => f.LearnFAMType == "NLM" && f.LearnFAMCode == int.Parse(x[0].ToString())).FirstOrDefault();
                if (tmp == null)
                {
                    if (_learner.NLM.Count < _maxNLMItem)
                    {
                        _learner.AddFAM(LearnerFAM.MultiOccurrenceFAMs.NLM, int.Parse(x[0].ToString()));
                    }
                    else
                    {
                        MessageBox.Show(String.Format("   You may only select {0} items.", _maxNLMItem.ToString())
                                                               , "Max number of selectable items reached."
                                                               , MessageBoxButton.OK
                                                               , MessageBoxImage.Information
                                                               , MessageBoxResult.OK);

                        x["IsSelected"] = Convert.ToBoolean(false);
                        OnPropertyChanged("NLMList");
                    }
                }
            }
            foreach (DataRowView x in e.RemovedItems)
            {
                Console.WriteLine(string.Format("Remove {0}", x[0].ToString()));
                foreach (int fam in _learner.NLM)
                {
                    if (fam.ToString() == x[0].ToString())
                    {
                        _learner.RemoveFAM(LearnerFAM.MultiOccurrenceFAMs.NLM, int.Parse(x[0].ToString()));
                        break;
                    }
                }
            }
        }
        private void SetNLMAsSelected(string Filter)
        {
            if (_dt != null)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr["Code"].ToString() == Filter)
                    {
                        dr["IsSelected"] = Convert.ToBoolean(true);
                        break;
                    }

                }
            }
        }
        private void ClearAllNLMSelected()
        {
            if (_dt != null)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    dr["IsSelected"] = Convert.ToBoolean(false);
                }
            }
        }
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
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                if (_learner != null)
                {
                    switch (columnName)
                    {
                        case "PlanEEPHours":
                            if (PlanEEPHours != null && PlanEEPHours.Length > 0)
                            {
                                sReturn += CheckPropertyLength(PlanEEPHours, CLASSNAME, columnName);
                                int number;
                                bool result = Int32.TryParse(PlanEEPHours, out number);
                                if (!result)
                                {
                                    sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                                }
                            }
                            break;
                        case "PlanLearnHours":
                            if (PlanLearnHours != null && PlanLearnHours.Length > 0)
                            {
                                sReturn += CheckPropertyLength(PlanLearnHours, CLASSNAME, columnName);
                                int number;
                                bool result = Int32.TryParse(PlanLearnHours, out number);
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
