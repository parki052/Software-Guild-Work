using SG_RPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS.Actions
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
