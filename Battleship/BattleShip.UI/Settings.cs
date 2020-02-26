using System;

namespace BattleShip.UI
{
    static class Settings
    {
        public const ConsoleColor TextColor = ConsoleColor.Cyan;
        public const ConsoleColor InputTextColor = ConsoleColor.White;

        internal static void SetSettings()
        {
            Console.CursorSize = 90;
            Console.Title = "                                                                    **BattleShip**";

            Console.SetWindowSize(Console.LargestWindowWidth / 3, Console.LargestWindowHeight / 2);
            Console.SetBufferSize(Console.LargestWindowWidth / 3, Console.LargestWindowHeight / 2);

            Console.ForegroundColor = TextColor;
        }
    }
}
