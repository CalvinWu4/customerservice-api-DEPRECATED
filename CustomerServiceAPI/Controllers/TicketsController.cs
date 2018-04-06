using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Models;
using CustomerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        private readonly TicketRepository _ticketRepository;

        public TicketsController(TicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        #region GET: api/tickets
        [HttpGet]
        public IActionResult GetAll([FromQuery(Name = "ClientId")] int clientId = -1)
        {
            var tickets = _ticketRepository.FetchAll();

            if (clientId != -1)
            {
                tickets = tickets.Where<Entities.Ticket>(t => t.ClientId == clientId);
            }

            var results = Mapper.Map<IEnumerable<TicketDto>>(tickets);
            return Ok(results);
        }
        #endregion

        #region GET api/tickets/{{id}}
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult Get(int id)
        {
            var ticket = _ticketRepository.Query(id);
            if (ticket == null) return NotFound();

            var result = Mapper.Map<TicketDto>(ticket);
            return Ok(result);
        }
        #endregion

        #region POST api/tickets
        [HttpPost]
        public IActionResult Post([FromBody]TicketForCreationDto ticket)
        {
            if (ticket == null) return BadRequest();
            var finalTicket = Mapper.Map<Entities.Ticket>(ticket);

            // set ticket as 'new' status
            finalTicket.Status = "new";

            // Default values until Clients & Agent API is mocked
            finalTicket.ClientId = 0;
            finalTicket.AgentId = 0;
            finalTicket.Opened = DateTime.Now;

            _ticketRepository.Add(finalTicket);
            if (!_ticketRepository.Save()) return BadRequest();
            var createdTicket = Mapper.Map<Models.TicketDto>(finalTicket);

            return CreatedAtRoute("GetTicket", new { id = createdTicket.Id }, createdTicket);
        }
        #endregion

        #region PUT api/tickets/{{id}}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TicketDtoForUpdate ticketData)
        {
            if (ticketData == null) return BadRequest();

            var ticket = _ticketRepository.Query(id);
            if (ticket == null) return NotFound();

            ticket.Title = ticketData.Title == null ? ticket.Title : ticketData.Title;
            ticket.Description = ticketData.Description == null ? ticket.Description : ticketData.Description;
            ticket.Status = ticketData.Status == null ? ticket.Status : ticketData.Status;

            _ticketRepository.Update(ticket);

            if (!_ticketRepository.Save()) return BadRequest();

            return new NoContentResult();
        }
        #endregion

        #region DELETE api/tickets/{{id}}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _ticketRepository.Query(id);
            if (ticket == null) return NotFound();

            _ticketRepository.Delete(ticket);
            if (!_ticketRepository.Save()) return BadRequest();

            return new NoContentResult();
        }
        #endregion
   
    }
}
