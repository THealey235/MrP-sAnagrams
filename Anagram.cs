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
        public static int permutations;

        //generates a string of _s the length of plainText
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

        //Executes the formula used to find all possible unique anagrams: (lengthOfWord)! / ((appearancesOfUniqueLetter)! x .... )
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
        public static int FindPermutations(string plainText)
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

        static int findCharNewIndex(int plainTextLength, char[] anagram, int openSpace, int openSpaces)
        {
            for (int i = 0; i < plainTextLength; i++)
            {
                if (anagram[i] == '_')
                {
                    //goes through each open space until we are on the one randomly picked
                    //essentially just counts down to the random space
                    if (openSpace != 0)
                    {
                        openSpace--;
                    }
                    else
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        //Creates/jumbles the anagrams. Finds all the anagrams
        public static List<string> Jumble(string plainText, int ammountOfAnagrams)
        {
            permutations = (ammountOfAnagrams > permutations) ? permutations : ammountOfAnagrams;

            int plainTextLength = plainText.Length;
            List<string> anagrams = new List<string>();

            //used to skip an anagram permutation if we have already generated it
            bool skipAnagram = false;

            //using the very efficient brute force method
            while (anagrams.Count() < permutations)
            {
                char[] anagram = GeneratePlaceholder(plainTextLength).ToCharArray();

                foreach (char c in plainText)
                {
                    int charIndex = plainText.IndexOf(c);
                    int openSpace;
                    int charNewIndex;
                    do
                    {
                        int openSpaces = 0;
                        foreach (char character in anagram)
                        {
                            if (character == '_')
                            {
                                openSpaces++;
                            }
                        }

                        openSpace = rand.Next(openSpaces);
                        Console.WriteLine(openSpace);
                        Console.WriteLine(anagram);
                        charNewIndex = findCharNewIndex(plainTextLength, anagram, openSpace, openSpaces);
                    }
                    while (openSpace == charIndex);
                    anagram[charNewIndex] = c;
                }
                string finishedAnagram = new string(anagram);
                foreach (string i in anagrams) { if (i == finishedAnagram) { skipAnagram = true; break; } }
                if (skipAnagram)
                {
                    skipAnagram = false;
                    continue;
                }
                anagrams.Add(finishedAnagram);
            }
            return anagrams;
        }
    }
}
