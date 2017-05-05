using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class LearningProvider : ChildEntity, IDataErrorInfo
    {
        #region ILR Properties
        public int? UKPRN { get { string UKPRN = XMLHelper.GetChildValue("UKPRN", Node, NSMgr); return (UKPRN != null ? int.Parse(UKPRN) : (int?)null); } set { XMLHelper.SetChildValue("UKPRN", value, Node, NSMgr); OnPropertyChanged("UKPRN"); } }
        #endregion

        #region Constructors
        internal LearningProvider(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
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