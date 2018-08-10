using Microsoft.AspNetCore.Mvc;
using jmseg.Models;
using jmseg.Business;
using Microsoft.AspNetCore.Authorization;
using jmseg.VO;
using System.Security.Claims;

namespace jmseg.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserBusiness userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(userBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        public IActionResult Get(int id)
        {
            var user = userBusiness.FindById(id);
            
            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null) {
                return BadRequest();
            }

            return new ObjectResult(userBusiness.Create(user));
        }

        [HttpPost("reset_password")]
        [Authorize("Bearer")]
        public IActionResult ResetPassword([FromBody] ResetPasswordVO obj)
        {
            User user = userBusiness.FindByEmail(User.Identity.Name);

            if (user == null) {
                return BadRequest();
            }

            object result = userBusiness.ResetPassword(user, obj);

            return Ok(result);
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] User user)
        {
            if (user == null) {
                return BadRequest();
            }

            var updatedUser = userBusiness.Update(user);
            
            if (updatedUser == null) {
                return BadRequest();
            }

            return new ObjectResult(updatedUser);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            userBusiness.Delete(id);
            return NoContent();
        }
    }
}
