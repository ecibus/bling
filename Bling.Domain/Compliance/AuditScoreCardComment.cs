﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.Compliance
{
    public class AuditScoreCardComment
    {
        public virtual int Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string FileId { get; set; }
        public virtual int ItemId { get; set; }
        public virtual string Comment { get; set; } 
    }
}
