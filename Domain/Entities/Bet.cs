using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bet
    {
        public string Id { get; set; }
        public int BetTypeId {get; set;}
        //public IEnumerable<int> numbers { get; set; }
        public string Numbers { get; set; }
        public int BetAmount { get; set; }
        public string SessionId { get; set; }
        //public string playerId { get; set; }
        public int PayoutAmount { get; set;}

    }
}
