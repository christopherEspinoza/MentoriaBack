using MentoriaAPI.Models;
using MentoriaAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MentoriaAPI.Controllers
{
    [ApiController]
    [Route("persona")]
    public class PersonaController: ControllerBase
    {

        private readonly ILogger<PersonaController> _logger;
        private PersonRepository repository = new PersonRepository();
        public PersonaController(ILogger<PersonaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("list-person")]
        public ActionResult<List<Person>> ListPerson()
        {
            var result = repository.ListPerson();
            return Ok(result);
        }

        [HttpGet("get-person-id")]
        public ActionResult<Person> GetPersonById([FromQuery] int id)
        {
            var result = repository.GetPersonById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete-person")]
        public ActionResult<string> DeletePersonById([FromQuery] int id)
        {
            return Ok(repository.DeletePerson(id));
        }

        [HttpPost("create-person")]
        public ActionResult<string> CreatePerson([FromBody] Person person)
        {
            return Ok(repository.CreateOrUpdatePerson(person));
        }

        [HttpPut("update-person")]
        public ActionResult<string> EditPerson([FromBody] Person person)
        {
            return Ok(repository.CreateOrUpdatePerson(person));
        }
    }
}
