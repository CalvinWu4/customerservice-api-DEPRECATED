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
    public class StartupFixture
    {
        public StartupFixture()
        {
            AutoMapperConfig.Setup();
        }
    }

    [CollectionDefinition("StartupFixture collection")]
    public class StartupCollection : ICollectionFixture<StartupFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    [Collection("StartupFixture collection")]
    public class EmptyRepoTests
    {
        private readonly TicketsController _emptyRepoController;
        private StartupFixture _startupFixture;

        public EmptyRepoTests(StartupFixture startupFixture)
        {
            _startupFixture = startupFixture;
            _emptyRepoController = new TicketsController(new TicketRepositoryMock());
        }

        #region GetAll Tests
        [Fact]
        public void GetAll_ShouldReturnEmptyTicketList()
        {
            var result = _emptyRepoController.GetAll(-2);   // Non-existing id

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(0); // Checking if it is empty
        }

        [Fact]
        public void GetAllNonExistingClientId_ShouldReturnEmptyTicketList() 
        {
            var result = _emptyRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(0); // Checking if it is empty
        }

        // Move to client controllers tests
//        [Fact]
//        public void GetValidClientId_ShouldReturnNonEmptyTicketList() 
//        {
//            var result = _emptyRepoController.GetAll(0);
//
//            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
//            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;
//
//            tickets.Count().Should().BeGreaterThan(0); // Checking if it is non-empty
//        }


        #endregion

        #region Get Tests
        [Fact]
        public void GetInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Get(0);

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Post Tests 
        [Fact]
        public void PostEmptyTicket_ShouldReturnBadRequest()
        {
            var result = _emptyRepoController.Post(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ValidPost_ShouldReturnTicket()
        {
            var result = _emptyRepoController.Post(new TicketForCreationDto());

            var createdAtRouteResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            createdAtRouteResult.Value.Should().BeAssignableTo<TicketDto>();
        }

        [Fact]
        public void AfterValidPost_CountShouldReturnOne()
        {
            _emptyRepoController.Post(new TicketForCreationDto());
            var result = _emptyRepoController.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tickets = okResult.Value.Should().BeAssignableTo<IEnumerable<TicketDto>>().Subject;

            tickets.Count().Should().Be(1);
        }
        #endregion

        #region Delete Tests
        [Fact]
        public void DeleteInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Delete(0);
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Update Tests
        [Fact]
        public void PutInvalidTicket_ShouldReturnNotFound()
        {
            var result = _emptyRepoController.Update(1, new TicketDtoForUpdate());

            Assert.IsType<NotFoundResult>(result);
        }
#endregion

    }

    [Collection("StartupFixture collection")]
    public class SingleTicketRepoTests
    {
        private readonly TicketsController _singleTicketRepoController;
        private StartupFixture _startupFixture;

        public SingleTicketRepoTests(StartupFixture startupFixture)
        {
            _startupFixture = startupFixture;
            _singleTicketRepoController = new TicketsController(new TicketRepositoryMock());
            _singleTicketRepoController.Post(new TicketForCreationDto());
        }

        // GetAll Tests are performed in the EmptyRepoTests Post Tests

        #region Get Tests
        [Fact]
        public void ValidGet_ShouldReturnTicket()
        {
            var result = _singleTicketRepoController.Get(0);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeAssignableTo<TicketDto>();
        }
        #endregion

        // Insert Post Test here to see if the ticket id increments for next release when use a mock 
        // in-memory database rather than mocking it with an array

        #region Update Tests
        [Fact]
        public void PutEmptyTicket_ShouldReturnBadRequest()
        {
            var result = _singleTicketRepoController.Update(0, null);

            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void UpdateValidTicket_ShouldReturnOk()
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
        #endregion

        #region Delete Tests
        [Fact]
        public void DeleteValidTicket_ShouldReturnNoContentResult()
        {
            var result = _singleTicketRepoController.Delete(0);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteValidTicket_GetTicketShouldReturnNotFound()
        {
            _singleTicketRepoController.Delete(0);
            var result = _singleTicketRepoController.Get(0);

            Assert.IsType<NotFoundResult>(result);
        }
#endregion

    }
}
