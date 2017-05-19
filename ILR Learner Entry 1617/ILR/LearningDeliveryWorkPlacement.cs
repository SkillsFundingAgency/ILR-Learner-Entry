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
        internal LearningDeliveryWorkPlacement(LearningDeliveryWorkPlacement MigrationLearnerEmploymentStatus, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.WorkPlaceStartDate = MigrationLearnerEmploymentStatus.WorkPlaceStartDate;
            this.WorkPlaceEndDate = MigrationLearnerEmploymentStatus.WorkPlaceEndDate;
            this.WorkPlaceMode = MigrationLearnerEmploymentStatus.WorkPlaceMode;
            this.WorkPlaceEmpId = MigrationLearnerEmploymentStatus.WorkPlaceEmpId;

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

				if (columnName == "WorkPlaceMode")
				{
					if (WorkPlaceMode != null && WorkPlaceMode.ToString().Length > 3)
					{
						result = "WorkPlaceMode exceeds maximum length (3 digits).";
						//WorkPlaceMode = (int?)int.Parse(WorkPlaceMode.ToString().Substring(0, 3));
					}
				}
				if (columnName == "WorkPlaceEmpId")
				{
					if (WorkPlaceEmpId != null && WorkPlaceEmpId.ToString().Length > 8)
					{
						result = "WorkPlaceEmpId exceeds maximum length (8 digits).";
						//WorkPlaceEmpId = (int?)int.Parse(WorkPlaceEmpId.ToString().Substring(0, 8));
					}
				} 
                return result;
            }
        }
        #endregion

    }
}
