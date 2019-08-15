using System;
using System.Collections.Generic;
using Winery.Contracts;

namespace Winery.Persistence
{
    public class WineData
    {

        public static List<Wine> Get()
        {
            var wineryData = new List<Wine>();
            var random = new Random();
            var maxSize = 57;

            for (int i = 1, j = i + 1; i <= maxSize; i++, j++)
            {
                wineryData.Add(new Wine
                {
                    Id = i,
                    Name = $"Wine {j}",
                    Type = j % 2 == 0 ? WineType.Red : WineType.White,
                    Country = $"Country {j}",
                    Score = random.Next(99),
                    Rank = j,
                    Price = random.Next(1000, 100000),
                    Year = random.Next(1950, 2018)
                });
            }
            return wineryData;
        }
    }
}
