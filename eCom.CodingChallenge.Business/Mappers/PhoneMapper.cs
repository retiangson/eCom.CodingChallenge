using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;

namespace eCom.CodingChallenge.Business.Mappers
{
    public static class PhoneMapper
    {
        public static List<PhoneDto> MapDtos(this IEnumerable<Phone> phones)
        {
            var phonesDto = new List<PhoneDto>();

            foreach (var item in phones)
            {
                phonesDto.Add(new PhoneDto()
                {
                    Number = item.Number,
                    Type = item.PhoneType?.MapToDto().Type
                });
            }
            return phonesDto;
        }

        public static PhoneTypeDto MapToDto(this PhoneType phoneType)
        {
            return new PhoneTypeDto()
            {
                Type = phoneType.Type,
            };
        }

        public static List<Phone> MapDbos(this IEnumerable<PhoneRequestDto> phones, int nameId = 0)
        {
            var phonesDbo = new List<Phone>();

            foreach (var phone in phones)
            {
                phonesDbo.Add(new Phone()
                {
                    Number = phone.Number,
                    PhoneTypeId = phone.PhoneTypeId,
                    NameId = nameId
                });
            }
            return phonesDbo;
        }

        public static HomePhoneDto MapDtos(this Phone phones)
        {
            return new HomePhoneDto()
            {
                Number = phones.Number
            };
        }
    }
}
