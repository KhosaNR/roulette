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
        public int betTypeId {get; set;}
        //public IEnumerable<int> numbers { get; set; }
        public string numbers { get; set; }
        public int betAmount { get; set; }
        public string sessionId { get; set; }
        //public string playerId { get; set; }
        public int payoutAmount { get; set;}

    }
}
