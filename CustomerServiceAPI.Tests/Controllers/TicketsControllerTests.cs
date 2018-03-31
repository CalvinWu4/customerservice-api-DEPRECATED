using System;
using Xunit;
using CustomerServiceAPI.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using CustomerServiceAPI.Controllers;
using System.Collections.Generic;
using CustomerServiceAPI.Models;
using System.Linq;
using CustomerServiceAPI;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Services;
using Xunit.Sdk;
using Moq;

namespace CustomerServiceAPI.Tests.Controllers
{
    public class Startup
    {
        public Startup()
        {
            AutoMapperConfig.Setup();
        }
    }
    public class EmptyRepoTests : IClassFixture<Startup>
    {
        private readonly TicketsController _emptyRepoController;
        private Startup _startup;

        public EmptyRepoTests(Startup startup)
        {
            _startup = startup;
            _emptyRepoController = new TicketsController(new TicketRepositoryMock());
        }

        public void Dispose()
        {
            _emptyRepoController.Dispose();
        }

        [Fact]
        public void withNoTickets_CountShouldReturnZero()
        {
            var result = _emptyRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(0);
        }

        [Fact]
        public void afterPost_CountShouldReturnOne()
        {
            _emptyRepoController.Post(new TicketForCreationDto());
            var result = _emptyRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(1);

        }
        //        var controller = new Mock<ITicketRepository>().Setup(m => m.GetTickets()).Returns(null);

        [Fact]
        public void deleteInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Delete(1); //Delete ticket with id of 1

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void getInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Get(1); //Get ticket with id of 1

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void putInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Update(1, new TicketDtoForUpdate());

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void postEmptyTicket_ShouldReturnBadRequest()
        {
            var result = _emptyRepoController.Post(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void post_ShouldReturnCreatedAtRoute()
        {
            var result = _emptyRepoController.Post(new TicketForCreationDto());

            Assert.IsType<CreatedAtRouteResult>(result);
        }
    }

    public class SingleTicketRepoTests: IClassFixture<Startup>
    {
        private readonly TicketsController _singleTicketRepoController;
        private Startup _startup;

        public SingleTicketRepoTests(Startup startup)
        {
            _startup = startup;
            _singleTicketRepoController = new TicketsController(new TicketRepositoryMock());
            _singleTicketRepoController.Post(new TicketForCreationDto());
        }

        public void Dispose()
        {
            _singleTicketRepoController.Dispose();
        }

        [Fact]
        public void withOneTicket_CountShouldReturn()
        {
            var result = _singleTicketRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(1);
        }

        [Fact]
        public void deleteValidTicket_ShouldReturnNoContentResult()
        {
            var result = _singleTicketRepoController.Delete(0);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void putEmptyTicket_ShouldReturnBadRequest()
        {
            var result = _singleTicketRepoController.Update(0, null);

            Assert.IsType<BadRequestResult>(result);
        }


        }
    }
