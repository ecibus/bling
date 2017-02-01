using System;
using Bling.Domain.Accounting;

namespace Bling.Web.Accounting
{
    public partial class AddBranchToRankingReport : AddBranch<RankingBranch>
    {
        public override bool SortByBranchName
        {
            get { return BranchName.Checked; }
        }
    }
}
