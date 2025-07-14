using BusinessLogic.Entities;
using BusinessLogic.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketManagement.Areas.Identity.Data;
using TicketManagement.Controllers;
using TicketManagement.Helpers;
using TicketManagement.Models;
using Xunit;

namespace TicketManagement.Tests.Controllers
{
    public class TicketControllerTests
    {
        [Fact]
        public void Index_ReturnsViewWithTickets()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TicketController>>();
            var mockUserManager = MockUserManager<TicketManagementUser>();
            
            var controller = new TicketController(mockLogger.Object, mockUserManager.Object);
            
            // Act
            var result = controller.Index();
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TicketDetailsModel>>(viewResult.Model);
        }
        
        [Fact]
        public void Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TicketController>>();
            var mockUserManager = MockUserManager<TicketManagementUser>();
            
            var controller = new TicketController(mockLogger.Object, mockUserManager.Object);
            
            // Act
            var result = controller.Details(null);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public void Create_ReturnsView()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TicketController>>();
            var mockUserManager = MockUserManager<TicketManagementUser>();
            
            var controller = new TicketController(mockLogger.Object, mockUserManager.Object);
            
            // Act
            var result = controller.Create();
            
            // Assert
            Assert.IsType<ViewResult>(result);
        }
        
        // Helper method to mock UserManager
        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }
    }
}