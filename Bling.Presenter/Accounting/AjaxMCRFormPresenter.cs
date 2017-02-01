using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using System.IO;
using Bling.Domain.Extension;
using Bling.Domain;

namespace Bling.Presenter.Accounting
{
    public class AjaxMCRFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private IMCRDao m_Dao;
        private string m_Path;
        private string m_Year;
        private string m_Quarter;
        private StringBuilder ErrorList;

        public AjaxMCRFormPresenter(IAjaxView view)
            : this(view, new MCRDao(DMDDataSession()))
        {
        }

        public AjaxMCRFormPresenter(IAjaxView view, IMCRDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GetEndingMCR(string year, string quarter)
        {
            if (quarter == "1")
            {
                quarter = "4";
                year = (Convert.ToInt32(year) - 1).ToString();
            }
            else
            {
                quarter = (Convert.ToInt32(quarter) - 1).ToString();
            }

            var mcr = m_Dao.GetLastMCREnding(year, quarter);

            m_View.ResponseText = String.Format(" {{ " +
                " \"Amount\" : \"{0}\", " +
                " \"Count\" : \"{1}\", " +
                " \"Average\" : \"{2}\"" +
                "}}",
                mcr == null ? 0 : mcr.Amount,
                mcr == null ? 0 : mcr.Count,
                mcr == null ? 0 : mcr.Average
                );
        }

        public void ValidateXML(string path, string schemaFile)
        {
            try
            {
                XMLValidator validator = new XMLValidator(path);
                validator.ValidateWithSchema(schemaFile);
                var errors = validator.Message;
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void GenerateXML(string path, string year, string quarter)
        {
            try
            {

                m_Path = path;
                m_Year = year;
                m_Quarter = quarter;

                //AZ, CA, HI, OR, and WA

                StringBuilder html = new StringBuilder();
                html.Append("<ul>");
                html.AppendFormat("<li>{0}<ul>{1}</ul></li>", GenerateXMLForState("Arizona", "AZ"), ErrorList);
                html.AppendFormat("<li>{0}<ul>{1}</ul></li>", GenerateXMLForState("California", "CA"), ErrorList);
                html.AppendFormat("<li>{0}<ul>{1}</ul></li>", GenerateXMLForState("Hawaii", "HI"), ErrorList);
                html.AppendFormat("<li>{0}<ul>{1}</ul></li>", GenerateXMLForState("Oregon", "OR"), ErrorList);
                html.AppendFormat("<li>{0}<ul>{1}</ul></li>", GenerateXMLForState("Washington", "WA"), ErrorList);
                html.AppendFormat("<li>{0}<ul>{1}</ul></li>", GenerateXMLForState("Utah", "UT"), ErrorList);
                html.Append("</ul>");

                m_View.ResponseText = html.ToString();

            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        private string GenerateXMLForState(string state, string abbr)
        {
            string targetFile = String.Format("MCR_RMLA_{0}_{1}_{2}.xml", m_Year, m_Quarter, abbr);


            using (TextWriter writer = File.CreateText(m_Path + "\\" + targetFile))
            {
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                writer.WriteLine("<Mcr type=\"E\" year=\"{0}\" periodType=\"MCRQ{1}\" formVersion=\"v3\" >", m_Year, m_Quarter);
                writer.WriteLine("   <Fc></Fc>");
                writer.WriteLine("   <Rmla stateCode=\"{0}\">", abbr);
                writer.WriteLine("<SectionISection>");
                writer.WriteLine(GetNode(abbr, 1));
                writer.WriteLine("</SectionISection>");
                /*
                writer.WriteLine("<ListSectionOfSectionILinesOfCreditItem>");
                writer.WriteLine("<DetailItemList>");
                writer.WriteLine(CreateLOCItem());
                writer.WriteLine("</DetailItemList>");
                writer.WriteLine("</ListSectionOfSectionILinesOfCreditItem>");
                */
                writer.WriteLine("<ListSectionOfSectionIMlosItem>");
                writer.WriteLine("<DetailItemList>");
                writer.WriteLine(CreateLOSItem(abbr));
                writer.WriteLine("</DetailItemList>");
                writer.WriteLine("</ListSectionOfSectionIMlosItem>");
                writer.WriteLine("<SectionIISection>");
                writer.WriteLine(GetNode(abbr, 2));
                writer.WriteLine("</SectionIISection>");
                writer.WriteLine("<SectionIIISection>");
                writer.WriteLine(GetNodeForSectionThree());
                writer.WriteLine("</SectionIIISection>");
                writer.WriteLine("   </Rmla>");
                writer.WriteLine("   <Rmlag>");
                writer.WriteLine("<ListSectionOfLinesOfCreditItem>");
                writer.WriteLine("<DetailItemList>");
                writer.WriteLine(CreateLOCItem());
                writer.WriteLine("</DetailItemList>");
                writer.WriteLine("</ListSectionOfLinesOfCreditItem>");
                writer.WriteLine("   </Rmlag>");
                writer.WriteLine("</Mcr>");
            }

            XMLValidator validator = new XMLValidator(m_Path + "\\" + targetFile);
            validator.ValidateWithSchema(m_Path + "\\" + "MCRUploadSchemaExpandedV2.xsd");
            var errors = validator.Message;
            ErrorList = new StringBuilder();
            errors.ForEach(x => ErrorList.AppendFormat("<li>{0}</li>", x));

            return String.Format("<a href='Report/{0}'>{1}</a>", targetFile, state);
        }

        
        private string GetNode(string state, int section)
        {
            StringBuilder node = new StringBuilder();            

            var rmla = section == 1 ? m_Dao.GetSectionOne(m_Year, m_Quarter, state)
                : m_Dao.GetSectionTwo(m_Year, m_Quarter, state)                ;

            foreach (var pair in rmla)
            {
                node.AppendFormat("<{0}>{1}</{0}>",
                pair.Key,
                pair.Value);
            }

            return node.ToString();
        }

        private string CreateLOSItem(string state)
        {
            StringBuilder node = new StringBuilder();

            var list = m_Dao.GetLOItem(m_Year, m_Quarter, state);

            foreach (var lo in list)
            {
                node.Append("<SectionIMlosItem>");
                //node.AppendFormat("<ItemId>{0}</ItemId>", lo.NMLSId);
                node.AppendFormat("<ACMLO>{0}</ACMLO>", lo.NMLSId);
                node.AppendFormat("<ACMLO_2>{0}</ACMLO_2>", lo.LoanAmount);
                node.AppendFormat("<ACMLO_3>{0}</ACMLO_3>", lo.NoOfLoans);

                node.Append("</SectionIMlosItem>");

            }

            return node.ToString();
        }

        private string CreateLOCItem()
        {
            StringBuilder node = new StringBuilder();

            node.Append("<LinesOfCreditItem>");
            node.AppendFormat("<LOC>{0}</LOC>", "Bank of America Home Loans");
            node.AppendFormat("<LOC_1>{0}</LOC_1>", "30000000");
            node.AppendFormat("<LOC_2>{0}</LOC_2>", "0");
            node.Append("</LinesOfCreditItem>");

            node.Append("<LinesOfCreditItem>");
            node.AppendFormat("<LOC>{0}</LOC>", "Ally Bank");
            node.AppendFormat("<LOC_1>{0}</LOC_1>", "20000000");
            node.AppendFormat("<LOC_2>{0}</LOC_2>", "0");
            node.Append("</LinesOfCreditItem>");

            node.Append("<LinesOfCreditItem>");
            node.AppendFormat("<LOC>{0}</LOC>", "ViewPoint Bank");
            node.AppendFormat("<LOC_1>{0}</LOC_1>", "25000000");
            node.AppendFormat("<LOC_2>{0}</LOC_2>", "0");
            node.Append("</LinesOfCreditItem>");

            
            return node.ToString();
        }

        private string GetNodeForSectionThree()
        {
            StringBuilder node = new StringBuilder();
            node.Append("<S100_1>0</S100_1>");
			node.Append("<S100_2>0</S100_2>");
			node.Append("<S110_1>0</S110_1>");
			node.Append("<S110_2>0</S110_2>");
			node.Append("<S120_1>0</S120_1>");
			node.Append("<S120_2>0</S120_2>");
			node.Append("<S130_1>0</S130_1>");
			node.Append("<S130_2>0</S130_2>");
			node.Append("<S140_1>0</S140_1>");
			node.Append("<S140_2>0</S140_2>");
			node.Append("<S150_1>0</S150_1>");
			node.Append("<S150_2>0</S150_2>");
			node.Append("<S160_1>0</S160_1>");
			node.Append("<S160_2>0</S160_2>");
			node.Append("<S200_1>0</S200_1>");
			node.Append("<S200_2>0</S200_2>");
			node.Append("<S210_1>0</S210_1>");
			node.Append("<S210_2>0</S210_2>");
			node.Append("<S220_1>0</S220_1>");
			node.Append("<S220_2>0</S220_2>");
			node.Append("<S230_1>0</S230_1>");
			node.Append("<S230_2>0</S230_2>");
			node.Append("<S240_1>0</S240_1>");
			node.Append("<S240_2>0</S240_2>");
			node.Append("<S300_1>0</S300_1>");
			node.Append("<S300_2>0</S300_2>");
			node.Append("<S305_1>0</S305_1>");
			node.Append("<S305_2>0</S305_2>");
			node.Append("<S310_1>0</S310_1>");
			node.Append("<S310_2>0</S310_2>");
			node.Append("<S315_1>0</S315_1>");
			node.Append("<S315_2>0</S315_2>");
			node.Append("<S320_1>0</S320_1>");
			node.Append("<S320_2>0</S320_2>");
			node.Append("<S325_1>0</S325_1>");
			node.Append("<S325_2>0</S325_2>");
			node.Append("<S330_1>0</S330_1>");
			node.Append("<S330_2>0</S330_2>");
			node.Append("<S335_1>0</S335_1>");
			node.Append("<S335_2>0</S335_2>");
			node.Append("<S340_1>0</S340_1>");
			node.Append("<S340_2>0</S340_2>");
			node.Append("<S345_1>0</S345_1>");
			node.Append("<S345_2>0</S345_2>");
			node.Append("<S350_1>0</S350_1>");
			node.Append("<S350_2>0</S350_2>");
			node.Append("<S355_1>0</S355_1>");
			node.Append("<S355_2>0</S355_2>");
			node.Append("<S400_1>0</S400_1>");
			node.Append("<S400_2>0</S400_2>");
			node.Append("<S410_1>0</S410_1>");
			node.Append("<S410_2>0</S410_2>");
			node.Append("<S420_1>0</S420_1>");
			node.Append("<S420_2>0</S420_2>");
			node.Append("<S430_1>0</S430_1>");
			node.Append("<S430_2>0</S430_2>");
			node.Append("<S440_1>0</S440_1>");
			node.Append("<S440_2>0</S440_2>");
			node.Append("<S450_1>0</S450_1>");
            node.Append("<S450_2>0</S450_2>");
            return node.ToString();
        }
    
    }
}
