using Application.Contracts;
using Application.DTO.Request.Account;
using Application.DTO.Response;
using Application.DTO.Response.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccount account) : ControllerBase
    {
        [HttpPost("identity/create")]
        public async Task<ActionResult<GeneralResponse>> CreateAccount(CreateAccountDTO model) {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model cannot be null");
            }
            else {
                return Ok(await account.CreateAccountAsync(model));
            }
        }
        [HttpPost("identity/login")]
        public async Task<ActionResult<GeneralResponse>> LoginAccount(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model cannot be null");
            }
            else { 
                return Ok(await account.LoginAccountAsync(model));
            }
        }

        [HttpPost("identity/refresh-token")]
        public async Task<ActionResult<GeneralResponse>> RefreshToken(RefreshTokenDTO model) {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model cannot be null");
            }
            else { 
                return Ok(await account.RefreshTokenAsync(model));
            }
        }

        [HttpPost("identity/role/create")]
        public async Task<ActionResult<GeneralResponse>> CreateRole(CreateRoleDTO model)
        { 
            if(!ModelState.IsValid) {
                return BadRequest("Model cannot be null");
            }
            else
            {
                return Ok(await account.CreateRoleAsync(model));
            }
        }

        [HttpGet("identity/role/list")]
        public async Task<ActionResult<IEnumerable<GetRoleDTO>>> GetRoles() => Ok(await account.GetRolesAsync());

        [HttpPost("/setting")]
        public async Task<IActionResult> CreateAdmin() {
            await account.CreateAdmin();
            return Ok();
        }

        [HttpGet("identity/users-with-roles")]
        public async Task<ActionResult<IEnumerable<GetUsersWithRolesResponseDTO>>> GetUsersWithRoles() => Ok(await account.GetUsersWithRolesAsync());

        [HttpPost("identity/change-role")]
        public async Task<ActionResult<GeneralResponse>> ChangeUserRole(ChangeUserRoleRequestDTO model) => Ok(await account.ChangeUserRoleAsync(model));
    }
}
