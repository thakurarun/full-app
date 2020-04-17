using backend.Controllers.Common;
using backend.Exceptions.Common;
using DataStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace backend.Controllers.Login.Register
{
    [Route("api/signup")]
    [ApiController]
    [AllowAnonymous]
    public class SignUpController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public SignUpController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(JsonUtil.CreateJsonSchema<SignUpModel>("SignupForm"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        public async Task<IActionResult> Post([FromBody] SignUpModel model)
        {
            Debug.Launch();
            if (ModelState.IsValid)
            {
                var profileId = Guid.NewGuid();
                await userRepository.SaveUserAsync(new DataStore.Entities.User
                {
                    Email = model.Email,
                    Password = model.Password,
                    Id = Guid.NewGuid(),
                    IsActive = DataStore.Entities.UserStatus.InActive,
                    UserProfile = new DataStore.Entities.Profile
                    {
                        Id = profileId,
                        Name = model.FullName,
                    }
                });
                return Ok();
            }
            throw new InvalidRequestParametersException();
        }
    }
}