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

using System.Xml.Serialization;

namespace RockPaperScissorsApp.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "../../../history.xml";
            
            Console.WriteLine("Welcome to the RockPaperScissors App");
            Console.WriteLine();

            string? name = null;
            while (name == null || name.Length == 0)
            {
                Console.WriteLine("Please enter a valid username: ");
                name = Console.ReadLine();
            }

            List<Records> records = ReadHistoryFromFile(filePath);
            Game game = new Game(name, records);
            Console.WriteLine($"Welcome {name}");

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
            WriteHistoryToFile(game, filePath);
        }

        private static void WriteHistoryToFile(Game game, string filePath)
        {
            string xml = game.SerializeAsXML();
            File.WriteAllText(filePath, xml);
        }



        // any objects inside the CLR are automatically cleaned up by the GC
        //  when we're done with them.
        // But, some objects contain/handle resources outside the CLR,
        //  like StreamReader opening a file.
        // for those, you have to explicitly call the Close of Dispose method. 
        // Typically you do this in a finally block to be 100% sure that it will be called in all cases
        // .NET has a special interface called Idisposale that basically tells you
        //  "This is a class that needs to be Disposed when you're done."


        private static List<Records>? ReadHistoryFromFile(string filePath)
        {
            XmlSerializer serializer = new(typeof(List<Serialization.Records>));

            //using statement can be a block, or just one line.
            using (StreamReader? reader = new(filePath))
            {
                try
                {
                    var records = (List<Serialization.Records>?)serializer.Deserialize(reader);

                    if (records is null) throw new InvalidDataException();
                    return records.Select(x => new Records(x)).ToList();
                }
                catch (IOException)
                {
                    return null;
                }
            }
            ////or
            //using StreamReader? reader = new(filePath);
            //try
            //{
            //    var records = (List<Record>?)serializer.Deserialize(reader);
            //    return records;
            //}
            //catch (FileNotFoundException)
            //{
            //    return null;
            //}
        }


        //private static List<Records>? ReadHistoryFromFileOld(string filePath)
        //{
        //    XmlSerializer serializer = new(typeof(List<Records>));
        //    StreamReader reader = null;

        //    try
        //    {
        //        reader = new(filePath);
        //        var records = (List<Records>?)serializer.Deserialize(reader);
        //        return records;
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if (reader != null)
        //        {
        //            reader.Close();
        //        }
        //    }
        //}
    }
}