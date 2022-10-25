using eCom.CodingChallenge.Business.Mappers;
using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;
using FizzWare.NBuilder;
using eCom.TestUtilities;
using NUnit.Framework;

namespace eCom.UnitTests.Mappers
{
    public class PhoneMapperTests
    {
        [Test]
        public void PhoneDtoListMapperTest()
        {
            //setup
            var phones = Builder<Phone>.CreateListOfSize(1)
                .All()
                .With(x => x.PhoneType = Builder<PhoneType>.CreateNew().Build())
               .Build();

            var expected = Builder<PhoneDto>.CreateListOfSize(1)
                .All()
                .With(x => x.Type = "Type1")
                .Build();

            //run
            var actual = phones.MapDtos();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }

        [Test]
        public void PhoneTypeDtotMapperTest()
        {
            //setup
            var phoneType = Builder<PhoneType>.CreateNew()
               .Build();

            var expected = Builder<PhoneTypeDto>.CreateNew()
                .Build();

            //run
            var actual = phoneType.MapToDto();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }

        [Test]
        public void PhoneRequestDtoListMapperTest()
        {
            //setup
            var phoneRequestDto = Builder<PhoneRequestDto>.CreateListOfSize(1)
               .Build();

            var expected = Builder<Phone>.CreateListOfSize(1)
                .All()
                .With(x=>x.Id = 0)
                .With(x=>x.NameId = 0)
                .Build();

            //run
            var actual = phoneRequestDto.MapDbos();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }

        [Test]
        public void HomePhoneDtoMapperTest()
        {
            //setup
            var phone = Builder<Phone>.CreateNew()
               .Build();

            var expected = Builder<HomePhoneDto>.CreateNew()
                .Build();

            //run
            var actual = phone.MapDtos();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }
    }
}
