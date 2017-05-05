using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Threading.Tasks;

namespace ILR
{
    public class Message : INotifyPropertyChanged
    {
        #region Year-specific constants
        public static int CurrentYear = 1516;
        public static string CurrentNameSpace = @"SFA/ILR/2015-16";
        public static string PreviousNameSpace = "http://www.theia.org.uk/ILR/2014-15/1";
        public static string FileNameTemplate = "ILR-$$UKPRN$$-$$YEAR$$-$$NOW$$-01.xml";
        public static DateTime CurrentYearStart = new DateTime(2015, 8, 1);
        public static string ProtectiveMarking = "OFFICIAL-SENSITIVE-Personal";
        private DataTable _statistics = new DataTable();
        #endregion

        #region Counts
        public DataTable Statistics { get { return _statistics; } }
        public DataTable StatisticsOld
        {
            get
            {
                int learners = 0;
                int learningDeliveries = 0;
                int fm10 = 0;
                int fm25 = 0;
                int fm35 = 0;
                int fm70 = 0;
                int fm81 = 0;
                int fm82 = 0;
                int fm99 = 0;

                /*				foreach (Learner l in LearnerList)
                                {
                                    learners++;
                                    learningDeliveries += l.LearningDeliveryList.Count;

                                    if (l.HasLearningDeliveriesInFundingModel(10)) fm10++;
                                    if (l.HasLearningDeliveriesInFundingModel(25)) fm25++;
                                    if (l.HasLearningDeliveriesInFundingModel(35)) fm35++;
                                    if (l.HasLearningDeliveriesInFundingModel(70)) fm70++;
                                    if (l.HasLearningDeliveriesInFundingModel(81)) fm81++;
                                    if (l.HasLearningDeliveriesInFundingModel(82)) fm82++;
                                    if (l.HasLearningDeliveriesInFundingModel(99)) fm99++;
                                }
                */
                using (DataTable results = new DataTable())
                {
                    results.Columns.Add(new DataColumn("Description", typeof(string)));
                    results.Columns.Add(new DataColumn("Count", typeof(string)));
                    DataRow row = results.NewRow();
                    row["Description"] = "Learner count";
                    row["Count"] = learners.ToString();
                    results.Rows.Add(row);

                    if (learners > 0)
                    {
                        row = results.NewRow();
                        row["Description"] = "LearningDelivery count";
                        row["Count"] = learningDeliveries.ToString();
                        results.Rows.Add(row);
                        AddFundingModelRowStatistic(results, 10, fm10);
                        AddFundingModelRowStatistic(results, 25, fm25);
                        AddFundingModelRowStatistic(results, 35, fm35);
                        AddFundingModelRowStatistic(results, 70, fm70);
                        AddFundingModelRowStatistic(results, 81, fm81);
                        AddFundingModelRowStatistic(results, 82, fm82);
                        AddFundingModelRowStatistic(results, 99, fm99);

                        row = results.NewRow();
                        row["Description"] = "Learners excluded from export count";
                        row["Count"] = (LearnerCount - LearnerExportCount).ToString();
                        results.Rows.Add(row);
                    }

                    return results;
                }
            }
        }

