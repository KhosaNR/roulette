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
        public bool HasSpun {get; set;}
        public int? WinningNumber {
            get;private set;
        }
        public ICollection<Bet> Bets { get; set; }
        public void SetWinningNumber()
        {
            WinningNumber = Utilities.GenerateRandomNumberWithinRange();
        }
    }
}
