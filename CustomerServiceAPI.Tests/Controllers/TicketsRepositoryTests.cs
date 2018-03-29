using System;
using Xunit;
using CustomerServiceAPI.Tests.Stubs;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using CustomerServiceAPI.Controllers;
using System.Collections.Generic;
using CustomerServiceAPI.Models;
using System.Linq;

namespace CustomerServiceAPI.Tests.Controllers
{
    public class TicketsRepositoryTests
    {
        public TicketsRepositoryTests()
        {
            AutoMapperConfig.Setup();
        }

        [Fact]
        public void Tickets_Get_All()
        {
            var controller = new TicketsController(new TicketRepositoryStub());
            var result = controller.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(0);
        }
    }
}
