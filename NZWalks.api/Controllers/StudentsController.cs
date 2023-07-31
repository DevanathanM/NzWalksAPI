using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents() 
        {
            String[] Studentnames = new string[] { "Deva", "Anand", "Anandhi", "Ela" };
            return Ok(Studentnames);
        }
    }
}
