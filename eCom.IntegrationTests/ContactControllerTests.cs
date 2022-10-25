using eCom.CodingChallenge.Business.Mappers;
using eCom.CodingChallenge.Clients;
using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Model;
using eCom.IntegrationTests.Setup;
using eCom.TestUtilities;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace eCom.IntegrationTests
{
    public class ContactControllerTests
    {
        private IEcomClient _swaggerClient;
        private eComFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new eComFactory();
            _swaggerClient = new EcomClient(_factory.CreateClient());

            //Setup Phone Type
            _factory.ExecOnService<eComDbContext>(async dbContext =>
            {
                await dbContext.PhoneTypes.AddRangeAsync(Builder<PhoneType>.CreateListOfSize(3)
                       .TheFirst(1)
                       .With(x => x.Type = "Home")
                       .TheNext(1)
                       .With(x => x.Type = "Work")
                       .TheLast(1)
                       .With(x => x.Type = "Mobile")
                       .Build()
                       );
                await dbContext.SaveChangesAsync();
            });
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

            var name = Builder<Name>.CreateNew()
                .With(x => x.Id = nameId)
                .With(x => x.Phones = phones)
                .With(x => x.Address = address)
                .Build();

            _factory.ExecOnService<eComDbContext>(dbContext =>
            {
                dbContext.Names.Add(name);
                dbContext.SaveChanges();
            });

            //run
            var actualNameResult = await _swaggerClient.Contact_GetAsync();

            //assert
            Assert.IsNotNull(actualNameResult);

            _factory.ExecOnService<eComDbContext>(async dbContext =>
            {
                var expectedResult = await dbContext.Set<Name>()
                .Include(x => x.Phones).ThenInclude(x => x.PhoneType)
                .Include(x => x.Address).ToListAsync();

                AssertExtensions.AreEqualByJson(expectedResult.MapToDtos(), actualNameResult);
            });
        }

        [Test]
        public async Task CreateAsyncTest()
        {
            //setup

            var phones = Builder<PhoneRequestDto>.CreateListOfSize(3)
                .Build();

            var address = Builder<AddressDto>.CreateNew()
                .Build();

            var name = Builder<NameRequestDto>.CreateNew()
                .Build();

            var contactsRequest = Builder<ContactsRequestDto>.CreateNew()
                .With(x => x.Name = name)
                .With(x => x.Address = address)
                .With(x => x.Phone = phones)
                .Build();

            //run
            await _swaggerClient.Contact_CreateAsync(contactsRequest);
            var expectedResult = await _swaggerClient.Contact_GetAsync();

            //assert
            Assert.IsNotNull(expectedResult);

            _factory.ExecOnService<eComDbContext>(async dbContext =>
            {
                var actualResult = await dbContext.Set<Name>()
                .Include(x => x.Phones).ThenInclude(x => x.PhoneType)
                .Include(x => x.Address).ToListAsync();

                AssertExtensions.AreEqualByJson(expectedResult, actualResult.MapToDtos());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateAsync(int nameId)
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

            _factory.ExecOnService<eComDbContext>(dbContext =>
            {
                dbContext.Names.Add(name);
                dbContext.SaveChanges();
            });

            var ReqPhones = Builder<PhoneRequestDto>.CreateListOfSize(3)
                .TheFirst(1).With(x => x.Number = "123")
                .TheNext(1).With(x => x.Number = "456")
                .TheLast(1).With(x => x.Number = "789")
                .Build();

            var ReaqAddress = Builder<AddressDto>.CreateNew()
                .With(x => x.City = "Caloocan")
                .Build();

            var ReqName = Builder<NameRequestDto>.CreateNew()
                .With(x => x.First = "FirstName")
                .Build();

            var contactsRequest = Builder<ContactsRequestDto>.CreateNew()
                .With(x => x.Name = ReqName)
                .With(x => x.Address = ReaqAddress)
                .With(x => x.Phone = ReqPhones)
                .Build();

            //run
            await _swaggerClient.Contact_UpdateAsync(nameId, contactsRequest);
            var expectedResult = await _swaggerClient.Contact_GetAsync();

            //assert

            _factory.ExecOnService<eComDbContext>(async dbContext =>
            {
                var actualResult = await dbContext.Set<Name>()
                .Include(x => x.Phones).ThenInclude(x => x.PhoneType)
                .Include(x => x.Address).ToListAsync();

                AssertExtensions.AreEqualByJson(expectedResult, actualResult.MapToDtos());
            });
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

            _factory.ExecOnService<eComDbContext>(dbContext =>
            {
                dbContext.Names.Add(name);
                dbContext.SaveChanges();
            });

            //run
            var actualNameResult = await _swaggerClient.Contact_GeByIdAsync(nameId);

            //assert
            Assert.IsNotNull(actualNameResult);

            _factory.ExecOnService<eComDbContext>(async dbContext =>
            {
                var expectedResult = await dbContext.Set<Name>()
                .Include(x => x.Phones).ThenInclude(x => x.PhoneType)
                .Include(x => x.Address)
                .Where(x => x.Id == nameId)
                .SingleAsync();

                AssertExtensions.AreEqualByJson(expectedResult.MapToDtos(), actualNameResult);
            });
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteAsyncTest(int nameId)
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

            _factory.ExecOnService<eComDbContext>(dbContext =>
            {
                dbContext.Names.Add(name);
                dbContext.SaveChanges();
            });

            //run
            await _swaggerClient.Contact_DeleteAsync(nameId);

            //assert
            _factory.ExecOnService<eComDbContext>(async dbContext =>
            {
                var expecterName = await dbContext.Names.FirstOrDefaultAsync();
                var expecterAddress = await dbContext.Addresses.FirstOrDefaultAsync();
                var expecterPhone = await dbContext.Phones.FirstOrDefaultAsync();

                Assert.IsNull(expecterName);
                Assert.IsNull(expecterAddress);
                Assert.IsNull(expecterPhone);
            });
        }

        [Test]
        public async Task GetCallListTest()
        {
            //setup
            var contacts = Builder<Name>.CreateListOfSize(3)
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
                .Build();

            var expectedResult = Builder<CallListDto>.CreateListOfSize(3)
                .TheFirst(1)
                .With(x => x.Name = Builder<NameDto>.CreateNew()
                    .With(x=>x.Id = 0)
                    .With(x=>x.First = "Janice")
                    .With(x=>x.Last = "Go")
                    .With(x => x.Middle = "C")
                    .With(x => x.Email = "testEmail@test.com")
                    .Build())
                .With(x=>x.Phone = Builder<HomePhoneDto>.CreateNew()
                    .With(x=>x.Number = "456")
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


            _factory.ExecOnService<eComDbContext>(dbContext =>
            {
                dbContext.AddRange(contacts);
                dbContext.SaveChanges();
            });


            //run
            var actualResult = await _swaggerClient.Contact_GetCallListAsync();

            //assert
            AssertExtensions.AreEqualByJson(expectedResult, actualResult);
        }

    }
}