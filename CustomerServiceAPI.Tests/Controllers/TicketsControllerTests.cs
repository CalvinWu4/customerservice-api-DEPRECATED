using System;
using Xunit;
using CustomerServiceAPI.Tests.Stubs;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using CustomerServiceAPI.Controllers;
using System.Collections.Generic;
using CustomerServiceAPI.Models;
using System.Linq;
using CustomerServiceAPI.Tests.Mocks;
using Xunit.Sdk;

namespace CustomerServiceAPI.Tests.Controllers
{
    public class TicketsControllerTests
    {
        public TicketsControllerTests()
        {
            AutoMapperConfig.Setup();
        }

        [Fact]
        public void ticket_repo_is_empty()
        {
            var controller = new TicketsController(new TicketRepositoryMock());
            var result = controller.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(0);
        }

        [Fact]
        public void delete_invalid_ticket()
        {
            //Arrange
            var controller = new TicketsController(new TicketRepositoryMock());

            //Act
            var result = controller.Delete(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
