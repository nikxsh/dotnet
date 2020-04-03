using System;
using System.Linq;

namespace Analytic
{
   public class Alphabet
	{

      /// <summary>
      /// Pangrams are sentences constructed by using every letter of the alphabet at least once
      /// </summary>
      public void Pangrams(string input)
      {
         var alphabateBucket = new bool[26];
         var searchArray = input.ToCharArray();

         foreach (var item in searchArray)
         {
            if (item >= 97 && item <= 122)
            {
               var aplhaIndex = 25 - (122 - item);
               var bucketValue = alphabateBucket.ElementAt(aplhaIndex);
               if (bucketValue)
                  continue;
               else
                  alphabateBucket[aplhaIndex] = true;
            }
         }


         var isPangram = alphabateBucket.Aggregate((first, next) => next && first);

         if (isPangram)
            Console.WriteLine("pangram");
         else
            Console.WriteLine("not pangram");

      }
   }
}
