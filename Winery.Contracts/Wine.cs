using System.Collections.Generic;

namespace Winery.Contracts
{
    public class Wine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WineType Type { get; set; }
        public string Country { get; set; }
        public int Score { get; set; }
        public int Price { get; set; }
        public int Year { get; set; }
        public int Rank { get; set; }
    }

    public enum WineType
    {
        Red,
        White
    }
}
