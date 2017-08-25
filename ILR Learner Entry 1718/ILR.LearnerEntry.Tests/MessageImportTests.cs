using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ILR.LearnerEntry.Tests
{
    [TestFixture]
    public class MessageImportTests
    {

        private const string ILRFileName = "internalIlr1718.ilr";
        private const string ILRFileToImport = "ILR-1234567-1617-01.xml";
        [OneTimeSetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test01_Import_WhenFileContainsStdCode_OutputFile_ShouldHaveCode()
        {
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");
            var learner = ilrMessage.LearnerList[0];
            Assert.NotNull(learner.LearningDeliveryList[0].StdCode, "Apprenticeship standard code is failed to import, import failed");

           // Assert.True(File.Exists(fileName), "Failed to create internal file");


        }
        [Test]
        public void Test01_Import_WhenFileContainsTrailBlazerFinRecord_Learner_ShouldHaveFinRecords()
        {
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");
            var learner = ilrMessage.LearnerList[0];
            Assert.NotNull(learner.LearningDeliveryList[0].ApprenticeshipFinancialRecordList, "Apprenticeship Financial record is failed to import, import failed");
            var finRecord = learner.LearningDeliveryList[0].ApprenticeshipFinancialRecordList[0];
            Assert.NotNull(finRecord.AFinAmount, "Apprenticeship finance amount is missing");
            Assert.AreEqual(finRecord.AFinAmount, 16500, "Apprenticeship finance amount is wrong");
            // Assert.True(File.Exists(fileName), "Failed to create internal file");
        }
        [Test]
        public void Test01_Import_WhenFileContainsTrailBlazerFinRecord_OutputFile_ShouldHaveFinRecords()
        {
           
            XNamespace ns = "SFA/ILR/2017-18";
            XNamespace nsa = "http://schemas.datacontract.org/2004/07/My.Namespace";

            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");
            XDocument xDoc = XDocument.Load(fileName);
            var query = from t in xDoc.Descendants(ns + "AFinAmount")
                        select t.Value;
            var val = query.First();
            Assert.NotNull(val, "AppFinRecords are not created");
            Assert.AreEqual(val, "16500", "ApprenticeShip finance amount is wrong");
            // Assert.True(File.Exists(fileName), "Failed to create internal file");
        }

        [Test]
        public void Test01_Export_WhenFileValidatedWithSchema_ShouldHaveNoErrors()
        {

            XNamespace ns = "SFA/ILR/2017-18";
            XNamespace nsa = "http://schemas.datacontract.org/2004/07/My.Namespace";

            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");

            string exportFileFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Exported");
            if (!Directory.Exists(exportFileFolder))
                Directory.CreateDirectory(exportFileFolder);
            else
            {
                Directory.Delete(exportFileFolder, true);
                Directory.CreateDirectory(exportFileFolder);
            }
            string xsdFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ILR-2017-18.xsd");


            
            XDocument xDoc = XDocument.Load(fileName);
            ilrMessage.Export(exportFileFolder, "01");

            string exportFile = Directory.GetFiles(exportFileFolder, "*.xml").FirstOrDefault();
            bool validFile =IsValidXml(exportFile, xsdFilePath, ns.NamespaceName);

             Assert.True(validFile, "Exported file failed to validate against the ILR schema!");
        }







        #region helper functions
        public static bool IsValidXml(string xmlFilePath, string xsdFilePath, string namespaceName)
        {
            var xdoc = XDocument.Load(xmlFilePath);
            var schemas = new XmlSchemaSet();
            schemas.Add(namespaceName, xsdFilePath);

            try
            {
                xdoc.Validate(schemas, null);
            }
            catch (XmlSchemaValidationException)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
