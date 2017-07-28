using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class PostAdd : ChildEntity, IDataErrorInfo
    {
        #region Accessors
        public override bool IsComplete
        {
            get
            {
                return this.AddLine1 != null;
            }
        }
        #endregion

        #region ILR Properties
        public string AddLine1 { get { return XMLHelper.GetChildValue("AddLine1", Node, NSMgr); } set { XMLHelper.SetChildValue("AddLine1", value, Node, NSMgr); } }
        public string AddLine2 { get { return XMLHelper.GetChildValue("AddLine2", Node, NSMgr); } set { XMLHelper.SetChildValue("AddLine2", value, Node, NSMgr); } }
        public string AddLine3 { get { return XMLHelper.GetChildValue("AddLine3", Node, NSMgr); } set { XMLHelper.SetChildValue("AddLine3", value, Node, NSMgr); } }
        public string AddLine4 { get { return XMLHelper.GetChildValue("AddLine4", Node, NSMgr); } set { XMLHelper.SetChildValue("AddLine4", value, Node, NSMgr); } }
        #endregion

        #region Constructors
        internal PostAdd(XmlNode PostAddNode, XmlNamespaceManager NSMgr)
        {
            this.Node = PostAddNode;
            this.NSMgr = NSMgr;
        }
        internal PostAdd(PostAdd MigrationPostAdd, XmlNode PostAddNode, XmlNamespaceManager NSMgr)
        {
            this.Node = PostAddNode;
            this.NSMgr = NSMgr;

            this.AddLine1 = MigrationPostAdd.AddLine1;
            this.AddLine2 = MigrationPostAdd.AddLine2;
            this.AddLine3 = MigrationPostAdd.AddLine3;
            this.AddLine4 = MigrationPostAdd.AddLine4;
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
				if (columnName == "AddLine1")
				{
					if (AddLine1 != null && AddLine1.ToString().Length > 100)
					{
						result = "AddLine1 exceeds maximum length (100 digits).";
						//AddLine1 = AddLine1.ToString().Substring(0, 100);
					}
				}
				if (columnName == "AddLine2")
				{
					if (AddLine2 != null && AddLine2.ToString().Length > 100)
					{
						result = "AddLine2 exceeds maximum length (100 digits).";
						//AddLine2 = AddLine2.ToString().Substring(0, 100);
					}
				}
				if (columnName == "AddLine3")
				{
					if (AddLine3 != null && AddLine3.ToString().Length > 100)
					{
						result = "AddLine3 exceeds maximum length (100 digits).";
						//AddLine3 = AddLine3.ToString().Substring(0, 100);
					}
				}
				if (columnName == "AddLine4")
				{
					if (AddLine4 != null && AddLine4.ToString().Length > 100)
					{
						result = "AddLine4 exceeds maximum length (100 digits).";
						//AddLine4 = AddLine4.ToString().Substring(0, 100);
					}
				}
                return result;
            }
        }

        #endregion
    }
}
