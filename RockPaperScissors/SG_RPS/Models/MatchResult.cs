using SG_RPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS.Models
{
    public class MatchResult
    {
        
        public int RoundNumber { get; set; }
        public RoundChoice PlayerChoice { get; set; }
        public RoundChoice ComputerChoice { get; set; }
        public RoundWinner RoundWinner { get; set; }

    }
}
