using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

 
namespace ILR
{
    public class DPOutcome : ChildEntity, IDataErrorInfo
    {

        #region ILR Properties
        public string OutType { get { return XMLHelper.GetChildValue("OutType", Node, NSMgr); } set { XMLHelper.SetChildValue("OutType", value, Node, NSMgr); OnPropertyChanged("OutType"); } }
        public int? OutCode { get { string OutCode = XMLHelper.GetChildValue("OutCode", Node, NSMgr); return (OutCode != null ? int.Parse(OutCode) : (int?)null); } set { XMLHelper.SetChildValue("OutCode", value, Node, NSMgr); OnPropertyChanged("OutCode"); } }
        public DateTime? OutStartDate { get { string OutStartDate = XMLHelper.GetChildValue("OutStartDate", Node, NSMgr); return (OutStartDate != null ? DateTime.Parse(OutStartDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("OutStartDate", value, Node, NSMgr); OnPropertyChanged("OutStartDate"); } }
        public DateTime? OutEndDate { get { string OutEndDate = XMLHelper.GetChildValue("OutEndDate", Node, NSMgr); return (OutEndDate != null ? DateTime.Parse(OutEndDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("OutEndDate", value, Node, NSMgr); OnPropertyChanged("OutEndDate"); } }
        public DateTime? OutCollDate { get { string OutCollDate = XMLHelper.GetChildValue("OutCollDate", Node, NSMgr); return (OutCollDate != null ? DateTime.Parse(OutCollDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("OutCollDate", value, Node, NSMgr); OnPropertyChanged("OutCollDate"); } }
        #endregion

        #region Constructors
        internal DPOutcome(XmlNode DPOutcomeNode, XmlNamespaceManager NSMgr)
        {
            this.Node = DPOutcomeNode;
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
                if (columnName == "OutType")
                {
                    if (OutType == null)
                    {
                        result = "OutType not supplied.";
                    }
                }

                if (columnName == "ULN")
                {
                    if (OutCode != null && OutCode.ToString().Length > 9)
                        result = "OutCode Number exceeds maximum length (9 digits).";
                }
                
                return result;
            }
        }

        #endregion

    }
}
