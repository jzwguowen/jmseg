using Microsoft.AspNetCore.Mvc;
using jmseg.Business;
using Microsoft.AspNetCore.Authorization;
using jmseg.Models;

namespace jmseg.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserBusiness userBusiness;

        public LoginController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody] User user)
        {
            if (user == null) {
                return BadRequest();
            }

            return userBusiness.FindByLogin(user);
        }

    }
}
