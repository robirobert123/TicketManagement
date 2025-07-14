using BusinessLogic.Entities;
using BusinessLogic.Handlers;
using DataAcces;
using DataAcces.Repositories;
using DataAcces.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TicketManagement.Tests.Handlers
{
    public class TicketHandlerTests
    {
        [Fact]
        public void GetAllTickets_ReturnsSuccessResult_WhenRepositoryReturnsData()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var tickets = new List<Ticket>
            {
                new Ticket { TicketID = 1, Title = "Test Ticket 1", Description = "Description 1" },
                new Ticket { TicketID = 2, Title = "Test Ticket 2", Description = "Description 2" }
            };
            
            mockTicketRepository.Setup(repo => repo.GetAllTickets()).Returns(tickets.AsQueryable());
            mockUnitOfWork.Setup(uow => uow.TicketRepository).Returns(mockTicketRepository.Object);
            
            var handler = new TicketHandler();
            
            // Act
            var result = handler.GetAllTickets();
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count());
        }
        
        [Fact]
        public void GetTicketById_ReturnsTicket_WhenTicketExists()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var ticket = new Ticket 
            { 
                TicketID = 1, 
                Title = "Test Ticket", 
                Description = "Test Description",
                PriorityID = 1,
                StatusID = 1,
                CategoryID = 1
            };
            
            mockTicketRepository.Setup(repo => repo.GetTicketById(1)).Returns(ticket);
            mockUnitOfWork.Setup(uow => uow.TicketRepository).Returns(mockTicketRepository.Object);
            
            var handler = new TicketHandler();
            
            // Act
            var result = handler.GetTicketById(1);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.TicketID);
            Assert.Equal("Test Ticket", result.Data.Title);
        }
        
        [Fact]
        public void GetTicketById_ReturnsFailure_WhenTicketDoesNotExist()
        {
            // Arrange
            var mockTicketRepository = new Mock<ITicketRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            mockTicketRepository.Setup(repo => repo.GetTicketById(999)).Returns((Ticket)null);
            mockUnitOfWork.Setup(uow => uow.TicketRepository).Returns(mockTicketRepository.Object);
            
            var handler = new TicketHandler();
            
            // Act
            var result = handler.GetTicketById(999);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Contains("not found", result.Message.ToLower());
        }
    }
}