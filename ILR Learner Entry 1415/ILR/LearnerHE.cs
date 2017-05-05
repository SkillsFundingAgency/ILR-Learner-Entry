using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class LearnerHE : ChildEntity, IDataErrorInfo
    {
        #region ILR Properties
        public long? UCASPERID { get { string UCASPERID = XMLHelper.GetChildValue("UCASPERID", Node, NSMgr); return (UCASPERID != null ? long.Parse(UCASPERID) : (long?)null); } set { XMLHelper.SetChildValue("UCASPERID", value, Node, NSMgr); OnPropertyChanged("UCASPERID"); } }
        public int? TTACCOM { get { string TTACCOM = XMLHelper.GetChildValue("TTACCOM", Node, NSMgr); return (TTACCOM != null ? int.Parse(TTACCOM) : (int?)null); } set { XMLHelper.SetChildValue("TTACCOM", value, Node, NSMgr); OnPropertyChanged("TTACCOM"); } }
        #endregion

        #region ILR Child Entites
        public List<LearnerHEFinancialSupport> LearnerHEFinancialSupportList = new List<LearnerHEFinancialSupport>();
        #endregion

        #region Child Entity Creation
        public LearnerHEFinancialSupport CreateLearnerHEFinancialSupport()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerHEFinancialSupport", NSMgr.LookupNamespace("ia"));
            LearnerHEFinancialSupport newInstance = new LearnerHEFinancialSupport(newNode, NSMgr);
            //XmlNode prevNode = LearnerHEFinancialSupportList.Last().Node;
            LearnerHEFinancialSupportList.Add(newInstance);
            //Node.InsertAfter(newNode, prevNode);
            Node.AppendChild(newNode);
            return newInstance;
        }
        #endregion

        #region Constructors
        internal LearnerHE(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            XmlNodeList learnerHEFinancialSupportNodes = Node.SelectNodes("./ia:LearnerHEFinancialSupport", NSMgr);
            foreach (XmlNode node in learnerHEFinancialSupportNodes)
                LearnerHEFinancialSupportList.Add(new LearnerHEFinancialSupport(node, NSMgr));
        }
        internal LearnerHE(LearnerHE MigrationLearnerHE, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.UCASPERID = MigrationLearnerHE.UCASPERID;
            this.TTACCOM = MigrationLearnerHE.TTACCOM;
        }
        #endregion

        #region Methods
        public void Delete(LearnerHEFinancialSupport LearnerHEFinancialSupport)
        {
            Node.RemoveChild(LearnerHEFinancialSupport.Node);
            this.LearnerHEFinancialSupportList.Remove(LearnerHEFinancialSupport);
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