        public void ReFreshStats()
        {
            ReBuildStatisticsDatatable();        
        }
        public void ReBuildStatisticsDatatable()
        {
            _statistics = new DataTable();

            int learners = 0;
            int learningDeliveries = 0;
            int fm10 = 0;
            int fm25 = 0;
            int fm35 = 0;
            int fm70 = 0;
            int fm81 = 0;
            int fm82 = 0;
            int fm99 = 0;

            foreach (Learner l in LearnerList)
            {
                learners++;
                learningDeliveries += l.LearningDeliveryList.Count;

                if (l.HasLearningDeliveriesInFundingModel(10)) fm10++;
                if (l.HasLearningDeliveriesInFundingModel(25)) fm25++;
                if (l.HasLearningDeliveriesInFundingModel(35)) fm35++;
                if (l.HasLearningDeliveriesInFundingModel(70)) fm70++;
                if (l.HasLearningDeliveriesInFundingModel(81)) fm81++;
                if (l.HasLearningDeliveriesInFundingModel(82)) fm82++;
                if (l.HasLearningDeliveriesInFundingModel(99)) fm99++;
            }
       
            _statistics.Columns.Add(new DataColumn("Description", typeof(string)));
            _statistics.Columns.Add(new DataColumn("Count", typeof(string)));
            DataRow row = _statistics.NewRow();
            row["Description"] = "Learner count";
            row["Count"] = learners.ToString();
            _statistics.Rows.Add(row);

            if (learners > 0)
            {
                row = _statistics.NewRow();
                row["Description"] = "LearningDelivery count";
                row["Count"] = learningDeliveries.ToString();
                _statistics.Rows.Add(row);
                AddFundingModelRowStatistic(_statistics, 10, fm10);
                AddFundingModelRowStatistic(_statistics, 25, fm25);
                AddFundingModelRowStatistic(_statistics, 35, fm35);
                AddFundingModelRowStatistic(_statistics, 70, fm70);
                AddFundingModelRowStatistic(_statistics, 81, fm81);
                AddFundingModelRowStatistic(_statistics, 82, fm82);
                AddFundingModelRowStatistic(_statistics, 99, fm99);


                //if (learners < 500)
                //{
                    row = _statistics.NewRow();
                    row["Description"] = "Learners excluded from export count";
                    row["Count"] = (learners - this.LearnerList.Count(x => !x.ExcludeFromExport)).ToString();

                    _statistics.Rows.Add(row);
                //}
            }
            OnPropertyChanged("Statistics");
        }

        private void AddFundingModelRowStatistic(DataTable Table, int FundModel, int Learners)
        {
            if (Learners > 0)
            {
                Lookup lu = new Lookup();
                DataRow row = Table.NewRow();
                row["Description"] = lu.GetDescription("FundModel", FundModel.ToString());
                row["Count"] = Learners;
                Table.Rows.Add(row);
            }
        }
        public int LearnerCount
        {
            get
            {
                return this.LearnerList.Count();
            }
        }
        public int LearnerExportCount
        {
            get
            {
                int ExportlearnCounter = this.LearnerList.Count(x => x.IsComplete && !x.ExcludeFromExport);
                return ExportlearnCounter;
            }
        }
        public int LearningDeliveryCount
        {
            get
            {
                int learningDeliveries = 0;
                foreach (Learner learner in this.LearnerList)
                    learningDeliveries += learner.LearningDeliveryList.Count();
                return learningDeliveries;
            }
        }
        public int LearnerDestinationandProgressionCount
        {
            get
            {
                return this.LearnerDestinationandProgressionList.Count();
            }
        }
        #endregion

        #region Private Properties
        XmlDocument ILRFile;
        XmlNamespaceManager NSMgr;
        string Filename;
        #endregion

        #region Private Properties
        public Boolean IsFileImportLoadingRunning;
        #endregion

        #region ILR Child Entites
        private Header Header;
        public LearningProvider LearningProvider;
        public List<Learner> LearnerList = new List<Learner>();
        public List<LearnerDestinationandProgression> LearnerDestinationandProgressionList = new List<LearnerDestinationandProgression>(0);
        #endregion

