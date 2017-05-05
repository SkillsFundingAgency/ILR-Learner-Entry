using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class Header : ChildEntity, IDataErrorInfo
    {
        public Source _source;
        public CollectionDetails _collectionDetails;

        #region ILR Child Entites
        public CollectionDetails CollectionDetails { get { return _collectionDetails; } set { _collectionDetails = value; } }
        public Source Source { get { return _source; } set { _source = value; } }
        #endregion

        #region Constructors
        internal Header(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            //Find the child nodes
            XmlNode collectionDetailsNode = Node.SelectSingleNode("./ia:CollectionDetails", NSMgr);
            XmlNode sourceNode = Node.SelectSingleNode("./ia:Source", NSMgr);

            //If we have no CollectionDetails node create one in the correct place
            if (collectionDetailsNode == null)
            {
                collectionDetailsNode = Node.OwnerDocument.CreateElement("CollectionDetails", NSMgr.LookupNamespace("ia"));
                if (sourceNode == null)
                    Node.AppendChild(collectionDetailsNode);
                else
                    Node.InsertBefore(collectionDetailsNode, sourceNode);
            }
            CollectionDetails = new CollectionDetails(collectionDetailsNode, NSMgr);

            //If we have no Source node create one in the correct place
            if (sourceNode == null)
            {
                sourceNode = Node.OwnerDocument.CreateElement("Source", NSMgr.LookupNamespace("ia"));
                Node.AppendChild(sourceNode);
            }
            Source = new Source(sourceNode, NSMgr);

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
