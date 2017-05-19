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
    /// Interaction logic for ucLLDDAndLearningSupport.xaml
    /// </summary>
    public partial class ucLLDDAndLearningSupport : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables
        private const String CLASSNAME = "Learner";
        private ILR.Schema XmlSchema = new ILR.Schema();
        private Learner _learner;
        private const Int32 _maxLRSItem = 4;
        private DataTable _dt;
        private string _alscost = string.Empty;
        private string _priorlearnfundadj = string.Empty;
        #endregion

        #region Constructor
        public ucLLDDAndLearningSupport()
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
                _alscost = string.Empty;
                if (value != null)
				{
                   // LLDDItemListControl.CurrentItem = null;

                    _learner = value;
                    _alscost = value.ALSCost.ToString();

                    lv_LSR.SelectionChanged -= lv_LSR_SelectionChanged;
                    this.DataContext = this;
                    ClearAllLRSSelected();
					LLDDItemListControl.CurrentItem = _learner;

                    // Need to finish of the Selection on reload ?
                    foreach (int? lrsCode in _learner.LSR)
                    {
                        SetLRSAsSelected(lrsCode.ToString());
					}
					lv_LSR.SelectionChanged += lv_LSR_SelectionChanged;
                    OnPropertyChanged("ALSCost");
                    OnPropertyChanged("LLDDTypeListDS");
					OnPropertyChanged("LLDDTypeListLD");
					OnPropertyChanged("LLDDHealthProblemList");
					OnPropertyChanged("LSRList");
					OnPropertyChanged("LLDDHealthProbTest");
					OnPropertyChanged("CurrentItem");
					OnPropertyChanged("LLDDHealthProblemList");
				}
                else
                {
					_learner = null;
                    lv_LSR.SelectionChanged -= lv_LSR_SelectionChanged;
                    ClearAllLRSSelected();
                    OnPropertyChanged("LSRList");
                    this.DataContext = null;
				}
            }
        }
        public string ALSCost
        {
            get { return _alscost; }
            set
            {
                _alscost = value;
                int number;
                bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                if (result)
                {
                    CurrentItem.ALSCost = number;
                }
            }
        }
		public int? LLDDHealthProbTest
		{
			get { return CurrentItem.LLDDHealthProb; }
			set
			{
                if (value != 1)
                {
                    if (CurrentItem.LLDDandHealthProblemList != null)
                    { CurrentItem.LLDDandHealthProblemList.Clear(); }
                }
                CurrentItem.LLDDHealthProb = value;
				OnPropertyChanged("LLDDHealthProbTest");
                LLDDItemListControl.CurrentItem = this.CurrentItem;
                OnPropertyChanged("LLDDHealthProblemList");
            }
        }
        public DataTable LSRList
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
        public DataTable LLDDTypeListDS { set; get; }
        public DataTable LLDDTypeListLD { set; get; }
        public DataTable LLDDHealthProblemList { set; get; }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        private void lv_LSR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView x in e.AddedItems)
            {
                LearnerFAM tmp = _learner.LearnerFAMList.Where(f => f.LearnFAMType == "LSR" && f.LearnFAMCode == int.Parse(x[0].ToString())).FirstOrDefault();
                if (tmp == null)
                {
                    if (_learner.LSR.Count < _maxLRSItem)
                    {
                        _learner.AddFAM(LearnerFAM.MultiOccurrenceFAMs.LSR, int.Parse(x[0].ToString()));
                    }
                    else
                    {
                        MessageBox.Show(String.Format("   You may only select {0} items.", _maxLRSItem.ToString())
                                                               , "Max number of selectable items reached."
                                                               , MessageBoxButton.OK
                                                               , MessageBoxImage.Information
                                                               , MessageBoxResult.OK);

                        x["IsSelected"] = Convert.ToBoolean(false);
                        OnPropertyChanged("LSRList");
                    }
                }
            }
            foreach (DataRowView x in e.RemovedItems)
            {
                foreach (int fam in _learner.LSR)
                {
                    if (fam.ToString() == x[0].ToString())
                    {
                        _learner.RemoveFAM(LearnerFAM.MultiOccurrenceFAMs.LSR, int.Parse(x[0].ToString()));
                        break;
                    }
                }
            }
        }
        private void SetLRSAsSelected(string Filter)
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
        private void ClearAllLRSSelected()
        {
            if (_dt != null)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    dr["IsSelected"] = Convert.ToBoolean(false);
                }
                OnPropertyChanged("LSRList");
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
				if (_learner != null)
				{
					switch (columnName)
					{
						case "LLDDHealthProbTest":
						case "LLDDHealthProblemList":
							if (CurrentItem.LLDDHealthProb == null)
							{
                                sReturn = "LLDDHealthProb nothing selected - required\r\n";
							}
							break;

						case "LLDDHealthProbTest1":
							if ((CurrentItem.LLDDHealthProb == null) 
								|| (
									(CurrentItem.LLDDHealthProb != null) && (CurrentItem.LLDDHealthProb == 1)
									&& ((CurrentItem.LLDDandHealthProblemList == null) || (CurrentItem.LLDDandHealthProblemList != null && CurrentItem.LLDDandHealthProblemList.Count < 1))
								   )
								)
							{
								return "LLDDandHealthProblem Not Selected- required\r\n";
							}
							break;
						case "ALSCost":
                            if (ALSCost != null && ALSCost.Length > 0)
                            {
                                sReturn += CheckPropertyLength(ALSCost, CLASSNAME, columnName);
                                int number;
                                bool result = Int32.TryParse(ALSCost, out number);
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


        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			if (CurrentItem != null)
			{
				//OnPropertyChanged("CurrentItem");
				CurrentItem.RefreshData();
			}
		}

    }
}