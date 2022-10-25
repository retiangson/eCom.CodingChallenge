using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.CodingChallenge.Contracts
{
    public class PhoneDto
    {
        public string Number { get; set; }
        public string? Type { get; set; }
    }

    public class PhoneRequestDto
    {
        public string Number { get; set; }
        public int PhoneTypeId { get; set; }
    }

    public class HomePhoneDto
    {
        public string Number { get; set; }
    }

}
