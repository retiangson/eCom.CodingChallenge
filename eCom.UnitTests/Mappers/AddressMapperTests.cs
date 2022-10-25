using eCom.CodingChallenge.Business.Mappers;
using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;
using eCom.TestUtilities;
using FizzWare.NBuilder;
using NUnit.Framework;


namespace eCom.UnitTests.Mappers
{
    public class AddressMapperTests
    {
        [Test]
        public void AddressDtoMapperTest()
        {
            //setup
            var address = Builder<Address>.CreateNew()
                .Build();

            var expected = Builder<AddressDto>.CreateNew()
                .Build();

            //run
            var actual = address.MapToDto();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }

        [Test]
        public void AddressMapperTest()
        {
            //setup
            var addressDto = Builder<AddressDto>.CreateNew()
              .Build();

            var expected = Builder<Address>.CreateNew()
                .With(x=> x.Id =0)
                .With(x=> x.NameId =0)
                .Build();

            //run
            var actual = addressDto.MapToDbo();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }
        
    }
}
