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
        private const String CLASSNAME = "LearnerDestinationandProgression";
        private const String TABS = "";
        #region Accessors
        public bool HasCurrentDPOutcomes
        {
            get
            {
                return this.DPOutcomeList.Exists(d => d.OutCollDate >= Message.CurrentYearStart);
            }
        }
        #endregion
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
        internal LearnerDestinationandProgression(LearnerDestinationandProgression MigrationLearnerDestinationandProgression, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.LearnRefNumber = MigrationLearnerDestinationandProgression.LearnRefNumber;
            this.ULN = MigrationLearnerDestinationandProgression.ULN;

            foreach (DPOutcome migrationItem in MigrationLearnerDestinationandProgression.DPOutcomeList.Where(d => d.OutCollDate >= Message.CurrentYearStart))
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("DPOutcome", NSMgr.LookupNamespace("ia"));
                DPOutcome newInstance = new DPOutcome(migrationItem, newNode, NSMgr);
                DPOutcomeList.Add(newInstance);
                Node.AppendChild(newNode);
            }
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

        public bool ExcludeFromExport
        {
            get
            {
                XmlAttribute attr = Node.Attributes["ExcludeFromExport"];
                return ((attr != null && attr.Value == "true") || !IsComplete);
            }
            set
            {
                if (value)
                    ((XmlElement)Node).SetAttribute("ExcludeFromExport", "true");
                else
                    ((XmlElement)Node).RemoveAttribute("ExcludeFromExport");
                OnPropertyChanged("ExcludeFromExport");
            }
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
                    if (ULN != null)
                        return CheckPropertyLength(LearnRefNumber, CLASSNAME, columnName, TABS);
                    //if (LearnRefNumber != null && LearnRefNumber.ToString().Length > 12)
                    //    result = String.Format("{0} exceeds maximum length (12 digits).", columnName);
                }

                if (columnName == "ULN")
                {
                    if (String.IsNullOrEmpty(ULN.ToString()))
                        result = String.Format("{0} required.", columnName);
                    if (ULN != null)
                        return CheckPropertyLength(ULN, CLASSNAME, columnName, TABS);    
                }
                return result;
            }
        }
        #endregion

    }
}
