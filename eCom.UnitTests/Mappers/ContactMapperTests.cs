using eCom.CodingChallenge.Business.Mappers;
using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;
using FizzWare.NBuilder;
using eCom.TestUtilities;
using NUnit.Framework;

namespace eCom.UnitTests.Mappers
{
    public class ContactMapperTests
    {

        [Test]
        public void ContactsDtosMapperTest()
        {
            //setup
            var names = Builder<Name>.CreateListOfSize(1)
                .All()
                .With(x => x.Address = Builder<Address>.CreateNew().Build())
                .With(x => x.Phones = Builder<Phone>.CreateListOfSize(1)
                   .All().With(x => x.PhoneType = Builder<PhoneType>.CreateNew().Build())
                   .Build())
               .Build();

            var expected = Builder<ContactsDto>.CreateListOfSize(1)
                .All()
                .With(x => x.Name = Builder<NameDto>.CreateNew().Build())
                .With(x => x.Address = Builder<AddressDto>.CreateNew().Build())
                .With(x => x.Phone = Builder<PhoneDto>.CreateListOfSize(1).Build())
                .Build();

            //run
            var actual = names.MapToDtos();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }

        [Test]
        public void ContactsDtoMapperTest()
        {
            //setup
            var names = Builder<Name>.CreateNew()
               .With(x => x.Address = Builder<Address>.CreateNew().Build())
               .With(x => x.Phones = Builder<Phone>.CreateListOfSize(1)
                   .All().With(x => x.PhoneType = Builder<PhoneType>.CreateNew().Build())
                   .Build())
              .Build();

            var expected = Builder<ContactsDto>.CreateNew()
                .With(x => x.Name = Builder<NameDto>.CreateNew().With(x => x.Id = 0).Build())
                .With(x => x.Address = Builder<AddressDto>.CreateNew().Build())
                .With(x => x.Phone = Builder<PhoneDto>.CreateListOfSize(1).Build())
                .Build();

            //run
            var actual = names.MapToDtos();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }

        [Test]
        public void ContactsRequestDtoMapperTest()
        {
            //setup
            var contactsRequestDto = Builder<ContactsRequestDto>.CreateNew()
                .With(x => x.Name = Builder<NameRequestDto>.CreateNew().Build())
                .With(x => x.Address = Builder<AddressDto>.CreateNew().Build())
                .With(x => x.Phone = Builder<PhoneRequestDto>.CreateListOfSize(1).Build())
                .Build();

            var expected = Builder<Name>.CreateNew()
                .With(x => x.Id = 0)
                .With(x => x.Address = Builder<Address>.CreateNew()
                    .With(x => x.Id = 0)
                    .With(x => x.NameId = 0)
                    .Build())
                .With(x => x.Phones = Builder<Phone>.CreateListOfSize(1)
                    .All()
                    .With(x => x.Id = 0)
                    .With(x => x.NameId = 0)
                    .Build())
                .Build();

            //run
            var actual = contactsRequestDto.MapToDbos();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }
    }
}
