using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
	public class Learner : ChildEntity, IDataErrorInfo
	{
		#region Accessors
		public override bool IsComplete
		{
			get
			{
				if (this.LearnRefNumber == null || this.Ethnicity == null || this.Sex == null || this.LLDDHealthProb == null || this.LearningDeliveryList.Count == 0)
					return false;
				if (this.TelNumber != null)
				{
					if (this.TelNumber.Contains(" "))
						return false;
				}
				if (this.LearningDeliveryList.Count(x => x.IsComplete == false) > 0)
					return false;

				return true;
			}
		}
		public override string IncompleteMessage
		{
			get
			{
				string message = "";
				if (this.LearnRefNumber == null)
					message += "LearnRefNumber missing\r\n";
				if (this.Ethnicity == null)
					message += "Ethnicity missing\r\n";
				if (this.Sex == null)
					message += "Sex missing\r\n";
				if (this.TelNumber != null)
				{
					if (this.TelNumber.Contains(" "))
						message += "TelNumber should not have spaces\r\n";
					  Int32 x;
					  if (!int.TryParse(this.TelNumber,out x))
						  message += "TelNumber Should contain only numbers\r\n";						
				}
				if (this.LLDDHealthProb == null)
					message += "LLDDHealthProb missing\r\n";
				if (this.LearningDeliveryList.Count == 0)
					message += "No LearningDelivery records\r\n";

				foreach (ILR.LearningDelivery ld in LearningDeliveryList.FindAll(x => x.IsComplete == false))
				{
					message += "\tAim Sequence Numbers (" + ld.AimSeqNumber.ToString() + ") missing :" + "\r\n" + ld.IncompleteMessage;
				}
				return message;
			}
		}
		public bool HasContinuingAims
		{
			get
			{
				//bool bRet = false;

				//if (   (LearningDeliveryList.Count(x => x.CompStatus!= 95) > 0) )
				//{
				//        bRet = true;
				//}
				//else if (this.LearnerFAMList.Count(x => x.LearnFAMType == "LDM" && x.LearnFAMCode == 323) > 0)
				//{

				//}
				//else if (this.LearningDeliveryList.Count(x => (x.AimType == 1 || x.AimType == 3 || x.AimType == 4) && x.LearnPlanEndDate >= Convert.ToDateTime("01 August 2012") && x.LearnActEndDate == null) > 0)
				//{

				//}
				////|| 
				//else if (this.LearningDeliveryList.Count(x =>     ((x.AimType == 1 || x.AimType == 3 || x.AimType == 4))
				//                                               && x.LearnPlanEndDate >= Convert.ToDateTime("01 August 2012") 
				//                                               && x.LearnActEndDate == null
				//                                               && x.AchDate == null
				//                                               && (x.Outcome == 4 || x.Outcome == 5)                                                          
				//                                         )
				//                                          > 0
				//          )

				//{

				//}

				//    //|| (this.LearningDeliveryList.Count(x =>     ((x.AimType == 1 || x.AimType == 3 || x.AimType == 4)
				//    //                                           && x.LearnPlanEndDate >= Convert.ToDateTime("01 August 2012") 
				//    //                                           && x.LearnActEndDate == null
				//    //                                           && x.AchDate == null
				//    //                                           && (x.Outcome == 4 || x.Outcome == 5) 
				//    //                                         )
				//    //                                   ) > 0)


				//{
				//    bRet = true;
				//}
				//return bRet;
				return (LearningDeliveryList.Count(x => x.CompStatus != 95) > 0);

			}
		}
		#endregion

		#region ILR Properties
		private void GiveFrountEndkickToRefresh()
		{
			OnPropertyChanged("IsComplete");
			OnPropertyChanged("IncompleteMessage");
            
		}
		#endregion

		#region ILR Properties
		public string LearnRefNumber { get { return XMLHelper.GetChildValue("LearnRefNumber", Node, NSMgr); } set { XMLHelper.SetChildValue("LearnRefNumber", value, Node, NSMgr); OnPropertyChanged("LearnRefNumber"); GiveFrountEndkickToRefresh(); } }
		public string PrevLearnRefNumber { get { return XMLHelper.GetChildValue("PrevLearnRefNumber", Node, NSMgr); } set { XMLHelper.SetChildValue("PrevLearnRefNumber", value, Node, NSMgr); OnPropertyChanged("PrevLearnRefNumber"); GiveFrountEndkickToRefresh(); } }
		public int? PrevUKPRN { get { string PrevUKPRN = XMLHelper.GetChildValue("PrevUKPRN", Node, NSMgr); return (PrevUKPRN != null ? int.Parse(PrevUKPRN) : (int?)null); } set { XMLHelper.SetChildValue("PrevUKPRN", value, Node, NSMgr); OnPropertyChanged("PrevUKPRN"); GiveFrountEndkickToRefresh(); } }
		public long? ULN { get { string ULN = XMLHelper.GetChildValue("ULN", Node, NSMgr); return (ULN != null ? long.Parse(ULN) : (long?)null); } set { XMLHelper.SetChildValue("ULN", value, Node, NSMgr); OnPropertyChanged("ULN"); GiveFrountEndkickToRefresh(); } }
		public string FamilyName { get { return XMLHelper.GetChildValue("FamilyName", Node, NSMgr); } set { XMLHelper.SetChildValue("FamilyName", value, Node, NSMgr); OnPropertyChanged("FamilyName"); GiveFrountEndkickToRefresh(); } }
		public string GivenNames { get { return XMLHelper.GetChildValue("GivenNames", Node, NSMgr); } set { XMLHelper.SetChildValue("GivenNames", value, Node, NSMgr); OnPropertyChanged("GivenNames"); GiveFrountEndkickToRefresh(); } }
		public DateTime? DateOfBirth { get { string DateOfBirth = XMLHelper.GetChildValue("DateOfBirth", Node, NSMgr); return (DateOfBirth != null ? DateTime.Parse(DateOfBirth) : (DateTime?)null); } set { XMLHelper.SetChildValue("DateOfBirth", value, Node, NSMgr); OnPropertyChanged("DateOfBirth"); GiveFrountEndkickToRefresh(); } }
		public int? Ethnicity { get { string Ethnicity = XMLHelper.GetChildValue("Ethnicity", Node, NSMgr); return (Ethnicity != null ? int.Parse(Ethnicity) : (int?)null); } set { XMLHelper.SetChildValue("Ethnicity", value, Node, NSMgr); OnPropertyChanged("Ethnicity"); GiveFrountEndkickToRefresh(); } }
		public string Sex { get { return XMLHelper.GetChildValue("Sex", Node, NSMgr); } set { XMLHelper.SetChildValue("Sex", value, Node, NSMgr); OnPropertyChanged("Sex"); GiveFrountEndkickToRefresh(); } }
		public int? LLDDHealthProb { get { string LLDDHealthProb = XMLHelper.GetChildValue("LLDDHealthProb", Node, NSMgr); return (LLDDHealthProb != null ? int.Parse(LLDDHealthProb) : (int?)null); } set { XMLHelper.SetChildValue("LLDDHealthProb", value, Node, NSMgr); OnPropertyChanged("LLDDHealthProb"); GiveFrountEndkickToRefresh(); } }
		public string NINumber { get { return XMLHelper.GetChildValue("NINumber", Node, NSMgr); } set { XMLHelper.SetChildValue("NINumber", value, Node, NSMgr); OnPropertyChanged("NINumber"); GiveFrountEndkickToRefresh(); } }
		public int? PriorAttain { get { string PriorAttain = XMLHelper.GetChildValue("PriorAttain", Node, NSMgr); return (PriorAttain != null ? int.Parse(PriorAttain) : (int?)null); } set { XMLHelper.SetChildValue("PriorAttain", value, Node, NSMgr); OnPropertyChanged("PriorAttain"); GiveFrountEndkickToRefresh(); } }

		//public int? Accom { get { string Accom = XMLHelper.GetChildValue("Accom", Node, NSMgr); return (Accom != null ? int.Parse(Accom) : (int?)null); } set { XMLHelper.SetChildValue("Accom", value, Node, NSMgr); OnPropertyChanged("Accom"); GiveFrountEndkickToRefresh(); } }
		//Make Acc 5 when Selected !!!
		public bool Accom { get { string Accom = XMLHelper.GetChildValue("Accom", Node, NSMgr); return (Accom != null ? true : false); } set { XMLHelper.SetChildValue("Accom", value ? (int?)5 : (int?)null, Node, NSMgr); OnPropertyChanged("Accom"); GiveFrountEndkickToRefresh(); } }
		public int? ALSCost { get { string ALSCost = XMLHelper.GetChildValue("ALSCost", Node, NSMgr); return (ALSCost != null ? int.Parse(ALSCost) : (int?)null); } set { XMLHelper.SetChildValue("ALSCost", value, Node, NSMgr); OnPropertyChanged("ALSCost"); GiveFrountEndkickToRefresh(); } }
		public int? PlanLearnHours { get { string PlanLearnHours = XMLHelper.GetChildValue("PlanLearnHours", Node, NSMgr); return (PlanLearnHours != null ? int.Parse(PlanLearnHours) : (int?)null); } set { XMLHelper.SetChildValue("PlanLearnHours", value, Node, NSMgr); OnPropertyChanged("PlanLearnHours"); GiveFrountEndkickToRefresh(); } }
		public int? PlanEEPHours { get { string PlanEEPHours = XMLHelper.GetChildValue("PlanEEPHours", Node, NSMgr); return (PlanEEPHours != null ? int.Parse(PlanEEPHours) : (int?)null); } set { XMLHelper.SetChildValue("PlanEEPHours", value, Node, NSMgr); OnPropertyChanged("PlanEEPHours"); GiveFrountEndkickToRefresh(); } }
		public int? Dest { get { string Dest = XMLHelper.GetChildValue("Dest", Node, NSMgr); return (Dest != null ? int.Parse(Dest) : (int?)null); } set { XMLHelper.SetChildValue("Dest", value, Node, NSMgr); OnPropertyChanged("Dest"); GiveFrountEndkickToRefresh(); } }
		#endregion

		#region Application Properties
		public bool ExcludeFromExport
		{
			get
			{
				XmlAttribute attr = Node.Attributes["ExcludeFromExport"];
				return (attr != null && attr.Value == "true");
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
		#endregion

		#region Renormalised Child Entities
		#region LearnerContact
		public string AddLine1
		{
			get
			{
				ILR.PostAdd postAdd = GetPostAdd();
				if (postAdd == null)
					return null;
				else
					return postAdd.AddLine1;
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
				if (existing != null)
				{
					if (((ILR.LearnerContact)existing).PostAdd == null)
						((ILR.LearnerContact)existing).CreatePostAdd();
					((ILR.LearnerContact)existing).PostAdd.AddLine1 = value;
				}
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 1;
					newLearnerContact.ContType = 2;
					newLearnerContact.CreatePostAdd();
					newLearnerContact.PostAdd.AddLine1 = value;
				}
				OnPropertyChanged("AddLine1");
			}
		}
		public string AddLine2
		{
			get
			{
				ILR.PostAdd postAdd = GetPostAdd();
				if (postAdd == null)
					return null;
				else
					return postAdd.AddLine2;
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
				if (existing != null)
				{
					if (((ILR.LearnerContact)existing).PostAdd == null)
						((ILR.LearnerContact)existing).CreatePostAdd();
					((ILR.LearnerContact)existing).PostAdd.AddLine2 = value;
				}
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 1;
					newLearnerContact.ContType = 2;
					newLearnerContact.CreatePostAdd();
					newLearnerContact.PostAdd.AddLine2 = value;
				}
				OnPropertyChanged("AddLine2");
			}
		}
		public string AddLine3
		{
			get
			{
				ILR.PostAdd postAdd = GetPostAdd();
				if (postAdd == null)
					return null;
				else
					return postAdd.AddLine3;
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
				if (existing != null)
				{
					if (((ILR.LearnerContact)existing).PostAdd == null)
						((ILR.LearnerContact)existing).CreatePostAdd();
					((ILR.LearnerContact)existing).PostAdd.AddLine3 = value;
				}
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 1;
					newLearnerContact.ContType = 2;
					newLearnerContact.CreatePostAdd();
					newLearnerContact.PostAdd.AddLine3 = value;
				}
				OnPropertyChanged("AddLine3");
			}
		}
		public string AddLine4
		{
			get
			{
				ILR.PostAdd postAdd = GetPostAdd();
				if (postAdd == null)
					return null;
				else
					return postAdd.AddLine4;
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
				if (existing != null)
				{
					if (((ILR.LearnerContact)existing).PostAdd == null)
						((ILR.LearnerContact)existing).CreatePostAdd();
					((ILR.LearnerContact)existing).PostAdd.AddLine4 = value;
				}
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 1;
					newLearnerContact.ContType = 2;
					newLearnerContact.CreatePostAdd();
					newLearnerContact.PostAdd.AddLine4 = value;
					OnPropertyChanged("AddLine4");
				}
			}
		}
		public string PostCode
		{
			get
			{
				return (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 2 select learnerContact.PostCode).FirstOrDefault();
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 2 select learnerContact).FirstOrDefault();
				if (existing != null)
					((ILR.LearnerContact)existing).PostCode = value;
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 2;
					newLearnerContact.ContType = 2;
					newLearnerContact.PostCode = value;
				}
				OnPropertyChanged("PostCode");
			}
		}
		public string TelNumber
		{
			get
			{
				return (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 3 select learnerContact.TelNumber).FirstOrDefault();
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 3 select learnerContact).FirstOrDefault();
				if (existing != null)
					((ILR.LearnerContact)existing).TelNumber = value;
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 3;
					newLearnerContact.ContType = 2;
					newLearnerContact.TelNumber = value;
				}
				OnPropertyChanged("TelNumber");
				GiveFrountEndkickToRefresh();
			}
		}
		public string Email
		{
			get
			{
				return (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 4 select learnerContact.Email).FirstOrDefault();
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 4 select learnerContact).FirstOrDefault();
				if (existing != null)
					((ILR.LearnerContact)existing).Email = value;
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 4;
					newLearnerContact.ContType = 2;
					newLearnerContact.Email = value;
				}
				OnPropertyChanged("Email");
			}
		}
		public string PriorPostCode
		{
			get
			{
				return (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 1 select learnerContact.PostCode).FirstOrDefault();
			}
			set
			{
				var existing = (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 1 select learnerContact).FirstOrDefault();
				if (existing != null)
					((ILR.LearnerContact)existing).PostCode = value;
				else
				{
					ILR.LearnerContact newLearnerContact = this.CreateLearnerContact();
					newLearnerContact.LocType = 2;
					newLearnerContact.ContType = 1;
					newLearnerContact.PostCode = value;
				}
				OnPropertyChanged("PriorPostCode");
			}
		}
		#endregion
		#region LearnerFAM
		public bool LDA
		{
			get
			{
				return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.LDA) == 1;
			}
			set
			{
				if (value)
					SetFAM(LearnerFAM.SingleOccurrenceFAMs.LDA, 1);
				else
					RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.LDA);
				OnPropertyChanged("LDA");
			}
		}
		public bool HNS
		{
			get
			{
				return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.HNS) == 1;
			}
			set
			{
				if (value)
					SetFAM(LearnerFAM.SingleOccurrenceFAMs.HNS, 1);
				else
					RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.HNS);
				OnPropertyChanged("HNS");
			}
		}
		public bool EHC
		{
			get
			{
				return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.EHC) == 1;
			}
			set
			{
				if (value)
					SetFAM(LearnerFAM.SingleOccurrenceFAMs.EHC, 1);
				else
					RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.EHC);
				OnPropertyChanged("EHC");
			}
		}
		public bool DLA
		{
			get
			{
				return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.DLA) == 1;
			}
			set
			{
				if (value)
					SetFAM(LearnerFAM.SingleOccurrenceFAMs.DLA, 1);
				else
					RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.DLA);
				OnPropertyChanged("DLA");
			}
		}
		public List<int> LSR
		{
			get
			{
				return GetFAMList(LearnerFAM.MultiOccurrenceFAMs.LSR);
			}
			set
			{
				ClearFAMList(LearnerFAM.MultiOccurrenceFAMs.LSR);
				foreach (int lFAMCode in value)
					AddFAM(LearnerFAM.MultiOccurrenceFAMs.LSR, lFAMCode);
				OnPropertyChanged("LSR");
			}
		}
		public List<int> NLM
		{
			get
			{
				return GetFAMList(LearnerFAM.MultiOccurrenceFAMs.NLM);
			}
			set
			{
				ClearFAMList(LearnerFAM.MultiOccurrenceFAMs.NLM);
				foreach (int lFAMCode in value)
					AddFAM(LearnerFAM.MultiOccurrenceFAMs.NLM, lFAMCode);
				OnPropertyChanged("NLM");
			}
		}
		public int? MGA
		{
			get
			{
				return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.MGA);
			}
			set
			{
				if (value != null)
					SetFAM(LearnerFAM.SingleOccurrenceFAMs.MGA, (int)value);
				else
					RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.MGA);
				OnPropertyChanged("MGA");
			}
		}
		public int? EGA
		{
			get
			{
				return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.EGA);
			}
			set
			{
				if (value != null)
					SetFAM(LearnerFAM.SingleOccurrenceFAMs.EGA, (int)value);
				else
					RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.EGA);
				OnPropertyChanged("EGA");
			}
		}
		public int? FME
		{
			get
			{
				return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.FME);
			}
			set
			{
				if (value != null)
					SetFAM(LearnerFAM.SingleOccurrenceFAMs.FME, (int)value);
				else
					RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.FME);
				OnPropertyChanged("FME");
			}
		}

		public Boolean PPE1
		{
			get
			{
				LearnerFAM tmp = this.LearnerFAMList.Where(x =>
																x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
															 && x.LearnFAMCode == 1
														 ).FirstOrDefault();
				return (tmp != null ? true : false);
			}
			set
			{
				LearnerFAM tmp = this.LearnerFAMList.Where(x =>
															   x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
															&& x.LearnFAMCode == 1
														).FirstOrDefault();
				if (value)
				{
					if (tmp == null)
					{
						this.AddFAM(LearnerFAM.MultiOccurrenceFAMs.PPE, 1);
					}
				}
				else
				{
					if (tmp != null)
					{
						this.Delete(tmp);
					}
				}
				OnPropertyChanged("PPE1");
			}
		}

		public Boolean PPE2
		{
			get
			{
				LearnerFAM tmp = this.LearnerFAMList.Where(x =>
																x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
															 && x.LearnFAMCode == 2
														 ).FirstOrDefault();
				return (tmp != null ? true : false);
			}
			set
			{
				LearnerFAM tmp = this.LearnerFAMList.Where(x =>
															   x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
															&& x.LearnFAMCode == 2
														).FirstOrDefault();
				if (value)
				{
					if (tmp == null)
					{
						this.AddFAM(LearnerFAM.MultiOccurrenceFAMs.PPE, 2);
					}
				}
				else
				{
					if (tmp != null)
					{
						this.Delete(tmp);
					}
				}
				OnPropertyChanged("PPE2");
			}
		}

		#endregion
		#region LLDDandHealthProblem
		public int? DS
		{
			get
			{
				return GetLLDDCode(LLDDandHealthProblem.LLDDandHealthProblems.DS);
			}
			set
			{
				if (value != null)
					SetLLDD(LLDDandHealthProblem.LLDDandHealthProblems.DS, (int)value);
				else
					RemoveLLDD(LLDDandHealthProblem.LLDDandHealthProblems.DS);
				OnPropertyChanged("DS");
			}
		}
		public int? LD
		{
			get
			{
				return GetLLDDCode(LLDDandHealthProblem.LLDDandHealthProblems.LD);
			}
			set
			{
				if (value != null)
					SetLLDD(LLDDandHealthProblem.LLDDandHealthProblems.LD, (int)value);
				else
					RemoveLLDD(LLDDandHealthProblem.LLDDandHealthProblems.LD);
				OnPropertyChanged("LD");
			}
		}
		#endregion
		#region ProviderSpecLearnerMonitoring
		public string ProvSpecMonA
		{
			get
			{
				return GetProvSpecMonValue(ProviderSpecLearnerMonitoring.Occurrence.A);
			}
			set
			{
				SetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence.A, value);
				OnPropertyChanged("ProvSpecMonA");
				GiveFrountEndkickToRefresh();
			}
		}
		public string ProvSpecMonB
		{
			get
			{
				return GetProvSpecMonValue(ProviderSpecLearnerMonitoring.Occurrence.B);
			}
			set
			{
				SetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence.B, value);
				OnPropertyChanged("ProvSpecMonB");
				GiveFrountEndkickToRefresh();
			}
		}
		#endregion
		#region ContactPreference
		public bool PMC1
		{
			get
			{
				return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 1).Count() > 0;
			}
			set
			{
				if (value && !PMC1)
				{
					ContactPreference newInstance = CreateContactPreference();
					newInstance.ContPrefType = "PMC";
					newInstance.ContPrefCode = 1;
				}
				else if (!value && PMC1)
				{
					ContactPreference deleteInstance = this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 1).First();
					this.Delete(deleteInstance);
				}
				OnPropertyChanged("PMC1");
			}
		}
		public bool PMC2
		{
			get
			{
				return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 2).Count() > 0;
			}
			set
			{
				if (value && !PMC2)
				{
					ContactPreference newInstance = CreateContactPreference();
					newInstance.ContPrefType = "PMC";
					newInstance.ContPrefCode = 2;
				}
				else if (!value && PMC2)
				{
					ContactPreference deleteInstance = this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 2).First();
					this.Delete(deleteInstance);
				}
				OnPropertyChanged("PMC2");
			}
		}
		public bool PMC3
		{
			get
			{
				return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 3).Count() > 0;
			}
			set
			{
				if (value && !PMC3)
				{
					ContactPreference newInstance = CreateContactPreference();
					newInstance.ContPrefType = "PMC";
					newInstance.ContPrefCode = 3;
				}
				else if (!value && PMC3)
				{
					ContactPreference deleteInstance = this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 3).First();
					this.Delete(deleteInstance);
				}
				OnPropertyChanged("PMC3");
			}
		}
		public bool RUI1
		{
			get
			{
				return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 1).Count() > 0;
			}
			set
			{
				if (value && !RUI1)
				{
					RUI4 = false;
					RUI5 = false;
					ContactPreference newInstance = CreateContactPreference();
					newInstance.ContPrefType = "RUI";
					newInstance.ContPrefCode = 1;
				}
				else if (!value && RUI1)
				{
					ContactPreference deleteInstance = this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 1).First();
					this.Delete(deleteInstance);
				}
				OnPropertyChanged("RUI1");
			}
		}
		public bool RUI2
		{
			get
			{
				return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 2).Count() > 0;
			}
			set
			{
				if (value && !RUI2)
				{
					RUI4 = false;
					RUI5 = false;
					ContactPreference newInstance = CreateContactPreference();
					newInstance.ContPrefType = "RUI";
					newInstance.ContPrefCode = 2;
				}
				else if (!value && RUI2)
				{
					ContactPreference deleteInstance = this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 2).First();
					this.Delete(deleteInstance);
				}
				OnPropertyChanged("RUI2");
			}
		}
		public bool RUI4
		{
			get
			{
				return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 4).Count() > 0;
			}
			set
			{
				if (value && !RUI4)
				{
					RUI1 = false;
					RUI2 = false;
					RUI5 = false;
					OnPropertyChanged("RUI1");
					OnPropertyChanged("RUI2");
					OnPropertyChanged("RUI5");

					ContactPreference newInstance = CreateContactPreference();
					newInstance.ContPrefType = "RUI";
					newInstance.ContPrefCode = 4;
				}
				else if (!value && RUI4)
				{
					ContactPreference deleteInstance = this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 4).First();
					this.Delete(deleteInstance);
				}
				OnPropertyChanged("RUI4");
			}
		}
		public bool RUI5
		{
			get
			{
				return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 5).Count() > 0;
			}
			set
			{
				if (value && !RUI5)
				{
					RUI1 = false;
					RUI2 = false;
					RUI4 = false;
					OnPropertyChanged("RUI1");
					OnPropertyChanged("RUI2");
					OnPropertyChanged("RUI4");

					ContactPreference newInstance = CreateContactPreference();
					newInstance.ContPrefType = "RUI";
					newInstance.ContPrefCode = 5;
				}
				else if (!value && RUI5)
				{
					ContactPreference deleteInstance = this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 5).First();
					this.Delete(deleteInstance);
				}
				OnPropertyChanged("RUI5");
			}
		}
		#endregion
		#region LearnerHE
		public long? UCASPERID
		{
			get
			{
				if (this.LearnerHE == null)
					return null;
				else
					return this.LearnerHE.UCASPERID;
			}
			set
			{
				if (this.LearnerHE == null)
				{
					if (value != null)
					{
						this.LearnerHE = this.CreateLearnerHE();
						this.LearnerHE.UCASPERID = value;
					}
				}
				else
				{
					if (value == null && this.LearnerHE.TTACCOM == null)
						this.LearnerHE = null;
					else
						this.LearnerHE.UCASPERID = value;
				}
				OnPropertyChanged("UCASPERID");
				GiveFrountEndkickToRefresh();
			}
		}
		public int? TTACCOM
		{
			get
			{
				if (this.LearnerHE == null)
					return null;
				else
					return this.LearnerHE.TTACCOM;
			}
			set
			{
				if (this.LearnerHE == null)
				{
					if (value != null)
					{
						this.LearnerHE = this.CreateLearnerHE();
						this.LearnerHE.TTACCOM = value;
					}
				}
				else
				{
					if (value == null && this.LearnerHE.TTACCOM == null)
						this.LearnerHE = null;
					else
						this.LearnerHE.TTACCOM = value;
				}
				OnPropertyChanged("TTACCOM");
				GiveFrountEndkickToRefresh();
			}
		}
		#endregion
		#region LearnerHEFinancialSupport
		public int? HEFinCash
		{
			get
			{
				return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Cash);
			}
			set
			{
				if (value != null)
					SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Cash, (int)value);
				else
					RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Cash);
				OnPropertyChanged("HEFinCash");
			}
		}
		public int? HEFinNearCash
		{
			get
			{
				return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.NearCash);
			}
			set
			{
				if (value != null)
					SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.NearCash, (int)value);
				else
					RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.NearCash);
				OnPropertyChanged("HEFinNearCash");
			}
		}
		public int? HEFinAccommodationDiscounts
		{
			get
			{
				return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.AccommodationDiscounts);
			}
			set
			{
				if (value != null)
					SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.AccommodationDiscounts, (int)value);
				else
					RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.AccommodationDiscounts);
				OnPropertyChanged("HEFinAccommodationDiscounts");
			}
		}
		public int? HEFinOther
		{
			get
			{
				return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Other);
			}
			set
			{
				if (value != null)
					SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Other, (int)value);
				else
					RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Other);
				OnPropertyChanged("HEFinOther");
			}
		}
		#endregion
		#endregion

		#region ILR Child Entites
		public List<LearnerContact> LearnerContactList = new List<LearnerContact>();
		public List<ContactPreference> ContactPreferenceList = new List<ContactPreference>();
		public List<LLDDandHealthProblem> LLDDandHealthProblemList = new List<LLDDandHealthProblem>();
		public List<LearnerFAM> LearnerFAMList = new List<LearnerFAM>();
		public List<ProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitoringList = new List<ProviderSpecLearnerMonitoring>();
		public List<LearnerEmploymentStatus> LearnerEmploymentStatusList = new List<LearnerEmploymentStatus>();
		public LearnerHE LearnerHE;
		public List<LearningDelivery> LearningDeliveryList = new List<LearningDelivery>();
		#endregion

		#region Child Entity Creation
		public LearnerContact CreateLearnerContact()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerContact", NSMgr.LookupNamespace("ia"));
			LearnerContact newInstance = new LearnerContact(newNode, NSMgr);
			LearnerContactList.Add(newInstance);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			return newInstance;
		}
		public ContactPreference CreateContactPreference()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("ContactPreference", NSMgr.LookupNamespace("ia"));
			ContactPreference newInstance = new ContactPreference(newNode, NSMgr);
			ContactPreferenceList.Add(newInstance);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			return newInstance;
		}
		public LLDDandHealthProblem CreateLLDDandHealthProblem()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("LLDDandHealthProblem", NSMgr.LookupNamespace("ia"));
			LLDDandHealthProblem newInstance = new LLDDandHealthProblem(newNode, NSMgr);
			LLDDandHealthProblemList.Add(newInstance);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			return newInstance;
		}
		public LearnerFAM CreateLearnerFAM()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerFAM", NSMgr.LookupNamespace("ia"));
			LearnerFAM newInstance = new LearnerFAM(newNode, NSMgr);
			LearnerFAMList.Add(newInstance);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			return newInstance;
		}
		public ProviderSpecLearnerMonitoring CreateProviderSpecLearnerMonitoring()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("ProviderSpecLearnerMonitoring", NSMgr.LookupNamespace("ia"));
			ProviderSpecLearnerMonitoring newInstance = new ProviderSpecLearnerMonitoring(newNode, NSMgr);
			ProviderSpecLearnerMonitoringList.Add(newInstance);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			return newInstance;
		}
		public LearnerEmploymentStatus CreateLearnerEmploymentStatus()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerEmploymentStatus", NSMgr.LookupNamespace("ia"));
			LearnerEmploymentStatus newInstance = new LearnerEmploymentStatus(newNode, NSMgr);
			LearnerEmploymentStatusList.Add(newInstance);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			return newInstance;
		}
		public LearnerHE CreateLearnerHE()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerHE", NSMgr.LookupNamespace("ia"));
			LearnerHE = new LearnerHE(newNode, NSMgr);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			return LearnerHE;
		}
		public LearningDelivery CreateLearningDelivery()
		{
			XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDelivery", NSMgr.LookupNamespace("ia"));
			LearningDelivery newInstance = new LearningDelivery(newNode, NSMgr);
			newInstance.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
			LearningDeliveryList.Add(newInstance);
			AppendToLastOfNodeNamed(newNode, newNode.Name);
			newInstance.AimSeqNumber = LearningDeliveryList.Count();
			return newInstance;
		}
		private void AppendToLastOfNodeNamed(XmlNode NewNode, string NodeName)
		{
			switch (NodeName)
			{
				case "LearnerContact":
					if (ContactPreferenceList.Count() == 0)
						AppendToLastOfNodeNamed(NewNode, "ContactPreference");
					else
						Node.InsertBefore(NewNode, ContactPreferenceList.First().Node);
					break;
				case "ContactPreference":
					if (LLDDandHealthProblemList.Count() == 0)
						AppendToLastOfNodeNamed(NewNode, "LLDDandHealthProblem");
					else
						Node.InsertBefore(NewNode, LLDDandHealthProblemList.First().Node);
					break;
				case "LLDDandHealthProblem":
					if (LearnerFAMList.Count() == 0)
						AppendToLastOfNodeNamed(NewNode, "LearnerFAM");
					else
						Node.InsertBefore(NewNode, LearnerFAMList.First().Node);
					break;
				case "LearnerFAM":
					if (ProviderSpecLearnerMonitoringList.Count() == 0)
						AppendToLastOfNodeNamed(NewNode, "ProviderSpecLearnerMonitoring");
					else
						Node.InsertBefore(NewNode, ProviderSpecLearnerMonitoringList.First().Node);
					break;
				case "ProviderSpecLearnerMonitoring":
					if (LearnerEmploymentStatusList.Count() == 0)
						AppendToLastOfNodeNamed(NewNode, "LearnerEmploymentStatus");
					else
						Node.InsertBefore(NewNode, LearnerEmploymentStatusList.First().Node);
					break;
				case "LearnerEmploymentStatus":
					if (LearnerHE == null)
						AppendToLastOfNodeNamed(NewNode, "LearnerHE");
					else
						Node.InsertBefore(NewNode, LearnerHE.Node);
					break;
				case "LearnerHE":
					if (LearningDeliveryList.Count() == 0)
						AppendToLastOfNodeNamed(NewNode, "LearningDelivery");
					else
						Node.InsertBefore(NewNode, LearningDeliveryList.First().Node);
					break;
				case "LearningDelivery":
					Node.AppendChild(NewNode);
					break;
			}
		}
		#endregion

		#region Constructors
		internal Learner(XmlNode LearnerNode, XmlNamespaceManager NSMgr)
		{
			this.Node = LearnerNode;
			this.NSMgr = NSMgr;

			XmlNodeList learnerContactNodes = LearnerNode.SelectNodes("./ia:LearnerContact", NSMgr);
			foreach (XmlNode node in learnerContactNodes)
				LearnerContactList.Add(new LearnerContact(node, NSMgr));

			XmlNodeList contactPreferenceNodes = LearnerNode.SelectNodes("./ia:ContactPreference", NSMgr);
			foreach (XmlNode node in contactPreferenceNodes)
				ContactPreferenceList.Add(new ContactPreference(node, NSMgr));

			XmlNodeList llddAndHealthProblemeNodes = LearnerNode.SelectNodes("./ia:LLDDandHealthProblem", NSMgr);
			foreach (XmlNode node in llddAndHealthProblemeNodes)
				LLDDandHealthProblemList.Add(new LLDDandHealthProblem(node, NSMgr));

			XmlNodeList learnerFAMNodes = LearnerNode.SelectNodes("./ia:LearnerFAM", NSMgr);
			foreach (XmlNode node in learnerFAMNodes)
				LearnerFAMList.Add(new LearnerFAM(node, NSMgr));
			XmlNodeList providerSpecLearnerMonitoringNodes = LearnerNode.SelectNodes("./ia:ProviderSpecLearnerMonitoring", NSMgr);
			foreach (XmlNode node in providerSpecLearnerMonitoringNodes)
				ProviderSpecLearnerMonitoringList.Add(new ProviderSpecLearnerMonitoring(node, NSMgr));
			XmlNodeList learnerEmploymentStatusNodes = LearnerNode.SelectNodes("./ia:LearnerEmploymentStatus", NSMgr);
			foreach (XmlNode node in learnerEmploymentStatusNodes)
				LearnerEmploymentStatusList.Add(new LearnerEmploymentStatus(node, NSMgr));
			XmlNode learnerHENode = LearnerNode.SelectSingleNode("./ia:LearnerHE", NSMgr);
			if (learnerHENode != null)
				LearnerHE = new LearnerHE(learnerHENode, NSMgr);

			XmlNodeList learningDeliveryNodes = LearnerNode.SelectNodes("./ia:LearningDelivery", NSMgr);
			foreach (XmlNode node in learningDeliveryNodes)
			{
				LearningDelivery ld = new LearningDelivery(node, NSMgr);
				ld.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
				LearningDeliveryList.Add(ld);
			}
		}

		private void LearningDeliveryPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			GiveFrountEndkickToRefresh();
		}
		internal Learner(Learner MigrationLearner, XmlNode LearnerNode, XmlNamespaceManager NSMgr)
		{
			this.Node = LearnerNode;
			this.NSMgr = NSMgr;

			this.LearnRefNumber = MigrationLearner.LearnRefNumber;
			this.ULN = MigrationLearner.ULN;
			this.FamilyName = MigrationLearner.FamilyName;
			this.GivenNames = MigrationLearner.GivenNames;
			this.DateOfBirth = MigrationLearner.DateOfBirth;
			this.Ethnicity = MigrationLearner.Ethnicity;
			this.Sex = MigrationLearner.Sex;
			this.LLDDHealthProb = MigrationLearner.LLDDHealthProb;
			this.NINumber = MigrationLearner.NINumber;
			this.PriorAttain = MigrationLearner.PriorAttain;
			this.Dest = MigrationLearner.Dest;

			XmlNodeList learnerContactyNodes = LearnerNode.SelectNodes("./ia:LearnerContact", NSMgr);
			foreach (XmlNode node in learnerContactyNodes)
				LearnerContactList.Add(new LearnerContact(node, NSMgr));

			foreach (LearnerContact migrationItem in MigrationLearner.LearnerContactList)
			{
				XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerContact", NSMgr.LookupNamespace("ia"));
				LearnerContact newInstance = new LearnerContact(migrationItem, newNode, NSMgr);
				LearnerContactList.Add(newInstance);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}
			foreach (ContactPreference migrationItem in MigrationLearner.ContactPreferenceList)
			{
				XmlNode newNode = Node.OwnerDocument.CreateElement("ContactPreference", NSMgr.LookupNamespace("ia"));
				ContactPreference newInstance = new ContactPreference(migrationItem, newNode, NSMgr);
				ContactPreferenceList.Add(newInstance);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}
			foreach (LLDDandHealthProblem migrationItem in MigrationLearner.LLDDandHealthProblemList)
			{
				XmlNode newNode = Node.OwnerDocument.CreateElement("LLDDandHealthProblem", NSMgr.LookupNamespace("ia"));
				LLDDandHealthProblem newInstance = new LLDDandHealthProblem(migrationItem, newNode, NSMgr);
				LLDDandHealthProblemList.Add(newInstance);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}
			foreach (LearnerFAM migrationItem in MigrationLearner.LearnerFAMList)
			{
				if (migrationItem.LearnFAMType != "ALS" && migrationItem.LearnFAMType != "LSR")
				{
					XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerFAM", NSMgr.LookupNamespace("ia"));
					LearnerFAM newInstance = new LearnerFAM(migrationItem, newNode, NSMgr);
					LearnerFAMList.Add(newInstance);
					AppendToLastOfNodeNamed(newNode, newNode.Name);
				}
			}
			foreach (ProviderSpecLearnerMonitoring migrationItem in MigrationLearner.ProviderSpecLearnerMonitoringList)
			{
				XmlNode newNode = Node.OwnerDocument.CreateElement("ProviderSpecLearnerMonitoring", NSMgr.LookupNamespace("ia"));
				ProviderSpecLearnerMonitoring newInstance = new ProviderSpecLearnerMonitoring(migrationItem, newNode, NSMgr);
				ProviderSpecLearnerMonitoringList.Add(newInstance);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}
			foreach (LearnerEmploymentStatus migrationItem in MigrationLearner.LearnerEmploymentStatusList)
			{
				XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerEmploymentStatus", NSMgr.LookupNamespace("ia"));
				LearnerEmploymentStatus newInstance = new LearnerEmploymentStatus(migrationItem, newNode, NSMgr);
				LearnerEmploymentStatusList.Add(newInstance);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}
			if (MigrationLearner.LearnerHE != null)
			{
				XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerHE", NSMgr.LookupNamespace("ia"));
				LearnerHE = new LearnerHE(MigrationLearner.LearnerHE, newNode, NSMgr);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}
			foreach (LearningDelivery migrationItem in MigrationLearner.LearningDeliveryList)
			{
				XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDelivery", NSMgr.LookupNamespace("ia"));
				LearningDelivery newInstance = new LearningDelivery(migrationItem, newNode, NSMgr);
				newInstance.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
				LearningDeliveryList.Add(newInstance);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}


			//
			/// This add new Aim for Traineeships.
			///
			AddNewLearningDeliveryForTraineeshipIfNeeded(MigrationLearner, LearnerNode, NSMgr);


		}


		internal void AddNewLearningDeliveryForTraineeshipIfNeeded(Learner MigrationLearner, XmlNode LearnerNode, XmlNamespaceManager NSMgr)
		{
			// Migrate Traineeships -- Add new Learning Delivery Record and its FAMs as needed
			List<LearningDelivery> AnyLdWithLdm323 = new List<LearningDelivery>(0);


			foreach (LearningDelivery ld in this.LearningDeliveryList)
			{
				var LookforFAM = (from ILR.LearningDeliveryFAM ldFam in ld.LearningDeliveryFAMList
								  where (ldFam.LearnDelFAMType == "LDM")
									 && (ldFam.LearnDelFAMCode == "323")
								  select ldFam).FirstOrDefault();
				if (LookforFAM != null)
					AnyLdWithLdm323.Add(ld);
			}

			if (AnyLdWithLdm323.Count() > 0)
			{
				var earliestAim = (from ILR.LearningDelivery ldInner in AnyLdWithLdm323
								   orderby ldInner.LearnStartDate
								   select ldInner).FirstOrDefault();

				var EmpOutcomeAimList = new[] { "Z0007836", "Z0007837", "Z0007838" };
				var OutcomeAimList = new[] { "Z0007834", "Z0007835", "Z0007836", "Z0007837", "Z0007838" };

				DateTime? _learnStartDate = this.LearningDeliveryList.OrderBy(l => l.LearnStartDate).FirstOrDefault().LearnStartDate;
				int? _fundModel = this.LearningDeliveryList.OrderBy(l => _learnStartDate).FirstOrDefault().FundModel;

				LearningDelivery _empOutcome = (from l in this.LearningDeliveryList where EmpOutcomeAimList.Contains(l.LearnAimRef) select l) as LearningDelivery;
				LearningDelivery _outcome = (from l in this.LearningDeliveryList where OutcomeAimList.Contains(l.LearnAimRef) select l) as LearningDelivery;


				XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDelivery", NSMgr.LookupNamespace("ia"));
				LearningDelivery newInstance = new LearningDelivery(newNode, NSMgr);
				newInstance.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
				newInstance.LearnAimRef = "ZPROG001";
				newInstance.AimType = 1;
				newInstance.AimSeqNumber = LearningDeliveryList.Count() + 1;
				newInstance.LearnStartDate = _learnStartDate;
				newInstance.FundModel = _fundModel;
				newInstance.ProgType = 24;
				if (_empOutcome != null) newInstance.EmpOutcome = _empOutcome.EmpOutcome;
				if (_outcome != null) newInstance.Outcome = _outcome.Outcome;

				// Add Fam SOF
				// Where Set to the Source of funding of the earliest learning aim where LearnDelFAMType=LDM and LearnDelFAMCode=323
				var sof = earliestAim.GetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF);


				// Add Fam FFI
				// If present set to the Fully funded indicator of the earliest learning aim where LearnDelFAMType=LDM and LearnDelFAMCode=323
				var ffi = earliestAim.GetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI);


				// Add Fam LSF
				// Set to LSF1 if Learning Support Funding is present and continuing (where Date applies to >01/08/2014) on any aims where LearnDelFAMType=LDM and LearnDelFAMCode=323.*

				foreach (LearningDelivery ld in AnyLdWithLdm323)
				{
					if (ld.LearningDeliveryFAMList.Count(x => x.LearnDelFAMType == "LSF" && ld.LearnPlanEndDate > Convert.ToDateTime("01 Aug 2014")) > 0)
					{
						newInstance.AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LSF, "1", Convert.ToDateTime("01 Aug 2014"), ld.LearnPlanEndDate);
						break;
					}
				}

				//DateFrom	Set to 01/08/2014 if Learning support funding (LSF) is recorded*
				// DateTo	Set to the planned end date of the programme aim or date that learning suppport is required until if earlier than the planned end date


				//*Note about Learning support funding:	Learning support funding for 2014/15 must be recorded on the Traineeship programme aim. 
				// Any learning suppport funding previously recorded on any of the Traineeship component aims should be stopped by recording a date of 31/07/2014 
				//      in the The Date applies to field for that aim	

				newInstance.AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM, "323");

				LearningDeliveryList.Add(newInstance);
				AppendToLastOfNodeNamed(newNode, newNode.Name);
			}
		}

		#endregion

		#region Methods
		public void Delete(ChildEntity Child)
		{
			Node.RemoveChild(Child.Node);
			switch (Child.GetType().ToString())
			{
				case "ILR.LearnerContact":
					this.LearnerContactList.Remove((LearnerContact)Child);
					break;
				case "ILR.ContactPreference":
					this.ContactPreferenceList.Remove((ContactPreference)Child);
					break;
				case "ILR.LLDDandHealthProblem":
					this.LLDDandHealthProblemList.Remove((LLDDandHealthProblem)Child);
					break;
				case "ILR.LearnerFAM":
					this.LearnerFAMList.Remove((LearnerFAM)Child);
					break;
				case "ILR.ProviderSpecLearnerMonitoring":
					this.ProviderSpecLearnerMonitoringList.Remove((ProviderSpecLearnerMonitoring)Child);
					break;
				case "ILR.LearnerEmploymentStatus":
					this.LearnerEmploymentStatusList.Remove((LearnerEmploymentStatus)Child);
					break;
				case "ILR.LearnerHE":
					this.LearnerHE = null;
					break;
				case "ILR.LearningDelivery":
					LearningDelivery toDelete = (LearningDelivery)Child;
					toDelete.LearningDeliveryPropertyChanged -= LearningDeliveryPropertyChanged;
					foreach (LearningDelivery ld in this.LearningDeliveryList.Where(x => x.AimSeqNumber > toDelete.AimSeqNumber))
						ld.AimSeqNumber -= 1;
					this.LearningDeliveryList.Remove(toDelete);
					break;
			}
			GiveFrountEndkickToRefresh();
		}
		private ILR.PostAdd GetPostAdd()
		{
			return (from ILR.LearnerContact learnerContact in this.LearnerContactList where learnerContact.LocType == 1 select learnerContact.PostAdd).FirstOrDefault();
		}
		#region FAM management
		public LearnerFAM GetFAM(LearnerFAM.SingleOccurrenceFAMs FAMType)
		{
			return this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString()).FirstOrDefault();
		}
		public int? GetFAMCode(LearnerFAM.SingleOccurrenceFAMs FAMType)
		{
			LearnerFAM lFAM = GetFAM(FAMType);
			if (lFAM == null)
				return null;
			else
				return lFAM.LearnFAMCode;
		}
		public void SetFAM(LearnerFAM.SingleOccurrenceFAMs FAMType, int FAMCode)
		{
			LearnerFAM lFAM = GetFAM(FAMType);
			if (lFAM == null)
			{
				lFAM = this.CreateLearnerFAM();
				lFAM.LearnFAMType = FAMType.ToString();
			}
			lFAM.LearnFAMCode = FAMCode;
		}
		public void RemoveFAM(LearnerFAM.SingleOccurrenceFAMs FAMType)
		{
			LearnerFAM lFAM = GetFAM(FAMType);
			if (lFAM != null)
				Delete(lFAM);
		}
		public void RemoveFAM(LearnerFAM.MultiOccurrenceFAMs FAMType, int Code)
		{
			LearnerFAM lFAM = this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString() && x.LearnFAMCode == Code).FirstOrDefault();
			if (lFAM != null)
				Delete(lFAM);
		}
		public List<int> GetFAMList(LearnerFAM.MultiOccurrenceFAMs FAMType)
		{
			List<int> result = new List<int>();
			foreach (LearnerFAM lFAM in this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString()))
				result.Add(lFAM.LearnFAMCode);
			return result;
		}
		public void ClearFAMList(LearnerFAM.MultiOccurrenceFAMs FAMType)
		{
			List<LearnerFAM> fmList = this.LearnerFAMList.Where(x => x.LearnFAMType.ToUpper() == FAMType.ToString().ToUpper()).ToList();
			foreach (LearnerFAM lFAM in fmList)
				Delete(lFAM);
		}
		public void AddFAM(LearnerFAM.MultiOccurrenceFAMs FAMType, int FAMCode)
		{
			if ((FAMType == LearnerFAM.MultiOccurrenceFAMs.LSR) || (FAMType == LearnerFAM.MultiOccurrenceFAMs.NLM))
			{
				LearnerFAM tmp = this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString() && x.LearnFAMCode == FAMCode).FirstOrDefault();
				if (tmp == null)
				{
					LearnerFAM lFAM = this.CreateLearnerFAM();
					lFAM.LearnFAMType = FAMType.ToString();
					lFAM.LearnFAMCode = FAMCode;
				}
				tmp = null;
			}
			else
			{
				LearnerFAM lFAM = this.CreateLearnerFAM();
				lFAM.LearnFAMType = FAMType.ToString();
				lFAM.LearnFAMCode = FAMCode;

			}
		}
		#endregion
		#region LLDD management
		public LLDDandHealthProblem GetLLDD(LLDDandHealthProblem.LLDDandHealthProblems LLDDType)
		{
			return this.LLDDandHealthProblemList.Where(x => x.LLDDType == LLDDType.ToString()).FirstOrDefault();
		}
		public int? GetLLDDCode(LLDDandHealthProblem.LLDDandHealthProblems LLDDType)
		{
			LLDDandHealthProblem lldd = GetLLDD(LLDDType);
			if (lldd == null)
				return null;
			else
				return lldd.LLDDCode;
		}
		public void SetLLDD(LLDDandHealthProblem.LLDDandHealthProblems LLDDType, int LLDDCode)
		{
			LLDDandHealthProblem lldd = GetLLDD(LLDDType);
			if (lldd == null)
			{
				lldd = this.CreateLLDDandHealthProblem();
				lldd.LLDDType = LLDDType.ToString();
			}
			lldd.LLDDCode = LLDDCode;
		}
		public void RemoveLLDD(LLDDandHealthProblem.LLDDandHealthProblems LLDDType)
		{
			LLDDandHealthProblem lldd = GetLLDD(LLDDType);
			if (lldd != null)
				Delete(lldd);
		}
		#endregion
		#region ProvSpecMon management
		public ProviderSpecLearnerMonitoring GetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence Occurrence)
		{
			return this.ProviderSpecLearnerMonitoringList.Where(x => x.ProvSpecLearnMonOccur == Occurrence.ToString()).FirstOrDefault();
		}
		public string GetProvSpecMonValue(ProviderSpecLearnerMonitoring.Occurrence Occurrence)
		{
			ProviderSpecLearnerMonitoring provSpecMon = GetProvSpecMon(Occurrence);
			if (provSpecMon == null)
				return null;
			else
				return provSpecMon.ProvSpecLearnMon;
		}
		public void SetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence Occurrence, string ProvSpecMonValue)
		{
			ProviderSpecLearnerMonitoring provSpecMon = GetProvSpecMon(Occurrence);
			if (ProvSpecMonValue != null && ProvSpecMonValue.Length != 0)
			{
				if (provSpecMon == null)
				{
					provSpecMon = this.CreateProviderSpecLearnerMonitoring();
					provSpecMon.ProvSpecLearnMonOccur = Occurrence.ToString();
				}
				provSpecMon.ProvSpecLearnMon = ProvSpecMonValue;
			}
			else
				if (provSpecMon != null)
					Delete(provSpecMon);
		}
		#endregion
		#region HE Fin management
		public LearnerHEFinancialSupport GetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType)
		{
			if (this.LearnerHE == null)
				return null;
			return this.LearnerHE.LearnerHEFinancialSupportList.Where(x => x.FINTYPE == (int)HEFinType).FirstOrDefault();
		}
		public int? GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType)
		{
			LearnerHEFinancialSupport heFinType = GetHEFin(HEFinType);
			if (heFinType == null)
				return null;
			else
				return heFinType.FINAMOUNT;
		}
		public void SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType, int HEFinAmount)
		{
			LearnerHEFinancialSupport heFin = GetHEFin(HEFinType);
			if (heFin == null)
			{
				if (this.LearnerHE == null)
					this.LearnerHE = this.CreateLearnerHE();
				heFin = this.LearnerHE.CreateLearnerHEFinancialSupport();
				heFin.FINTYPE = (int)HEFinType;
			}
			heFin.FINAMOUNT = HEFinAmount;
		}
		public void RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType)
		{
			LearnerHEFinancialSupport heFin = GetHEFin(HEFinType);
			if (heFin != null)
				Delete(heFin);
		}
		public List<int> GetHEFinList(LearnerFAM.MultiOccurrenceFAMs FAMType)
		{
			List<int> result = new List<int>();
			foreach (LearnerFAM lFAM in this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString()))
				result.Add(lFAM.LearnFAMCode);
			return result;
		}
		public void ClearHEFinList(LearnerFAM.MultiOccurrenceFAMs FAMType)
		{
			foreach (LearnerFAM lFAM in this.LearnerFAMList.Where(x => x.LearnFAMType.ToString().ToUpper() == FAMType.ToString()))
				Delete(lFAM);
		}
		public void AddHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType, int HEFinAmount)
		{
			LearnerHEFinancialSupport lFAM = this.LearnerHE.CreateLearnerHEFinancialSupport();
			lFAM.FINTYPE = (int)HEFinType;
			lFAM.FINAMOUNT = HEFinAmount;
		}
		#endregion

		public void ResequenceAimSeqNumber()
		{
			int i = 1;
			foreach (LearningDelivery ld in this.LearningDeliveryList)
			{
				ld.AimSeqNumber = i;
				i++;
			}
		}
		public bool HasLearningDeliveriesInFundingModel(int FundModel)
		{
			foreach (LearningDelivery ld in LearningDeliveryList)
			{
				if (ld.FundModel == FundModel)
					return true;
			}
			return false;
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
				if (columnName == "LearnRefNumber")
				{
					if (LearnRefNumber != null)
					{
						if (LearnRefNumber.Length > 12)
							result = "LearnRefNumber exceeds maximum lenght (12).";
						if (Message.CountLearnRefNumberInstances(LearnRefNumber) > 1)
							result = "LearnRefNumber is not unique.";
					}
				}

				if (columnName == "NINumber")
				{
					if (NINumber != null && NINumber.Length > 9)
						result = "NI Number exceeds maximum length (9).";
				}
				if (columnName == "GivenNames")
				{
				
				}				
				return result;
			}
		}


		#endregion

	}
}