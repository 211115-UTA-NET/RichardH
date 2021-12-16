using System.Xml.Serialization;

namespace RockPaperScissorsApp.App
{
    internal partial class Game
    {

        // Fields
        private enum Cast
        {
            Rock,
            Paper,
            Scissors
        }
        private int computer = -1;
        private int player = -1;
        private string playerName;
        private List<Round> allRecords = new List<Round>();
        public XmlSerializer Serializer { get; } = new(typeof(List<Serialization.Records>));


        // Constructors
        public Game(string playerName, List<Round>? allRecords = null)
        {
            this.playerName = playerName;
            if (allRecords != null)
            {
                this.allRecords = allRecords;
            }    
        }


        // Methods
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
            bool loop = true;
            int playInt = -1;

            while (loop == true)
            {
                Console.Clear();
                Console.WriteLine("Playing a new game");
                Console.WriteLine("Select your sign:");
                Console.WriteLine("[0] - Rock");
                Console.WriteLine("[1] - Paper");
                Console.WriteLine("[2] - Scissors");

                string? playerIn = Console.ReadLine();
               
                if (Int32.TryParse(playerIn, out int testInt))
                {
                    testInt = Int32.Parse(playerIn);

                    if ((0 > testInt) || (testInt > 2))
                    {
                        Console.WriteLine("Not a valid selection. Please try again");
                        Console.WriteLine("Press Enter to continue.");
                        Console.ReadLine();
                        return playInt;
                    }

                }
                else
                {
                    Console.WriteLine("Not a valid selection. Please try again");
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
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
            var record = new Round(res, playerName, ((Cast)player).ToString(), ((Cast)computer).ToString(), DateTime.Now);
            allRecords.Add(record);
        }


        public void Summary()
        {
            Console.WriteLine("Player\t\tTime\t\tPlayer\t\tComputer\t\tResult");
            foreach (var record in allRecords)
            {
                Console.WriteLine($"{record.PlayerName}\t\t{record.Time.ToShortDateString()}\t{record.PlayerThrow}\t\t{record.ComputerThrow}\t\t{record.Res}");
            }
        }


        public string SerializeAsXML()
        {


            var xmlRecords = new List<Serialization.Records>();

            foreach (Round record in allRecords)
            {
                //var xml = new Xml.Records();
                //xml.When = record.time;
                //xml.PlayerThrow = record.playerThrow;
                //xml.ComputerThrow = record.computerThrow;


                var xml = new Serialization.Records();
                {
                    xml.Time = record.Time;
                    xml.PlayerName = record.PlayerName;
                    xml.ComputerThrow = record.ComputerThrow;
                    xml.PlayerThrow = record.PlayerThrow;
                    xml.Res = record.Res.ToString();
                }

                xmlRecords.Add(xml);

            }

            var stringwriter = new StringWriter();
            Serializer.Serialize(stringwriter, xmlRecords);
            stringwriter.Close();
            return stringwriter.ToString();
        }
    }
}
