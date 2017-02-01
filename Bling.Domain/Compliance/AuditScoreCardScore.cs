using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class AuditScoreCardScore
    {
        public virtual int Id { get; set; }
        public virtual string FileId { get; set; }
        public virtual int ScoreId { get; set; }
        public virtual double Score { get; set; }
        public virtual string CreatedBy { get; set; }
    }
}
