using System;
using System.Collections.Generic;

namespace eCom.CodingChallenge.Domain.Model
{
    public partial class PhoneType
    {
        public PhoneType()
        {
            Phones = new HashSet<Phone>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Phone> Phones { get; set; }
    }
}
