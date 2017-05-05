using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class LLDDandHealthProblem : ChildEntity, IDataErrorInfo
    {
        #region Enums
        public enum LLDDandHealthProblems { DS, LD }
        #endregion

        #region ILR Properties
        public string LLDDType { get { return XMLHelper.GetChildValue("LLDDType", Node, NSMgr); } set { XMLHelper.SetChildValue("LLDDType", value, Node, NSMgr); } }
        public int? LLDDCode { get { string LLDDCode = XMLHelper.GetChildValue("LLDDCode", Node, NSMgr); return (LLDDCode != null ? int.Parse(LLDDCode) : (int?)null); } set { XMLHelper.SetChildValue("LLDDCode", value, Node, NSMgr); } }
        #endregion

        #region Constructors
        internal LLDDandHealthProblem(XmlNode LLDDandHealthProblemNode, XmlNamespaceManager NSMgr)
        {
            this.Node = LLDDandHealthProblemNode;
            this.NSMgr = NSMgr;
        }
        internal LLDDandHealthProblem(LLDDandHealthProblem MigrationLLDDandHealthProblem, XmlNode LLDDandHealthProblemNode, XmlNamespaceManager NSMgr)
        {
            this.Node = LLDDandHealthProblemNode;
            this.NSMgr = NSMgr;

            this.LLDDType = MigrationLLDDandHealthProblem.LLDDType;
            this.LLDDCode = MigrationLLDDandHealthProblem.LLDDCode;
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
