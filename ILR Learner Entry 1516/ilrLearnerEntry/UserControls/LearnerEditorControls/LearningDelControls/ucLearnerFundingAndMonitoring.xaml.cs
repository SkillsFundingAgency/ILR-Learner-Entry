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
    /// Interaction logic for ucLearnerFundingAndMonitoring.xaml
    /// </summary>
    public partial class ucLearnerFundingAndMonitoring : UserControl, INotifyPropertyChanged
    {

        #region Private Variables
        private ILR.LearningDelivery _learningDelivery;
        private List<String> LDMList = new List<String>(4);
        //private DataTable _dt;
        #endregion

        #region Constructor
        public ucLearnerFundingAndMonitoring()
        {
            InitializeComponent();
            ALBControl.UserControlTitle = "24+ Advanced Learning Loans Bursary fund";
            LSFControl.UserControlTitle = "Learning support funding";
        }
        #endregion

        #region Public Properties
        public ILR.LearningDelivery CurrentItem
        {
            get { return _learningDelivery; }
            set
            {
                if (value != null)
                {
                    _learningDelivery = value;
                    LDMList.Clear();
                    Fill_LDM_FAM_LIST();
                    this.DataContext = this;
                    //ALBControl.UserControlTitle = "24+ Advances learning loans bursary";
                    //LSFControl.UserControlTitle = "Learning support Funding";
                    ALBControl.CurrentItem = _learningDelivery;
                    LSFControl.CurrentItem = _learningDelivery;
                    OnPropertyChanged("PlusLoanBursaryFundList");
                    OnPropertyChanged("SourceOfFundingList");
                    OnPropertyChanged("FullOrCoFundedList");
                    OnPropertyChanged("NatioanSkillAgencyList");
                    OnPropertyChanged("SpecialProjectList");
                    OnPropertyChanged("EligibitiytAppFundingList");
                    OnPropertyChanged("ASLList");
                    OnPropertyChanged("PODList");

                    OnPropertyChanged("CurrentItem");

                    OnPropertyChanged("LDM4");
                    OnPropertyChanged("LDM3");
                    OnPropertyChanged("LDM2");
                    OnPropertyChanged("LDM1");
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }

        private void Fill_LDM_FAM_LIST()
        {

            LDMList.Clear();
            OnPropertyChanged("LDM4");
            OnPropertyChanged("LDM3");
            OnPropertyChanged("LDM2");
            OnPropertyChanged("LDM1");
            foreach (String code in _learningDelivery.LDM)
            {
                LDMList.Add(code);
                if (LDMList.Count >= 4) { break; }
            }
            while (LDMList.Count < 4)
            {
                LDMList.Add(String.Empty);
            }
            OnPropertyChanged("LDM4");
            OnPropertyChanged("LDM3");
            OnPropertyChanged("LDM2");
            OnPropertyChanged("LDM1");

        }
        public String LDM1
        {
            get { return LDMList.Count > 0 ? LDMList[0] : String.Empty; }
            set { SetLdmValue(0, value); }
        }
        public String LDM2
        {
            get { return LDMList.Count > 1 ? LDMList[1] : String.Empty; }
            set { SetLdmValue(1, value); }
        }

        public String LDM3
        {
            get { return LDMList.Count > 2 ? LDMList[2] : String.Empty; }
            set { SetLdmValue(2, value); }
        }

        public String LDM4
        {
            get { return LDMList.Count > 3 ? LDMList[3] : String.Empty; }
            set { SetLdmValue(3, value); }
        }

        private void SetLdmValue(Int32 index, String value)
        {

            LDMList[index] = value;

            List<String> TmpList = new List<String>(0);
            foreach (String code in LDMList)
            {
                if (!string.IsNullOrEmpty(code.Trim())){ TmpList.Add(code); }
                if (TmpList.Count >= 4) { break; }
            }
            _learningDelivery.LDM = TmpList;
            Fill_LDM_FAM_LIST();
        }

        public DataTable PlusLoanBursaryFundList { set; get; }
        public DataTable SourceOfFundingList { set; get; }
        public DataTable FullOrCoFundedList { set; get; }
        public DataTable NatioanSkillAgencyList { set; get; }
        public DataTable SpecialProjectList { set; get; }
        public DataTable ASLList { set; get; }

        public DataTable PODList{ set; get; }
        
        public DataTable EligibitiytAppFundingList { set; get; }
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


    }
}
