using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MiguelBonelloEPSolution.Filters;
using System.Linq;

namespace MiguelBonelloEPSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        // Constructor
        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls();
            return View(polls);
        }

        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }

        [HttpGet]
        [Route("CreatePoll")]
        public IActionResult CreatePoll()
        {
            return View();
        }

        [HttpPost]
        [Route("CreatePoll")]
        public IActionResult CreatePoll([FromForm] Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return View(poll);
            }

            _pollRepository.CreatePoll(poll.Title, poll.Question, poll.Option1Text, poll.Option2Text, poll.Option3Text);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Vote")]
        [ServiceFilter(typeof(VoteOnceFilter))]
        public IActionResult Vote(int pollId, int option)
        {
            _pollRepository.Vote(pollId, option);
            return RedirectToAction("Details", new { id = pollId });
        }

        [HttpGet]
        [Route("Results/{id:int}")]
        public IActionResult Results(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }
    }
}
