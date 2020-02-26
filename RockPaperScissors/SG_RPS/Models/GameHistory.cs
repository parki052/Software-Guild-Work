using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS.Models
{
    public class GameHistory
    {
        public List<MatchResult> AllRoundHistory { get; set; } = new List<MatchResult>();

    }
}
