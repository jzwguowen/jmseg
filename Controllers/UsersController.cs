using Microsoft.AspNetCore.Mvc;
using jmseg.Models;
using jmseg.Business;
using Microsoft.AspNetCore.Authorization;

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

        //
        // Retorna uma lista com todos os usuários.
        //
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(userBusiness.FindAll());
        }

        //
        // Retorna um único usuário, identificado pelo id.
        //
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

        //
        // Cria um novo usuário conforme dados de entrada.
        //
        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null) {
                return BadRequest();
            }

            return new ObjectResult(userBusiness.Create(user));
        }

        //
        // Atualiza as informações de um usuário.
        //
        [HttpPut("{id}")]
        [Authorize("Bearer")]
        public IActionResult Put(int id, [FromBody] User user)
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

        //
        // Remove um usuário, identificado pelo id.
        //
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            userBusiness.Delete(id);
            return NoContent();
        }
    }
}
