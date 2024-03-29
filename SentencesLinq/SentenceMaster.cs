﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SentencesLinq
{
    public class SentenceMaster
    {
        public string LongestWord { get; set; } = String.Empty;

        public static string[] GetSentences()
        {
            string[] strings =
             {
                "You only live forever in the lights you make ",
                "When we were young we used to say ",
                "That you only hear the music when your heart begins to break ",
                "Now we are the kids from yesterday "
            };

            return strings;
        }

        public int GetWordsCountInSentence(string item)
        {
            //regex match
            int wordsInSentence = item.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Count();

            //Console.WriteLine(wordsInSentence);
            return wordsInSentence;

        }

        public string[] SplitSentenceIntoArray(string item)
        {
            return item.Split(' ').Select(w => w).ToArray();
        }

        public void PrintWordStartsWithVowel(string[] words)
        {
            string[] vowels = { "a", "e", "i", "o", "u" };
            var selectedWords = words.Where(w => vowels.Any(e => w.StartsWith(e)));
            //  Where(w => w.StartsWith(vowels.Any(e => e.ToList().ForEach(i => return i )))));
            selectedWords.ToList().ForEach(i => Console.WriteLine(i));
        }

        public string GetLongestWord(string[] sentences)
        {
            string longestWord;

            foreach (var item in sentences)
            {
                longestWord = SplitSentenceIntoArray(item).OrderByDescending(w => w.Length).First();
                //TODO: ПЕРЕРОБИТИ НА ЛІКН
                if (LongestWord.Length < longestWord.Length)
                {
                    LongestWord = longestWord;
                }

            }
            return LongestWord;
        }

        public int AverageWordCount(string[] sentences)
        {
            int totalCount = default(Int32);

            sentences.ToList().ForEach(i => totalCount += GetWordsCountInSentence(i));
            
            return totalCount / sentences.Count();
        }

        public void WordsInAlpabeticOrder(string[] sentences)
        {
            string joinedSentences = String.Concat(sentences);

            var sentencesArr = SplitSentenceIntoArray(joinedSentences);
            
            RemoveDuplicate(sentencesArr).OrderBy(w => w).ToList().ForEach(w => Console.Write(w + " "));
   
        }

        public IEnumerable<string> RemoveDuplicate(string[] words)
        {
            var listWithoutDuplicates = words.Distinct(StringComparer.CurrentCultureIgnoreCase)
                                             .Where(w => string.IsNullOrEmpty(w) != true)
                                             .ToList();


            return listWithoutDuplicates;
        }

    }
}

