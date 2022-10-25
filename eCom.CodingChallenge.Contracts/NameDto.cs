using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.CodingChallenge.Contracts
{
    public class NameDto
    {
        public long Id { get; set; }
        public string? First { get; set; }
        public string? Middle { get; set; }
        public string? Last { get; set; }
        public string? Email { get; set; }
    }

    public class NameRequestDto
    {
        public string? First { get; set; }
        public string? Middle { get; set; }
        public string? Last { get; set; }
        public string? Email { get; set; }
    }
}
