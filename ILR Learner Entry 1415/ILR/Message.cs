using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Data;

namespace ILR
{
	public class Message
	{
		#region Counts
		public DataTable Statistics
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
					}

					return results;
				}
			}
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
			NSMgr.AddNamespace("ia", "http://www.theia.org.uk/ILR/2014-15/1");

			//Load the data
			Load(Filename, ILRFile, NSMgr);


		}
		protected void Load1314(string Filename)
		{
			//Instantiate a new document to hold the ILR data
			this.ILRFile = new XmlDocument();

			//Get a namespace manager from the XML document
			NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
			NSMgr.AddNamespace("ia", "http://www.theia.org.uk/ILR/2013-14/1");

			//Load the data
			Load(Filename, ILRFile, NSMgr);

		}
		private void Load(string Filename, XmlDocument Document, XmlNamespaceManager NSMgr)
		{
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
				newInstance.Message = this;
				newInstance.ResequenceAimSeqNumber();
				LearnerList.Add(newInstance);
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
		public void Export(string ExportFolder)
		{
			//Calculate filename
			string filename = ExportFolder;
			if (!filename.EndsWith("\\"))
				filename += "\\";
			filename += "ILR-A-" + LearningProvider.UKPRN.ToString() + "-1415-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-01.xml";

			//Update header information
			this.Header.CollectionDetails.FilePreparationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			this.Header.Source.UKPRN = LearningProvider.UKPRN;
			this.Header.Source.SerialNo = "01";
			this.Header.Source.DateTime = DateTime.Now;

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
			NSMgr.AddNamespace("ia", "http://www.theia.org.uk/ILR/2014-15/1");

			this.ILRFile.AppendChild(this.ILRFile.CreateElement("Message", NSMgr.LookupNamespace("ia")));

			// Clear Old Learner and Old LearnerDestinationandProgression Records
			this.LearnerList.Clear();
			this.LearnerDestinationandProgressionList.Clear();

			Message importMessage = new Message();

			System.IO.FileInfo fi = new System.IO.FileInfo(FilenameToLoad);
			if (fi.Name.Contains("-1415-"))
			{
				this.Load(FilenameToLoad);
				//importMessage.Load(Filename);
			}
			else if (fi.Name.Contains("-1314-"))
			{
				importMessage.Load1314(FilenameToLoad);


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

				foreach (Learner learner in importMessage.LearnerList)
				{
					if (learner.HasContinuingAims)
					{
						XmlNode newNode = ILRFile.CreateElement("Learner", NSMgr.LookupNamespace("ia"));
						Learner newInstance = new Learner(learner, newNode, NSMgr);
						newInstance.Message = this;
						LearnerList.Add(newInstance);
						AppendToLastOfNodeNamed(newNode, newNode.Name);
						newInstance.ResequenceAimSeqNumber();
					}
				}
			}
			else
			{
				throw (new Exception("Unable to identify Year from filename. Confirm the file name matchesd the ILR Specification."));
			}
			this.Filename = InterlStoreFilename;
			Save();
		}

		//public void Import1415(string Filename)
		//{
		//	//Instantiate a new document to hold the ILR data
		//	this.ILRFile = new XmlDocument();

		//	//Get a namespace manager from the XML document
		//	NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
		//	NSMgr.AddNamespace("ia", "http://www.theia.org.uk/ILR/2014-15/1");

		//	this.ILRFile.AppendChild(this.ILRFile.CreateElement("Message", NSMgr.LookupNamespace("ia")));
		//	this.LearnerList.Clear();

		//	Message importMessage = new Message();
		//	importMessage.Load(Filename);

		//	LearningProvider.UKPRN = importMessage.LearningProvider.UKPRN;

		//	foreach (Learner learner in importMessage.LearnerList)
		//	{
		//		if (learner.HasContinuingAims)
		//		{
		//			XmlNode newNode = ILRFile.CreateElement("Learner", NSMgr.LookupNamespace("ia"));
		//			Learner newInstance = new Learner(learner, newNode, NSMgr);
		//			newInstance.Message = this;
		//			LearnerList.Add(newInstance);
		//			AppendToLastOfNodeNamed(newNode, newNode.Name);
		//		}
		//	}

		//	Save();
		//}

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
				ILRFile.LoadXml("<Message xmlns=\"http://www.theia.org.uk/ILR/2014-15/1\" xmlns:schemaLocation=\"http://www.theia.org.uk/ILR/2011-12/1 ILR-2014-15-Structure.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" />");
				ILRFile.Save(Filename);
			}
			Load(Filename);
		}
		protected Message()
		{
		}
		#endregion

	}
}
