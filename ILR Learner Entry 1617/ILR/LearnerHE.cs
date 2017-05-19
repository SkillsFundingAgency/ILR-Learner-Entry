using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
	public class LearnerHE : ChildEntity, IDataErrorInfo
	{
		#region ILR Properties
		public string UCASPERID { get { string UCASPERID = XMLHelper.GetChildValue("UCASPERID", Node, NSMgr); return (UCASPERID != null ? UCASPERID : null); } set { XMLHelper.SetChildValue("UCASPERID", value, Node, NSMgr); OnPropertyChanged("UCASPERID"); } }
		public int? TTACCOM { get { string TTACCOM = XMLHelper.GetChildValue("TTACCOM", Node, NSMgr); return (TTACCOM != null ? int.Parse(TTACCOM) : (int?)null); } set { XMLHelper.SetChildValue("TTACCOM", value, Node, NSMgr); OnPropertyChanged("TTACCOM"); } }
		#endregion

		#region ILR Child Entites
		public List<LearnerHEFinancialSupport> LearnerHEFinancialSupportList = new List<LearnerHEFinancialSupport>();
		#endregion

		#region Child Entity Creation
		public LearnerHEFinancialSupport CreateLearnerHEFinancialSupport()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerHEFinancialSupport", NSMgr.LookupNamespace("ia"));
			LearnerHEFinancialSupport newInstance = new LearnerHEFinancialSupport(newNode, NSMgr);
			//XmlNode prevNode = LearnerHEFinancialSupportList.Last().Node;
			LearnerHEFinancialSupportList.Add(newInstance);
			//Node.InsertAfter(newNode, prevNode);
			Node.AppendChild(newNode);
			return newInstance;
		}
		#endregion

		#region Constructors
		internal LearnerHE(XmlNode Node, XmlNamespaceManager NSMgr)
		{
			this.Node = Node;
			this.NSMgr = NSMgr;

			XmlNodeList learnerHEFinancialSupportNodes = Node.SelectNodes("./ia:LearnerHEFinancialSupport", NSMgr);
			foreach (XmlNode node in learnerHEFinancialSupportNodes)
				LearnerHEFinancialSupportList.Add(new LearnerHEFinancialSupport(node, NSMgr));
		}
		internal LearnerHE(LearnerHE MigrationLearnerHE, XmlNode Node, XmlNamespaceManager NSMgr)
		{
			this.Node = Node;
			this.NSMgr = NSMgr;

			this.UCASPERID = MigrationLearnerHE.UCASPERID;
			this.TTACCOM = MigrationLearnerHE.TTACCOM;
		}
		#endregion

		#region Methods
		public void Delete(LearnerHEFinancialSupport LearnerHEFinancialSupport)
		{
			Node.RemoveChild(LearnerHEFinancialSupport.Node);
			this.LearnerHEFinancialSupportList.Remove(LearnerHEFinancialSupport);
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

				if (columnName == "UCASPERID")
				{
					if (UCASPERID != null && UCASPERID.ToString().Length > 0)
					{
						//Int64? x;
						//Int64.TryParse(UCASPERID.ToString(), out x);
						//if (x == null)
						//{
						//	result = "UCASPERID contains non numeric charatures.";
						//}
						try
						{
							var z =Convert.ToInt64(UCASPERID.ToString());
						}
						catch(InvalidCastException)
						{
							result = "UCASPERID contains non numeric charatures.";
						}
						catch (Exception)
						{
							result = "UCASPERID contains non numeric charatures.";
						}

					} 
					else if (UCASPERID != null && UCASPERID.ToString().Length < 10)
					{
							result = "UCASPERID if supplied must be 10 characture long.";
					}
				}
				if (columnName == "TTACCOM")
				{
					if (TTACCOM != null && TTACCOM.ToString().Length > 3)
					{
						result = "TTACCOM exceeds maximum length (3).";
						//TTACCOM = (int?)int.Parse(TTACCOM.ToString().Substring(0, 3));
					}
				}
				return result;
			}
		}
		#endregion

	}
}
