using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCom.CodingChallenge.Domain.Model
{
    public partial class Address
    {
        [Key]
        public int Id { get; set; }
        public int NameId { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zip { get; set; } = null!;

        public virtual Name Name { get; set; } = null!;
    }
}
