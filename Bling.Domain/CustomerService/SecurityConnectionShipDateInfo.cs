using System;
using log4net;

namespace Bling.Domain.CustomerService
{
    public class SecurityConnectionShipDateInfo
    {
        private string m_DocumentType;
        protected static ILog m_logger = LogManager.GetLogger(typeof(SecurityConnectionShipDateInfo));

        public string LoanNumber { get; set; }        
        public string ShippedDate { get; set; }

        public SecurityConnectionShipDateInfo(string tabDelimitedData)
        {
            string[] data = tabDelimitedData.Split('\t');

            LoanNumber = data[1];
            DocumentType = data[11];
            ShippedDate = data[13];

            //LoanNumber = data[0];
            //DocumentType = data[3];
            //ShippedDate = data[4];
        }

        public string DocumentType
        {
            get { return m_DocumentType; }
            //set { m_DocumentType = value.ToLower() == "mort" ? "Deed of Trust" : value; }
            set 
            {
                m_DocumentType = "";
                switch (value.ToLower())
                {
                    case "recorded mortgage":
                        m_DocumentType = "DEED OF TRUST";
                        break;

                    case "titlepolicy":
                        m_DocumentType = "TITLE POLICY";
                        break;

                    default:
                        break;

                }
                //m_DocumentType = value.ToLower() == "recorded mortgage" ? "Deed of Trust" : value; 
            }
        }        

        public override string ToString()
        {            
            return String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", LoanNumber, DocumentType, ShippedDate);
        }
    }
}
