using System;

namespace Bling.Domain
{
    public class Address
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }

        public virtual string Add1
        {
            get { return Street; }
        }

        public virtual string Add2
        {
            get { return String.Format("{0}, {1} {2}", City, State, Zip.Length == 6 ? Zip.Substring(0, 5) : Zip); }
        }
    }
}
