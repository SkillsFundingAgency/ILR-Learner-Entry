using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.DPOutcomeControls
{
    /// <summary>
    /// Interaction logic for ucDPO_LearnerOutcomeList.xaml
    /// </summary>
    public partial class ucDPO_LearnerOutcomeList : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables
        private LearnerDestinationandProgression _learnerDP;
        private ILR.Schema XmlSchema = new ILR.Schema();
        private const String CLASSNAME = "LearnerDestinationandProgression";
        #endregion

        #region Constructor
        public ucDPO_LearnerOutcomeList()
        {
            InitializeComponent();
            lv.Visibility = System.Windows.Visibility.Visible;
            OutcomeDetailControl.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        #region Public Properties
        public LearnerDestinationandProgression CurrentItem
        {
            get { return _learnerDP; }
            set
            {
                _learnerDP = null;
                if (value == null)
                {
                    this.DataContext = null;
                    if (LearnerOutcomeItemsCV != null)
                    {
                        LearnerOutcomeItemsCV.CurrentChanged -= LearnerOutcomeItemsCV_CurrentChanged;
                    }
                    LearnerOutcomeItemsCV = null;
                    ULN = string.Empty;
                    OutcomeDetailControl.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    _learnerDP = value;
                    ULN = _learnerDP.ULN.ToString();
                    if (LearnerOutcomeItemsCV == null)
                    {
                        LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                        LearnerOutcomeItemsCV.CurrentChanged += LearnerOutcomeItemsCV_CurrentChanged;
                    }
                    if (value.OutcomeCount > 0)
                    {
                        foreach (ILR.DPOutcome xDp in _learnerDP.DPOutcomeList)
                        {
                            xDp.IsSelected = false;
                        }
                        LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                        LearnerOutcomeItemsCV.MoveCurrentToFirst();
                        if (_learnerDP.DPOutcomeList.Count > 0)
                        {
                            (LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome).IsSelected = true;
                        }
                        SetSubControl(LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome);
                    }
                    else
                    {
                        LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                        SetSubControl(null);
                    }
                    LearnerOutcomeItemsCV.Refresh();
                    this.DataContext = this;
                }
                OnPropertyChanged("CurrentItem");
                OnPropertyChanged("LearnerOutcomeItemsCV");
                ShowHidChildControls();
            }
        }
        private string _uln;
        public string ULN
        {
            get { return _uln; }
            set
            {
                _uln = value;
                long number;
                bool result = Int64.TryParse(System.Convert.ToString(value), out number);
                if (result)
                {
                    _learnerDP.ULN = number;
                }
                OnPropertyChanged("ULN");
            }
        }
        void LearnerOutcomeItemsCV_CurrentChanged(object sender, EventArgs e)
        {
            if (LearnerOutcomeItemsCV.CurrentItem != null)
            {
                if (!(LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome).IsSelected)
                {
                    (LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome).IsSelected = true;
                }
                SetSubControl((LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome));
            }
        }
        public ICollectionView LearnerOutcomeItemsCV
        {
            get;
            private set;
        }
        #endregion

        #region PRIVATE Methods
        private void SetSubControl(ILR.DPOutcome outcome)
        {
            if (LearnerOutcomeItemsCV.CurrentItem != null)
            {
                OutcomeDetailControl.CurrentItem = outcome;
            }
            else
            {
                OutcomeDetailControl.CurrentItem = null;
            }
        }
        private void ShowHidChildControls()
        {
            if (_learnerDP!= null &&_learnerDP.DPOutcomeList.Count > 0)
            {
                OutcomeDetailControl.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                OutcomeDetailControl.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void AddNewOutcome(object sender, RoutedEventArgs e)
        {
            ILR.DPOutcome dpo = _learnerDP.CreateOutcome();
            if (LearnerOutcomeItemsCV == null)
            {

                LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                LearnerOutcomeItemsCV.CurrentChanged += LearnerOutcomeItemsCV_CurrentChanged;
            }
            else
            {
                LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
            }
            LearnerOutcomeItemsCV.MoveCurrentTo(dpo);
            foreach (ILR.DPOutcome xDp in LearnerOutcomeItemsCV)
            {
                xDp.IsSelected = false;
            }
            SetSubControl(LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome);
            if (LearnerOutcomeItemsCV.CurrentItem != dpo)
            {

                if (!dpo.IsSelected)
                {
                    dpo.IsSelected = true;
                }
            }
            ShowHidChildControls();
            LearnerOutcomeItemsCV.Refresh();
            OnPropertyChanged("CurrentItem");
            OnPropertyChanged("LearnerOutcomeItemsCV");
        }
        private void RemoveOutcome(object sender, RoutedEventArgs e)
        {
            if (LearnerOutcomeItemsCV.CurrentItem != null)
            {
                ILR.DPOutcome ld2Remove = LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome;
                if (ld2Remove != null)
                {
                    _learnerDP.Delete(ld2Remove);
                    if (!LearnerOutcomeItemsCV.IsEmpty)
                    {
                        if (!LearnerOutcomeItemsCV.MoveCurrentToPrevious())
                        {
                            LearnerOutcomeItemsCV.MoveCurrentToFirst();
                            LearnerOutcomeItemsCV.Refresh();
                            OnPropertyChanged("LearnerOutcomeItemsCV");
                        }
                        if ((LearnerOutcomeItemsCV.CurrentItem != null) && (LearnerOutcomeItemsCV.CurrentItem != ld2Remove))
                        {
                            ILR.DPOutcome lr = LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome;
                            foreach (ILR.DPOutcome xDp in LearnerOutcomeItemsCV)
                            {
                                xDp.IsSelected = false;
                            }
                            lr.IsSelected = true;
                        }
                        else
                        {
                            LearnerOutcomeItemsCV.MoveCurrentToNext();
                            if (LearnerOutcomeItemsCV.CurrentItem != null)
                            {
                                ILR.DPOutcome lr = LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome;
                                lr.IsSelected = true;
                            }
                        }
                    }
                }
                LearnerOutcomeItemsCV.Refresh();
                OnPropertyChanged("LearnerOutcomeItemsCV");
                ShowHidChildControls();
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
                    case "ULN":
                         if (String.IsNullOrEmpty(ULN.ToString()))
                                sReturn = String.Format("{0} required.", columnName);
                        
                            if (ULN != null && ULN.Length > 0)
                        {
                            sReturn += CheckPropertyLength(ULN, CLASSNAME, columnName);
                            long number;
                            bool result = Int64.TryParse(ULN, out number);
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
