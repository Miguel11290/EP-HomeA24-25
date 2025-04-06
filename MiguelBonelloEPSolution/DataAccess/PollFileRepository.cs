using Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class PollFileRepository : IPollRepository
    {
        private readonly string _filePath;

        public PollFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Poll>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Poll>>(json) ?? new List<Poll>();
        }

        public Poll GetPollById(int id)
        {
            return GetPolls().FirstOrDefault(p => p.Id == id);
        }

        public void AddPoll(Poll poll)
        {
            var polls = GetPolls();
            polls.Add(poll);
            SavePolls(polls);
        }

        public void UpdatePoll(Poll poll)
        {
            var polls = GetPolls();
            var existingPoll = polls.FirstOrDefault(p => p.Id == poll.Id);
            if (existingPoll != null)
            {
                existingPoll.Title = poll.Title;
                existingPoll.Question = poll.Question;
                existingPoll.Option1Text = poll.Option1Text;
                existingPoll.Option2Text = poll.Option2Text;
                existingPoll.Option3Text = poll.Option3Text;
                existingPoll.Option1VotesCount = poll.Option1VotesCount;
                existingPoll.Option2VotesCount = poll.Option2VotesCount;
                existingPoll.Option3VotesCount = poll.Option3VotesCount;
                existingPoll.DateCreated = poll.DateCreated;
                SavePolls(polls);
            }
        }

        public void DeletePoll(int id)
        {
            var polls = GetPolls();
            var poll = polls.FirstOrDefault(p => p.Id == id);
            if (poll != null)
            {
                polls.Remove(poll);
                SavePolls(polls);
            }
        }

        public void CreatePoll(string title, string question, string option1Text, string option2Text, string option3Text)
        {
            var polls = GetPolls();

            var poll = new Poll
            {
                Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1,
                Title = title,
                Question = question,
                Option1Text = option1Text,
                Option2Text = option2Text,
                Option3Text = option3Text,
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                DateCreated = DateTime.UtcNow
            };

            polls.Add(poll);
            SavePolls(polls);
        }

        public void Vote(int pollId, int option)
        {
            var polls = GetPolls();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);
            if (poll != null)
            {
                switch (option)
                {
                    case 1:
                        poll.Option1VotesCount++;
                        break;
                    case 2:
                        poll.Option2VotesCount++;
                        break;
                    case 3:
                        poll.Option3VotesCount++;
                        break;
                    default:
                        throw new ArgumentException("Invalid option");
                }
                SavePolls(polls);
            }
        }

        private void SavePolls(IEnumerable<Poll> polls)
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(polls, Formatting.Indented));
        }
    }
}
