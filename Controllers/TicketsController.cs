using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerServiceAPI.Models;
using CustomerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        private ITicketRepository _ticketRepository;

        public TicketsController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var tickets = _ticketRepository.GetTickets();
            var results = Mapper.Map<IEnumerable<TicketDto>>(tickets);

            return Ok(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
            return Ok(_ticketRepository.GetTicket(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
