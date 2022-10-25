using eCom.CodingChallenge.Business.Services;
using eCom.CodingChallenge.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eCom.CodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("contacts")]
        [ProducesResponseType(typeof(ICollection<ContactsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync()
        {
            var consumer = await _contactService.GetAsync();

            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }

        [HttpPost]
        [Route("contacts")]
        public async Task<ActionResult> CreateAsync(ContactsRequestDto contactsRequest)
        {
            await _contactService.CreateAsync(contactsRequest);
            return Ok();
        }

        [HttpPut]
        [Route("contacts/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, ContactsRequestDto contactsRequest)
        {
            await _contactService.UpdateAsync(id, contactsRequest);
            return Ok();
        }

        [HttpGet]
        [Route("contacts/{id}")]
        [ProducesResponseType(typeof(ContactsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GeByIdAsync(int id)
        {
            var consumer = await _contactService.GetByIdAsync(id);

            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }

        [HttpDelete]
        [Route("contacts/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _contactService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("contacts/call-list")]
        [ProducesResponseType(typeof(ICollection<CallListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCallList()
        {
            var consumer = await _contactService.GetCallListAsync();    

            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }
    }
}
