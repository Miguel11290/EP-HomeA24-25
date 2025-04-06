using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiguelBonelloEPSolution.Pages.Polls
{
    public class IndexModel : PageModel
    {
        private readonly IPollRepository _pollRepository;

        public IndexModel(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
            Polls = new List<Poll>(); // Initialize the Polls property
        }

        public IEnumerable<Poll> Polls { get; set; }

        public void OnGet()
        {
            Polls = _pollRepository.GetPolls();
        }
    }
}
