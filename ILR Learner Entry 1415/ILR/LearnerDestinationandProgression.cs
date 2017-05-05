using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

 
namespace ILR
{
    public class LearnerDestinationandProgression : ChildEntity, IDataErrorInfo
    {
        #region ILR Properties
        public string LearnRefNumber { get { return XMLHelper.GetChildValue("LearnRefNumber", Node, NSMgr); } set { XMLHelper.SetChildValue("LearnRefNumber", value, Node, NSMgr); OnPropertyChanged("LearnRefNumber"); } }
        public long? ULN { get { string ULN = XMLHelper.GetChildValue("ULN", Node, NSMgr); return (ULN != null ? long.Parse(ULN) : (long?)null); } set { XMLHelper.SetChildValue("ULN", value, Node, NSMgr); OnPropertyChanged("ULN"); } }

        public int OutcomeCount
        {
            get
            {
                return this.DPOutcomeList.Count();
            }
        }
        #endregion

        #region ILR Child Entites
        public List<DPOutcome> DPOutcomeList = new List<DPOutcome>();
        #endregion

        #region Child Entity Creation
        public DPOutcome CreateOutcome()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("DPOutcome", NSMgr.LookupNamespace("ia"));
            DPOutcome newInstance = new DPOutcome(newNode, NSMgr);
            //XmlNode prevNode = DPOutcomeList.Last().Node;
            DPOutcomeList.Add(newInstance);
            //Node.InsertAfter(newNode, prevNode);
            Node.AppendChild(newNode);
            return newInstance;
        }
        #endregion

        #region Constructors
        internal LearnerDestinationandProgression(XmlNode LearnerDestinationandProgressionNode, XmlNamespaceManager NSMgr)
        {
            this.Node = LearnerDestinationandProgressionNode;
            this.NSMgr = NSMgr;

            XmlNodeList dpOutcomeNodes = LearnerDestinationandProgressionNode.SelectNodes("./ia:DPOutcome", NSMgr);
            foreach (XmlNode node in dpOutcomeNodes)
                DPOutcomeList.Add(new DPOutcome(node, NSMgr));
        }
        #endregion

        #region Methods
        public void Delete(DPOutcome DPOutcome)
        {
            Node.RemoveChild(DPOutcome.Node);
            this.DPOutcomeList.Remove(DPOutcome);
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

                if (columnName == "LearnRefNumber")
                {
                    if (String.IsNullOrEmpty(LearnRefNumber))
                        result = String.Format("{0} required.", columnName);

                    if (LearnRefNumber != null && LearnRefNumber.ToString().Length > 9)
                        result = String.Format("{0} exceeds maximum length (9 digits).",columnName);
                }

                if (columnName == "ULN")
                {
                    if (ULN != null && ULN.ToString().Length >10)
                        result = String.Format("{0} exceeds maximum length (10 digits).", columnName);
                }
                return result;
            }
        }
        #endregion

    }
}
