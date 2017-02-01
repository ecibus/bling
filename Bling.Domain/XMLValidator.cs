using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Bling.Domain
{
    public class XMLValidator
    {
        private string m_XMLFile;

        public List<string> Message { get; set; }

        public XMLValidator (string xmlFile)
        {
            m_XMLFile = xmlFile;
            Message = new List<string>();
        }

        public void ValidateWithSchema(string schemaFile)
        {
            try
            {
                FileStream fs = File.Open(m_XMLFile, FileMode.Open);

                XmlTextReader reader = new XmlTextReader(fs);
                XmlValidatingReader xvr = new XmlValidatingReader(reader);
                xvr.ValidationType = ValidationType.Schema;
                xvr.Schemas.Add(null, schemaFile);
                xvr.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xvr);
                reader.Close();
            }
            catch (Exception e)
            {
                Message.Add(e.Message);
            }
        }

        private void ValidationCallback(object sender, ValidationEventArgs args)
        {
            Message.Add(args.Message);
            //Console.WriteLine(args.Message);
            //Console.WriteLine(args.Exception.SourceSchemaObject.ToString());

        }
    }
}
