using System;

namespace BattleShip.UI
{
    public static class RNG
    {
        public static Random rand = new Random();

        public static int GetRandomNumber(int lowerBound, int higherBound)
        {
            higherBound += 1;

            return rand.Next(lowerBound, higherBound);          
        }
    }
}
