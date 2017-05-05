using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

 
namespace ILR
{
    public class LearnerContact : ChildEntity, IDataErrorInfo
    {
        #region Accessors
        public override bool IsComplete
        {
            get
            {
                return this.ContType != null && this.LocType != null && (this.LocType!=1 || (this.PostAdd!=null && this.PostAdd.IsComplete));
            }
        }
        #endregion

        #region ILR Properties
        public int? LocType { get { string LocType = XMLHelper.GetChildValue("LocType", Node, NSMgr); return (LocType != null ? int.Parse(LocType) : (int?)null); } set { XMLHelper.SetChildValue("LocType", value, Node, NSMgr); } }
        public int? ContType { get { string ContType = XMLHelper.GetChildValue("ContType", Node, NSMgr); return (ContType != null ? int.Parse(ContType) : (int?)null); } set { XMLHelper.SetChildValue("ContType", value, Node, NSMgr); } }
        public string PostCode { get { return XMLHelper.GetChildValue("PostCode", Node, NSMgr); } set { XMLHelper.SetChildValue("PostCode", value, Node, NSMgr); } }
        public string TelNumber { get { return XMLHelper.GetChildValue("TelNumber", Node, NSMgr); } set { XMLHelper.SetChildValue("TelNumber", value, Node, NSMgr); } }
        public string Email { get { return XMLHelper.GetChildValue("Email", Node, NSMgr); } set { XMLHelper.SetChildValue("Email", value, Node, NSMgr); } }
        #endregion

        #region ILR Child Entites
        public PostAdd PostAdd;
        #endregion

        #region Child Entity Creation
        public PostAdd CreatePostAdd()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("PostAdd", NSMgr.LookupNamespace("ia"));
            PostAdd = new PostAdd(newNode, NSMgr);
            Node.AppendChild(newNode);
            return PostAdd;
        }
        #endregion

        #region Constructors
        internal LearnerContact(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            if (LocType == 1 && ContType == 2)
            {
                XmlNode postAddNode = Node.SelectSingleNode("./ia:PostAdd", NSMgr);
                if(postAddNode!=null)
                    PostAdd=new PostAdd(postAddNode, NSMgr);
            }
        }
        internal LearnerContact(LearnerContact MigrationLearnerContact, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.LocType = MigrationLearnerContact.LocType;
            this.ContType = MigrationLearnerContact.ContType;
            this.Email = MigrationLearnerContact.Email;
            this.TelNumber = MigrationLearnerContact.TelNumber;
            this.PostCode = MigrationLearnerContact.PostCode;
            if (MigrationLearnerContact.PostAdd != null)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("PostAdd", NSMgr.LookupNamespace("ia"));
                this.PostAdd = new PostAdd(MigrationLearnerContact.PostAdd, newNode, NSMgr);
                Node.AppendChild(newNode);
            }
        }
        #endregion

        #region Methods
        public void Delete(PostAdd postAdd)
        {
            Node.RemoveChild(postAdd.Node);
            this.PostAdd = null;
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
				if (columnName == "LocType")
				{
					if (LocType != null && LocType.ToString().Length > 3)
					{
						result = "LocType exceeds maximum length (3).";
						//LocType = (int?)int.Parse(LocType.ToString().Substring(0, 3));
					}
				}
				if (columnName == "ContType")
				{
					if (ContType != null && ContType.ToString().Length > 2)
					{
						result = "ContType exceeds maximum length (2).";
						//ContType = (int?)int.Parse(ContType.ToString().Substring(0, 2));
					}
				} 
                return result;
            }
        }

        #endregion


    }
}
