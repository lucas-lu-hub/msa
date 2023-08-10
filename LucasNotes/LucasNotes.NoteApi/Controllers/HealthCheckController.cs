using Microsoft.AspNetCore.Mvc;

namespace LucasNotes.NoteApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public IActionResult api()
        {
            return Ok();
        }
    }
}
