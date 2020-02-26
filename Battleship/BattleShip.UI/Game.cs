namespace BattleShip.UI
{
    public class Game
    {
        public Player Player1 { get; set; } = new Player() { Index = 1 };
        public Player Player2 { get; set; } = new Player() { Index = 2 };
        public bool Player1Turn { get; set; } = false;
        public bool GameOver { get; set; } = false;
       

    }
}
