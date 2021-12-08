// App to play rock-paper-scissors with the computer

// MVP
// - play multiple rounds without being forced to exit
// - get a sumary/record of all the rounds played so far

// Stretch goal features
// - persistence (save data from previous runs)
// - more complex variants of RPS - RPS+lizard+spock
// - logging (to help with debuging the app if something goes wrong)
// - multi-player
// - difficulty setting for the computer (remembers your moves, and attemps to predict your next throw)
// - move timer
// - rempove saved records

// in general, we want to write something simple, but 
//      in a way that allows for extending it or genrealizing it in the future.




using System;

namespace RockPaperScissorsApp.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the RockPaperScissors App");
            Console.WriteLine();

            var game = new Game();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Play a round? y/n");

                string? input;

                input = Console.ReadLine();
                if (input != "y") { break; }

                game.PlayRound();
            }

            game.Summary();

        }
    }
}