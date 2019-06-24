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
            foreach (var sentence in sentences)
            {
                Console.WriteLine(master.GetWordsCountInSentence(sentence)); //delegate create mb
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

            
            Console.WriteLine(master.AverageWordCount(sentences));

            //Put the words into alphabetical order and remove the duplicates (case insensitive)
            master.WordsInAlpabeticOrder(sentences);


            Console.ReadKey();
        }

    }
}
