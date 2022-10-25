using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.CodingChallenge.Contracts
{
    public class ContactsDto
    {
        public NameDto Name { get; set; }
        public AddressDto Address { get; set; }
        public ICollection<PhoneDto> Phone { get; set; }
    }

    public class ContactsRequestDto
    {
        public NameRequestDto Name { get; set; }
        public AddressDto Address { get; set; }
        public ICollection<PhoneRequestDto>? Phone { get; set; }
    }
}
