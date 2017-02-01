using System;

namespace Bling.Domain.Secondary
{
    public class LoanSolutionProgram
    {
        public virtual int Id { get; set; }                
        public virtual string InvestorName { get; set; }
        public virtual string InvestorProductName { get; set; }
        public virtual string InvestorProductCodeAlias { get; set; }

        public LoanSolutionProgram()
        {
        }

        public LoanSolutionProgram(string csv)        
        {
            string [] data = new string [3];

            data = csv.Split(',');

            InvestorName = data[0];
            InvestorProductName = data[1];
            InvestorProductCodeAlias = data.Length == 3 ? data[2] : "";

        }
    }
}
