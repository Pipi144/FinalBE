using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssignmentBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameRuleController : ControllerBase
    {
        // GET: api/<GameRuleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GameRuleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GameRuleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GameRuleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameRuleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
