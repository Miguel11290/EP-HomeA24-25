using Domain;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IPollRepository
    {
        List<Poll> GetPolls();
        Poll GetPollById(int id);
        void AddPoll(Poll poll);
        void UpdatePoll(Poll poll);
        void DeletePoll(int id);
        void CreatePoll(string title, string question, string option1Text, string option2Text, string option3Text);
        void Vote(int pollId, int option);
    }
}
