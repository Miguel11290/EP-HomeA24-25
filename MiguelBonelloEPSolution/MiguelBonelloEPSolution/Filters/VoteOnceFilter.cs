using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace MiguelBonelloEPSolution.Filters
{
    public class VoteOnceFilter : ActionFilterAttribute
    {
        private readonly PollDbContext _context;

        public VoteOnceFilter(PollDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("pollId", out var pollIdObj) && pollIdObj is int pollId)
            {
                var identity = context.HttpContext.User.Identity;

                if (identity != null && identity.Name != null)
                {
                    var username = identity.Name;
                    var hasVoted = _context.Votes.Any(v => v.PollId == pollId && v.Username == username);

                    if (hasVoted)
                    {
                        context.Result = new ContentResult
                        {
                            Content = "You have already voted in this poll.",
                        };
                    }
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Poll ID is required.");
            }

            base.OnActionExecuting(context);
        }
    }
}
