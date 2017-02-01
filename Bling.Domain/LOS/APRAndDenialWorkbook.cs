using System;
using System.Collections.Generic;
using System.IO;

namespace Bling.Domain.LOS
{
    public class APRAndDenialWorkbook
    {
        public void Generate(string path, string filename, List<HMDA> hmdas)
        {
            var excel = new Excel.ApplicationClass();
            try
            {
                string targetFile = String.Format("{0}\\{1}", path, filename);

                File.Copy(String.Format("{0}\\Template.xls", path), targetFile);

                var workbook = excel.Workbooks.Open(targetFile, 0, false, 5, "", "", true, 
                    Excel.XlPlatform.xlWindows, "\t", false, false, 0, false);

                var sheet = (Excel.Worksheet)workbook.Sheets["Data"];

                int count = hmdas.Count;

                var data = new string[count, 5];
                                
                int i = 0;
                hmdas.ForEach(hmda =>
                {
                    data[i, 0] = hmda.LoanNumber;
                    data[i, 1] = hmda.ActionDate;
                    data[i, 2] = hmda.APRDenialRace;
                    data[i, 3] = hmda.ActionType;
                    data[i, 4] = hmda.APRFromTIL;
                    i++;
                });

                sheet.get_Range("A3", String.Format("E{0}", 2 + count)).Value = data;

                sheet = (Excel.Worksheet)workbook.Sheets["Summary Table"];
                ((Excel._Worksheet)sheet).Activate();
                workbook.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                excel.Workbooks.Close();
            }
        }
    }
}
