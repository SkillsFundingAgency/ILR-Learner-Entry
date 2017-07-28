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

namespace ilrLearnerEntry.UserControls
{
    /// <summary>
    /// Interaction logic for ucLearnerEditControl.xaml
    /// </summary>
    public partial class ucLearnerEditControl : UserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private String _learnerfilterString = string.Empty;
        private String _filterText = String.Empty;
        #endregion
        #region Constructor
        public ucLearnerEditControl()
        {
            InitializeComponent();
            LearnerDetailGrid.Visibility = System.Windows.Visibility.Hidden;
            this.DataContext = this;
            SetupLookups();
        }
        #endregion
        #region Public Properties
        public ICollectionView LearnerItemsCV
        {
            get;
            private set;
        }
        private bool LearnerFilter(object item)
        {
            Learner learner = item as Learner;
            if (!string.IsNullOrEmpty(_learnerfilterString))
            {
                bool bReturn = false;
                if ((learner.FamilyName != null) && (learner.FamilyName.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.GivenNames != null) && (learner.GivenNames.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.ULN != null) && (learner.ULN.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.LearnRefNumber != null) && (learner.LearnRefNumber.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.PostCode != null) && (learner.PostCode.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                return bReturn;
            }
            else
            {
                return true;
            }
        }
        public string LearnerFilterString
        {
            get { return _learnerfilterString; }
            set
            {
                _learnerfilterString = value;
                OnPropertyChanged("LearnerFilterString");
                if (LearnerItemsCV != null)
                {
                    LearnerItemsCV.Refresh();
                    OnPropertyChanged("LearnerItemsCV");
                }
            }
        }

        #endregion

        #region Private Properties
        #endregion
        
        #region Public Methods
        public void UpdateChildControlAsNewDataLoaded()
        {
            SetupListData();
            if (App.ILRMessage.LearnerList.Count > 0)
            {
                LearnerItemsCV.MoveCurrentToFirst();
                OnPropertyChanged("LearnerItemsCV");
            }
        }
        #endregion

        #region Private Methods
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text == "Enter value to search for")
            {
                txtFilter.Text = "";
            }
        }
        private void SetupListData()
        {
            LearnerItemsCV = null;
            if (App.ILRMessage.LearnerList.Count > 0)
            {

                LearnerItemsCV = CollectionViewSource.GetDefaultView(App.ILRMessage.LearnerList);
                using (LearnerItemsCV.DeferRefresh())
                {
                    LearnerItemsCV.SortDescriptions.Clear();
                    LearnerItemsCV.SortDescriptions.Add(new SortDescription("IsComplete", ListSortDirection.Ascending));
                    LearnerItemsCV.SortDescriptions.Add(new SortDescription("LearnRefNumber", ListSortDirection.Ascending));
                    LearnerItemsCV.Filter = LearnerFilter;
                }
            }
            else
            {
                if (LearnerItemsCV != null)
                {
                    LearnerItemsCV.Refresh();
                }
                LearnerDetailGrid.Visibility = System.Windows.Visibility.Hidden;
                LearnerItemsCV = null;
            }
        }
        private void SetSubControl(Learner learner)
        {
            if (LearnerItemsCV.CurrentItem != null)
            {
                LearnerDetailGrid.Visibility = System.Windows.Visibility.Visible;

                LearnerHeaderControl.CurrentItem = learner;
                LearnerDetailControl.CurrentItem = learner;
                LLDDAndLearningSupportControl.CurrentItem = learner;
                ProviderSpecifiedLearningMonitorInformationControl.CurrentItem = learner;
                EmploymentStatusListControl.CurrentItem = learner;
                LearningDeliveryListControl.CurrentItem = learner;
                LearnerHEInformationControl.CurrentItem = learner;
                LearnerFAMsControl.CurrentItem = learner;
            }
            else
            {
                LearnerHeaderControl.CurrentItem = null;
                LearnerDetailControl.CurrentItem = null;
                LLDDAndLearningSupportControl.CurrentItem = null;
                ProviderSpecifiedLearningMonitorInformationControl.CurrentItem = null;
                EmploymentStatusListControl.CurrentItem = null;
                LearningDeliveryListControl.CurrentItem = null;
                LearnerFAMsControl.CurrentItem = null;

                LearnerDetailGrid.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void DataItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.ILRMessage.LearnerList.Count > 0)
            {
                if (e.AddedItems.Count > 0)
                {
                    Learner lr = e.AddedItems[0] as Learner;
                    LearnerItemsCV.MoveCurrentTo(lr);
                    lr.IsSelected = true;
                    SetSubControl(lr);
                }
                LearnerDetailGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((DataItemListBox != null) && (txtFilter.Text != null) && (this.LearnerFilterString != txtFilter.Text))
            {
                this.LearnerFilterString = txtFilter.Text;
            }
        }
        private void AddLearner_Click(object sender, RoutedEventArgs e)
        {
            Learner NewLr = App.ILRMessage.CreateLearner();
            App.ILRMessage.Save();
            if (App.ILRMessage.LearnerList.Count == 1)
            {
                SetupListData();
            }
            NewLr.IsSelected = true;
            LearnerItemsCV.MoveCurrentTo(NewLr);
            LearnerItemsCV.Refresh();
            OnPropertyChanged("LearnerItemsCV");
            NewLr.RefreshData();
        }
        private void RemoveLearner_Click(object sender, RoutedEventArgs e)
        {
            if (LearnerItemsCV.CurrentItem != null)
            {
                Learner lr2Remove = LearnerItemsCV.CurrentItem as Learner;
                if (lr2Remove != null)
                {
                    MessageBoxResult result = MessageBox.Show(String.Format("Are you sure you want to delete Learner {0} {0} Learner Ref - {3} {0} Name : {1} {2}", Environment.NewLine, lr2Remove.GivenNames, lr2Remove.FamilyName, lr2Remove.LearnRefNumber)
                                                            , "Confirmation"
                                                            , MessageBoxButton.YesNo
                                                            , MessageBoxImage.Stop
                                                            , MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        App.ILRMessage.Delete(lr2Remove);
                        App.ILRMessage.Save();
                        LearnerItemsCV.Refresh();
                        if (App.ILRMessage.LearnerList.Count > 0)
                        {
                            if (!LearnerItemsCV.MoveCurrentToPrevious())
                            {
                                LearnerItemsCV.MoveCurrentToFirst();
                                LearnerItemsCV.Refresh();
                                OnPropertyChanged("LearnerItemsCV");
                            }
                            if ((LearnerItemsCV.CurrentItem != null) && (LearnerItemsCV.CurrentItem != lr2Remove))
                            {
                                Learner lr = LearnerItemsCV.CurrentItem as Learner;
                                lr.IsSelected = true;

                            }
                            else
                            {
                                LearnerItemsCV.MoveCurrentToNext();
                                if (LearnerItemsCV.CurrentItem != null)
                                {
                                    Learner lr = LearnerItemsCV.CurrentItem as Learner;
                                    lr.IsSelected = true;

                                }
                            }
                            LearnerItemsCV.Refresh();
                            OnPropertyChanged("LearnerItemsCV");
                        }
                        else
                        {
                            SetupListData();
                        }
                    }
                }
            }
        }
        private void SetupLookups()
        {
            ILR.Lookup lp = new ILR.Lookup();
            LearnerHeaderControl.GenderList = lp.GetLookup("Sex");
            LearnerDetailControl.EthnicityList = lp.GetLookup("Ethnicity");
            LearnerDetailControl.ECFList = lp.GetLookup("LearnerFAM", "ECF");
            LearnerDetailControl.MCFList = lp.GetLookup("LearnerFAM", "MCF");
            LearnerDetailControl.RestrictedIndList = lp.GetLookup("ContactPreference", "RUI");
            LearnerFAMsControl.PriorAttainmentList = lp.GetLookup("PriorAttain");
            LearnerFAMsControl.FMEList = lp.GetLookup("LearnerFAM", "FME");
            LearnerFAMsControl.NLMList = lp.GetLookup("LearnerFAM", "NLM");
            LLDDAndLearningSupportControl.LLDDHealthProblemList = lp.GetLookup("LLDDHealthProb");
            LLDDAndLearningSupportControl.LSRList = lp.GetLookup("LearnerFAM", "LSR");
            LLDDAndLearningSupportControl.LLDDItemListControl.LLDDCatlList = lp.GetLookup("LLDDCat");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.AimTypeList = lp.GetLookup("AimType");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.FundModelList = lp.GetLookup("FundModel");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.ProgTypeList = lp.GetLookup("ProgType");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.HHSList = lp.GetLookup("LearningDeliveryFAM", "HHS");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.CompStatusList = lp.GetLookup("CompStatus");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.OutcomeList = lp.GetLookup("Outcome");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.WithdrawReasonList = lp.GetLookup("WithdrawReason");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.EmpOutcomeList = lp.GetLookup("EmpOutcome");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.SOC2000List = lp.GetLookup("SOC2000");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.EconomicList = lp.GetLookup("SEC");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.ELQList = lp.GetLookup("ELQ");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.MSTUFEEList = lp.GetLookup("MSTuFee");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.SPECFEEList = lp.GetLookup("SpecFee");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.MODESTUDList = lp.GetLookup("ModeStud");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.FUNDLEVList = lp.GetLookup("FundLev");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.FUNDCOMPList = lp.GetLookup("FundComp");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.QUALENT3List = lp.GetLookup("QualEnt3");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.TYPEYRList = lp.GetLookup("TypeYr");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.SourceOfFundingList = lp.GetLookup("LearningDeliveryFAM", "SOF");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.FullOrCoFundedList = lp.GetLookup("LearningDeliveryFAM", "FFI");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.NatioanSkillAgencyList = lp.GetLookup("LearningDeliveryFAM", "NSA");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.EligibitiytAppFundingList = lp.GetLookup("LearningDeliveryFAM", "EEF");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.SpecialProjectList = lp.GetLookup("LearningDeliveryFAM", "SPP");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.ASLList = lp.GetLookup("LearningDeliveryFAM", "ASL");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.PODList = lp.GetLookup("LearningDeliveryFAM", "POD");
            //LearningDeliveryListControl.LearningDeliveryItemControl.TrailblazersListControl.TrlblazItemControl.TBFinTypeList = lp.GetLookup("TBFinType");
            //LearningDeliveryListControl.LearningDeliveryItemControl.TrailblazersListControl.TrlblazItemControl.TBFinCodetList = lp.GetLookup("TrailblazerApprenticeshipFinancialRecord", "PMR");

            LearningDeliveryListControl.LearningDeliveryItemControl.FinancialDetailsListControl.FinTypeList = lp.GetLookup("AppFinRecord");
            //LearningDeliveryListControl.LearningDeliveryItemControl.FinancialDetailsListControl.FinCodetList_TNP = lp.GetLookup("ApprenticeshipFinancialRecord", "TNP");
            //LearningDeliveryListControl.LearningDeliveryItemControl.FinancialDetailsListControl.FinCodetList_PMR = lp.GetLookup("ApprenticeshipFinancialRecord", "PMR");

            LearningDeliveryListControl.LearningDeliveryItemControl.WorkplaceListControls.WorkPlacementItemControl.WorkplacementCodeList = lp.GetLookup("WorkPlaceMode");
            EmploymentStatusListControl.EmpStausItemControl.EmpStatList = lp.GetLookup("EmpStat");
            EmploymentStatusListControl.EmpStausItemControl.LengthOfUnemploymentList = lp.GetLookup("EmploymentStatusMonitoring", "LOU");
            EmploymentStatusListControl.EmpStausItemControl.BenifitStatusIndicatorList = lp.GetLookup("EmploymentStatusMonitoring", "BSI");
            EmploymentStatusListControl.EmpStausItemControl.EmploymentIntensityIndicatorList = lp.GetLookup("EmploymentStatusMonitoring", "EII");
            EmploymentStatusListControl.EmpStausItemControl.LengthOfEmploymentList = lp.GetLookup("EmploymentStatusMonitoring", "LOE");
            LearnerHEInformationControl.TermTimeAccList = lp.GetLookup("TTAccom");
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

        private void SaveLearner_Click(object sender, RoutedEventArgs e)
        {
            App.ILRMessage.Save();
        }


    }
}
