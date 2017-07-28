using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class TrailblazerApprenticeshipFinancialRecord : ChildEntity, IDataErrorInfo
    {

        #region ILR Properties
        public string TBFinType { get { return XMLHelper.GetChildValue("TBFinType", Node, NSMgr); } set { XMLHelper.SetChildValue("TBFinType", value, Node, NSMgr); OnPropertyChanged("TBFinType"); } }
        public int? TBFinCode { get { string TBFinCode = XMLHelper.GetChildValue("TBFinCode", Node, NSMgr); return (TBFinCode != null ? int.Parse(TBFinCode) : (int?)null); } set { XMLHelper.SetChildValue("TBFinCode", value, Node, NSMgr); OnPropertyChanged("TBFinCode"); } }
        public DateTime? TBFinDate { get { string TBFinDate = XMLHelper.GetChildValue("TBFinDate", Node, NSMgr); return (TBFinDate != null ? DateTime.Parse(TBFinDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("TBFinDate", value, Node, NSMgr); OnPropertyChanged("TBFinDate"); } }
        public int? TBFinAmount { get { string TBFinAmount = XMLHelper.GetChildValue("TBFinAmount", Node, NSMgr); return (TBFinAmount != null ? int.Parse(TBFinAmount) : (int?)null); } set { XMLHelper.SetChildValue("TBFinAmount", value, Node, NSMgr); OnPropertyChanged("TBFinAmount"); } }
        #endregion

        #region Constructors
        internal TrailblazerApprenticeshipFinancialRecord(XmlNode ApprenticeshipTrailblazerFinancialDetailsNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ApprenticeshipTrailblazerFinancialDetailsNode;
            this.NSMgr = NSMgr;
        }
        internal TrailblazerApprenticeshipFinancialRecord(TrailblazerApprenticeshipFinancialRecord MigrationLearnerEmploymentStatus, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.TBFinType = MigrationLearnerEmploymentStatus.TBFinType;
            this.TBFinCode = MigrationLearnerEmploymentStatus.TBFinCode;
            this.TBFinDate = MigrationLearnerEmploymentStatus.TBFinDate;
            this.TBFinAmount = MigrationLearnerEmploymentStatus.TBFinAmount;

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
                if (columnName == "TBFinCode")
                {
                    if (TBFinCode != null && TBFinCode.ToString().Length > 8)
                    {
                        result = "TBFinCode exceeds maximum length (8).";
                        TBFinCode = (int?)int.Parse(TBFinCode.ToString().Substring(0, 8));
                    }
                }
                if (columnName == "TBFinAmount")
                {
                    if (TBFinAmount != null && TBFinAmount.ToString().Length > 8)
                    {
                        result = "TBFinAmount exceeds maximum length (8).";
                        TBFinAmount = (int?)int.Parse(TBFinAmount.ToString().Substring(0, 8));
                    }
                }
                return result;
            }
        }
        #endregion
    }
}
