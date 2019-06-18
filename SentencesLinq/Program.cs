using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentencesLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            SentenceMaster master = new SentenceMaster();
            var sentences = SentenceMaster.GetSentences();

            //Calculate of the word count of each sentences
            foreach (var item in sentences)
            {
                Console.WriteLine(master.GetWordsCountInSentence(item)); //delegate create mb
            }
            Console.WriteLine(new string('*',40));

            //Split the sentences into an array of words and select the ones that start with a vowel
            foreach (var item in sentences)
            {
                master.PrintWordStartsWithVowel(master.SplitSentenceIntoArray(item));
            }
            Console.WriteLine(new string('*', 40));

            //Find the longest word
            Console.WriteLine(master.GetLongestWord(sentences));

            //Display the average word count of the sentences
            Console.WriteLine(master.AverageWordCount(sentences));
           
            





            Console.ReadKey();
        }

    }
}
