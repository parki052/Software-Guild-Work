using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    public class Player
    {
        public string Name { get; set; } = "";
        public int Index { get; set; } = 0;
        public bool IsPlayerTurn { get; set; } = false;
        public Board PlayerBoard { get; set; } = new Board();

    }
}
