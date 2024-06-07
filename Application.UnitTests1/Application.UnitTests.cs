using Application.DTO.Request.Account;
using Application.DTO.Response;
using Application.DTO.Response.Account;
using Application.Extensions;
using Application.Services;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<HttpClientService> _httpClientServiceMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _httpClientServiceMock = new Mock<HttpClientService>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };

            _httpClientServiceMock.Setup(service => service.GetPublicClient())
                                  .Returns(httpClient);

            _httpClientServiceMock.Setup(service => service.GetPrivateClient())
                                  .ReturnsAsync(httpClient);

            _accountService = new AccountService(_httpClientServiceMock.Object);
        }

        [Fact]
        public async Task LoginAccountAsync_ReturnsSuccess()
        {
            // Arrange
            var loginDto = new LoginDTO { EmailAddress = "testuser@example.com", Password = "testpassword" };
            var expectedResponse = new LoginResponse(true, "Success");
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expectedResponse)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _accountService.LoginAccountAsync(loginDto);

            // Assert
            Assert.True(result.Flag);
            Assert.Equal("Success", result.Message);
        }

        [Fact]
        public async Task CreateAccountAsync_ReturnsSuccess()
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
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expectedResponse)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _accountService.CreateAccountAsync(createAccountDto);

            // Assert
            Assert.True(result.Flag);
            Assert.Equal("Account created successfully", result.Message);
        }

        [Fact]
        public async Task CreateAdminAtFirstStart_ExecutesWithoutError()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            await _accountService.CreateAdminAtFirstStart();

            // Assert
            _httpMessageHandlerMock
                .Protected()
                .Verify("SendAsync", Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == new Uri("http://localhost" + Constant.CreateAdminRoute)),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetRolesAsync_ReturnsRoles()
        {
            // Arrange
            var roles = new List<GetRoleDTO> { new GetRoleDTO("1", "Admin") };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(roles)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _accountService.GetRolesAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Admin", result.First().Name);
        }

        [Fact]
        public async Task GetUsersWithRolesAsync_ReturnsUsersWithRoles()
        {
            // Arrange
            var usersWithRoles = new List<GetUsersWithRolesResponseDTO>
            {
                new GetUsersWithRolesResponseDTO { Name = "User1", Email = "user1@example.com", RoleName = "Admin", RoleId = "1" }
            };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(usersWithRoles)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _accountService.GetUsersWithRolesAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("User1", result.First().Name);
        }

        [Fact]
        public async Task ChangeUserRoleAsync_ReturnsSuccess()
        {
            // Arrange
            var changeUserRoleDto = new ChangeUserRoleRequestDTO("user1@example.com", "User");
            var expectedResponse = new GeneralResponse(true, "Role changed");
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expectedResponse)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _accountService.ChangeUserRoleAsync(changeUserRoleDto);

            // Assert
            Assert.True(result.Flag);
            Assert.Equal("Role changed", result.Message);
        }

        [Fact]
        public async Task RefreshTokenAsync_ReturnsSuccess()
        {
            // Arrange
            var refreshTokenDto = new RefreshTokenDTO();
            var expectedResponse = new LoginResponse(true, "Token refreshed");
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expectedResponse)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _accountService.RefreshTokenAsync(refreshTokenDto);

            // Assert
            Assert.True(result.Flag);
            Assert.Equal("Token refreshed", result.Message);
        }
    }
}
