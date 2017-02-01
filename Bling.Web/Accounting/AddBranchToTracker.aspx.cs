using System;
using Bling.Domain.Accounting;

namespace Bling.Web.Accounting
{
    public partial class AddBranchToTracker : AddBranch<TrackerBranch> 
    {
        public override bool SortByBranchName
        {
            get { return BranchName.Checked; }
        }        
    }
}
