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
    public class TicketsController : Controller
    {
        private ITicketRepository _ticketRepository;

        public TicketsController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        // GET: api/tickets
        [HttpGet]
        public IActionResult GetAll([FromQuery(Name = "ClientId")] int clientId = -1)
        {
            var tickets = _ticketRepository.GetTickets();

            if (clientId != -1)
            {
                tickets = tickets.Where<Entities.Ticket>(t => t.ClientId == clientId);
            }

            var results = Mapper.Map<IEnumerable<TicketDto>>(tickets);

            return Ok(results);
        }

        // GET api/tickets/5
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult Get(int id)
        {
            var ticket = _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<TicketDto>(ticket);

            return Ok(result);
        }

        // POST api/tickets
        [HttpPost]
        public IActionResult Post([FromBody]TicketForCreationDto ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            var finalTicket = Mapper.Map<Entities.Ticket>(ticket);

            // set ticket as 'new' status
            finalTicket.Status = "new";

            // Default values until Clients & Agent API is mocked
            finalTicket.ClientId = 0;
            finalTicket.AgentId = 0;
            finalTicket.Opened = DateTime.Now;

            _ticketRepository.AddTicket(finalTicket);

            if(!_ticketRepository.Save())
            {
                return StatusCode(500, "An error happened while creating ticket");
            }

            var createdTicket = Mapper.Map<Models.TicketDto>(finalTicket);

            return CreatedAtRoute("GetTicket", new { id = createdTicket.Id }, createdTicket);
        }

        // PUT api/tickets/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TicketDtoForUpdate ticketData)
        {
            if (ticketData == null)
            {
                return BadRequest();
            }

            var ticket = _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Title = ticketData.Title == null ? ticket.Title : ticketData.Title;
            ticket.Description = ticketData.Description == null ? ticket.Description : ticketData.Description;
            ticket.Status = ticketData.Status == null ? ticket.Status : ticketData.Status;

            _ticketRepository.UpdateTicket(ticket);

            if(!_ticketRepository.Save())
            {
                return BadRequest();
            }

            return new NoContentResult();
        }

        // DELETE api/tickets/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _ticketRepository.DeleteTicket(ticket);
            if(!_ticketRepository.Save())
            {
                return BadRequest();
            }

            return new NoContentResult();
        }
    }
}
