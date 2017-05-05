using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    internal static class XMLHelper
    {
        private static Dictionary<string, List<string>> Entities = new Dictionary<string, List<string>>()
        {
            {"CollectionDetails",new List<string>() {"Collection","Year","FilePreparationDate"}},
            {"ContactPreference",new List<string>() {"ContPrefType","ContPrefCode"}},
            {"DPOutcome",new List<string>() {"OutType","OutCode","OutStartDate","OutEndDate","OutCollDate"}},
            {"EmploymentStatusMonitoring",new List<string>() {"DateEmpStatApp","LearnRefNumber","ESMType","ESMCode"}},
            {"Learner",new List<string>() {"LearnRefNumber","PrevLearnRefNumber","PrevUKPRN","ULN","FamilyName","GivenNames","DateOfBirth","Ethnicity","Sex","LLDDHealthProb","NINumber","PriorAttain","Accom","ALSCost","PlanLearnHours","PlanEEPHours","MathGrade","EngGrade","LearnerContact","ContactPreference","LLDDandHealthProblem","LearnerFAM","ProviderSpecLearnerMonitoring","LearnerEmploymentStatus","LearnerHE","LearningDelivery"}},
            {"LearnerContact",new List<string>() {"LocType","ContType","PostCode","TelNumber","Email","PostAdd"}},
            {"LearnerDestinationandProgression",new List<string>() {"LearnRefNumber","ULN"}},
            {"LearnerEmploymentStatus",new List<string>() {"EmpStat","DateEmpStatApp","EmpId","EmploymentStatusMonitoring"}},
            {"LearnerFAM",new List<string>() {"LearnFAMType","LearnFAMCode"}},
            {"LearnerHE",new List<string>() {"UCASPERID","TTACCOM","LearnerHEFinancialSupport"}},
            {"LearnerHEFinancialSupport",new List<string>() {"FINTYPE","FINAMOUNT"}},
            {"LearningDelivery",new List<string>() {"LearnAimRef","AimType","AimSeqNumber","LearnStartDate","OrigLearnStartDate","LearnPlanEndDate","FundModel","ProgType","FworkCode","PwayCode","PartnerUKPRN","DelLocPostCode","AddHours","PriorLearnFundAdj","OtherFundAdj","ConRefNumber","EmpOutcome","CompStatus","LearnActEndDate","WithdrawReason","Outcome","AchDate","OutGrade","SWSupAimId","LearningDeliveryFAM","TrailblazerApprenticeshipFinancialRecord","ProviderSpecDeliveryMonitoring","LearningDeliveryHE","LearningDeliveryWorkPlacement"}},
            {"LearningDeliveryFAM",new List<string>() {"LearnDelFAMType","LearnDelFAMCode","LearnDelFAMDateFrom","LearnDelFAMDateTo"}},
            {"LearningDeliveryHE",new List<string>() {"NUMHUS","SSN","QUALENT3","SOC2000","SEC","TOTALTS","UCASAPPID","TYPEYR","MODESTUD","FUNDLEV","FUNDCOMP","STULOAD","YEARSTU","MSTUFEE","PCOLAB","PCFLDCS","PCSLDCS","PCTLDCS","SPECFEE","NETFEE","GROSSFEE","DOMICILE","ELQ","HEPostCode"}},
            {"LearningDeliveryWorkPlacement",new List<string>() {"WorkPlaceStartDate","WorkPlaceEndDate","WorkPlaceMode","WorkPlaceEmpId"}},
            {"LearningProvider",new List<string>() {"UKPRN"}},
            {"LLDDandHealthProblem",new List<string>() {"LLDDCat","PrimaryLLDD"}},
            {"PostAdd",new List<string>() {"AddLine1","AddLine2","AddLine3","AddLine4"}},
            {"ProviderSpecDeliveryMonitoring",new List<string>() {"ProvSpecDelMonOccur","ProvSpecDelMon"}},
            {"ProviderSpecLearnerMonitoring",new List<string>() {"ProvSpecLearnMonOccur","ProvSpecLearnMon"}},
            {"Source",new List<string>() {"ProtectiveMarking","UKPRN","SoftwareSupplier","SoftwarePackage","Release","SerialNo","DateTime","ReferenceData","ComponentSetVersion"}},
            {"SourceFile",new List<string>() {"SourceFileName","FilePreparationDate","SoftwareSupplier","SoftwarePackage","Release","SerialNo","DateTime"}},
            {"TrailblazerApprenticeshipFinancialRecord",new List<string>() {"TBFinType","TBFinCode","TBFinDate","TBFinAmount"}}
        };

        internal static string GetChildValue(string Name, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            string result = null;
            XmlNode childNode = Node.SelectSingleNode("./ia:" + Name, NSMgr);
            if (childNode != null)
                result = childNode.InnerText;
            return result;
        }

        internal static void SetChildValue(string Name, object Value, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            XmlNode childNode = Node.SelectSingleNode("./ia:" + Name, NSMgr);
            if (Value == null || (Value.GetType().ToString()=="System.String" && Value.ToString().Length==0))
            {
                if (childNode != null)
                    childNode.ParentNode.RemoveChild(childNode);
                return;
            }
            if (childNode == null)
                childNode = CreateChildNode(Name, Node, NSMgr);
            switch (Value.GetType().ToString())
            {
                case "System.DateTime":
                    DateTime dateTimeValue = (DateTime)Value;
                    if (dateTimeValue.Hour == 0 && dateTimeValue.Minute == 0 && dateTimeValue.Second == 0 && dateTimeValue.Millisecond == 0)
                        childNode.InnerText = dateTimeValue.ToString("yyyy-MM-dd");
                    else
                        childNode.InnerText = dateTimeValue.ToString("yyyy-MM-ddThh:mm:ss");
                    break;
                default:
                    childNode.InnerText = Value.ToString();
                    break;
            }
        }
        private static XmlNode CreateChildNode(string Name, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement(Name, NSMgr.LookupNamespace("ia"));

            if (!Node.HasChildNodes)
                Node.AppendChild(newNode);
            else
            {
                List<string> attributes = Entities[Node.Name];
                int newNodeIndex = attributes.IndexOf(Name);
                foreach (XmlNode childNode in Node.ChildNodes)
                {
                    if (attributes.IndexOf(childNode.Name)==-1 || attributes.IndexOf(childNode.Name) > newNodeIndex)
                    {
                        Node.InsertBefore(newNode, childNode);
                        break;
                    }
                }
                if (newNode.ParentNode==null)
                    Node.AppendChild(newNode);
            }
            return newNode;
        }
    }
}