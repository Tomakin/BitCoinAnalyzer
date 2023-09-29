using BitCoinAnalyzer.API.Controllers;
using BitCoinAnalyzer.API.DTOModels;
using BitCoinAnalyzer.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Test
{
    public class AccountControllerTests
    {
        Mock<IUserService> userServiceMock;
        Mock<ILogger<AccountController>> loggerMock;
        AccountController controller;

        public AccountControllerTests()
        {
            userServiceMock = new Mock<IUserService>();
            loggerMock = new Mock<ILogger<AccountController>>();
            controller = new AccountController(userServiceMock.Object, loggerMock.Object);
        }

        [Fact]
        public void Authenticate_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var model = new AuthenticateRequest { Username = "testuser", Password = "password" };
            userServiceMock.Setup(service => service.Authenticate(model)).Returns(new AuthenticateResponse());

            // Act
            var result = controller.Authenticate(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void Register_ValidModel_ReturnsCreatedResult()
        {
            // Arrange
            var model = new RegisterModel { Username = "testuser", Password = "password" };
            userServiceMock.Setup(service => service.IsUsernameTaken(model.Username)).Returns(false);

            // Act
            var result = controller.Register(model);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public void Register_UsernameTaken_ReturnsBadRequest()
        {
            // Arrange
            var model = new RegisterModel { Username = "test", Password = "password" };
            userServiceMock.Setup(service => service.IsUsernameTaken(model.Username)).Returns(true);

            // Act
            var result = controller.Register(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Kullanıcı adı zaten alınmış", badRequestResult.Value);
        }

        [Fact]
        public void CheckUserName_UsernameTaken_ReturnsConflictResult()
        {
            // Arrange
            var username = "test";
            userServiceMock.Setup(service => service.IsUsernameTaken(username)).Returns(true);

            // Act
            var result = controller.CheckUserName(username);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(409, conflictResult.StatusCode);
        }

        [Fact]
        public void CheckUserName_UsernameNotTaken_ReturnsOkResult()
        {
            // Arrange
            var username = "newuser";
            userServiceMock.Setup(service => service.IsUsernameTaken(username)).Returns(false);

            // Act
            var result = controller.CheckUserName(username);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Kullanıcı adı kullanılabilir.", okResult.Value);
        }
    }
}
