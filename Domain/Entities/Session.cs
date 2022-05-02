using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helpers;

namespace Domain.Entities
{
    public class Session
    {
        public string Id { get; set; }
        //public IEnumerable<Bet> bets { get; set; }
        public bool hasSpun {get; set;}
        public int? winningNumber {
            get;private set;
        }
        public ICollection<Bet> bets { get; set; }
        public void SetWinningNumber()
        {
            winningNumber = Utilities.GenerateRandomNumberWithinRange();
        }
    }
}
