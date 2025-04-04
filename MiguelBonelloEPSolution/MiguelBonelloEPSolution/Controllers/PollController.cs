using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MiguelBonelloEPSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : Controller
    {
        private readonly PollRepository _pollRepository;

        // Constructor
        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var polls = await _pollRepository.GetPollsAsync();
            return View(polls);
        }

        [HttpPost]
        [Route("CreatePoll")]
        public async Task<IActionResult> CreatePoll([FromServices] PollRepository pollRepository, [FromBody] Poll poll)
        {
            if(poll == null)
            {
                return BadRequest("Poll data is null");
            }

            await pollRepository.CreatePoll(poll.Title, poll.Option1Text, poll.Option2Text, poll.Option3Text);
            return View("Poll created successfully");
        }
    }
}
