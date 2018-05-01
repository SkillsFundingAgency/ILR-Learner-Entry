using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class LearnerEmploymentStatus : ChildEntity, IDataErrorInfo
    {
        private const String CLASSNAME = "LearnerEmploymentStatus";
        private const String TABS = "\t";

        #region ILR Properties
        public int? EmpStat { get { string EmpStat = XMLHelper.GetChildValue("EmpStat", Node, NSMgr); return (EmpStat != null ? int.Parse(EmpStat) : (int?)null); } set { XMLHelper.SetChildValue("EmpStat", value, Node, NSMgr); OnPropertyChanged("EmpStat"); } }
        public DateTime? DateEmpStatApp { get { string DateEmpStatApp = XMLHelper.GetChildValue("DateEmpStatApp", Node, NSMgr); return (!string.IsNullOrEmpty(DateEmpStatApp) ? DateTime.Parse(DateEmpStatApp) : (DateTime?)null); } set { XMLHelper.SetChildValue("DateEmpStatApp", value, Node, NSMgr); OnPropertyChanged("DateEmpStatApp"); } }
        public int? EmpId { get { string EmpId = XMLHelper.GetChildValue("EmpId", Node, NSMgr); return (!string.IsNullOrEmpty(EmpId) ? int.Parse(EmpId) : (int?)null); } set { XMLHelper.SetChildValue("EmpId", value, Node, NSMgr); OnPropertyChanged("EmpId"); } }
        #endregion

        #region Lookup Properties
        public string EmpStatDescription
        {
            get
            {
                Lookup lookup = new Lookup();
                return lookup.GetDescription("EmpStat", this.EmpStat.ToString());
            }
        }
        #endregion

        #region ILR Child Entites
        public List<EmploymentStatusMonitoring> EmploymentStatusMonitoringList = new List<EmploymentStatusMonitoring>();
        #endregion

        #region Child Entity Creation
        public EmploymentStatusMonitoring CreateEmploymentStatusMonitoring()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("EmploymentStatusMonitoring", NSMgr.LookupNamespace("ia"));
            EmploymentStatusMonitoring newInstance = new EmploymentStatusMonitoring(newNode, NSMgr);
            EmploymentStatusMonitoringList.Add(newInstance);
            Node.AppendChild(newNode);
            return newInstance;
        }
        #endregion

        #region Renormalised Child Entities
        public bool SEI
        {
            get
            {
                return GetESMCode(EmploymentStatusMonitoring.ESMTypes.SEI) == 1;
            }
            set
            {
                if (value)
                    SetESM(EmploymentStatusMonitoring.ESMTypes.SEI, 1);
                else
                    RemoveESM(EmploymentStatusMonitoring.ESMTypes.SEI);
                OnPropertyChanged("SEI");
            }
        }
        public bool PEI
        {
            get
            {
                return GetESMCode(EmploymentStatusMonitoring.ESMTypes.PEI) == 1;
            }
            set
            {
                if (value)
                    SetESM(EmploymentStatusMonitoring.ESMTypes.PEI, 1);
                else
                    RemoveESM(EmploymentStatusMonitoring.ESMTypes.PEI);
                OnPropertyChanged("PEI");
            }
        }
        public bool SEM
        {
            get
            {
                return GetESMCode(EmploymentStatusMonitoring.ESMTypes.SEM) == 1;
            }
            set
            {
                if (value)
                    SetESM(EmploymentStatusMonitoring.ESMTypes.SEM, 1);
                else
                    RemoveESM(EmploymentStatusMonitoring.ESMTypes.SEM);
                OnPropertyChanged("SEM");
            }
        }
        public int? EII
        {
            get
            {
                return GetESMCode(EmploymentStatusMonitoring.ESMTypes.EII);
            }
            set
            {
                if (value != null)
                    SetESM(EmploymentStatusMonitoring.ESMTypes.EII, (int)value);
                else
                    RemoveESM(EmploymentStatusMonitoring.ESMTypes.EII);
                OnPropertyChanged("EII");
            }
        }
        public int? LOU
        {
            get
            {
                return GetESMCode(EmploymentStatusMonitoring.ESMTypes.LOU);
            }
            set
            {
                if (value != null)
                    SetESM(EmploymentStatusMonitoring.ESMTypes.LOU, (int)value);
                else
                    RemoveESM(EmploymentStatusMonitoring.ESMTypes.LOU);
                OnPropertyChanged("LOU");
            }
        }
        public int? LOE
        {
            get
            {
                return GetESMCode(EmploymentStatusMonitoring.ESMTypes.LOE);
            }
            set
            {
                if (value != null)
                    SetESM(EmploymentStatusMonitoring.ESMTypes.LOE, (int)value);
                else
                    RemoveESM(EmploymentStatusMonitoring.ESMTypes.LOE);
                OnPropertyChanged("LOE");
            }
        }
        public int? BSI
        {
            get
            {
                return GetESMCode(EmploymentStatusMonitoring.ESMTypes.BSI);
            }
            set
            {
                if (value != null)
                    SetESM(EmploymentStatusMonitoring.ESMTypes.BSI, (int)value);
                else
                    RemoveESM(EmploymentStatusMonitoring.ESMTypes.BSI);
                OnPropertyChanged("BSI");
            }
        }
        #endregion

        #region Constructors
        internal LearnerEmploymentStatus(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            XmlNodeList employmentStatusMonitoringNodes = Node.SelectNodes("./ia:EmploymentStatusMonitoring", NSMgr);
            foreach (XmlNode node in employmentStatusMonitoringNodes)
                EmploymentStatusMonitoringList.Add(new EmploymentStatusMonitoring(node, NSMgr));
        }
        internal LearnerEmploymentStatus(LearnerEmploymentStatus MigrationLearnerEmploymentStatus, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.EmpStat = MigrationLearnerEmploymentStatus.EmpStat;
            this.DateEmpStatApp = MigrationLearnerEmploymentStatus.DateEmpStatApp;
            this.EmpId = MigrationLearnerEmploymentStatus.EmpId;

            foreach (EmploymentStatusMonitoring migrationItem in MigrationLearnerEmploymentStatus.EmploymentStatusMonitoringList.Where(x => x.ESMType != "RON"))
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("EmploymentStatusMonitoring", NSMgr.LookupNamespace("ia"));
                EmploymentStatusMonitoring newInstance = new EmploymentStatusMonitoring(migrationItem, newNode, NSMgr);
                EmploymentStatusMonitoringList.Add(newInstance);
                Node.AppendChild(newNode);
            }
        }
        #endregion

        #region Methods
        public void Delete(EmploymentStatusMonitoring EmploymentStatusMonitoring)
        {
            Node.RemoveChild(EmploymentStatusMonitoring.Node);
            this.EmploymentStatusMonitoringList.Remove(EmploymentStatusMonitoring);
        }
        #region ESM management
        public EmploymentStatusMonitoring GetESM(EmploymentStatusMonitoring.ESMTypes ESMType)
        {
            return this.EmploymentStatusMonitoringList.Where(x => x.ESMType == ESMType.ToString()).FirstOrDefault();
        }
        public int? GetESMCode(EmploymentStatusMonitoring.ESMTypes FAMType)
        {
            EmploymentStatusMonitoring lFAM = GetESM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.ESMCode;
        }
        public void SetESM(EmploymentStatusMonitoring.ESMTypes ESMType, int ESMCode)
        {
            EmploymentStatusMonitoring lFAM = GetESM(ESMType);
            if (lFAM == null)
            {
                lFAM = this.CreateEmploymentStatusMonitoring();
                lFAM.ESMType = ESMType.ToString();
            }
            lFAM.ESMCode = ESMCode;
        }
        public void RemoveESM(EmploymentStatusMonitoring.ESMTypes ESMType)
        {
            EmploymentStatusMonitoring lFAM = GetESM(ESMType);
            if (lFAM != null)
                Delete(lFAM);
        }
        #endregion
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
                switch (columnName)
                {
                    case "EmpStat":
                        if (EmpStat != null)
                            result += CheckPropertyLength(EmpStat, CLASSNAME, columnName, TABS);
                        break;
                    case "EmpId":
                        if (EmpId != null)
                            result += CheckPropertyLength(EmpId, CLASSNAME, columnName, TABS);
                        break;
                    default:
                        break;
                }
                return result;
            }
        }
        #endregion
    }
}
