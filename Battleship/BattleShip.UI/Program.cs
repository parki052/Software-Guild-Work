namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.SetSettings();

            Gameflow.Run();
        }


    }
}
