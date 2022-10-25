using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;

namespace eCom.CodingChallenge.Business.Mappers
{
    public static class CallListMapper
    {
        public static List<CallListDto> MapCallListToDtos(this IEnumerable<Name> names)
        {
            var ContactsDtos = new List<CallListDto>();

            foreach (var name in names)
            {
                ContactsDtos.Add(new CallListDto
                {
                    Name = new NameDto()
                    {
                        First = name.First,
                        Middle = name.Middle,
                        Last = name.Last,
                        Email = name.Email
                    },
                    Phone = name.Phones.First().MapDtos()
                });
            }

            return ContactsDtos;
        }
    }
}
