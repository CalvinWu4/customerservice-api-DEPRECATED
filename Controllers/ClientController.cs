using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerServiceAPI.Models;
using CustomerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: api/clients/:clientId/clients
        [HttpGet]
        public IActionResult Get()
        {
            var client = _clientRepository.GetClients();
            var results = Mapper.Map<IEnumerable<ClientDto>>(client);

            return Ok(results);
        }

        // GET: api/clients/:clientId/clients/:client
        [HttpGet("{id}", Name = "GetClient")]
        public IActionResult Get(int Id)
        {
            var client = _clientRepository.GetClient(Id);
            if (client == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<ReviewDto>(client);

            return Ok(result);
        }
        // POST api/clients
        [HttpPost]
        public IActionResult Post([FromBody]ClientDtoForCreation client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            var finalClient = Mapper.Map<Entities.Client>(client);
            _clientRepository.AddClient(finalClient);

            if (!_clientRepository.Save())
            {
                return StatusCode(500, "An error happened while creating ticket");
            }

            var createdClient = Mapper.Map<Models.ClientDto>(finalClient);

            return CreatedAtRoute("GetClient", new { Id = createdClient.Id }, createdClient);
        }

        // PUT api/clients/5
        [HttpPut("{id}")]
        public IActionResult Update(int Id, [FromBody] ClientDtoForUpdate clientData)
        {
            if (clientData == null)
            {
                return BadRequest();
            }

            var client = _clientRepository.GetClient(Id);
            if (client == null)
            {
                return NotFound();
            }

            client.FirstName = clientData.FirstName == null ? client.FirstName : clientData.FirstName;
            client.LastName = clientData.LastName == null ? client.LastName : clientData.LastName;
            client.street = clientData.street == null ? client.street : clientData.street;

            _clientRepository.UpdateClient(client);

            if (!_clientRepository.Save())
            {
                return BadRequest();
            }

            return new NoContentResult();
        }

        // DELETE api/clients/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var client = _clientRepository.GetClient(Id);
            if (client == null)
            {
                return NotFound();
            }

            _clientRepository.DeleteClient(client);
            if (!_clientRepository.Save())
            {
                return BadRequest();
            }

            return new NoContentResult();
        }
    }
}
