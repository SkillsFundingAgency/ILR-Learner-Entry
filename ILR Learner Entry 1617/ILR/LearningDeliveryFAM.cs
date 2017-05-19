using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class LearningDeliveryFAM : ChildEntity, IDataErrorInfo
    {
        #region Enums
        public enum SingleOccurrenceFAMs { SOF, FFI, EEF, RES, ADL, ASL, SPP, NSA, WPP, POD, FLN }
        public enum MultiOccurrenceFAMs { LDM, HEM, ALB, LSF, HHS, ACT }
        public enum DatedFAMs { ABCDF, GHIH } //{ ALB, LSF }
        #endregion

        #region Accessors
        public override bool IsComplete
        {
            get
            {
                if (this.LearnDelFAMCode == null || this.LearnDelFAMType == null)
                    return false;

                return true;
            }
        }
        public override string IncompleteMessage
        {
            get
            {
                string message = "";

                if (this.LearnDelFAMType == null)
                    message += "LearnDelFAMType missing\r\n";
                if (this.LearnDelFAMCode == null)
                    message += "LearnDelFAMCode missing\r\n";

                return message;
            }
        }
        #endregion

        #region ILR Properties
        public string LearnDelFAMType { get { return XMLHelper.GetChildValue("LearnDelFAMType", Node, NSMgr); } set { XMLHelper.SetChildValue("LearnDelFAMType", value, Node, NSMgr); OnPropertyChanged("LearnDelFAMType"); } }
        public string LearnDelFAMCode { get { return XMLHelper.GetChildValue("LearnDelFAMCode", Node, NSMgr); } set { XMLHelper.SetChildValue("LearnDelFAMCode", value, Node, NSMgr); OnPropertyChanged("LearnDelFAMCode"); } }
        public DateTime? LearnDelFAMDateFrom { get { string LearnDelFAMDateFrom = XMLHelper.GetChildValue("LearnDelFAMDateFrom", Node, NSMgr); return (LearnDelFAMDateFrom != null ? DateTime.Parse(LearnDelFAMDateFrom) : (DateTime?)null); } set { XMLHelper.SetChildValue("LearnDelFAMDateFrom", value, Node, NSMgr); OnPropertyChanged("LearnDelFAMDateFrom"); } }
        public DateTime? LearnDelFAMDateTo { get { string LearnDelFAMDateTo = XMLHelper.GetChildValue("LearnDelFAMDateTo", Node, NSMgr); return (LearnDelFAMDateTo != null ? DateTime.Parse(LearnDelFAMDateTo) : (DateTime?)null); } set { XMLHelper.SetChildValue("LearnDelFAMDateTo", value, Node, NSMgr); OnPropertyChanged("LearnDelFAMDateTo"); } }
        #endregion

        #region Constructors
        internal LearningDeliveryFAM(XmlNode LearningDeliveryFAMNode, XmlNamespaceManager NSMgr)
        {
            this.Node = LearningDeliveryFAMNode;
            this.NSMgr = NSMgr;
        }
        internal LearningDeliveryFAM(LearningDeliveryFAM MigrationLearningDeliveryFAM, XmlNode LearningDeliveryFAMNode, XmlNamespaceManager NSMgr)
        {
            this.Node = LearningDeliveryFAMNode;
            this.NSMgr = NSMgr;

            if ((MigrationLearningDeliveryFAM.LearnDelFAMType == "LDM" && MigrationLearningDeliveryFAM.LearnDelFAMCode == "125") || MigrationLearningDeliveryFAM.LearnDelFAMType == "WPL")
            {
                this.LearnDelFAMType = "LDM";
                this.LearnDelFAMCode = "350";
            }
            else
            {
                this.LearnDelFAMType = MigrationLearningDeliveryFAM.LearnDelFAMType;
                this.LearnDelFAMCode = MigrationLearningDeliveryFAM.LearnDelFAMCode;
            }
            this.LearnDelFAMDateFrom = MigrationLearningDeliveryFAM.LearnDelFAMDateFrom;
            this.LearnDelFAMDateTo = MigrationLearningDeliveryFAM.LearnDelFAMDateTo;
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
                if (columnName == "NINumber")
                {
                    result = "Error message here";
                }

                switch (columnName.ToUpper())
                {
                    case "LEARNDELFAMCODE":
                        switch (LearnDelFAMType.ToUpper())
                        {
                            case "ALB":
                            case "ACT":
                                int tmpint;
                                bool x = Int32.TryParse(System.Convert.ToString(LearnDelFAMCode), out tmpint);
                                if (x)
                                {
                                    switch (LearnDelFAMType.ToUpper())
                                    {
                                        //case "LSF":
                                        //    if (tmpint != 1) { result = "Invalid Value"; }
                                        //    break;
                                        //case "ALB":
                                        //    if (tmpint != 1) { result = "Invalid Value"; }
                                        //    break;
                                        case "ACT":
                                            if ((tmpint < 1) || (tmpint > 2)) { result = "Invalid Value"; }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    result = "Invalid Value";
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                }
                return result;
            }
        }
        #endregion

    }
}
