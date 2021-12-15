using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Interface;
using DataLayer.Dtos;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APIs.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly string key;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            key = _configuration.GetValue<string>("AppSettings:Key");

        }

        [HttpPost("Authenticate")]
        public async Task<UserDto> AuthenticateUser(LoginDto dto) => await _userService.AuthenticateUser(dto, key);
        [HttpPost("RegisterUser")]
        public async Task<UserDto> NewUser(RegisterDto loginDto) => await _userService.NewUser(loginDto);
        [HttpPost("StampUseProfile")]
        public async Task<int> UpdateUserProfile(UpdateProfileDto dto, string userId) => await _userService.UpdateUserProfile(dto, userId);
        [HttpGet("StampUser")]
        public async Task<UpdateProfileDto> GetUserProfile(string userId) => await _userService.GetUserProfile(userId);
        [HttpPost("[action]")]
        public async Task<int> PostSecurityQuestions(string userId, List<BaseDto> dto) => await _userService.PostSecurityQuestions(userId, dto);
        [HttpPost("UserAgreementCompliance")]
        public async Task<int> PostUserAgreement(string userId, int agreementType) => await _userService.PostUserAgreement(userId, agreementType);
        [HttpGet("DetailsOfCompliance")]
        public async Task<IEnumerable<BaseDto>> GetUserComplianceType(string userId) => await _userService.GetUserComplianceType(userId);


    }
}
