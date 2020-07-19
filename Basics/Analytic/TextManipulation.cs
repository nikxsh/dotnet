using System;
using System.Linq;

namespace Analytic
{
    public class TextManipulation
    {
        public TextManipulation()
        {
            //Repeated String
            Problem1("aba", 10);
            Problem1("a", 1000000000000);
            Problem1("cfimaakj", 554045874191);
            Problem1("d", 554045874191); 
        }

        /// <summary>
        /// Lilah has a string, S, of lowercase English letters that she repeated infinitely many times.
        /// Given an integer, N, find and print the number of letter a's in the first N letters of Lilah's infinite string.
        /// For example, if the string S = 'abcac' and N = 10, the substring we consider is abcacabcac, the first  10 characters of her infinite string. 
        /// There are 4 occurrences of 'a' in the substring.
        ///     Case 1: 
        ///         S = aba
        ///         N = 10
        ///         Substring = abaabaabaa
        ///         Output > 7 
        ///     Case 2: 
        ///         S = a
        ///         N = 1000000000000
        ///         Output > 1000000000000
        ///     Case 2: 
        ///         S = d
        ///         N = 1000000000000
        ///         Output > 0
        /// </summary>
        public void Problem1(string s, long n)
        {
            long output = 0;

            Func<string, long> CountOccurrences = str => str.Count(x => x == 'a');

            var strLength = s.Length;

            if (CountOccurrences(s) == 0)
            {
                Console.WriteLine(0);
                return;
            }

            if (strLength == 1)
            {   
                Console.WriteLine(n);
                return;
            }

            long availableSlots = n / strLength;

            output = availableSlots * CountOccurrences(s);

            var remainingSlots = (int)(n % strLength);

            var croppedString = s.Substring(0, remainingSlots);

            output += CountOccurrences(croppedString); //aba 10/3 = 3, 10%3 = 1 > 3 * 2 = 6 + 1, 

            Console.WriteLine(output);
        }
    }
}
