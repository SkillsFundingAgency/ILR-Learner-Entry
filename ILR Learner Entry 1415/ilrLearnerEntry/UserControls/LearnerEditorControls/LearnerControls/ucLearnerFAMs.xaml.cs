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
        private Learner _learner;
        private DataTable _dt;
        private const Int32 _maxNLMItem = 2;

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
                if (value != null)
                {
                    _learner = value;

                    this.DataContext = this;
                    lv_NLM.SelectionChanged -= lv_NLM_SelectionChanged;
                    OnPropertyChanged("FMEList");
                    OnPropertyChanged("PriorAttainmentList");
                    OnPropertyChanged("MGAList");
                    OnPropertyChanged("EGAList");
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

        public DataTable PriorAttainmentList { set; get; }
        public DataTable MGAList { set; get;}
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
