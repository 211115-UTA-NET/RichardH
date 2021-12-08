using System;

namespace RockPaperScissorsApp.App
{
    internal partial class Game
    {
        private enum Cast
        {
            Rock,
            Paper,
            Scissors
        }

        private int computer = -1;
        private int player = -1;

        List<Records> allRecords = new List<Records>();


        public void Summary()
        {
            Console.WriteLine("Time\t\tPlayer\t\tcomputer\t\tResult");
            foreach (var record in allRecords)
            {
                Console.WriteLine($"{record.time.ToShortDateString}\t{record.player}\t\t{record.computer}\t\t{record.res}");
            }
            
        }

        internal void PlayRound()
        {
            computer = getComputer();
            player = getPlayer();

            checkWin(computer, player);         
        }

        private int getComputer()
        {
            Random rnd = new Random();
            return rnd.Next(0,3);
        }

        private int getPlayer()
        {
            Console.Clear();
            Console.WriteLine("Playing a new game");
            Console.WriteLine("Select your sign:");
            Console.WriteLine("[0] - Rock");
            Console.WriteLine("[1] - Paper");
            Console.WriteLine("[2] - Scissors");

            string? playerIn = Console.ReadLine();
            int playInt;

            if (Int32.TryParse(playerIn, out int testInt))
            {
                playInt = Int32.Parse(playerIn);

                if ((0 > playInt) || (playInt > 2))
                {
                    Console.WriteLine("Not a valid selection.");
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("Not a valid selection.");
                return -1;
            }

            return playInt;
        }

        private void checkWin(int computer, int player)
        {
            if (player == -1)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine($"The Computer Chose: {(Cast)computer}");
            Console.WriteLine($"You Chose: {(Cast)player}");
            int res;
            
            if (((computer + 1) % 3) == player)
            {
                Console.WriteLine("You WIN!");
                res = 0;
            }
            else if (((player + 1) % 3) == computer)
            {
                Console.WriteLine("You LOSE!");
                res = 1;

            }
            else
            {
                Console.WriteLine("DRAW");
                res = 2;
            }
            var record = new Records(res, ((Cast)player).ToString(), ((Cast)computer).ToString());
            allRecords.Add(record);

        }
    }
}
