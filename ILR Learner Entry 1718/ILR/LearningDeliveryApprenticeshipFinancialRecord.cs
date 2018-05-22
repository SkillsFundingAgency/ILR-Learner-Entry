using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class ApprenticeshipFinancialRecord : ChildEntity, IDataErrorInfo
    {

        #region ILR Properties
        public string AFinType { get { return XMLHelper.GetChildValue("AFinType", Node, NSMgr); } set { XMLHelper.SetChildValue("AFinType", value, Node, NSMgr); OnPropertyChanged("AFinType"); } }
        public int? AFinCode { get { string AFinCode = XMLHelper.GetChildValue("AFinCode", Node, NSMgr); return (!string.IsNullOrEmpty(AFinCode) ? int.Parse(AFinCode) : (int?)null); } set { XMLHelper.SetChildValue("AFinCode", value, Node, NSMgr); OnPropertyChanged("AFinCode"); } }
        public DateTime? AFinDate { get { string TBFinDate = XMLHelper.GetChildValue("AFinDate", Node, NSMgr); return (!string.IsNullOrEmpty(TBFinDate) ? DateTime.Parse(TBFinDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("AFinDate", value, Node, NSMgr); OnPropertyChanged("AFinDate"); } }
        public int? AFinAmount { get { string AFinAmount = XMLHelper.GetChildValue("AFinAmount", Node, NSMgr); return (!string.IsNullOrEmpty(AFinAmount) ? int.Parse(AFinAmount) : (int?)null); } set { XMLHelper.SetChildValue("AFinAmount", value, Node, NSMgr); OnPropertyChanged("AFinAmount"); } }
        #endregion

        #region Constructors
        internal ApprenticeshipFinancialRecord(XmlNode ApprenticeshipTrailblazerFinancialDetailsNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ApprenticeshipTrailblazerFinancialDetailsNode;
            this.NSMgr = NSMgr;
        }
        internal ApprenticeshipFinancialRecord(ApprenticeshipFinancialRecord MigrationApprenticeshipFinancialRecord, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.AFinType = MigrationApprenticeshipFinancialRecord.AFinType;
            this.AFinCode = MigrationApprenticeshipFinancialRecord.AFinCode;
            this.AFinDate = MigrationApprenticeshipFinancialRecord.AFinDate;
            this.AFinAmount = MigrationApprenticeshipFinancialRecord.AFinAmount;

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
                if (columnName == "AFinCode")
                {
                    if (AFinCode != null && AFinCode.ToString().Length > 8)
                    {
                        result = "AFinCode exceeds maximum length (8).";
                        AFinCode = (int?)int.Parse(AFinCode.ToString().Substring(0, 8));
                    }
                }
                if (columnName == "AFinAmount")
                {
                    if (AFinAmount != null && AFinAmount.ToString().Length > 8)
                    {
                        result = "AFinAmount exceeds maximum length (8).";
                        AFinAmount = (int?)int.Parse(AFinAmount.ToString().Substring(0, 8));
                    }
                }
                return result;
            }
        }
        #endregion
    }
}
