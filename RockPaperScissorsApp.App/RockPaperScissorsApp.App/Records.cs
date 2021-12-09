namespace RockPaperScissorsApp.App
{
    internal class Records
    {
        // XmlSerializer (and often other serializers)
        //  they expect classes that have a zero-argument constructor
        //  and they can fill in public properties via their setters.
        // A class that ONLY has a zero-argument constructor and get/set
        //  is sometimes called a POCO (plain old CLR object)

        // c# has "attributes" - they don't do anything by themselves,
        //  they're there for some other code to notice then add and make changes to their own behavior.


        // Fields
        internal enum Result
        {
            Win,
            Loss,
            Draw
        }
        internal Result Res { get; set; }
        internal string PlayerName { get; set; }
        internal string PlayerThrow { get; set; }
        internal string ComputerThrow { get; set; }
        internal DateTime Time { get; set; }


        // Constructor
        public Records(int Res, string PlayerName, string Player, string Computer, DateTime Time)
        {
            this.Res = (Result)Res;
            this.PlayerName = PlayerName;
            this.PlayerThrow = Player;
            this.ComputerThrow = Computer;
            this.Time = Time;
        }

        public Records(Serialization.Records xmlRecords)
        {
            if (xmlRecords.Res == null) 
            {
                throw new ArgumentNullException("Result cannot be null", nameof(xmlRecords));
            }
            else
            {
                this.Res = (Result) Enum.Parse(typeof(Result), xmlRecords.Res);
            }
            this.PlayerName = xmlRecords.PlayerName ?? throw new ArgumentNullException("playerName cannot be null", nameof(xmlRecords));
            this.PlayerThrow = xmlRecords.PlayerThrow ?? throw new ArgumentNullException("player move cannot be nul", nameof(xmlRecords));
            this.ComputerThrow = xmlRecords.ComputerThrow ?? throw new ArgumentNullException("CPU move cannot be null", nameof(xmlRecords));
            this.Time = xmlRecords.Time;        
        }
    }
}
 