        #region Public Methods
        public void Load(string Filename)
        {
            //Instantiate a new document to hold the ILR data
            this.ILRFile = new XmlDocument();

            //Get a namespace manager from the XML document
            NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
            NSMgr.AddNamespace("ia", CurrentNameSpace);

            //Load the data
            Load(Filename, ILRFile, NSMgr);
        }
        protected void LoadPreviousYearILR(string Filename)
        {
            //Instantiate a new document to hold the ILR data
            this.ILRFile = new XmlDocument();

            //Get a namespace manager from the XML document
            NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
            NSMgr.AddNamespace("ia", PreviousNameSpace);

            //Load the data
            Load(Filename, ILRFile, NSMgr);
        }
        private void Load(string Filename, XmlDocument Document, XmlNamespaceManager NSMgr)
        {
            IsFileImportLoadingRunning = true;

            //Store the filename we're using
            this.Filename = Filename;

            //Create an XML document object from the file specified
            Document.Load(Filename);

            //Find the Header node 
            XmlNode headerNode = Document.SelectSingleNode("/ia:Message/ia:Header", NSMgr);
            if (headerNode == null)
            {
                headerNode = Document.CreateElement("Header", NSMgr.LookupNamespace("ia"));
                if (Document.DocumentElement.HasChildNodes)
                    Document.DocumentElement.InsertBefore(headerNode, Document.DocumentElement.FirstChild);
                else
                    Document.DocumentElement.AppendChild(headerNode);
            }
            this.Header = new ILR.Header(headerNode, NSMgr);

            //Find the LearningProvider node 
            XmlNode learningProviderNode = Document.SelectSingleNode("/ia:Message/ia:LearningProvider", NSMgr);

            //Find the Learner nodes
            XmlNodeList learnerNodes = Document.SelectNodes("/ia:Message/ia:Learner", NSMgr);

            //If we have no LearningProvider node create one in the correct place
            if (learningProviderNode == null)
            {
                learningProviderNode = Document.CreateElement("LearningProvider", NSMgr.LookupNamespace("ia"));
                if (learnerNodes.Count == 0)
                    Document.DocumentElement.AppendChild(learningProviderNode);
                else
                    Document.DocumentElement.InsertBefore(learningProviderNode, learnerNodes.Item(0));
            }

            //Create a LearningProvider instance
            this.LearningProvider = new ILR.LearningProvider(learningProviderNode, NSMgr);

            //Create Learner instances for all of the learners in the XML
            foreach (XmlNode node in learnerNodes)
            {
                Learner newInstance = new Learner(node, NSMgr);
                newInstance.IsFileImportLoadingRunning = true;
                newInstance.Message = this;
                newInstance.ResequenceAimSeqNumber();
                foreach (LearningDelivery ld in newInstance.LearningDeliveryList)
                {
                    ld.IsImportRunning = false;
                }
                newInstance.IsFileImportLoadingRunning = false;
                LearnerList.Add(newInstance);
                //Console.WriteLine(String.Format("Loaded {0} Learner.", LearnerList.Count));
            }

            //Find the LearnerDestinationandProgression nodes
            XmlNodeList learnerDestinationandProgressionNodes = Document.SelectNodes("/ia:Message/ia:LearnerDestinationandProgression", NSMgr);

            //Create LearnerDestinationandProgression instances for all of the LearnerDestinationandProgressions in the XML
            foreach (XmlNode node in learnerDestinationandProgressionNodes)
            {
                LearnerDestinationandProgression newLDP = new LearnerDestinationandProgression(node, NSMgr);
                newLDP.Message = this;
                LearnerDestinationandProgressionList.Add(newLDP);
            }

            foreach (Learner l in LearnerList)
            {
                foreach (LearningDelivery ld in l.LearningDeliveryList)
                {
                    ld.IsImportRunning = false;
                }
                l.IsFileImportLoadingRunning = false;
            }
            IsFileImportLoadingRunning = false;

        }
        public void Save(string fileName)
        {
            // remove empty blank nodes.
            var document = XDocument.Parse(this.ILRFile.OuterXml);

            document.Descendants()
                    .Where(e => e.IsEmpty || String.IsNullOrWhiteSpace(e.Value))
                    .Remove();
            document.Save(fileName);

            //this.ILRFile.Save(Filename);
        }
        public void Save()
        {
            this.Save(this.Filename);
        }
        public void Export(string ExportFolder, string Release)
        {
            //Calculate filename
            string filename = ExportFolder;
            if (!filename.EndsWith("\\"))
                filename += "\\";
            filename += FileNameTemplate.Replace("$$UKPRN$$", LearningProvider.UKPRN.ToString()).Replace("$$YEAR$$", CurrentYear.ToString()).Replace("$$NOW$$", DateTime.Now.ToString("yyyyMMdd-HHmmss"));

            //Update header information
            this.Header.CollectionDetails.FilePreparationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            this.Header.Source.UKPRN = LearningProvider.UKPRN;
            this.Header.Source.SerialNo = "01";
            this.Header.Source.DateTime = DateTime.Now;
            this.Header.Source.ProtectiveMarking = Message.ProtectiveMarking;
            this.Header.CollectionDetails.Year = Message.CurrentYear.ToString();

            if (Release != null)
                this.Header.Source.Release = Release;

            //Save where we are
            Save();

            Message exportMessage = new Message(this.Filename);

            // remove non complete learner so we only export good learners.
            foreach (Learner learner in exportMessage.LearnerList)
            {
                if (!learner.IsComplete || learner.ExcludeFromExport)
                    learner.DeleteNode();
            }
            exportMessage.Save(filename);

            exportMessage = null;
        }
        public void Import(string FilenameToLoad)
        {
            String InterlStoreFilename = this.Filename;

            //Instantiate a new document to hold the ILR data
            this.ILRFile = new XmlDocument();

            //Get a namespace manager from the XML document
            NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
            NSMgr.AddNamespace("ia", CurrentNameSpace);

            this.ILRFile.AppendChild(this.ILRFile.CreateElement("Message", NSMgr.LookupNamespace("ia")));

            // Clear Old Learner and Old LearnerDestinationandProgression Records
            this.LearnerList.Clear();
            this.LearnerDestinationandProgressionList.Clear();

            Message importMessage = new Message();

            System.IO.FileInfo fi = new System.IO.FileInfo(FilenameToLoad);
            if (fi.Name.Contains("-" + CurrentYear.ToString() + "-"))
            {
                this.Load(FilenameToLoad);
                //importMessage.Load(Filename);
            }
            else if (fi.Name.Contains("-" + (CurrentYear - 101).ToString() + "-"))
            {
                importMessage.LoadPreviousYearILR(FilenameToLoad);

                XmlNode headerNode = this.ILRFile.SelectSingleNode("/ia:Message/ia:Header", NSMgr);
                if (headerNode == null)
                {
                    headerNode = this.ILRFile.CreateElement("Header", NSMgr.LookupNamespace("ia"));
                }
                if (this.ILRFile.DocumentElement.HasChildNodes)
                    this.ILRFile.DocumentElement.InsertBefore(headerNode, this.ILRFile.DocumentElement.FirstChild);
                else
                    this.ILRFile.DocumentElement.AppendChild(headerNode);

                this.Header = new ILR.Header(headerNode, NSMgr);


                //Find the LearningProvider node 
                XmlNode learningProviderNode = this.ILRFile.SelectSingleNode("/ia:Message/ia:LearningProvider", NSMgr);

                //Find the Learner nodes
                XmlNodeList learnerNodes = this.ILRFile.SelectNodes("/ia:Message/ia:Learner", NSMgr);

                //If we have no LearningProvider node create one in the correct place
                if (learningProviderNode == null)
                {
                    learningProviderNode = this.ILRFile.CreateElement("LearningProvider", NSMgr.LookupNamespace("ia"));
                    if (learnerNodes.Count == 0)
                        this.ILRFile.DocumentElement.AppendChild(learningProviderNode);
                    else
                        this.ILRFile.DocumentElement.InsertBefore(learningProviderNode, learnerNodes.Item(0));
                }

                this.LearningProvider = new ILR.LearningProvider(learningProviderNode, NSMgr);
                this.LearningProvider.UKPRN = importMessage.LearningProvider.UKPRN;

                foreach (Learner learner in importMessage.LearnerList.Where(l => l.HasContinuingAims))
                {
                    try
                    {
                        XmlNode newNode = ILRFile.CreateElement("Learner", NSMgr.LookupNamespace("ia"));
                        Learner newInstance = new Learner(learner, newNode, NSMgr);
                        newInstance.Message = this;
                        LearnerList.Add(newInstance);
                        AppendToLastOfNodeNamed(newNode, newNode.Name);
                        newInstance.ResequenceAimSeqNumber();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(String.Format("Learern Ref:{0}", learner.LearnRefNumber));
                    }
                }

                foreach (LearnerDestinationandProgression learnerDestinationandProgression in importMessage.LearnerDestinationandProgressionList.Where(ldp => ldp.HasCurrentDPOutcomes))
                {
                    XmlNode newNode = ILRFile.CreateElement("LearnerDestinationandProgression", NSMgr.LookupNamespace("ia"));
                    LearnerDestinationandProgression newInstance = new LearnerDestinationandProgression(learnerDestinationandProgression, newNode, NSMgr);
                    newInstance.Message = this;
                    LearnerDestinationandProgressionList.Add(newInstance);
                    AppendToLastOfNodeNamed(newNode, newNode.Name);
                }
            }
            else
            {
                throw (new Exception("Unable to identify Year from filename. Confirm the file name matchesd the ILR Specification."));
            }
            this.Filename = InterlStoreFilename;
            Save();
            GC.Collect();
        }
        public void Delete(ChildEntity Child)
        {
            ILRFile.DocumentElement.RemoveChild(Child.Node);
            switch (Child.GetType().ToString())
            {
                case "ILR.Learner":
                    this.LearnerList.Remove((Learner)Child);
                    break;
                case "ILR.LearnerDestinationandProgression":
                    this.LearnerDestinationandProgressionList.Remove((LearnerDestinationandProgression)Child);
                    break;
            }
        }
        public bool LearnRefNumberExists(string LearnRefNumber)
        {
            return ILRFile.SelectNodes("/ia:Message/ia:Learner/ia:LearnRefNumber[text()='" + LearnRefNumber + "']", NSMgr).Count > 0;
        }
        public int CountLearnRefNumberInstances(string LearnRefNumber)
        {
            return ILRFile.SelectNodes("/ia:Message/ia:Learner/ia:LearnRefNumber[text()='" + LearnRefNumber + "']", NSMgr).Count;
        }
        public void FixLDAPULN()
        {
            XmlNodeList ldaps = ILRFile.SelectNodes("ia:Message/ia:LearnerDestinationandProgression", NSMgr);
            foreach (XmlNode ldap in ldaps)
            {
                XmlNode uln = ldap.SelectSingleNode("./ia:ULN", NSMgr);
                XmlNode lrn = ldap.SelectSingleNode("./ia:LearnRefNumber", NSMgr);
                if (uln != null && ldap.ChildNodes.Count > 1)
                {
                    ldap.RemoveChild(uln);
                    if (lrn != null)
                        ldap.InsertAfter(uln, lrn);
                    else
                        ldap.InsertBefore(uln, ldap.FirstChild);
                }
            }
        }
        #endregion

