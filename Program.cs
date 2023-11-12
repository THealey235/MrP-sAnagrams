using System;

namespace MrPickeringsAnagramCreator;

class Program
{
    public static void Main(string[] args)
    {
        
        Console.Write("Input Plain Text: "); string plainText = Console.ReadLine();
        int ammountOfAnagrams = 1; ; //so the compiler doesn't shout at me
        Anagram.permutations = Anagram.FindPermutations(plainText);
        while (true){
            Console.Write($"How many angarams do you want generated ({Anagram.permutations} can be generated): "); string anagramsDesired = Console.ReadLine();
            try
            {
                ammountOfAnagrams = int.Parse(anagramsDesired);
                break;
            }
            catch (Exception) { Console.WriteLine("Input must be a 32-bit integer."); }
        }
        List<string> anagrams = Anagram.Jumble(plainText, ammountOfAnagrams);
        foreach(string str in anagrams)
        {
            Console.WriteLine(str);            
        } 
    }
}