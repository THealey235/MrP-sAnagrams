using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace MrPickeringsAnagramCreator
{
    internal class Anagram
    {
        static Random rand = new Random();
        static string GeneratePlaceholder(int len)
        {
            string temp = "";
            for (int i = 0; i < len; i++){temp += "_";}
            return temp;
        }
        
        //Used in the FindPermutations function to find the total ammount of possible unique anagrams from an input
        static int Factorial(int num)
        {
            int total = 1;
            for (int i = num; i > 1; i--)
            {
                total *= i;
            }
            return total;
        }

        static int CalculatePermutations(List<int> list, int length)
        {
            int total = 0; ;
            foreach (int i in list)
            {
                if (total == 0)
                {
                    total = Factorial(i);
                }
                else
                {
                    total *= Factorial(i);
                }
            }
            return (Factorial(length) / total);
        }
        
        //Used in the FindPermutations function to check if the duplicates of that letter have already been found in the input
        static bool CheckForDuplicates(char c, List<char> list)
        {
            foreach (char character in list )
            {
                if (character == c)
                {
                    return true;
                }
            }
            return false;
        }

        //To find the total possible ammount of unique anagrams that can be created from an input
        static int FindPermutations(string plainText)
        {
            List<int> letterOccurance = new List<int>();
            List<char> lettersCheck = new List<char>();
            int total;
            for (int i = 0; i < plainText.Length; i++)
            {
                if (CheckForDuplicates(plainText[i], lettersCheck)) { continue; }
                total = 0;
                foreach (char j in plainText)
                {
                    if (plainText[i] == j) {total++;}
                }
                letterOccurance.Add(total);
                lettersCheck.Add(plainText[i]);
            }
            return CalculatePermutations(letterOccurance, plainText.Length);
        }

        //Creates/jumbles the anagrams. Finds all the anagrams
        public static List<string> Jumble(string plainText)
        {
            Console.WriteLine(FindPermutations(plainText));
            //pt = plain text
            int ptLength = plainText.Length;
            List<string> anagrams = new List<string>();
            string placeholder = GeneratePlaceholder(ptLength);
            char[] anagram = placeholder.ToCharArray();
            foreach (char c in plainText)
            {
                int charIndex = plainText.IndexOf(c);
                int charNewIndex;
                do
                {
                    charNewIndex = rand.Next(ptLength);
                }
                while (charNewIndex == charIndex || anagram[charNewIndex] != '_');
                anagram[charNewIndex] = c;
            }
            anagrams.Add(new string(anagram));

            return anagrams;
        }
    }
}
