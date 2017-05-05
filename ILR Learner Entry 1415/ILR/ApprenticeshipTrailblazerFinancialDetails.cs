using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class ApprenticeshipTrailblazerFinancialDetails : ChildEntity, IDataErrorInfo
    {

        #region ILR Properties
        public string TBFinType { get { return XMLHelper.GetChildValue("TBFinType", Node, NSMgr); } set { XMLHelper.SetChildValue("TBFinType", value, Node, NSMgr); OnPropertyChanged("TBFinType"); } }
        public int? TBFinCode { get { string TBFinCode = XMLHelper.GetChildValue("TBFinCode", Node, NSMgr); return (TBFinCode != null ? int.Parse(TBFinCode) : (int?)null); } set { XMLHelper.SetChildValue("TBFinCode", value, Node, NSMgr); OnPropertyChanged("TBFinCode"); } }
        public DateTime? TBFinDate { get { string TBFinDate = XMLHelper.GetChildValue("TBFinDate", Node, NSMgr); return (TBFinDate != null ? DateTime.Parse(TBFinDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("TBFinDate", value, Node, NSMgr); OnPropertyChanged("TBFinDate"); } }
        public int? TBFinAmount { get { string TBFinAmount = XMLHelper.GetChildValue("TBFinAmount", Node, NSMgr); return (TBFinAmount != null ? int.Parse(TBFinAmount) : (int?)null); } set { XMLHelper.SetChildValue("TBFinAmount", value, Node, NSMgr); OnPropertyChanged("TBFinAmount"); } }
        #endregion

        #region Constructors
        internal ApprenticeshipTrailblazerFinancialDetails(XmlNode ApprenticeshipTrailblazerFinancialDetailsNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ApprenticeshipTrailblazerFinancialDetailsNode;
            this.NSMgr = NSMgr;
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
                return result;
            }
        }
        #endregion
    }
}
