using System;

namespace Bling.Domain.HR
{
    public class CensusDateRange
    {
        public virtual int Id { get; set; }
        public virtual DateTime From { get; set; }
        public virtual DateTime To { get; set; }
    }
}
