using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

 
namespace ILR
{
    public class ContactPreference : ChildEntity, IDataErrorInfo
    {

        #region ILR Properties
        public string ContPrefType { get { return XMLHelper.GetChildValue("ContPrefType", Node, NSMgr); } set { XMLHelper.SetChildValue("ContPrefType", value, Node, NSMgr); OnPropertyChanged("ContPrefType"); } }
        public int? ContPrefCode { get { string ContPrefCode = XMLHelper.GetChildValue("ContPrefCode", Node, NSMgr); return (ContPrefCode != null ? int.Parse(ContPrefCode) : (int?)null); } set { XMLHelper.SetChildValue("ContPrefCode", value, Node, NSMgr); OnPropertyChanged("ContPrefCode"); } }
        #endregion

        #region Constructors
        internal ContactPreference(XmlNode ContactPreferenceNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ContactPreferenceNode;
            this.NSMgr = NSMgr;
        }
        internal ContactPreference(ContactPreference MigrationContactPreference, XmlNode ContactPreferenceNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ContactPreferenceNode;
            this.NSMgr = NSMgr;

            this.ContPrefType = MigrationContactPreference.ContPrefType;
            this.ContPrefCode = MigrationContactPreference.ContPrefCode;
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
