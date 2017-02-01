using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain
{
    public class Appraiser
    {
        public virtual string Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Company { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime LicenseExpirationDate { get; set; }
        public virtual string Region { get; set; }
        public virtual string ApprovedLoanTypes { get; set; }
        public virtual string OtherCounty { get; set; }
        public virtual string EMail { get; set; }
        public virtual bool Exclude { get; set; }
        public virtual Address Address { get; set; }
        public virtual PhoneNumber Phone { get; set; }
        public virtual PhoneNumber Fax { get; set; }

        public virtual bool ApprovedLoanTypesContains(string loantype)
        {
            if (ApprovedLoanTypes.ToLower().Contains(loantype.ToLower()))
                return true;

            return false;
        }

        public static string ToHTMLDropDown(IList<Appraiser> appraisers, string lastAppraiser, string loantype)
        {
            List<Appraiser> list = appraisers.ToList();
            
            if (list.Count(x => x.ApprovedLoanTypesContains(loantype)) == 0)
                return "No Available Appraiser";

            bool nextIsSelected = false;
            string nextAppraiser = "";

            foreach (Appraiser a in appraisers)
            {
                if (a.Id == lastAppraiser)
                {
                    nextIsSelected = true;
                    continue;
                }

                if (nextIsSelected && a.ApprovedLoanTypesContains(loantype))
                {
                    nextAppraiser = a.Id;
                    break;
                }
            }

            StringBuilder dropdown = new StringBuilder();
            dropdown.Append("<select id='ddAppraiser'>");

            list.Where(x => x.ApprovedLoanTypesContains(loantype)).ToList()
                .ForEach(a => dropdown.AppendFormat("<option value=\"{0}\" {3}>{1} {2} ({4})</option>",
                a.Id, a.FirstName, a.LastName, a.Id == nextAppraiser ? "selected=\"selected\"" : "", a.Status));
            
            dropdown.Append("</select>");
            return dropdown.ToString();
        }
    }
}
