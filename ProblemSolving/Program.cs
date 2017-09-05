using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemSolving
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] grades = new int[n];
            for (int grades_i = 0; grades_i < n; grades_i++)
            {
                grades[grades_i] = Convert.ToInt32(Console.ReadLine());
            }
            int[] result = solve(grades);
            Console.WriteLine(String.Join("\n", result));
            Console.ReadKey();
        }

        private static int[] solve(int[] grades)
        {
            var result = new List<int>();

            for (int i = 0; i < grades.Length; i++)
            {
                var roundTo = 5 - (grades[i] % 5);
                var rounded = grades[i] + roundTo;
                if (roundTo > 0 && rounded >= 40)
                {
                    if (roundTo < 3)
                        result.Add(rounded);
                    else if (roundTo >= 3)
                        result.Add(grades[i]);
                }
                else if(rounded < 40)
                    result.Add(grades[i]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Pangrams are sentences constructed by using every letter of the alphabet at least once
        /// </summary>
        private static void Pangrams(string input)
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


        private static void StringInString(string input, string key)
        {
            var charArray = key.ToCharArray();
            var searchArray = input.ToCharArray();

            int n = 0;

            for (int i = 0; i < searchArray.Length;)
            {
                if (charArray.Length == n + 1)
                    break;

                if (searchArray[i] == charArray[n])
                {
                    while (searchArray[i] == charArray[n])
                    {
                        i++;
                        if (i >= searchArray.Length || charArray[n + 1] == searchArray[i])
                            break;
                    }

                    n++;
                    continue;
                }

                i++;
            }

            if (charArray.Length == n + 1)
                Console.WriteLine("YES");
            else
                Console.WriteLine("NO");
        }


        /// <summary>
        /// Each unencrypted letter is replaced with the letter occurring  spaces after it when 
        /// listed alphabetically. Think of the alphabet as being both case-sensitive and circular;
        /// if rotates past the end of the alphabet, it loops back to the beginning
        /// </summary>
        private static void CaesarCipher(int length, string input, int rotateBy)
        {
            //65(A)-90(Z) | 97(a)-122(z) | total: 26
            var charArray = input.ToCharArray();
            var output = new StringBuilder();

            int rotationIndex = rotateBy % 26;

            foreach (char item in charArray)
            {
                var cipherAscii = item + rotationIndex;

                if (item >= 65 && item <= 90)
                {
                    if (cipherAscii > 90)
                        output.Append((char)(64 + (cipherAscii - 90)));
                    else
                        output.Append((char)cipherAscii);
                }
                else if (item >= 97 && item <= 122)
                {
                    if (cipherAscii > 122)
                        output.Append((char)(96 + (cipherAscii - 122)));
                    else
                        output.Append((char)cipherAscii);
                }
                else
                    output.Append(item);
            }

            Console.WriteLine("{0}", output.ToString());
        }
    }
}
