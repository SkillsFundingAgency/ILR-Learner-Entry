using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Reflection;
using System.IO;

namespace ILR
{
    public class Lookup
    {
        private XmlDocument lookupsXML;

        public string GetDescription(string Item, string Code)
        {
            string result = null;
            if (Code.Length >0)
            {
                XmlNode item = lookupsXML.SelectSingleNode("lookups/simple/" + Item + "/option[@code=" + Code + "]");
                if (item != null)
                    result = item.Attributes["description"].Value;
            }
            return result;
        }

        public Lookup()
        {
            lookupsXML = new XmlDocument();

            Assembly assembly = Assembly.GetExecutingAssembly();

            string lookupXMLResourceName = assembly.GetManifestResourceNames().Where(x => x.EndsWith("Lookups.xml")).FirstOrDefault();

            lookupsXML.LoadXml(GetResourceXML(lookupXMLResourceName));
        }

        private string GetResourceXML(string ResourceName)
        {
            string result = "";
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(ResourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

        public DataTable GetLookup(string Item)
        {
            using (DataTable result = new DataTable())
            {
                result.Columns.Add(new DataColumn("Code"));
                result.Columns.Add(new DataColumn("Description"));

                result.Columns[0].DataType = typeof(string);
                result.Columns[1].DataType = typeof(string);

                DataRow row = result.NewRow();
                row["Code"] = "";
                row["Description"] = "";
                result.Rows.Add(row);

                XmlNode item = lookupsXML.SelectSingleNode("lookups/simple/" + Item);

                foreach (XmlNode option in item.ChildNodes)
                {
                    row = result.NewRow();
                    row["Code"] = option.Attributes["code"].Value;
                    row["Description"] = option.Attributes["code"].Value + " - " + option.Attributes["description"].Value;
                    result.Rows.Add(row);
                }

                return result;
            }
        }
        public DataTable GetLookup(string Item, string Type)
        {

            using (DataTable result = new DataTable())
            {
                result.Columns.Add(new DataColumn("Code"));
                result.Columns.Add(new DataColumn("Description"));

                result.Columns[0].DataType = typeof(string);
                result.Columns[1].DataType = typeof(string);

                DataRow row = result.NewRow();
                row["Code"] = null;
                row["Description"] = "";
                result.Rows.Add(row);

                XmlNode item = lookupsXML.SelectSingleNode("lookups/typed/" + Item + "/" + Type);

                foreach (XmlNode option in item.ChildNodes)
                {
                    row = result.NewRow();
                    row["Code"] = option.Attributes["code"].Value;
                    row["Description"] = option.Attributes["code"].Value + " - " + option.Attributes["description"].Value;
                    result.Rows.Add(row);
                }

                return result;
            }
        }
    }
}
