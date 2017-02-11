using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bling.Domain.RestApi.TurningPoint
{
    public class ByteUser
    {
        public virtual int UserId { get; set; }
        public virtual string Fullname { get; set; }
        public virtual string Username { get; set; }
    }
}
