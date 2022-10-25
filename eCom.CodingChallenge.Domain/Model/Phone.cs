using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCom.CodingChallenge.Domain.Model
{
    public partial class Phone
    {
        [Key]
        public int Id { get; set; }
        public int NameId { get; set; }
        public int PhoneTypeId { get; set; }
        public string Number { get; set; } = null!;

        public virtual Name Name { get; set; } = null!;
        public virtual PhoneType PhoneType { get; set; } = null!;
    }
}
