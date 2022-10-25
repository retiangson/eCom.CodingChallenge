using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;

namespace eCom.CodingChallenge.Business.Mappers
{
    public static class AddressMapper
    {
        public static AddressDto MapToDto(this Address address)
        {
            return new AddressDto()
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                Zip = address.Zip
            };
        }

        public static Address MapToDbo(this AddressDto address)
        {
            return new Address()
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                Zip = address.Zip
            };
        }
    }
}
