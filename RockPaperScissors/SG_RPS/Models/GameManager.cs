using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS.Models
{
    public class GameManager
    {
        public GameHistory GameHistory { get; set; }
        public GameState GameState { get; set; } 
        
        public GameManager( GameHistory history, GameState state )
        {
            GameHistory = history;
            GameState = state;
        }
    }
}
