using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS.Models
{
    public class GameState
    {
        public string PlayerName { get; set; }
        public int TotalRounds { get; set; }
        public int CurrentRound { get; set; } = 0;

        public GameState(string playerName, int totalRounds)
        {

            PlayerName = playerName;
            TotalRounds = totalRounds;
        }
    }
}
