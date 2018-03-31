using Xunit;
using CustomerServiceAPI.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using CustomerServiceAPI.Controllers;
using System.Collections.Generic;
using CustomerServiceAPI.Models;
using System.Linq;

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
        public void WithNoTickets_CountShouldReturnZero()
        {
            var result = _emptyRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(0);
        }

        [Fact]
        public void AfterPost_CountShouldReturnOne()
        {
            _emptyRepoController.Post(new TicketForCreationDto());
            var result = _emptyRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(1);

        }
        //        var controller = new Mock<ITicketRepository>().Setup(m => m.GetTickets()).Returns(null);

        [Fact]
        public void DeleteInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Delete(1); //Delete ticket with id of 1

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Get(1); //Get ticket with id of 1

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PutInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Update(1, new TicketDtoForUpdate());

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void PostEmptyTicket_ShouldReturnBadRequest()
        {
            var result = _emptyRepoController.Post(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Post_ShouldReturnCreatedAtRoute()
        {
            var result = _emptyRepoController.Post(new TicketForCreationDto());

            Assert.IsType<CreatedAtRouteResult>(result);
        }
    }

    public class SingleTicketRepoTests : IClassFixture<Startup>
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
        public void WithOneTicket_CountShouldReturnOne()
        {
            var result = _singleTicketRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(1);
        }

        [Fact]
        public void DeleteValidTicket_ShouldReturnNoContentResult()
        {
            var result = _singleTicketRepoController.Delete(0);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteValidTicket_getTicketShouldReturnNotFound()
        {
            _singleTicketRepoController.Delete(0);
            var result = _singleTicketRepoController.Get(0);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PutEmptyTicket_ShouldReturnBadRequest()
        {
            var result = _singleTicketRepoController.Update(0, null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetValidTicket_ShouldReturnOk()
        {
            var result = _singleTicketRepoController.Get(0);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PutValidTicket_ShouldReturnOk()
        {
            var result = _singleTicketRepoController.Update(0, new TicketDtoForUpdate());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateValidTicketWithNullData_ShouldReturnBadRequest()
        {
            var result = _singleTicketRepoController.Update(0, null);

            Assert.IsType<BadRequestResult>(result);
        }

    }
}
