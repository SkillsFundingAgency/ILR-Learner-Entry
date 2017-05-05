using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class LearningDeliveryWorkPlacement : ChildEntity, IDataErrorInfo
    {
        #region ILR Properties
        public DateTime? WorkPlaceStartDate { get { string WorkPlaceStartDate = XMLHelper.GetChildValue("WorkPlaceStartDate", Node, NSMgr); return (WorkPlaceStartDate != null ? DateTime.Parse(WorkPlaceStartDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("WorkPlaceStartDate", value, Node, NSMgr); OnPropertyChanged("WorkPlaceStartDate"); } }
        public DateTime? WorkPlaceEndDate { get { string WorkPlaceEndDate = XMLHelper.GetChildValue("WorkPlaceEndDate", Node, NSMgr); return (WorkPlaceEndDate != null ? DateTime.Parse(WorkPlaceEndDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("WorkPlaceEndDate", value, Node, NSMgr); OnPropertyChanged("WorkPlaceEndDate"); } }
        public int? WorkPlaceMode { get { string WorkPlaceMode = XMLHelper.GetChildValue("WorkPlaceMode", Node, NSMgr); return (WorkPlaceMode != null ? int.Parse(WorkPlaceMode) : (int?)null); } set { XMLHelper.SetChildValue("WorkPlaceMode", value, Node, NSMgr); OnPropertyChanged("WorkPlaceMode"); } }
        public int? WorkPlaceEmpId { get { string WorkPlaceEmpId = XMLHelper.GetChildValue("WorkPlaceEmpId", Node, NSMgr); return (WorkPlaceEmpId != null ? int.Parse(WorkPlaceEmpId) : (int?)null); } set { XMLHelper.SetChildValue("WorkPlaceEmpId", value, Node, NSMgr); OnPropertyChanged("WorkPlaceEmpId"); } }
        #endregion

        #region Constructors
        internal LearningDeliveryWorkPlacement(XmlNode LearningDeliveryWorkPlacementNode, XmlNamespaceManager NSMgr)
        {
            this.Node = LearningDeliveryWorkPlacementNode;
            this.NSMgr = NSMgr;
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
                return result;
            }
        }
        #endregion

    }
}
