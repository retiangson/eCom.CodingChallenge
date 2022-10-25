using eCom.CodingChallenge.Business.Mappers;
using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;
using eCom.TestUtilities;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace eCom.UnitTests.Mappers
{
    public class CallListMapperTests
    {
        [Test]
        public void MapCallListToDtosMapperTest()
        {
            //setup
            var names = Builder<Name>.CreateListOfSize(2)
                .All()
                .With(x => x.Phones = Builder<Phone>.CreateListOfSize(1).Build())
              .Build();

            var expected = Builder<CallListDto>.CreateListOfSize(2)
                .TheFirst(1)
                .With(x => x.Name = Builder<NameDto>.CreateNew().With(x => x.Id = 0).Build())
                .TheLast(1)
                .With(x => x.Name = Builder<NameDto>.CreateNew()
                    .With(x => x.Id = 0)
                    .With(x=>x.First = "First2")
                    .With(x=>x.Middle = "Middle2")
                    .With(x=>x.Last = "Last2")
                    .With(x=>x.Email = "Email2")
                    .Build())
                .All()
                .With(x => x.Phone = Builder<HomePhoneDto>.CreateNew().Build())
                .Build();

            //run
            var actual = names.MapCallListToDtos();

            //assert
            AssertExtensions.AreEqualByJson(expected, actual);
        }
    }
}
