using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class Source : ChildEntity, IDataErrorInfo
    {
        #region ILR Properties
        public string ProtectiveMarking { get { return XMLHelper.GetChildValue("ProtectiveMarking", Node, NSMgr); } set { XMLHelper.SetChildValue("ProtectiveMarking", value, Node, NSMgr); } }
        public int? UKPRN { get { string UKPRN = XMLHelper.GetChildValue("UKPRN", Node, NSMgr); return (UKPRN != null ? int.Parse(UKPRN) : (int?)null); } set { XMLHelper.SetChildValue("UKPRN", value, Node, NSMgr); } }
        public string SoftwareSupplier { get { return XMLHelper.GetChildValue("SoftwareSupplier", Node, NSMgr); } set { XMLHelper.SetChildValue("SoftwareSupplier", value, Node, NSMgr); } }
        public string SoftwarePackage { get { return XMLHelper.GetChildValue("SoftwarePackage", Node, NSMgr); } set { XMLHelper.SetChildValue("SoftwarePackage", value, Node, NSMgr); } }
        public string Release { get { return XMLHelper.GetChildValue("Release", Node, NSMgr); } set { XMLHelper.SetChildValue("Release", value, Node, NSMgr); } }
        public string SerialNo { get { return XMLHelper.GetChildValue("SerialNo", Node, NSMgr); } set { XMLHelper.SetChildValue("SerialNo", value, Node, NSMgr); } }
        public DateTime? DateTime { get { string DateTime = XMLHelper.GetChildValue("DateTime", Node, NSMgr); return (DateTime != null ? System.DateTime.Parse(DateTime) : (DateTime?)null); } set { XMLHelper.SetChildValue("DateTime", value, Node, NSMgr); } }
        public string ReferenceData { get { return XMLHelper.GetChildValue("ReferenceData", Node, NSMgr); } set { XMLHelper.SetChildValue("ReferenceData", value, Node, NSMgr); } }
        public string ComponentSetVersion { get { return XMLHelper.GetChildValue("ComponentSetVersion", Node, NSMgr); } set { XMLHelper.SetChildValue("ComponentSetVersion", value, Node, NSMgr); } }
        #endregion

        #region Constructors
        internal Source(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            if (ProtectiveMarking == null)
                ProtectiveMarking = Message.ProtectiveMarking;
            if (SoftwareSupplier == null)
                SoftwareSupplier = "Skills Funding Agency";
            if (SoftwarePackage == null)
                SoftwarePackage = "ILR Learner Entry";
            if (Release == null)
                Release = "0.2.0.0";
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
				if (columnName == "UKPRN")
				{
					if (UKPRN != null && UKPRN.ToString().Length > 8)
					{
						result = "UKPRN exceeds maximum length (8 digits).";
					//	UKPRN = (int?)int.Parse(UKPRN.ToString().Substring(0, 8));
					}
				}
                return result;
            }
        }

        #endregion

    }
}
