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
        #region 1415 realted stuff for Migration only. Delete this next year.

        #region Enums
        protected enum LLDDandHealthProblems { DS, LD }
        #endregion

        #region ILR Properties
        protected string LLDDType { get { return XMLHelper.GetChildValue("LLDDType", Node, NSMgr); } set { XMLHelper.SetChildValue("LLDDType", value, Node, NSMgr); } }
        protected int? LLDDCode { get { string LLDDCode = XMLHelper.GetChildValue("LLDDCode", Node, NSMgr); return (LLDDCode != null ? int.Parse(LLDDCode) : (int?)null); } set { XMLHelper.SetChildValue("LLDDCode", value, Node, NSMgr); } }
        #endregion

        #region Methods
        internal int Get1516Equivalent()
        {
            switch (this.LLDDType)
            {
                case "DS":
                    switch (this.LLDDCode)
                    {
                        case 1: return 4;
                        case 2: return 5;
                        case 3: return 6;
                        case 4: return 93;
                        case 5: return 95;
                        case 6: return 1;
                        case 7: return 9;
                        case 8: return 16;
                        case 9: return 7;
                        case 10: return 15;
                        case 90: return 2;
                        case 97: return 97;
                    }
                    break;
                case "LD":
                    switch (this.LLDDCode)
                    {
                        case 1: return 10;
                        case 2: return 11;
                        case 10: return 12;
                        case 11: return 13;
                        case 19: return 94;
                        case 20: return 14;
                        case 90: return 3;
                        case 97: return 96;
                    }
                    break;
            }
            return 99;
        }
        #endregion

        #endregion

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
            this.LLDDCat = MigrationLLDDandHealthProblem.Get1516Equivalent();
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
