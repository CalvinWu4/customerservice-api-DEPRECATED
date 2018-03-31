using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CustomerServiceAPI.Models;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Services;



namespace CustomerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: api/clients
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clientRepository.GetClients());
        }

        // GET api/values/5
        [HttpGet("{id}", Name ="GetClient")]
        public IActionResult Get(int id)
        {
            var client = _clientRepository.GetClient(id);
            if (client == null) return NotFound();

            return Ok(client);
        }

        // POST api/clients
        [HttpPost]
        public IActionResult Post([FromBody]ClientDtoForCreation client)
        {
            if (client == null) return BadRequest();

            var finalClient = Mapper.Map<Client>(client);
            _clientRepository.AddClient(finalClient);

            if(!_clientRepository.Save())
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetClient", new { id = finalClient.Id }, finalClient);
        }

        // PUT api/clients/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ClientDtoForUpdate clientData)
        {
            if (clientData == null) return BadRequest();

            var client = _clientRepository.GetClient(id);
            if (client == null) return NotFound();

            client.FirstName = clientData.FirstName == null ? client.FirstName : clientData.FirstName;
            client.LastName = clientData.LastName == null ? client.LastName : clientData.LastName;
            client.Email = clientData.Email == null ? client.Email : clientData.Email;

            _clientRepository.UpdateClient(client);
            if (!_clientRepository.Save()) return BadRequest();

            return NoContent();
        }

        // DELETE api/clients/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _clientRepository.GetClient(id);
            if (client == null) NotFound();

            _clientRepository.DeleteClient(client);
            if (!_clientRepository.Save())
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
