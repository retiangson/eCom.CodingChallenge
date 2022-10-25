using eCom.CodingChallenge.Business.Mappers;
using eCom.CodingChallenge.Contracts;
using eCom.CodingChallenge.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.CodingChallenge.Business.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactsDto>?> GetAsync();
        Task CreateAsync(ContactsRequestDto ContactsRequest);
        Task UpdateAsync(int id, ContactsRequestDto contactsRequest);
        Task<ContactsDto?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<CallListDto>?> GetCallListAsync();
    }

    [Service(ServiceLifetime.Transient)]
    public class ContactService : IContactService
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public async Task<IEnumerable<ContactsDto>?> GetAsync()
        {
            return (await _contactsRepository.GetAsync())?.MapToDtos();
        }

        public async Task CreateAsync(ContactsRequestDto contact)
        {
           await _contactsRepository.CreateAsync(contact.MapToDbos());
        }

        public async Task UpdateAsync(int id, ContactsRequestDto contactsRequest)
        {
            var nameToUpdate = await _contactsRepository.GetByIdAsync(id);

            nameToUpdate.First = contactsRequest.Name.First;
            nameToUpdate.Middle = contactsRequest.Name.Middle;  
            nameToUpdate.Last = contactsRequest.Name.Last;
            nameToUpdate.Email = contactsRequest.Name.Email;

            nameToUpdate.Address = contactsRequest.Address.MapToDbo();
            nameToUpdate.Phones = contactsRequest.Phone?.MapDbos(nameToUpdate.Id);

            await _contactsRepository.UpdateAsync(nameToUpdate);
        }

        public async Task<ContactsDto?> GetByIdAsync(int id)
        {
            return (await _contactsRepository.GetByIdAsync(id))?.MapToDtos();
        }

        public async Task DeleteAsync(int id)
        {
            var nameToBeDelete = await _contactsRepository.GetByIdAsync(id);

            if (nameToBeDelete != null)
            await _contactsRepository.DeleteAsync(nameToBeDelete);
        }

        public async Task<IEnumerable<CallListDto>?> GetCallListAsync() {

            var returnValue = await _contactsRepository.GetCallListAsync(1);

            return returnValue?.OrderBy(x => x.Last)
                       .ThenBy(x => x.First)
                       .MapCallListToDtos();
        }
    }
}
