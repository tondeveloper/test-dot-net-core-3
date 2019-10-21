using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;
using Some.Services;


namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SomeController : ControllerBase
    {
        private ISomeService myClass;

        public SomeController(ISomeService someService)
        {
            myClass = someService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var users = await myClass.GetAll();
            return Ok(users);
        }



        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]SomeProperty someParams)
        { 
            string cat = someParams.firstName;
            string dog = someParams.lastName;

            SomeProperty some = myClass.CreateSomething(cat, dog);
            await myClass.Add(some);
 
            return Ok("added");
        }
   
        [HttpPost("remove")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove([FromBody]RemoveParams someParams)
        {
            await myClass.Remove(someParams.id);

            return Ok("remove");
        }
     
        [HttpPost("clear")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove()
        {
            await myClass.Clear();

            return Ok("clear");
        }
        [AllowAnonymous]
        [HttpPost("some")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Some()
        {
            //return some json
            return Ok(new { some="123"});
        }
    }
}