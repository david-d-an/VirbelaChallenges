using System;
using System.Threading;
using System.Threading.Tasks;
using Exercise1.Common.Tasks;
using Exercise1.Data.Models.VirbelaListing;
using Exercise1.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Exercise1.Data.Models.Authentication;
using Exercise1.Api.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace Exercise1.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private IConfiguration _config;
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;

        public UserController(
            ILogger<UserController> logger,
            IConfiguration config,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._config = config;
            this._userService = userService;
            this._unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel login)    
        {
            AuthenticateResponse response = await _userService.Authenticate(login);
            if (response?.Userid == null) {
                _logger.LogWarning($"Unauthorized access has been attempted for user id '{login.Userid}'.");
                return Unauthorized();
            }

            // _logger.LogInformation($"User id '{login.Userid}' has been successfully logge in.");
            return Ok(response); 
        }

        // TO DO: Assess YAGNI
        [HttpGet("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Get(
            string id, 
            CancellationToken cancellationToken)
        {
            _logger.LogWarning("User/Get(id) has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;           
        }

        // TO DO: Assess YAGNI
        [HttpPut("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Put(
            string id, 
            CancellationToken cancellationToken)
        {
            _logger.LogWarning("UserController.Put has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;           
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(
            Listinguser listinguserCreateRequest, 
            CancellationToken cancellationToken) 
        {
            try {
                var password = listinguserCreateRequest.Password;
                #nullable enable
                listinguserCreateRequest.Password = 
                    new PasswordHasher<object?>()
                    .HashPassword(null, password);
                #nullable disable

                object parameters = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string> ("UserId", listinguserCreateRequest.Userid)
                };

                var existingUsers =  await _unitOfWork
                    .ListinguserRepository
                    .GetAsync(parameters);

                if (existingUsers != null && existingUsers.Count() > 0) {
                    _logger.LogError(
                        $@"UserId '{listinguserCreateRequest.Userid}'
                        already exists. Registration unsuccessful..");
                    return Problem(
                        detail: $"UserId already exists: {listinguserCreateRequest.Userid}",
                        statusCode: 500,
                        title: "Registration Unsucessful");
                }

                var listinguser = await _unitOfWork.ListinguserRepository
                                    .PostAsync(listinguserCreateRequest);
                _unitOfWork.Commit();

                _logger.LogInformation(
                    $@"User account for '{listinguser.Id}: {listinguser.Userid}'
                    has been successfully created.");
                return CreatedAtAction(
                    nameof(Post), 
                    new { Id = listinguser.Id }, 
                    listinguser);
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                _unitOfWork.Rollback();
                return BadRequest();
            }
        }

        // TO DO: Assess YAGNI
        [HttpDelete("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Delete(
            string id, 
            CancellationToken cancellationToken)
        {
            _logger.LogWarning("UserController.Delete has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;
        }

    }
}