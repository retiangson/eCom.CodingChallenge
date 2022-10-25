using eCom.CodingChallenge.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.CodingChallenge.Domain.Repositories
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Name>?> GetAsync();
        Task CreateAsync(Name name);
        Task UpdateAsync(Name name);
        Task<Name?> GetByIdAsync(int id);
        Task DeleteAsync(Name name);
        Task<IEnumerable<Name>?> GetCallListAsync(int phoneTypeId);
    }

    [Service(ServiceLifetime.Transient)]
    public class ContactsRepository : IContactsRepository
    {
        private readonly eComDbContext _context;

        public ContactsRepository(eComDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Name>?> GetAsync()
        {
            var query = _context.Set<Name>();

            return await query
                .Include(x => x.Phones).ThenInclude(x => x.PhoneType)
                .Include(x => x.Address)
                .ToListAsync();
        }

        public async Task CreateAsync(Name name)
        {
            _context.Names.AddRange(name);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Name name)
        {
            _context.UpdateRange(name);
            await _context.SaveChangesAsync();
        }

        public async Task<Name?> GetByIdAsync(int id)
        {
            var query = _context.Set<Name>();

            return await query
                .Include(x => x.Phones).ThenInclude(x => x.PhoneType)
                .Include(x => x.Address)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Name name)
        {
            _context.Names.RemoveRange(name);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Name>?> GetCallListAsync(int phoneTypeId)
        {
            var query = _context.Set<Name>();

            return await query.Include(x => x.Phones.Where(x => x.PhoneTypeId == phoneTypeId))
                .ToListAsync();
        }
    }
}
