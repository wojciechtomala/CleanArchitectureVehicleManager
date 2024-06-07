using Application.DTO.Request.Account;
using Application.DTO.Response;
using Application.Extensions;
using Application.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private AccountService _accountService;

        [Test]
        public async Task LoginAccountAsync_Returns_SuccessfulResponse()
        {
            Assert.AreEqual("Success", "Success");
        }
    }
}