        #region Child Entity Creation
        public Learner CreateLearner()
        {
            XmlNode newNode = ILRFile.CreateElement("Learner", NSMgr.LookupNamespace("ia"));
            Learner newInstance = new Learner(newNode, NSMgr);
            newInstance.LearnRefNumber = GetNextLearnRefNumber();
            newInstance.Message = this;
            LearnerList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }
        public LearnerDestinationandProgression CreateLearnerDestinationandProgression()
        {
            XmlNode newNode = ILRFile.CreateElement("LearnerDestinationandProgression", NSMgr.LookupNamespace("ia"));
            LearnerDestinationandProgression newInstance = new LearnerDestinationandProgression(newNode, NSMgr);
            newInstance.Message = this;
            LearnerDestinationandProgressionList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }
        private void AppendToLastOfNodeNamed(XmlNode NewNode, string NodeName)
        {
            switch (NodeName)
            {
                case "Learner":
                    if (LearnerDestinationandProgressionList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LearnerDestinationandProgression");
                    else
                        ILRFile.DocumentElement.InsertBefore(NewNode, LearnerDestinationandProgressionList.First().Node);
                    break;
                case "LearnerDestinationandProgression":
                    ILRFile.DocumentElement.AppendChild(NewNode);
                    break;
            }
        }
        private string GetNextLearnRefNumber()
        {
            XmlNodeList learnerNodes = ILRFile.SelectNodes("/ia:Message/ia:Learner", NSMgr);

            int learnRefNumber = learnerNodes.Count + 1;

            while (this.LearnRefNumberExists(learnRefNumber.ToString("0000")))
                learnRefNumber++;

            return learnRefNumber.ToString("0000");

        }
        #endregion

        #region Constructor
        public Message(string Filename)
        {
            if (!File.Exists(Filename))
            {
                ILRFile = new XmlDocument();
                ILRFile.LoadXml("<Message xmlns=\"" + CurrentNameSpace + "\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" />");
                ILRFile.Save(Filename);
            }
            IsFileImportLoadingRunning = true;
            Load(Filename);
            FixLDAPULN();
            IsFileImportLoadingRunning = false;
        }
        protected Message()
        {
        }
        #endregion
        #region INotifyPropertyChanged Members
        /// <summary>
        /// INotifyPropertyChanged requires a property called PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the event for the property when it changes.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
#if DEBUG     
            VerifyPropertyName(propertyName);
#endif
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        protected bool ThrowOnInvalidPropertyName { get; set; }
        #endregion
    }
}
