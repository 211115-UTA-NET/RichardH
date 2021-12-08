using System;
using System.Collections.Generic;


namespace RockPaperScissorsApp.App
{
    internal class Records
    {
        internal enum Result
        {
            Win,
            Loss,
            Draw
        }

        internal Result res { get; }
        internal string player { get; }
        internal string computer { get; }
        internal DateTime time { get; }

        public Records(int res, string player, string computer)
        {
            this.res = (Result)res;
            this.player = player;
            this.computer = computer;
            this.time = DateTime.Now;
        }




    }
}
