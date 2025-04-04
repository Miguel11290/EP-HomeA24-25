using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        public async Task<List<Poll>> GetPollsAsync()
        {
            return await _context.Polls.ToListAsync();
        }

        public async Task<Poll> GetPollByIdAsync(int id)
        {
            return await _context.Polls.FindAsync(id);
        }

        public async Task AddPollAsync(Poll poll)
        {
            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePollAsync(Poll poll)
        {
            _context.Polls.Update(poll);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePollAsync(int id)
        {
            var poll = await _context.Polls.FindAsync(id);
            if (poll != null)
            {
                _context.Polls.Remove(poll);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreatePoll(string title, string option1Text, string option2Text, string option3Text)
        {
            var poll = new Poll
            {
                Title = title,
                Option1Text = option1Text,
                Option2Text = option2Text,
                Option3Text = option3Text,
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                DateCreated = DateTime.UtcNow
            };

            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Poll>> GetPolls()
        {
            return await _context.Polls.OrderByDescending(p => p.DateCreated).ToListAsync();
        }
    }
}
