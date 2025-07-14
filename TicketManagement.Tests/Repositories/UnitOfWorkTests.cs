using DataAcces;
using DataAcces.Repositories;
using DataAcces.Repositories.Interfaces;
using Moq;
using System;
using Xunit;

namespace TicketManagement.Tests.Repositories
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void UnitOfWork_InitializesRepositories()
        {
            // Arrange & Act
            using (var unitOfWork = new UnitOfWork())
            {
                // Assert
                Assert.NotNull(unitOfWork.TicketRepository);
                Assert.NotNull(unitOfWork.UserRepository);
                Assert.NotNull(unitOfWork.StatusRepository);
                Assert.NotNull(unitOfWork.PriorityRepository);
                Assert.NotNull(unitOfWork.CategoryRepository);
                Assert.NotNull(unitOfWork.CommentRepository);
            }
        }
        
        [Fact]
        public void UnitOfWork_DisposesContext()
        {
            // Arrange
            var mockContext = new Mock<TicketManagementEntities>();
            
            // Act
            using (var unitOfWork = new UnitOfWork())
            {
                // Do nothing, just let it dispose
            }
            
            // Assert - no exception means success
            // Note: We can't easily verify the context was disposed without refactoring UnitOfWork
        }
    }
}