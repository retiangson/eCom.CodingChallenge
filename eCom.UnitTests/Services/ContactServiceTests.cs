using eCom.CodingChallenge.Business.Mappers;
using eCom.CodingChallenge.Business.Services;
using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;
using eCom.CodingChallenge.Domain.Repositories;
using eCom.TestUtilities;
using FizzWare.NBuilder;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCom.UnitTests.Services
{
    public class ContactServiceTests
    {
        private IContactService _service;
        private AutoMocker _mocker;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _service = _mocker.CreateInstance<ContactService>();
        }

        [Test]
        public async Task GetAsyncTest()
        {
            //setup
            var nameId = 1;

            var phones = Builder<Phone>.CreateListOfSize(3)
                .All()
                .And(x => x.NameId = nameId)
                .Build();

            var address = Builder<Address>.CreateNew()
                .With(x => x.NameId = nameId)
                .Build();

            var name = Builder<Name>.CreateListOfSize(1)
                .All()
                .With(x => x.Id = nameId)
                .With(x => x.Phones = phones)
                .With(x => x.Address = address)
                .Build() as IEnumerable<Name>;

            _mocker.GetMock<IContactsRepository>().Setup(x => x.GetAsync())
                .Returns(Task.FromResult(name));

            //run
            var actualResult = await _service.GetAsync();
            var expectedResult = name.MapToDtos();

            //assert
            Assert.IsNotNull(actualResult);
            AssertExtensions.AreEqualByJson(expectedResult, actualResult);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GeByIdAsyncTest(int nameId)
        {
            //setup
            var phones = Builder<Phone>.CreateListOfSize(3)
                .All()
                .And(x => x.NameId = nameId)
                .Build();

            var address = Builder<Address>.CreateNew()
                .With(x => x.NameId = nameId)
                .Build();

            var name = Builder<Name>.CreateNew()
                .With(x => x.Id = nameId)
                .With(x => x.Phones = phones)
                .With(x => x.Address = address)
                .Build();

            _mocker.GetMock<IContactsRepository>().Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(name));

            //run
            var actualResult = await _service.GetByIdAsync(nameId);
            var expectedResult = name.MapToDtos();

            //assert
            Assert.IsNotNull(actualResult);
            AssertExtensions.AreEqualByJson(expectedResult, actualResult);
        }

        [Test]
        public async Task GetCallListTest()
        {
            //setup
            var name = Builder<Name>.CreateListOfSize(3)
                .TheFirst(1)
                .With(x => x.Id = 1)
                .With(x => x.First = "Ronald")
                .With(x => x.Last = "Tiangson")
                .With(x => x.Phones = Builder<Phone>.CreateListOfSize(3)
                    .TheFirst(1).With(x => x.Id = 1).With(x => x.Number = "123").TheNext(1).With(x => x.Id = 2).TheLast(1).With(x => x.Id = 3)
                    .All()
                    .With(x => x.NameId = 1)
                    .Build())
                .With(x => x.Address = Builder<Address>.CreateNew()
                    .With(x => x.Id = 1)
                    .With(x => x.NameId = 1)
                    .Build())
                .TheNext(1)
                .With(x => x.Id = 2)
                .With(x => x.First = "Janice")
                .With(x => x.Last = "Go")
                .With(x => x.Phones = Builder<Phone>.CreateListOfSize(3)
                    .TheFirst(1).With(x => x.Id = 4).With(x => x.Number = "456").TheNext(1).With(x => x.Id = 5).TheLast(1).With(x => x.Id = 6)
                    .All()
                    .With(x => x.NameId = 2)
                    .Build())
                .With(x => x.Address = Builder<Address>.CreateNew()
                    .With(x => x.Id = 2)
                    .With(x => x.NameId = 2)
                    .Build())
                .TheLast(1)
                .With(x => x.Id = 3)
                .With(x => x.First = "Celestine")
                .With(x => x.Last = "Tiangson")
                .With(x => x.Phones = Builder<Phone>.CreateListOfSize(3)
                    .TheFirst(1).With(x => x.Id = 7).With(x => x.Number = "789").TheNext(1).With(x => x.Id = 8).TheLast(1).With(x => x.Id = 9)
                    .All()
                    .With(x => x.NameId = 3)
                    .Build())
                .With(x => x.Address = Builder<Address>.CreateNew()
                    .With(x => x.Id = 3)
                    .With(x => x.NameId = 3)
                    .Build())
                .All()
                .With(x => x.Middle = "C")
                .With(x => x.Email = "testEmail@test.com")
                .Build() as IEnumerable<Name>;

            var expectedResult = Builder<CallListDto>.CreateListOfSize(3)
                .TheFirst(1)
                .With(x => x.Name = Builder<NameDto>.CreateNew()
                    .With(x => x.Id = 0)
                    .With(x => x.First = "Janice")
                    .With(x => x.Last = "Go")
                    .With(x => x.Middle = "C")
                    .With(x => x.Email = "testEmail@test.com")
                    .Build())
                .With(x => x.Phone = Builder<HomePhoneDto>.CreateNew()
                    .With(x => x.Number = "456")
                    .Build())
                .TheNext(1)
                .With(x => x.Name = Builder<NameDto>.CreateNew()
                    .With(x => x.Id = 0)
                    .With(x => x.First = "Celestine")
                    .With(x => x.Last = "Tiangson")
                    .With(x => x.Middle = "C")
                    .With(x => x.Email = "testEmail@test.com")
                    .Build())
                .With(x => x.Phone = Builder<HomePhoneDto>.CreateNew()
                    .With(x => x.Number = "789")
                    .Build())
                .TheLast(1)
                .With(x => x.Name = Builder<NameDto>.CreateNew()
                    .With(x => x.Id = 0)
                    .With(x => x.First = "Ronald")
                    .With(x => x.Last = "Tiangson")
                    .With(x => x.Middle = "C")
                    .With(x => x.Email = "testEmail@test.com")
                    .Build())
                .With(x => x.Phone = Builder<HomePhoneDto>.CreateNew()
                    .With(x => x.Number = "123")
                    .Build())
                .Build();


            _mocker.GetMock<IContactsRepository>().Setup(x => x.GetCallListAsync(It.IsAny<int>()))
              .Returns(Task.FromResult(name));


            //run
            var actualResult = await _service.GetCallListAsync();
            //var expectedResult = name.MapToDtos();

            //assert
            AssertExtensions.AreEqualByJson(expectedResult, actualResult);
        }
    }
}
