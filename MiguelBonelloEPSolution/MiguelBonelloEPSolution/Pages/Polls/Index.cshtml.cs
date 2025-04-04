using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiguelBonelloEPSolution.Pages.Polls
{
    public class IndexModel : PageModel
    {
        private readonly PollRepository _pollRepository;

        public IndexModel(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public List<Poll> Polls { get; set; }

        public void OnGetAsync()
        {
        }
    }
}
