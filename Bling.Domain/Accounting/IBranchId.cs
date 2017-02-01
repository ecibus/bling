using System;

namespace Bling.Domain.Accounting
{
    public interface IBranchId
    {
        string BranchId { get; set; }
        string CreatedBy { get; set; }
    }
}
