using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BetType
    {
        public int Id { get; set; }
        public BetName betName { get; set; }
        public int payoutRate { get; set; }
    }
}
