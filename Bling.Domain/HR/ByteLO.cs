using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.HR
{
    public class ByteLO
    {
        public virtual string UserNameBranch { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string BranchName { get; set; }
        public virtual string BranchId { get; set; }

        public virtual string FullName
        {
            get { return String.Format("{0} {1}", FirstName.Capitalize(), LastName.Capitalize()); }
        }

    }
}
