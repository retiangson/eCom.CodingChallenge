using eCom.CodingChallenge.Domain.Model;
using eCom.CodingChallenge.Domain.Repositories;
using eCom.TestUtilities;
using eCom.UnitTests.Setup;
using FizzWare.NBuilder;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace eCom.UnitTests.Repositories
{
    public class ContactsRepositoryTests
    {
        private eComDbContext _context;
        private ContactsRepository _repository;

        [SetUp]
        public void Setup()
        {
            _context = eComContextFactory.CreateContextForInMemoryDB();
            _repository = new ContactsRepository(_context);
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
                .Build();

            _context.Names.AddRange(name);
            _context.SaveChanges();

            //run
            var actual = await _repository.GetAsync();

            //assert
            Assert.IsNotNull(actual);
            AssertExtensions.AreEqualByJson(name, actual);
        }

        [Test]
        public async Task CreateAsyncTest()
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

            //run
            await _repository.CreateAsync(name);

            var expected = _context.Names.First();

            //assert
            AssertExtensions.AreEqualByJson(expected, name);
        }

        [Test]
        public async Task UpdateAsyncTest()
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

            _context.Names.Add(name);
            _context.SaveChanges();

            var toUpdate = _context.Names.Where(x => x.Id == nameId).First();

            toUpdate.First = "FirstNameUpdate";

            //run
            await _repository.UpdateAsync(toUpdate);

            var expected = _context.Names.First();

            //assert
            AssertExtensions.AreEqualByJson(expected, toUpdate);
        }

        [Test]
        public async Task GetByIdAsyncTest()
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

            _context.Names.Add(name);
            _context.SaveChanges();

            //run
            var actual = await _repository.GetByIdAsync(nameId);

            //assert
            Assert.IsNotNull(actual);
            AssertExtensions.AreEqualByJson(name, actual);
        }

        [Test]
        public async Task DeleteAsyncTest()
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

            _context.Names.Add(name);
            _context.SaveChanges();

            var toDelete = _context.Names.Where(x => x.Id == nameId).First();

            //run
            await _repository.DeleteAsync(toDelete);

            var expected = _context.Names;

            //assert
            Assert.IsEmpty(expected);
        }

        [Test]
        public async Task GetCallListAsyncTest()
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
                .Build();

            _context.Names.AddRange(name);
            _context.SaveChanges();

            //run
            var actual = await _repository.GetCallListAsync(nameId);

            //assert
            Assert.IsNotNull(actual);
            AssertExtensions.AreEqualByJson(name, actual);
        }
    }
}
