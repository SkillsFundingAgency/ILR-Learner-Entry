using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public abstract class ChildEntity : _base
    {
        #region Internal Properties
        internal XmlNode Node;
        internal XmlNamespaceManager NSMgr;
        internal Message Message;
        #endregion

        #region Accessors
        public virtual bool IsComplete
        {
            get
            {
                return true;
            }
        }
        public virtual string IncompleteMessage
        {
            get
            {
                return "Some data is missing";
            }
        }
        public virtual bool IsValid
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Public Methods
        public void DeleteNode()
        {
            Node.ParentNode.RemoveChild(Node);
        }
        #endregion

  
    }
}