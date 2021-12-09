using System.Xml.Serialization;

namespace RockPaperScissorsApp.App.Serialization
{
    public class Records
    {
        [XmlAttribute]
        public string? Res { get; set; }
        public string? PlayerName { get; set; }
        public string? PlayerThrow { get; set; }
        public string? ComputerThrow { get; set; }
        public DateTime Time { get; set; }
    }
}
