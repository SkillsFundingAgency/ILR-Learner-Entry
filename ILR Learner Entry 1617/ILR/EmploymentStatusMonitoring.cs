using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;


namespace ILR
{
	public class EmploymentStatusMonitoring : ChildEntity, IDataErrorInfo
	{
		#region Enums
		public enum ESMTypes { SEI, EII, LOU, LOE, BSI, PEI, SEM }
		#endregion

		#region ILR Properties
		public string ESMType { get { return XMLHelper.GetChildValue("ESMType", Node, NSMgr); } set { XMLHelper.SetChildValue("ESMType", value, Node, NSMgr); } }
		public int? ESMCode { get { string ESMCode = XMLHelper.GetChildValue("ESMCode", Node, NSMgr); return (ESMCode != null ? int.Parse(ESMCode) : (int?)null); } set { XMLHelper.SetChildValue("ESMCode", value, Node, NSMgr); } }
		#endregion

		#region Constructors
		internal EmploymentStatusMonitoring(XmlNode EmploymentStatusMonitoringNode, XmlNamespaceManager NSMgr)
		{
			this.Node = EmploymentStatusMonitoringNode;
			this.NSMgr = NSMgr;
		}
		internal EmploymentStatusMonitoring(EmploymentStatusMonitoring MigrationEmploymentStatusMonitoring, XmlNode EmploymentStatusMonitoringNode, XmlNamespaceManager NSMgr)
		{
			this.Node = EmploymentStatusMonitoringNode;
			this.NSMgr = NSMgr;

			this.ESMType = MigrationEmploymentStatusMonitoring.ESMType;
			this.ESMCode = MigrationEmploymentStatusMonitoring.ESMCode;
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
				if (columnName == "ESMCode")
				{
					if (ESMCode != null && ESMCode.ToString().Length > 8)
					{
						//ESMCode = (int?)int.Parse(ESMCode.ToString().Substring(0, 8));
						return "ESMCode exceeds maximum length (8 digits).";
					}
				}
				return result;
			}
		}

		#endregion

	}
}