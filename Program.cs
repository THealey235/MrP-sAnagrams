﻿using System;

namespace MrPickeringsAnagramCreator;

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Input Plain Text: "); string plainText = Console.ReadLine();
        List<string> anagrams = Anagram.Jumble(plainText);
        foreach(string str in anagrams)
        {
            Console.WriteLine(str);
        }
        
        
    }
}