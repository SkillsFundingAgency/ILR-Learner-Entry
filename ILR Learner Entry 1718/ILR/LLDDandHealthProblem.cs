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
        #region ILR Properties
        public int? LLDDCat { get { string LLDDCat = XMLHelper.GetChildValue("LLDDCat", Node, NSMgr); return (LLDDCat != null ? int.Parse(LLDDCat) : (int?)null); } set { XMLHelper.SetChildValue("LLDDCat", value, Node, NSMgr); } }
        public bool? PrimaryLLDD { get { string PrimaryLLDD = XMLHelper.GetChildValue("PrimaryLLDD", Node, NSMgr); return (PrimaryLLDD != null ? (PrimaryLLDD == "1") : (bool?)null); } set { XMLHelper.SetChildValue("PrimaryLLDD", (value!=null?(bool)value:false) ? (int?)1 : (int?)null, Node, NSMgr); OnPropertyChanged("PrimaryLLDD"); } }       
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
            this.LLDDCat = MigrationLLDDandHealthProblem.LLDDCat;
            this.PrimaryLLDD = MigrationLLDDandHealthProblem.PrimaryLLDD;
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
                if (columnName == "LLDDCat")
				{
                    if (LLDDCat != null && LLDDCat.ToString().Length > 2)
					{
                        result = "LLDDCat exceeds maximum length (2 digits).";
                        //LLDDCat = (int?)int.Parse(LLDDCat.ToString().Substring(0, 2));
					}
				}
                return result;
            }
        }

        #endregion

    }
}
