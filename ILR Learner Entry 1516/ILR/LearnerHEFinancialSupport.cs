using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
	public class LearnerHEFinancialSupport : ChildEntity, IDataErrorInfo
	{
		#region Enums
		public enum LearnerHEFinancialSupportTypes { Cash = 1, NearCash = 2, AccommodationDiscounts = 3, Other = 4 }
		#endregion

		#region ILR Properties
		public int? FINTYPE { get { string FINTYPE = XMLHelper.GetChildValue("FINTYPE", Node, NSMgr); return (FINTYPE != null ? int.Parse(FINTYPE) : (int?)null); } set { XMLHelper.SetChildValue("FINTYPE", value, Node, NSMgr); } }
		public int? FINAMOUNT { get { string FINAMOUNT = XMLHelper.GetChildValue("FINAMOUNT", Node, NSMgr); return (FINAMOUNT != null ? int.Parse(FINAMOUNT) : (int?)null); } set { XMLHelper.SetChildValue("FINAMOUNT", value, Node, NSMgr); } }
		#endregion

		#region Constructors
		internal LearnerHEFinancialSupport(XmlNode LearnerHEFinancialSupportNode, XmlNamespaceManager NSMgr)
		{
			this.Node = LearnerHEFinancialSupportNode;
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

				if (columnName == "FINTYPE")
				{
					if (FINTYPE != null && FINTYPE.ToString().Length > 8)
					{
						result = "FINTYPE exceeds maximum length (8 digits).";
						//FINTYPE = (int?)int.Parse(FINTYPE.ToString().Substring(0, 8));
					}
				}
				if (columnName == "FINAMOUNT")
				{
					if (FINAMOUNT != null && FINAMOUNT.ToString().Length > 8)
					{
						result = "FINAMOUNT exceeds maximum length (8 digits).";
						//FINAMOUNT = (int?)int.Parse(FINAMOUNT.ToString().Substring(0, 8));
					}
				} 
				return result;
			}
		}

		#endregion

	}
}
