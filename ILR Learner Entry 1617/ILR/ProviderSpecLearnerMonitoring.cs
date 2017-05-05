using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class ProviderSpecLearnerMonitoring : ChildEntity, IDataErrorInfo
    {
        #region Enums
        public enum Occurrence { A, B }
        #endregion

        #region ILR Properties
        public string ProvSpecLearnMonOccur { get { return XMLHelper.GetChildValue("ProvSpecLearnMonOccur", Node, NSMgr); } set { XMLHelper.SetChildValue("ProvSpecLearnMonOccur", value, Node, NSMgr); } }
        public string ProvSpecLearnMon { get { return XMLHelper.GetChildValue("ProvSpecLearnMon", Node, NSMgr); } set { XMLHelper.SetChildValue("ProvSpecLearnMon", value, Node, NSMgr); } }
        #endregion

        #region Constructors
        internal ProviderSpecLearnerMonitoring(XmlNode ProviderSpecLearnerMonitoringNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ProviderSpecLearnerMonitoringNode;
            this.NSMgr = NSMgr;
        }
        internal ProviderSpecLearnerMonitoring(ProviderSpecLearnerMonitoring MigrationProviderSpecLearnerMonitoring, XmlNode ProviderSpecLearnerMonitoringNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ProviderSpecLearnerMonitoringNode;
            this.NSMgr = NSMgr;

            this.ProvSpecLearnMonOccur = MigrationProviderSpecLearnerMonitoring.ProvSpecLearnMonOccur;
            this.ProvSpecLearnMon = MigrationProviderSpecLearnerMonitoring.ProvSpecLearnMon;
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
