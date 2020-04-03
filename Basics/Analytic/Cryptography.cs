using System;
using System.Text;

namespace Analytic
{
	public class Cryptography
	{
      /// <summary>
      /// Each unencrypted letter is replaced with the letter occurring  spaces after it when 
      /// listed alphabetically. Think of the alphabet as being both case-sensitive and circular;
      /// if rotates past the end of the alphabet, it loops back to the beginning
      /// </summary>
      public void CaesarCipher(int length, string input, int rotateBy)
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
