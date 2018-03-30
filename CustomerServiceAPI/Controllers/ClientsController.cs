﻿using System;
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

        // GET: api/values
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
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // POST api/values
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
