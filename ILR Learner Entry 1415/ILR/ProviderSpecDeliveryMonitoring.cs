using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class ProviderSpecDeliveryMonitoring : ChildEntity, IDataErrorInfo
    {
        #region Enums
        public enum Occurrence { A, B, C, D }
        #endregion

        #region ILR Properties
        public string ProvSpecDelMonOccur { get { return XMLHelper.GetChildValue("ProvSpecDelMonOccur", Node, NSMgr); } set { XMLHelper.SetChildValue("ProvSpecDelMonOccur", value, Node, NSMgr); } }
        public string ProvSpecDelMon { get { return XMLHelper.GetChildValue("ProvSpecDelMon", Node, NSMgr); } set { XMLHelper.SetChildValue("ProvSpecDelMon", value, Node, NSMgr); } }
        #endregion

        #region Constructors
        internal ProviderSpecDeliveryMonitoring(XmlNode ProviderSpecDeliveryMonitoringNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ProviderSpecDeliveryMonitoringNode;
            this.NSMgr = NSMgr;
        }
        internal ProviderSpecDeliveryMonitoring(ProviderSpecDeliveryMonitoring MigrationProviderSpecDeliveryMonitoring, XmlNode ProviderSpecDeliveryMonitoringNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ProviderSpecDeliveryMonitoringNode;
            this.NSMgr = NSMgr;

            this.ProvSpecDelMonOccur = MigrationProviderSpecDeliveryMonitoring.ProvSpecDelMonOccur;
            this.ProvSpecDelMon = MigrationProviderSpecDeliveryMonitoring.ProvSpecDelMon;
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
