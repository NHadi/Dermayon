using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dermayon.Common.Api;
using Dermayon.Sample.SocialMedia.User.Framework.BLL.Contracts;
using Dermayon.Sample.SocialMedia.User.Framework.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dermayon.Sample.SocialMedia.User.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountUserController : ControllerBase
    {
        private readonly IAccountUserBLL _accountUserBLL;
        public AccountUserController(IAccountUserBLL accountUserBLL)
        {
            _accountUserBLL = accountUserBLL;
        }
        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var responAPI = _accountUserBLL.ListUser();
                return Ok(new ApiOkResponse(responAPI, responAPI.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(400, "Terjadi Kesalahan"));
            }
        }

        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="id">Id User</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var responAPI = _accountUserBLL.Detail(id);
                return Ok(new ApiOkResponse(responAPI, 1));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(400, "Terjadi Kesalahan"));
            }
        }
        
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AccountUserCreateRequest request)
        {
            try
            {
                await _accountUserBLL.AddAsync(request);
                return Ok(new ApiResponse(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(400, "Terjadi Kesalahan"));
            }
        }
       

        /// <summary>
        /// Remove User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _accountUserBLL.DeleteAsync(id);
                return Ok(new ApiResponse(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(400, "Terjadi Kesalahan"));
            }
        }
    }
}
