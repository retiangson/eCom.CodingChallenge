using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;

namespace eCom.CodingChallenge.Business.Mappers
{
    public static class ContactMapper
    {
        public static List<ContactsDto> MapToDtos(this IEnumerable<Name> names)
        {
            var ContactsDtos = new List<ContactsDto>();

            foreach (var name in names)
            {
                ContactsDtos.Add(new ContactsDto
                {
                    Name = new NameDto()
                    {
                        Id = name.Id,
                        First = name.First,
                        Middle = name.Middle,
                        Last = name.Last,
                        Email = name.Email
                    },
                    Address = name.Address.MapToDto(),
                    Phone = name.Phones.MapDtos()
                });
            }

            return ContactsDtos;
        }

        public static ContactsDto MapToDtos(this Name name)
        {
            return new ContactsDto()
            {
                Name = new NameDto()
                {
                    First = name.First,
                    Middle = name.Middle,
                    Last = name.Last,
                    Email = name.Email
                },
                Address = name.Address.MapToDto(),
                Phone = name.Phones.MapDtos()
            };
        }

        public static Name MapToDbos(this ContactsRequestDto contact)
        {
            return new Name()
            {
                First = contact.Name.First,
                Middle = contact.Name.Middle,
                Last = contact.Name.Last,
                Email = contact.Name.Email,
                Address = contact.Address.MapToDbo(),
                Phones = contact.Phone?.MapDbos()
            };
        }


    }
}
