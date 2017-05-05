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
