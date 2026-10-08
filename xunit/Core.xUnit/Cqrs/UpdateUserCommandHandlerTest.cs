using AutoMapper;
using Core.Application.Features.Users.Commands.UpdateUser;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.xUnit.Cqrs
{
    public class UpdateUserCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<User>> _userRepositoryMock; // Mock the generic repository
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IGenericRepository<User>>(); // Create the repository mock

            // Set up the UnitOfWork to return the User repository mock when Repository<User>() is called
            _unitOfWorkMock.Setup(uow => uow.Repository<User>()).Returns(_userRepositoryMock.Object);

            // You can now set up your command handler with the mocked UnitOfWork
            var mapperMock = new Mock<IMapper>();
            _handler = new UpdateUserCommandHandler(_unitOfWorkMock.Object, mapperMock.Object, null, null);
        }

        [Fact]
        public async Task Handle_ShouldUpdateUser_WhenCalled()
        {
            // Arrange
            var existingUser = new User { UserSerialID = 682, UserName = "Kokila" };

            // Mock UpdateAsync to simulate successful update
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(existingUser, existingUser.UserSerialID))
                .Returns(Task.CompletedTask); // UpdateAsync doesn't return a result, so return completed task

            // Act
            // Assuming you call `Handle` in your handler and it eventually triggers the update
            await _handler.Handle(new UpdateUserCommand { UserSerialID = existingUser.UserSerialID }, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(existingUser, existingUser.UserSerialID), Times.Once);
        }
    }


}

