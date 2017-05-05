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
using ilrLearnerEntry.Utils;


namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls.ucLLDDControls
{
    /// <summary>
    /// Interaction logic for ucLLDDList.xaml
    /// </summary>
    public partial class ucLLDDList : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        private Learner _learner;
        private List<Bob> _catList = new List<Bob>(0);
        private Int32 _maxLLDDCatItem = 4;
        private String _title = String.Empty;

        #region Constructor
        public ucLLDDList()
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
                this.lv_LLDDCats.SelectionChanged -= lv_LLDDCats_SelectionChanged;
                this.cbLLDDCatPrimary.SelectionChanged -= ComboBox_SelectionChanged;
                this.DataContext = null;
                _learner = null;
                ClearAllLLDDCatSelected();
                OnPropertyChanged("CurrentItem");
                RefreshMe();

                if (value != null)
                {
                    _learner = value;
                    this.DataContext = this;

                    if ((_learner.LLDDandHealthProblemList != null) && (_learner.LLDDandHealthProblemList.Count > 0))
                    {
                        foreach (LLDDandHealthProblem lrnRecs in _learner.LLDDandHealthProblemList)
                        {
                            SetLLDDCatSelected(lrnRecs.LLDDCat.ToString());
                        }
                        if (_learner.LLDDandHealthProblemList.Where(f => f.PrimaryLLDD == true).Count() > 0)
                        {
                            SetLLDDCatPrimary(_learner.LLDDandHealthProblemList.Where(f => f.PrimaryLLDD == true).FirstOrDefault().LLDDCat.ToString());
                        }
                    }
                    RefreshMe();
                    CurrentItem.RefreshData();
                    OnPropertyChanged("CurrentItem");
                    this.lv_LLDDCats.SelectionChanged += lv_LLDDCats_SelectionChanged;
                    this.cbLLDDCatPrimary.SelectionChanged += ComboBox_SelectionChanged;
                }
                else
                {
                    this.DataContext = null;
                    _learner = null;
                    this.lv_LLDDCats.SelectionChanged -= lv_LLDDCats_SelectionChanged;
                    this.cbLLDDCatPrimary.SelectionChanged -= ComboBox_SelectionChanged;
                    ClearAllLLDDCatSelected();
                }
                RefreshMe();
            }
        }
        public void RefreshMe()
        {
            OnPropertyChanged("LLDDCatList");
            OnPropertyChanged("LLDDCatSelList");
            OnPropertyChanged("LLDDCatSelPrimary");
        }
        public DataTable LLDDCatlList
        {
            set
            {
                _catList.Clear();
                DataTable tmpDT = value;
                foreach (DataRow dr in tmpDT.Rows)
                {
                    if (!(String.IsNullOrWhiteSpace(dr["Code"].ToString())))
                    {
                        Bob b = new Bob();
                        b.Code = dr["Code"].ToString();
                        b.Description = dr["Description"].ToString();
                        b.IsSelected = false;
                        b.IsPrimary = false;
                        _catList.Add(b);
                    }
                }
                ClearAllLLDDCatSelected();
            }
        }
        public List<Bob> LLDDCatList
        {
            get { return _catList; }
            set
            {
                //foreach (Bob b in _catList)
                //{
                //}
            }
        }

        public List<Bob> LLDDCatSelList
        {
            get { return _catList.Where(f => f.IsSelected == true).ToList(); }
        }
        public Bob LLDDCatSelPrimary
        {
            get { return _catList.Where(f => f.IsPrimary == true).FirstOrDefault(); }
        }
        public String UserControlTitle { get { return _title; } set { _title = value; OnPropertyChanged("UserControlTitle"); } }
        public Int32 MaxItems
        {
            get { return _maxLLDDCatItem; }
            set { _maxLLDDCatItem = value; }
        }
        #endregion


        #region PRIVATE Methods     
        #endregion

        private void ClearAllLLDDCatSelected()
        {
            if (_catList != null)
            {
                foreach (Bob b in LLDDCatList)
                {
                    b.IsSelected = false;
                    b.IsPrimary = false;
                }
            }
            RefreshMe();
        }
        private void SetLLDDCatSelected(string Filter)
        {
            if (_catList != null)
            {

                _catList.Where(f => f.Code == Filter).FirstOrDefault().IsSelected = true;
                RefreshMe();
            }
        }
        //private void SetLLDDCatlSelList()
        //{
        //}
        private void SetLLDDCatPrimary(string Filter)
        {
            if (_catList != null)
            {
                foreach (Bob b in _catList) { b.IsPrimary = false; }

                _catList.Where(f => f.Code == Filter).FirstOrDefault().IsPrimary = true;
                OnPropertyChanged("LLDDCatSelPrimary");
            }
        }
        private void lv_LLDDCats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Bob x in e.AddedItems)
            {
                LLDDandHealthProblem tmp = _learner.LLDDandHealthProblemList.Where(f => f.LLDDCat == int.Parse(x.Code.ToString())).FirstOrDefault();
                if (tmp == null)
                {
                    if (_learner.LLDDandHealthProblemList.Count < MaxItems)
                    {
                        _catList.Where(f => f.Code == x.Code.ToString()).FirstOrDefault().IsSelected = true;
                        _catList.Where(f => f.Code == x.Code.ToString()).FirstOrDefault().IsPrimary = false;

                        LLDDandHealthProblem newLLDD = _learner.CreateLLDDandHealthProblem();
                        newLLDD.LLDDCat = int.Parse(x.Code.ToString());
                        newLLDD.PrimaryLLDD = Convert.ToBoolean(false);
                        _learner.RefreshData();
                    }
                    else
                    {
                        MessageBox.Show(String.Format("   You may only select {0} items.", MaxItems.ToString())
                                                               , "Max number of selectable items reached."
                                                               , MessageBoxButton.OK
                                                               , MessageBoxImage.Information
                                                               , MessageBoxResult.OK);
                        _catList.Where(f => f.Code == x.Code.ToString()).FirstOrDefault().IsSelected = false;
                        _catList.Where(f => f.Code == x.Code.ToString()).FirstOrDefault().IsPrimary = false;
                    }
                }
            }

            foreach (Bob x in e.RemovedItems)
            {
                _catList.Where(f => f.Code == x.Code.ToString()).FirstOrDefault().IsSelected = false;
                foreach (LLDDandHealthProblem hp in _learner.LLDDandHealthProblemList)
                {
                    if (hp.LLDDCat.ToString() == x.Code.ToString())
                    {
                        _learner.Delete(hp);
                        break;
                    }
                }
            }
            RefreshMe();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Bob x in e.AddedItems)
            {
                _catList.Where(f => f.Code == x.Code.ToString()).FirstOrDefault().IsPrimary = true;
                SetThisToPrimaryLLDDCat(int.Parse(x.Code.ToString()));
                _learner.RefreshData();
            }

            foreach (Bob x in e.RemovedItems)
            {
                _catList.Where(f => f.Code == x.Code.ToString()).FirstOrDefault().IsPrimary = false;
            }

        }
        private void SetThisToPrimaryLLDDCat(int CatCode)
        {
            foreach (LLDDandHealthProblem lrnRecs in _learner.LLDDandHealthProblemList)
            {
                if (CatCode == lrnRecs.LLDDCat)
                {
                    lrnRecs.PrimaryLLDD = true;
                    _catList.Where(f => f.Code == CatCode.ToString()).FirstOrDefault().IsPrimary = true;
                    SetLLDDCatPrimary(CatCode.ToString());
                }
                else
                {
                    _catList.Where(f => f.Code == CatCode.ToString()).FirstOrDefault().IsPrimary = false;
                    lrnRecs.PrimaryLLDD = false;
                }
            }
        }
        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            if (CurrentItem != null)
            {
                CurrentItem.RefreshData();
            }
        }

        #region IDataErrorInfo Members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (_learner != null)
                {
                    switch (columnName)
                    {
                        case "LLDDCatlSelList":
                        case "LLDDCatlList":
                            if (_learner.LLDDHealthProb == 1 && ((_learner.LLDDandHealthProblemList == null) || (_learner.LLDDandHealthProblemList.Count < 1)))
                            {
                                result = "LLDDHealthProb nothing selected - required";
                            }
                            break;
                        default:
                            // Console.WriteLine(String.Format("Passed : {0}", columnName));
                            break;
                    }
                }
                return result;
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


    public class Bob

    {
        private String _code = string.Empty;
        private String _description = string.Empty;
        private Boolean _isSelected = false;
        private Boolean _isPrimary = false;

        #region Constructor
#if DEBUG
        public Bob()
        {
            ThrowOnInvalidPropertyName = true;
        }
#endif     
        #endregion

        public String Code { get { return _code; } set { _code = value; OnPropertyChanged("Code"); } }
        public String Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }
        public Boolean IsSelected { get { return _isSelected; } set { _isSelected = value; OnPropertyChanged("IsSelected"); } }
        public Boolean IsPrimary { get { return _isPrimary; } set { _isPrimary = value; OnPropertyChanged("IsPrimary"); } }

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


