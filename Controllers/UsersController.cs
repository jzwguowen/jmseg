using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jmseg.Model;

namespace jmseg.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        
        [HttpGet]
        public IActionResult Get() {
            return Ok(new User());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            return Ok(new User());
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user) {
            return Ok(new User());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user) {
            return Ok(new User());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            return NoContent();
        }
    }
}
