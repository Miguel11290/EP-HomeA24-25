using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiguelBonelloEPSolution.Filters;

namespace MiguelBonelloEPSolution.Pages.Polls
{
    [ServiceFilter(typeof(VoteOnceFilter))]
    public class DetailsModel : PageModel
    {
        private readonly IPollRepository _pollRepository;

        public DetailsModel(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
            Poll = new Poll(); // Initialize the Poll property
        }

        public Poll Poll { get; set; }

        public IActionResult OnGet(int id)
        {
            Poll = _pollRepository.GetPollById(id);
            if (Poll == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostVote(int pollId, int vote)
        {
            // Voting logic here
            return RedirectToPage("/Polls/Details", new { id = pollId });
        }
    }
}
