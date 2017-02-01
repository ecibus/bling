using System;

namespace Bling.Domain.Underwriting
{
    public class ScoreCard
    {
        public virtual int Id { get; set; }
        public virtual string FileId { get; set; }
        public virtual int ScoreId { get; set; }
        public virtual double Score { get; set; }
        public virtual string CreatedBy { get; set; }
        
    }
}
