using Application.Contracts;
using Application.DTO.Request.Account;
using Application.DTO.Response;
using Application.DTO.Response.Account;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace API.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccount> _accountMock;
        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _accountMock = new Mock<IAccount>();
            _accountController = new AccountController(_accountMock.Object);
        }

        [Fact]
        public async Task CreateAccount_ValidModel_ReturnsOk()
        {
            // Arrange
            var createAccountDto = new CreateAccountDTO
            {
                EmailAddress = "testuser@example.com",
                Password = "testpassword",
                ConfirmPassword = "testpassword",
                Name = "Test User",
                Role = "User"
            };
            var expectedResponse = new GeneralResponse(true, "Account created successfully");

            _accountMock.Setup(account => account.CreateAccountAsync(createAccountDto))
                        .ReturnsAsync(expectedResponse);

            // Act
            var result = await _accountController.CreateAccount(createAccountDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.True(response.Flag);
        }

        [Fact]
        public async Task CreateRole_ValidModel_ReturnsOk()
        {
            // Arrange
            var createRoleDto = new CreateRoleDTO { Name = "Admin" };
            var expectedResponse = new GeneralResponse(true, "Role created");

            _accountMock.Setup(account => account.CreateRoleAsync(createRoleDto))
                        .ReturnsAsync(expectedResponse);

            // Act
            var result = await _accountController.CreateRole(createRoleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.True(response.Flag);
        }

        [Fact]
        public async Task GetRoles_ReturnsOk()
        {
            // Arrange
            var roles = new List<GetRoleDTO> { new GetRoleDTO("1", "Admin") };

            _accountMock.Setup(account => account.GetRolesAsync())
                        .ReturnsAsync(roles);

            // Act
            var result = await _accountController.GetRoles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<List<GetRoleDTO>>(okResult.Value);
            Assert.Single(response);
        }

        [Fact]
        public async Task CreateAdmin_ReturnsOk()
        {
            // Arrange
            _accountMock.Setup(account => account.CreateAdmin())
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _accountController.CreateAdmin();

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetUsersWithRoles_ReturnsOk()
        {
            // Arrange
            var usersWithRoles = new List<GetUsersWithRolesResponseDTO>
            {
                new GetUsersWithRolesResponseDTO { Name = "User1", Email = "user1@example.com", RoleName = "Admin", RoleId = "1" }
            };

            _accountMock.Setup(account => account.GetUsersWithRolesAsync())
                        .ReturnsAsync(usersWithRoles);

            // Act
            var result = await _accountController.GetUsersWithRoles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<List<GetUsersWithRolesResponseDTO>>(okResult.Value);
            Assert.Single(response);
        }

        [Fact]
        public async Task ChangeUserRole_ValidModel_ReturnsOk()
        {
            // Arrange
            var changeUserRoleDto = new ChangeUserRoleRequestDTO("user1@example.com", "User");
            var expectedResponse = new GeneralResponse(true, "Role changed");

            _accountMock.Setup(account => account.ChangeUserRoleAsync(changeUserRoleDto))
                        .ReturnsAsync(expectedResponse);

            // Act
            var result = await _accountController.ChangeUserRole(changeUserRoleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);
            Assert.True(response.Flag);
        }
    }
}
