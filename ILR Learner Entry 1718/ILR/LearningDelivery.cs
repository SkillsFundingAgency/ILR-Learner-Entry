using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace ILR
{
    public class LearningDelivery : ChildEntity, IDataErrorInfo
    {
        private const String CLASSNAME = "LearningDelivery";
        private const String TABS = "\t\t";
        DateTime FIRST_AUG_2013 = new DateTime(2013, 8, 1);
        DateTime FIRST_AUG_2015 = new DateTime(2015, 8, 1);

        #region LD updateLearnerEvent
        public event PropertyChangedEventHandler LearningDeliveryPropertyChanged;

        /// <summary>
        /// Fires the event for the property when it changes.
        /// </summary>
        public void OnLearningDeliveryPropertyChanged()
        {
            if (!this.IsImportRunning)
                LearningDeliveryPropertyChanged(this, new PropertyChangedEventArgs("From LD record"));
        }

        #endregion

        #region Accessors
        public Boolean IsImportRunning { get; set; }
        public override bool IsComplete
        {
            get
            {
                if (IncompleteMessage == null || IncompleteMessage == string.Empty)
                    return true;
                else
                    return false;
            }
        }
        public override string IncompleteMessage
        {
            get
            {
                string message = "";
                message += this["LearnAimRef"];
                message += this["AimType"];
                message += this["AimSeqNumber"];
                message += this["LearnStartDate"];
                message += this["LearnPlanEndDate"];
                message += this["FundModel"];
                message += this["CompStatus"];
				message += this["DelLocPostCode"];			

				return message;
            }
        }
        public bool ShouldProbablyMigrate
        {
            get
            {                
                switch (this.AimType)
                {
                    case 1:
                    case 4:
                        return ((this.FundModel != 70 && this.LearnPlanEndDate >= FIRST_AUG_2015 && this.LearnActEndDate == null) ||(this.FundModel == 70));
                    case 5:
                        if (this.FundModel == 70)
                            return true;
                        else
                        {
                            if ((this.LearnPlanEndDate >= FIRST_AUG_2015) && ((this.LearnActEndDate == null) || (this.Outcome == 8 || this.Outcome == 6)))
                                return this.LearnActEndDate == null || (this.Outcome == 4 || this.Outcome == 5 || this.Outcome == 6);
                            else
                                return false;
                        }
                    case 3:
                        return this.LearnPlanEndDate >= FIRST_AUG_2013 && this.LearnActEndDate == null;
                }
                return false;
            }
        }
        #endregion
        Nullable<int> _defaultStdCode ;

        #region ILR Properties
        public string LearnAimRef { get { return XMLHelper.GetChildValue("LearnAimRef", Node, NSMgr); } set { XMLHelper.SetChildValue("LearnAimRef", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("LearnAimRef"); } }
        public int? AimType { get { string AimType = XMLHelper.GetChildValue("AimType", Node, NSMgr); return (AimType != null ? int.Parse(AimType) : (int?)null); } set { XMLHelper.SetChildValue("AimType", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("AimType"); OnPropertyChanged("AimTypeDescription"); } }
        public int? AimSeqNumber { get { string AimSeqNumber = XMLHelper.GetChildValue("AimSeqNumber", Node, NSMgr); return (AimSeqNumber != null ? int.Parse(AimSeqNumber) : (int?)null); } set { XMLHelper.SetChildValue("AimSeqNumber", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("AimSeqNumber"); } }
        public DateTime? LearnStartDate { get { string LearnStartDate = XMLHelper.GetChildValue("LearnStartDate", Node, NSMgr); return (LearnStartDate != null ? DateTime.Parse(LearnStartDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("LearnStartDate", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("LearnStartDate"); } }
        public DateTime? OrigLearnStartDate { get { string OrigLearnStartDate = XMLHelper.GetChildValue("OrigLearnStartDate", Node, NSMgr); return (OrigLearnStartDate != null ? DateTime.Parse(OrigLearnStartDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("OrigLearnStartDate", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("OrigLearnStartDate"); } }
        public DateTime? LearnPlanEndDate { get { string LearnPlanEndDate = XMLHelper.GetChildValue("LearnPlanEndDate", Node, NSMgr); return (LearnPlanEndDate != null ? DateTime.Parse(LearnPlanEndDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("LearnPlanEndDate", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("LearnPlanEndDate"); } }
        public int? FundModel { get { string FundModel = XMLHelper.GetChildValue("FundModel", Node, NSMgr); return (FundModel != null ? int.Parse(FundModel) : (int?)null); } set { XMLHelper.SetChildValue("FundModel", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("FundModel"); } }
        public int? ProgType { get { string ProgType = XMLHelper.GetChildValue("ProgType", Node, NSMgr); return (ProgType != null ? int.Parse(ProgType) : (int?)null); } set { XMLHelper.SetChildValue("ProgType", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("ProgType"); } }
        public int? FworkCode { get { string FworkCode = XMLHelper.GetChildValue("FworkCode", Node, NSMgr); return (FworkCode != null ? int.Parse(FworkCode) : (int?)null); } set { XMLHelper.SetChildValue("FworkCode", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("FworkCode"); } }
        public int? PwayCode { get { string PwayCode = XMLHelper.GetChildValue("PwayCode", Node, NSMgr); return (PwayCode != null ? int.Parse(PwayCode) : (int?)null); } set { XMLHelper.SetChildValue("PwayCode", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("PwayCode"); } }
        public int? StdCode { get { string StdCode = XMLHelper.GetChildValue("StdCode", Node, NSMgr); return (StdCode != null ? int.Parse(StdCode) : _defaultStdCode); } set { XMLHelper.SetChildValue("StdCode", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("StdCode"); } }
        public int? PartnerUKPRN { get { string PartnerUKPRN = XMLHelper.GetChildValue("PartnerUKPRN", Node, NSMgr); return (PartnerUKPRN != null ? int.Parse(PartnerUKPRN) : (int?)null); } set { XMLHelper.SetChildValue("PartnerUKPRN", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("PartnerUKPRN"); } }
        public string DelLocPostCode { get { return XMLHelper.GetChildValue("DelLocPostCode", Node, NSMgr); } set { XMLHelper.SetChildValue("DelLocPostCode", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("DelLocPostCode"); } }
        public int? AddHours { get { string AddHours = XMLHelper.GetChildValue("AddHours", Node, NSMgr); return (AddHours != null ? int.Parse(AddHours) : (int?)null); } set { XMLHelper.SetChildValue("AddHours", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("AddHours"); } }
        public int? PriorLearnFundAdj { get { string PriorLearnFundAdj = XMLHelper.GetChildValue("PriorLearnFundAdj", Node, NSMgr); return (PriorLearnFundAdj != null ? int.Parse(PriorLearnFundAdj) : (int?)null); } set { XMLHelper.SetChildValue("PriorLearnFundAdj", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("PriorLearnFundAdj"); } }
        public int? OtherFundAdj { get { string OtherFundAdj = XMLHelper.GetChildValue("OtherFundAdj", Node, NSMgr); return (OtherFundAdj != null ? int.Parse(OtherFundAdj) : (int?)null); } set { XMLHelper.SetChildValue("OtherFundAdj", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("OtherFundAdj"); } }
        public string ConRefNumber { get { return XMLHelper.GetChildValue("ConRefNumber", Node, NSMgr); } set { XMLHelper.SetChildValue("ConRefNumber", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("ConRefNumber"); } }
        public string EPAOrgID { get { return XMLHelper.GetChildValue("EPAOrgID", Node, NSMgr); } set { XMLHelper.SetChildValue("EPAOrgID", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("EPAOrgID"); } }
        public int? EmpOutcome { get { string EmpOutcome = XMLHelper.GetChildValue("EmpOutcome", Node, NSMgr); return (EmpOutcome != null ? int.Parse(EmpOutcome) : (int?)null); } set { XMLHelper.SetChildValue("EmpOutcome", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("EmpOutcome"); } }
        public int? CompStatus { get { string CompStatus = XMLHelper.GetChildValue("CompStatus", Node, NSMgr); return (CompStatus != null ? int.Parse(CompStatus) : (int?)null); } set { XMLHelper.SetChildValue("CompStatus", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("CompStatus"); } }
        public DateTime? LearnActEndDate { get { string LearnActEndDate = XMLHelper.GetChildValue("LearnActEndDate", Node, NSMgr); return (LearnActEndDate != null ? DateTime.Parse(LearnActEndDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("LearnActEndDate", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("LearnActEndDate"); } }
        public int? WithdrawReason { get { string WithdrawReason = XMLHelper.GetChildValue("WithdrawReason", Node, NSMgr); return (WithdrawReason != null ? int.Parse(WithdrawReason) : (int?)null); } set { XMLHelper.SetChildValue("WithdrawReason", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("WithdrawReason"); } }
        public int? Outcome { get { string Outcome = XMLHelper.GetChildValue("Outcome", Node, NSMgr); return (Outcome != null ? int.Parse(Outcome) : (int?)null); } set { XMLHelper.SetChildValue("Outcome", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("Outcome"); } }
        public DateTime? AchDate { get { string AchDate = XMLHelper.GetChildValue("AchDate", Node, NSMgr); return (AchDate != null ? DateTime.Parse(AchDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("AchDate", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("AchDate"); } }
        public string OutGrade { get { return XMLHelper.GetChildValue("OutGrade", Node, NSMgr); } set { XMLHelper.SetChildValue("OutGrade", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("OutGrade"); } }
        public string SWSupAimId { get { return XMLHelper.GetChildValue("SWSupAimId", Node, NSMgr); } set { XMLHelper.SetChildValue("SWSupAimId", value, Node, NSMgr); OnLearningDeliveryPropertyChanged(); OnPropertyChanged("SWSupAimId"); } }


      
        #endregion

        #region Lookup Properties
        public string AimTypeDescription
        {
            get
            {
                Lookup lookup = new Lookup();
                return lookup.GetDescription("AimType", this.AimType.ToString());
            }
        }
        #endregion

        #region Renormalised Child Entities
        #region LearningDeliveryFAM
        public bool RES
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.RES) == "1";
            }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.RES, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.RES);
                OnPropertyChanged("RES");
            }
        }
        public bool ADL
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.ADL) == "1";
            }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ADL, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ADL);
                OnPropertyChanged("ADL");
            }
        }
        public bool WPP
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.WPP) == "1";
            }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.WPP, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.WPP);
                OnPropertyChanged("WPP");
            }
        }
        public bool FLN
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.FLN) == "1";
            }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FLN, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FLN);
                OnPropertyChanged("FLN");
            }
        }

        public string SOF
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF);
            }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF);
                OnPropertyChanged("SOF");
            }
        }
        public string FFI
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI);
            }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI);
                OnPropertyChanged("FFI");
            }
        }
        public string EEF
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.EEF);
            }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.EEF, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.EEF);
                OnPropertyChanged("EEF");
            }
        }
        public string ASL
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.ASL);
            }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ASL, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ASL);
                OnPropertyChanged("ASL");
            }
        }
        //public string SPP
        //{
        //    get
        //    {
        //        return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.SPP);
        //    }
        //    set
        //    {
        //        if (value != null)
        //            SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SPP, value);
        //        else
        //            RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SPP);
        //        OnPropertyChanged("SPP");
        //    }
        //}
        public string NSA
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.NSA);
            }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.NSA, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.NSA);
                OnPropertyChanged("NSA");
            }
        }
        public string POD
        {
            get
            {
                return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.POD);
            }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.POD, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.POD);
                OnPropertyChanged("POD");
            }
        }

        public List<LearningDeliveryFAM> HEM
        {
            get
            {
                return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "HEM");
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM, fam.LearnDelFAMCode);
                OnPropertyChanged("HEM");
            }
        }
        public List<LearningDeliveryFAM> LDM2
        {
            get
            {
                return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "LDM");
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM, fam.LearnDelFAMCode);
                OnPropertyChanged("LDM");
            }
        }
        public List<String> HEM2
        {
            get
            {
                List<String> LDMList = new List<string>(0);

                foreach (LearningDeliveryFAM fam in this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "HEM"))
                {
                    if (!String.IsNullOrEmpty(fam.LearnDelFAMCode))
                    {
                        LDMList.Add(fam.LearnDelFAMCode);
                    }
                }
                return LDMList;
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM);
                Int16 i = 1;
                foreach (String code in value)
                {
                    if (!String.IsNullOrEmpty(code))
                    {
                        AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM, code);
                        i++;
                    }
                    if (i > 3) { break; }
                }
                OnPropertyChanged("HEM");
            }
        }
        public List<String> LDM
        {
            get
            {
                List<String> LDMList = new List<string>(0);

                foreach (LearningDeliveryFAM fam in this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "LDM"))
                {
                    if (!String.IsNullOrEmpty(fam.LearnDelFAMCode))
                    {
                        LDMList.Add(fam.LearnDelFAMCode);
                    }
                }
                return LDMList;
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM);
                Int16 i = 1;
                foreach (String code in value)
                {
                    if (!String.IsNullOrEmpty(code))
                    {
                        AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM, code);
                        i++;
                    }
                    if (i > 4) { break; }
                }
                OnPropertyChanged("LDM");
            }
        }
        public List<LearningDeliveryFAM> HHS
        {
            get
            {
                return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "HHS");
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, fam.LearnDelFAMCode);
                OnPropertyChanged("HHS");
            }
        }

        public List<LearningDeliveryFAM> ALB
        {
            get
            {
                return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "ALB");
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.ALB);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.ALB, fam.LearnDelFAMCode);
                OnPropertyChanged("ALB");
            }
        }
        public List<LearningDeliveryFAM> LSF
        {
            get
            {
                return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "LSF");
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.LSF);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LSF, fam.LearnDelFAMCode);
                OnPropertyChanged("LSF");
            }
        }
        public List<LearningDeliveryFAM> ACT
        {
            get
            {
                return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "ACT");
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.ACT);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.ACT, fam.LearnDelFAMCode);
                OnPropertyChanged("ACT");
            }
        }
        //public string ALB1
        //{
        //    get
        //    {
        //        return GetFAMCode(LearningDeliveryFAM.DatedFAMs.ALB);
        //    }
        //    set
        //    {
        //        if (value != null)
        //            SetFAM(LearningDeliveryFAM.DatedFAMs.ALB, value);
        //        else
        //            RemoveFAM(LearningDeliveryFAM.DatedFAMs.ALB);
        //        OnPropertyChanged("ALB");
        //    }
        //}

        //public DateTime? ALBFrom
        //{
        //    get
        //    {
        //        return GetFAMFrom(LearningDeliveryFAM.DatedFAMs.ALB);
        //    }
        //    set
        //    {
        //        SetFAMFrom(LearningDeliveryFAM.DatedFAMs.ALB, value);
        //        OnPropertyChanged("ALBFrom");
        //    }
        //}
        //public DateTime? ALBTo
        //{
        //    get
        //    {
        //        return GetFAMTo(LearningDeliveryFAM.DatedFAMs.ALB);
        //    }
        //    set
        //    {
        //        SetFAMTo(LearningDeliveryFAM.DatedFAMs.ALB, value);
        //        OnPropertyChanged("ALBTo");
        //    }
        //}
        //public bool? LSF1
        //{
        //    get
        //    {
        //        return GetFAMCode(LearningDeliveryFAM.DatedFAMs.LSF) == "1";
        //    }
        //    set
        //    {
        //        if (value == true)
        //            SetFAM(LearningDeliveryFAM.DatedFAMs.LSF, "1");
        //        else
        //            RemoveFAM(LearningDeliveryFAM.DatedFAMs.LSF);
        //        OnPropertyChanged("LSF");
        //    }
        //}
        //public DateTime? LSFFrom
        //{
        //    get
        //    {
        //        return GetFAMFrom(LearningDeliveryFAM.DatedFAMs.LSF);
        //    }
        //    set
        //    {
        //        SetFAMFrom(LearningDeliveryFAM.DatedFAMs.LSF, value);
        //        OnPropertyChanged("LSFFrom");
        //    }
        //}
        //public DateTime? LSFTo
        //{
        //    get
        //    {
        //        return GetFAMTo(LearningDeliveryFAM.DatedFAMs.LSF);
        //    }
        //    set
        //    {
        //        SetFAMTo(LearningDeliveryFAM.DatedFAMs.LSF, value);
        //        OnPropertyChanged("LSFTo");
        //    }
        //}

        //public bool HHS_OneOrMoreApply
        //{
        //    get
        //    {
        //        return !HHS_NoneApply && !HHS_WontSay;
        //    }
        //    set
        //    {

        //    }
        //}
        //public bool HHS_OneOrMoreApply
        //{
        //    get
        //    {
        //        return !HHS_NoneApply && !HHS_WontSay;
        //    }
        //    set
        //    {
        //    }
        //}
        //public bool HHS_OneOrMoreApply_NoMemberIsEmployed
        //{
        //    get
        //    {
        //        List<string> hhs = GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //        return hhs.Contains("2") || hhs.Contains("1");
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //            AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //        }
        //        else
        //            RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //    }
        //}
        //public bool HHS_OneOrMoreApply_OneAdult
        //{
        //    get
        //    {
        //        List<string> hhs = GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //        return hhs.Contains("2")||hhs.Contains("1");
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //            AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //        }
        //        else
        //            RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //    }
        //}
        //public bool HHS_OneOrMoreApply_Kids
        //{
        //    get
        //    {
        //        List<string> hhs = GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //        return hhs.Contains("3")||hhs.Contains("1");
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //            AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "3");
        //        }
        //        else
        //            RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "3");
        //    }
        //}


        public bool HHS_NoneApply
        {
            get
            {
                return GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS).Contains("99");
            }
            set
            {
                if (value)
                {
                    ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "99");
                }
                else
                    RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "99");
            }
        }
        public bool HHS_WontSay
        {
            get
            {
                return GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS).Contains("98");
            }
            set
            {
                if (value)
                {
                    ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "98");
                }
                else
                    RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "98");
            }
        }

        #endregion
        #region ProviderSpecDeliveryMonitoring
        public string ProvSpecMonA
        {
            get
            {
                return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.A);
            }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.A, value);
                OnPropertyChanged("ProvSpecMonA");
            }
        }
        public string ProvSpecMonB
        {
            get
            {
                return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.B);
            }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.B, value);
                OnPropertyChanged("ProvSpecMonB");
            }
        }
        public string ProvSpecMonC
        {
            get
            {
                return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.C);
            }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.C, value);
                OnPropertyChanged("ProvSpecMonC");
            }
        }
        public string ProvSpecMonD
        {
            get
            {
                return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.D);
            }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.D, value);
                OnPropertyChanged("ProvSpecMonD");
            }
        }
        #endregion
        #endregion

        #region FAM Lookups
        public bool HasFAM(string FAMType, string FAMCode)
        {
            var fams = from LearningDeliveryFAM ldFAM in this.LearningDeliveryFAMList where ldFAM.LearnDelFAMType == FAMType && ldFAM.LearnDelFAMCode == FAMCode select ldFAM;
            return fams.Count() > 0;
        }
        public bool HasFAMType(string FAMType)
        {
            return this.LearningDeliveryFAMList.Exists(f => f.LearnDelFAMType == FAMType);
        }
        #endregion

        #region ILR Child Entites
        public List<LearningDeliveryFAM> LearningDeliveryFAMList = new List<LearningDeliveryFAM>();
        private List<TrailblazerApprenticeshipFinancialRecord> TrailblazerApprenticeshipFinancialRecordList = new List<TrailblazerApprenticeshipFinancialRecord>();

        public List<TrailblazerApprenticeshipFinancialRecord> GetTrailblazerApprenticeshipFinancialRecords
        {
            get {
                return TrailblazerApprenticeshipFinancialRecordList;
            }
        }

        public List<ApprenticeshipFinancialRecord> ApprenticeshipFinancialRecordList = new List<ApprenticeshipFinancialRecord>();
        public List<ProviderSpecDeliveryMonitoring> ProviderSpecDeliveryMonitoringList = new List<ProviderSpecDeliveryMonitoring>();
        public LearningDeliveryHE LearningDeliveryHE;
        public List<LearningDeliveryWorkPlacement> LearningDeliveryWorkPlacementList = new List<LearningDeliveryWorkPlacement>();
        #endregion

        #region Child Entity Creation
        public LearningDeliveryFAM CreateLearningDeliveryFAM()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryFAM", NSMgr.LookupNamespace("ia"));
            LearningDeliveryFAM newInstance = new LearningDeliveryFAM(newNode, NSMgr);
            LearningDeliveryFAMList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }
        public ApprenticeshipFinancialRecord CreateApprenticeshipFinancialRecord()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("AppFinRecord", NSMgr.LookupNamespace("ia"));
            ApprenticeshipFinancialRecord newInstance = new ApprenticeshipFinancialRecord(newNode, NSMgr);
            ApprenticeshipFinancialRecordList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }
        public TrailblazerApprenticeshipFinancialRecord CreateTrailblazerApprenticeshipFinancialRecord()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("TrailblazerApprenticeshipFinancialRecord", NSMgr.LookupNamespace("ia"));
            TrailblazerApprenticeshipFinancialRecord newInstance = new TrailblazerApprenticeshipFinancialRecord(newNode, NSMgr);
            TrailblazerApprenticeshipFinancialRecordList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }
        public ProviderSpecDeliveryMonitoring CreateProviderSpecDeliveryMonitoring()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("ProviderSpecDeliveryMonitoring", NSMgr.LookupNamespace("ia"));
            ProviderSpecDeliveryMonitoring newInstance = new ProviderSpecDeliveryMonitoring(newNode, NSMgr);
            ProviderSpecDeliveryMonitoringList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }
        public LearningDeliveryHE CreateLearningDeliveryHE()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryHE", NSMgr.LookupNamespace("ia"));
            LearningDeliveryHE = new LearningDeliveryHE(newNode, NSMgr);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return LearningDeliveryHE;
        }
        public LearningDeliveryWorkPlacement CreateLearningDeliveryWorkPlacement()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryWorkPlacement", NSMgr.LookupNamespace("ia"));
            LearningDeliveryWorkPlacement newInstance = new LearningDeliveryWorkPlacement(newNode, NSMgr);
            LearningDeliveryWorkPlacementList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }
        private void AppendToLastOfNodeNamed(XmlNode NewNode, string NodeName)
        {
            switch (NodeName)
            {
                case "LearningDeliveryFAM":
                    if (LearningDeliveryWorkPlacementList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LearningDeliveryWorkPlacement");
                    else
                        Node.InsertBefore(NewNode, LearningDeliveryWorkPlacementList.First().Node);
                    break;
                case "LearningDeliveryWorkPlacement":
                    if (TrailblazerApprenticeshipFinancialRecordList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "TrailblazerApprenticeshipFinancialRecord");
                    else
                        Node.InsertBefore(NewNode, TrailblazerApprenticeshipFinancialRecordList.First().Node);
                    break;
                case "TrailblazerApprenticeshipFinancialRecord":
                    if (ProviderSpecDeliveryMonitoringList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "ProviderSpecDeliveryMonitoring");
                    else
                        Node.InsertBefore(NewNode, ProviderSpecDeliveryMonitoringList.First().Node);
                    break;
                case "AppFinRecord":
                    if (ProviderSpecDeliveryMonitoringList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "ProviderSpecDeliveryMonitoring");
                    else
                        Node.InsertBefore(NewNode, ProviderSpecDeliveryMonitoringList.First().Node);
                    break;
                case "ProviderSpecDeliveryMonitoring":
                    if (LearningDeliveryHE == null)
                        AppendToLastOfNodeNamed(NewNode, "LearningDeliveryHE");
                    else
                        Node.InsertBefore(NewNode, LearningDeliveryHE.Node);
                    break;
                case "LearningDeliveryHE":
                    Node.AppendChild(NewNode);
                    break;

                    //case "LearningDeliveryFAM":
                    //    if (TrailblazerApprenticeshipFinancialRecordList.Count() == 0)
                    //        AppendToLastOfNodeNamed(NewNode, "TrailblazerApprenticeshipFinancialRecord");
                    //    else
                    //        Node.InsertBefore(NewNode, TrailblazerApprenticeshipFinancialRecordList.First().Node);
                    //    break;
                    //case "TrailblazerApprenticeshipFinancialRecord":
                    //    if (ProviderSpecDeliveryMonitoringList.Count() == 0)
                    //        AppendToLastOfNodeNamed(NewNode, "ProviderSpecDeliveryMonitoring");
                    //    else
                    //        Node.InsertBefore(NewNode, ProviderSpecDeliveryMonitoringList.First().Node);
                    //    break;
                    //case "ProviderSpecDeliveryMonitoring":
                    //    if (LearningDeliveryHE == null)
                    //        AppendToLastOfNodeNamed(NewNode, "LearningDeliveryHE");
                    //    else
                    //        Node.InsertBefore(NewNode, LearningDeliveryHE.Node);
                    //    break;
                    //case "LearningDeliveryHE":
                    //    if (LearningDeliveryWorkPlacementList.Count() == 0)
                    //        AppendToLastOfNodeNamed(NewNode, "LearningDeliveryWorkPlacement");
                    //    else
                    //        Node.InsertBefore(NewNode, LearningDeliveryWorkPlacementList.First().Node);
                    //    break;
                    //case "LearningDeliveryWorkPlacement":
                    //    Node.AppendChild(NewNode);
                    //    break;

            }
        }
        #endregion

        #region Constructors
        internal LearningDelivery(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.IsImportRunning = true;
            this.Node = Node;
            this.NSMgr = NSMgr;

            XmlNodeList nodes = Node.SelectNodes("./ia:LearningDeliveryFAM", NSMgr);
            foreach (XmlNode node in nodes)
                LearningDeliveryFAMList.Add(new LearningDeliveryFAM(node, NSMgr));

            nodes = Node.SelectNodes("./ia:TrailblazerApprenticeshipFinancialRecord", NSMgr);
            foreach (XmlNode node in nodes)
                TrailblazerApprenticeshipFinancialRecordList.Add(new TrailblazerApprenticeshipFinancialRecord(node, NSMgr));

            nodes = Node.SelectNodes("./ia:AppFinRecord", NSMgr);
            foreach (XmlNode node in nodes)
                ApprenticeshipFinancialRecordList.Add(new ApprenticeshipFinancialRecord(node, NSMgr));

            nodes = Node.SelectNodes("./ia:ProviderSpecDeliveryMonitoring", NSMgr);
            foreach (XmlNode node in nodes)
                ProviderSpecDeliveryMonitoringList.Add(new ProviderSpecDeliveryMonitoring(node, NSMgr));

            XmlNode learningDeliveryHENode = Node.SelectSingleNode("./ia:LearningDeliveryHE", NSMgr);
            if (learningDeliveryHENode != null)
                LearningDeliveryHE = new LearningDeliveryHE(learningDeliveryHENode, NSMgr);
            //else
            //LearningDeliveryHE = this.CreateLearningDeliveryHE();

            nodes = Node.SelectNodes("./ia:LearningDeliveryWorkPlacement", NSMgr);
            foreach (XmlNode node in nodes)
                LearningDeliveryWorkPlacementList.Add(new LearningDeliveryWorkPlacement(node, NSMgr));

            OnPropertyChanged("CompStatus");

            this.IsImportRunning = false;

        }
        internal LearningDelivery(LearningDelivery MigrationLearningDelivery, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            IsImportRunning = true;
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.LearnAimRef = MigrationLearningDelivery.LearnAimRef;
            this.AimType = MigrationLearningDelivery.AimType;

            this.AimSeqNumber = MigrationLearningDelivery.AimSeqNumber;

            this.LearnStartDate = MigrationLearningDelivery.LearnStartDate;
            this.OrigLearnStartDate = MigrationLearningDelivery.OrigLearnStartDate;
            this.LearnPlanEndDate = MigrationLearningDelivery.LearnPlanEndDate;
            this.FundModel = MigrationLearningDelivery.FundModel;
            if (MigrationLearningDelivery.ProgType != 10)
                this.ProgType = MigrationLearningDelivery.ProgType;
            this.FworkCode = MigrationLearningDelivery.FworkCode;
            this.PwayCode = MigrationLearningDelivery.PwayCode;

            int stdCode;
            if (MigrationLearningDelivery.HasFAMType("TBS") && int.TryParse(MigrationLearningDelivery.GetLegacyFAM("TBS").LearnDelFAMCode, out stdCode))
                this.StdCode = stdCode;
            else
                this.StdCode = MigrationLearningDelivery.StdCode;

            this.PartnerUKPRN = MigrationLearningDelivery.PartnerUKPRN;
            this.DelLocPostCode = MigrationLearningDelivery.DelLocPostCode;
            this.ConRefNumber = MigrationLearningDelivery.ConRefNumber;
            this.PriorLearnFundAdj = MigrationLearningDelivery.PriorLearnFundAdj;
            this.OtherFundAdj = MigrationLearningDelivery.OtherFundAdj;
            this.EmpOutcome = MigrationLearningDelivery.EmpOutcome;
            this.CompStatus = MigrationLearningDelivery.CompStatus;
            this.LearnActEndDate = MigrationLearningDelivery.LearnActEndDate;
            this.WithdrawReason = MigrationLearningDelivery.WithdrawReason;
            if (Convert.ToInt32(MigrationLearningDelivery.Outcome)==6 || Convert.ToInt32(MigrationLearningDelivery.Outcome) == 7)
                this.Outcome = null;
            else
                this.Outcome = MigrationLearningDelivery.Outcome;

            this.AchDate = MigrationLearningDelivery.AchDate;
            this.OutGrade = MigrationLearningDelivery.OutGrade;
            this.SWSupAimId = MigrationLearningDelivery.SWSupAimId;

            foreach (LearningDeliveryFAM migrationItem in MigrationLearningDelivery.LearningDeliveryFAMList.Where(f=>f.LearnDelFAMType!="TBS"))
            {
                if (migrationItem.LearnDelFAMType != "SPP" && migrationItem.LearnDelFAMType != "WPL" && !(migrationItem.LearnDelFAMType == "LDM" && migrationItem.LearnDelFAMType == "125") || (this.FundModel == 35 && this.LearnStartDate < FIRST_AUG_2013 && (this.ProgType == 2 || this.ProgType == 3 || this.ProgType == 10 || this.ProgType == 20 || this.ProgType == 21 || this.ProgType == 22 || this.ProgType == 23)))
                {
                    XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryFAM", NSMgr.LookupNamespace("ia"));
                    LearningDeliveryFAM newInstance = new LearningDeliveryFAM(migrationItem, newNode, NSMgr);
                    LearningDeliveryFAMList.Add(newInstance);
                    AppendToLastOfNodeNamed(newNode, newNode.Name);
                }
            }
            foreach (LearningDeliveryWorkPlacement migrationItem in MigrationLearningDelivery.LearningDeliveryWorkPlacementList)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryWorkPlacement", NSMgr.LookupNamespace("ia"));
                LearningDeliveryWorkPlacement newInstance = new LearningDeliveryWorkPlacement(migrationItem, newNode, NSMgr);
                LearningDeliveryWorkPlacementList.Add(newInstance);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }
            //foreach (LearningDeliveryWorkPlacement migrationItem in MigrationLearningDelivery.LearningDeliveryWorkPlacementList)
            //{
            //    XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryWorkPlacement", NSMgr.LookupNamespace("ia"));
            //    LearningDeliveryWorkPlacement newInstance = new LearningDeliveryWorkPlacement(migrationItem, newNode, NSMgr);
            //    LearningDeliveryWorkPlacementList.Add(newInstance);
            //    AppendToLastOfNodeNamed(newNode, newNode.Name);
            //}

            foreach (TrailblazerApprenticeshipFinancialRecord migrationItem in MigrationLearningDelivery.TrailblazerApprenticeshipFinancialRecordList)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("AppFinRecord", NSMgr.LookupNamespace("ia"));
                ApprenticeshipFinancialRecord migrationItemAP = CreateApprenticeshipFinancialRecord();

                migrationItemAP.AFinCode = migrationItem.TBFinCode;
                migrationItemAP.AFinType = migrationItem.TBFinType;
                migrationItemAP.AFinDate = migrationItem.TBFinDate;
                migrationItemAP.AFinAmount = migrationItem.TBFinAmount;

               // ApprenticeshipFinancialRecord newInstance = new ApprenticeshipFinancialRecord(migrationItemAP, newNode, NSMgr);
               // ApprenticeshipFinancialRecordList.Add(migrationItemAP);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }
            foreach(ProviderSpecDeliveryMonitoring migrationItem in MigrationLearningDelivery.ProviderSpecDeliveryMonitoringList)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("ProviderSpecDeliveryMonitoring", NSMgr.LookupNamespace("ia"));
                ProviderSpecDeliveryMonitoring migrationItemAP = CreateProviderSpecDeliveryMonitoring();

                migrationItemAP.ProvSpecDelMon = migrationItem.ProvSpecDelMon;
                migrationItemAP.ProvSpecDelMonOccur = migrationItem.ProvSpecDelMonOccur;
                ProviderSpecDeliveryMonitoringList.Add(migrationItemAP);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            if (MigrationLearningDelivery.LearningDeliveryHE != null)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryHE", NSMgr.LookupNamespace("ia"));
                LearningDeliveryHE = new LearningDeliveryHE(MigrationLearningDelivery.LearningDeliveryHE, newNode, NSMgr);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }
            IsImportRunning = false;
            //OnPropertyChanged("LearningDelivery");

        }
        #endregion

        #region Methods
        public void Delete(ChildEntity Child)
        {
            Node.RemoveChild(Child.Node);
            switch (Child.GetType().ToString())
            {
                case "ILR.LearningDeliveryFAM":
                    this.LearningDeliveryFAMList.Remove((LearningDeliveryFAM)Child);
                    break;
                case "ILR.ApprenticeshipFinancialRecord":
                    this.ApprenticeshipFinancialRecordList.Remove((ApprenticeshipFinancialRecord)Child);
                    break;
                case "ILR.ProviderSpecDeliveryMonitoring":
                    this.ProviderSpecDeliveryMonitoringList.Remove((ProviderSpecDeliveryMonitoring)Child);
                    break;
                case "ILR.LearningDeliveryHE":
                    this.LearningDeliveryHE = null;
                    break;
                case "ILR.LearningDeliveryWorkPlacement":
                    this.LearningDeliveryWorkPlacementList.Remove((LearningDeliveryWorkPlacement)Child);
                    break;
            }
        }
        #region FAM management
        private LearningDeliveryFAM GetLegacyFAM(string FAMType)
        {
            return this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType).FirstOrDefault();
        } 
        public LearningDeliveryFAM GetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType)
        {
            return this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()).FirstOrDefault();
        }
        public string GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMCode;
        }
        public void SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType, string FAMCode)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }
            lFAM.LearnDelFAMCode = FAMCode;
        }
        public void RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM != null)
                Delete(lFAM);
        }
        public List<string> GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType)
        {
            List<string> result = new List<string>();
            foreach (LearningDeliveryFAM lFAM in this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()))
                result.Add(lFAM.LearnDelFAMCode);
            return result;
        }
        public void ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType)
        {
            List<LearningDeliveryFAM> TmpList = new List<LearningDeliveryFAM>(0);
            foreach (LearningDeliveryFAM lFAM in this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()))
            {
                TmpList.Add(lFAM);
            }

            foreach (LearningDeliveryFAM lFAM in TmpList)
            { Delete(lFAM); }
            TmpList = null;

        }
        public void AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType, string FAMCode)
        {
            if (this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()
                                                     && x.LearnDelFAMCode == FAMCode.ToString()
                                                  ).Count() == 0)
            {
                LearningDeliveryFAM lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
                lFAM.LearnDelFAMCode = FAMCode;
            }
        }
        public void AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType, string FAMCode, DateTime? FromDate, DateTime? ToDate)
        {
            if (this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()
                                                     && x.LearnDelFAMCode == FAMCode.ToString()
                                                  ).Count() == 0)
            {
                LearningDeliveryFAM lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
                lFAM.LearnDelFAMCode = FAMCode;
                lFAM.LearnDelFAMDateFrom = FromDate;
                lFAM.LearnDelFAMDateTo = ToDate;
            }
        }
        public void RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType, string FAMCode)
        {
            LearningDeliveryFAM ldFAM = this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString() && x.LearnDelFAMCode == FAMCode).FirstOrDefault();
            if (ldFAM != null)
                Delete(ldFAM);
        }
        public LearningDeliveryFAM GetFAM(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            return this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()).FirstOrDefault();
        }
        public string GetFAMCode(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMCode;
        }
        public DateTime? GetFAMFrom(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMDateFrom;
        }
        public DateTime? GetFAMTo(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMDateTo;
        }
        public void SetFAM(LearningDeliveryFAM.DatedFAMs FAMType, string FAMCode)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }
            lFAM.LearnDelFAMCode = FAMCode;
        }
        public void SetFAMFrom(LearningDeliveryFAM.DatedFAMs FAMType, DateTime? FromDate)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }
            lFAM.LearnDelFAMDateFrom = FromDate;
        }
        public void SetFAMTo(LearningDeliveryFAM.DatedFAMs FAMType, DateTime? ToDate)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }
            lFAM.LearnDelFAMDateTo = ToDate;
        }
        public void RemoveFAM(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM != null)
                Delete(lFAM);
        }
        #endregion
        #region ProvSpecMon management
        public ProviderSpecDeliveryMonitoring GetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence Occurrence)
        {
            return this.ProviderSpecDeliveryMonitoringList.Where(x => x.ProvSpecDelMonOccur == Occurrence.ToString()).FirstOrDefault();
        }
        public string GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence Occurrence)
        {
            ProviderSpecDeliveryMonitoring provSpecMon = GetProvSpecMon(Occurrence);
            if (provSpecMon == null)
                return null;
            else
                return provSpecMon.ProvSpecDelMon;
        }
        public void SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence Occurrence, string ProvSpecMonValue)
        {
            ProviderSpecDeliveryMonitoring provSpecMon = GetProvSpecMon(Occurrence);
            if (ProvSpecMonValue != null && ProvSpecMonValue.Length != 0)
            {
                if (provSpecMon == null)
                {
                    provSpecMon = this.CreateProviderSpecDeliveryMonitoring();
                    provSpecMon.ProvSpecDelMonOccur = Occurrence.ToString();
                }
                provSpecMon.ProvSpecDelMon = ProvSpecMonValue;
            }
            else
                if (provSpecMon != null)
                Delete(provSpecMon);
        }
        #endregion
        private void GiveFrountEndkickToRefresh()
        {
            OnPropertyChanged("LearnAimRef");
            OnPropertyChanged("AimType");
            OnPropertyChanged("AimSeqNumber");
            OnPropertyChanged("LearnStartDate");
            OnPropertyChanged("LearnPlanEndDate");
            OnPropertyChanged("FundModel");
            OnPropertyChanged("CompStatus");
        }
        public void RefreshData()
        {
            GiveFrountEndkickToRefresh();
        }
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
                string result = null;
                switch (columnName)
                {
                    case "LearnAimRef":
                        if ((LearnAimRef == null)
                            || ((LearnAimRef != null && LearnAimRef.ToString().Length == 0))
                            )
                            return "\t\tLearnAimRef missing\r\n";
                        if (LearnAimRef != null)
                            return CheckPropertyLength(LearnAimRef, CLASSNAME, columnName, TABS);
                        break;
                    case "AimType":
                        if ((AimType == null)
                            || ((AimType != null && AimType.ToString().Length == 0))
                            )
                            return "\t\tAimType missing\r\n";
                        if (AimType != null)
                            return CheckPropertyLength(AimType, CLASSNAME, columnName, TABS);
                        break;
                    case "AimSeqNumber":
                        if ((AimSeqNumber == null)
                                || ((AimSeqNumber != null && AimSeqNumber.ToString().Length == 0)
                               )
                            )
                            return "\t\tAimSeqNumber missing\r\n";
                        break;
                    case "LearnStartDate":
                        if ((LearnStartDate == null)
                            || ((LearnStartDate != null && LearnStartDate.ToString().Length == 0))
                            )
                            return "\t\tLearnStartDate - required\r\n";
                        break;
                    case "LearnPlanEndDate":
                        if ((LearnPlanEndDate == null)
                        || ((LearnPlanEndDate != null && LearnPlanEndDate.ToString().Length == 0))
                        )
                            return "\t\tLearnPlanEndDate - required\r\n";
                        break;
                    //case "LearnActEndDate":
                    //    if (LearnActEndDate != null)
                    //    {
                    //        //if (LearnActEndDate < (System.DateTime.Now.AddYears(-100)))
                    //        // 			return "Learn Act EndDate - required\r\n";
                    //        //if (LearnActEndDate < LearnStartDate)
                    //        //    return "Learn Act EndDate - before Start Date";
                    //    }
                    //    break;
                    case "FundModel":
                        if ((FundModel == null)
                            || ((FundModel != null && FundModel.ToString().Length == 0))
                            )
                            return "\t\tFundModel - required\r\n";
                        if (FundModel != null)
                            return CheckPropertyLength(FundModel, CLASSNAME, columnName, TABS);
                        break;
                    case "ProgType":
                        if (ProgType != null && ProgType.ToString().Length > GetItemSize("LearningDelivery." + columnName))
                            if (ProgType != null)
                                return CheckPropertyLength(ProgType, CLASSNAME, columnName, TABS);
                        break;
                    case "FworkCode":
                        if (FworkCode != null)
                            return CheckPropertyLength(FworkCode, CLASSNAME, columnName, TABS);
                        break;
                    case "PwayCode":
                        if (PwayCode != null)
                            return CheckPropertyLength(PwayCode, CLASSNAME, columnName, TABS);
                        break;
                    case "StdCode":
                        if (StdCode != null)
                            return CheckPropertyLength(StdCode, CLASSNAME, columnName, TABS);
                        break;
                    case "PartnerUKPRN":
                        if (PartnerUKPRN != null)
                            return CheckPropertyLength(PartnerUKPRN, CLASSNAME, columnName, TABS);
                        break;
                    case "PriorLearnFundAdj":
                        if (PriorLearnFundAdj != null)
                            return CheckPropertyLength(PriorLearnFundAdj, CLASSNAME, columnName, TABS);
                        break;
                    case "OtherFundAdj":
                        if (OtherFundAdj != null)
                            return CheckPropertyLength(OtherFundAdj, CLASSNAME, columnName, TABS);
                        break;
                    case "EmpOutcome":
                        if (EmpOutcome != null)
                            return CheckPropertyLength(EmpOutcome, CLASSNAME, columnName, TABS);
                        break;
                    case "CompStatus":
                        if ((CompStatus == null)
                        || ((CompStatus != null && CompStatus.ToString().Length == 0))
                        )
                            return "\t\tCompStatus - required\r\n";
                        if (CompStatus != null)
                            return CheckPropertyLength(CompStatus, CLASSNAME, columnName, TABS);
                        break;
                    case "WithdrawReason":
                        if (WithdrawReason != null)
                            return CheckPropertyLength(WithdrawReason, CLASSNAME, columnName, TABS);
                        break;
                    case "Outcome":
                        if (Outcome != null)
                            return CheckPropertyLength(Outcome, CLASSNAME, columnName, TABS);
                        break;
                    case "AddHours":
                        if (AddHours != null)
                            return CheckPropertyLength(AddHours, CLASSNAME, columnName, TABS);
                        break;
                    case "ConRefNumber":
                        if (ConRefNumber != null)
                            return CheckPropertyLength(ConRefNumber, CLASSNAME, columnName, TABS);
                        break;
					case "DelLocPostCode":
						if ((DelLocPostCode == null)
					   || ((DelLocPostCode != null && DelLocPostCode.ToString().Length == 0))
					   )
							return "\t\tDel Loc Post Code - required\r\n";
						break;
                    case "EPAOrgID":
                        if (EPAOrgID != null)
                            return CheckPropertyLength(EPAOrgID, CLASSNAME, columnName, TABS);
                        break;                        
                    default:
                        break;
                }
                return result;
            }
        }
        #endregion

    }
}
