using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public string Username { get; set; }
    }
}
