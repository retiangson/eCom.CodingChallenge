using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCom.CodingChallenge.Domain.Model
{
    public partial class Name
    {
        public Name()
        {
            Phones = new HashSet<Phone>();
        }
        [Key]
        public int Id { get; set; }
        public string First { get; set; } = null!;
        public string Middle { get; set; } = null!;
        public string Last { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual Address? Address { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }
}
