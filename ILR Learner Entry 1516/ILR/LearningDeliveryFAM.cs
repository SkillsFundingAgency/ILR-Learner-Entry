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
        public enum SingleOccurrenceFAMs { SOF, FFI, WPL, EEF, RES, ADL, ASL, SPP, NSA, WPP, POD, TBS, FLN }
        public enum MultiOccurrenceFAMs { LDM, HEM, ALB, LSF, HHS }
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

            this.LearnDelFAMType = MigrationLearningDeliveryFAM.LearnDelFAMType;
            if (MigrationLearningDeliveryFAM.LearnDelFAMType == "HEM" && (MigrationLearningDeliveryFAM.LearnDelFAMCode == "2" || MigrationLearningDeliveryFAM.LearnDelFAMCode == "4"))
                this.LearnDelFAMCode = "5";
            else
                this.LearnDelFAMCode = MigrationLearningDeliveryFAM.LearnDelFAMCode;
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
                return result;
            }
        }
        #endregion

    }
}
