using System;

namespace Bling.Domain.Underwriting
{
    public class ScoreCardComment
    {
        public virtual int Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string FileId { get; set; }
        public virtual int GroupId { get; set; }
        public virtual string Comment { get; set; }        
    }
}
