using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class LearnerFAM : ChildEntity, IDataErrorInfo
    {
        #region Enums
        public enum SingleOccurrenceFAMs { LDA, HNS, EHC, DLA, MGA, EGA, FME }
        public enum MultiOccurrenceFAMs { LSR, NLM, PPE }
        #endregion

        #region ILR Properties
        public string LearnFAMType
        {
            get
            {
                return XMLHelper.GetChildValue("LearnFAMType", Node, NSMgr);
            }
            set
            {
                XMLHelper.SetChildValue("LearnFAMType", value, Node, NSMgr);
            }
        }
        public int LearnFAMCode
        {
            get
            {
                string LearnFAMCode = XMLHelper.GetChildValue("LearnFAMCode", Node, NSMgr);
                return int.Parse(LearnFAMCode);
            }
            set
            {
                XMLHelper.SetChildValue("LearnFAMCode", value, Node, NSMgr);
            }
        }
        #endregion

        #region Constructors
        internal LearnerFAM(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;
        }
        internal LearnerFAM(LearnerFAM MigrationLearnerFAM, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.LearnFAMType = MigrationLearnerFAM.LearnFAMType;
            this.LearnFAMCode = MigrationLearnerFAM.LearnFAMCode;
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