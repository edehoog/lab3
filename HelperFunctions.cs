using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Security.Cryptography;

namespace lab3
{
    public class HelperFunctions
    {
        /**
         * Counts number of words, separated by spaces, in a line.
         * @param line string in which to count words
         * @param start_idx starting index to search for words
         * @return number of words in the line
         */
        public static int WordCount(ref string line, int start_idx)
        {
            if (line == String.Empty)
            {
                return 0;
            }
            string lineSection = line.Substring(start_idx);

            char delimiterChars = ' ';

            string[] words = lineSection.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            int count = words.Length;

            return count;
        }
        /**
        * Reads a file to count the number of words each actor speaks.
        *
        * @param filename file to open
        * @param mutex mutex for protected access to the shared wcounts map
        * @param wcounts a shared map from character -> word count
        */
        public static void CountCharacterWords(string filename, Mutex mutex, Dictionary<string, int> wcounts)
        {
            string line;  // for storing each line read from the file
            string character = "";  // empty character to start
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            while ((line = file.ReadLine()) != null)
            {
                // Is the line a dialogueLine?
                int dialogueIndex = IsDialogueLine(line, ref character); 

                // If yes, get the index and the character name.
                // if index > 0 and character not empty
                if(dialogueIndex > 0 && character != string.Empty)
                {
                    // get the word counts
                    int wordCount = WordCount(ref line, dialogueIndex);

                    mutex.WaitOne();
                    // if the key exists, update the word counts
                    if (wcounts.ContainsKey(character))
                    {
                        wcounts[character] += wordCount;
                    }
                    //  else add a new key-value to the dictionary
                    else
                    {
                        wcounts[character] = wordCount;
                    }
                    mutex.ReleaseMutex();
                }
            }
            //Character reset
            character = "";
            // Close the file
            file.Close();
        }
        /**
         * Checks if the line specifies a character's dialogue, returning
         * the index of the start of the dialogue.  If the
         * line specifies a new character is speaking, then extracts the
         * character's name.
         *
         * Assumptions: (doesn't have to be perfect)
         *     Line that starts with exactly two spaces has
         *       CHARACTER. <dialogue>
         *     Line that starts with exactly four spaces
         *       continues the dialogue of previous character
         *
         * @param line line to check
         * @param character extracted character name if new character,
         *        otherwise leaves character unmodified
         * @return index of start of dialogue if a dialogue line,
         *      -1 if not a dialogue line
         */
        static int IsDialogueLine(string line, ref string character)
        {
            //can debug but it is completed to an extent that is usable for the sake of this lab

            // new character
            if (line.Length >= 3 && line[0] == ' '
                && line[1] == ' ' && line[2] != ' ')
            {
                // extract character name
                int start_idx = 2;
                int end_idx = 3;
                while (end_idx <= line.Length && line[end_idx - 1] != '.')
                {
                    ++end_idx;
                }
                // no name found
                if (end_idx >= line.Length)
                {
                    return 0;
                }
                // extract character's name
                character = line.Substring(start_idx, end_idx - start_idx - 1);
                return end_idx;
            }

            // previous character
            if (line.Length >= 5 && line[0] == ' '
                && line[1] == ' ' && line[2] == ' '
                && line[3] == ' ' && line[4] != ' ')
            {
                // continuation
                return 4;
            }
            return 0;
        }

        /**
         * Sorts characters in descending order by word count
         *
         * @param wcounts a map of character -> word count
         * @return sorted vector of {character, word count} pairs
         */
        public static List<Tuple<int, string>> SortCharactersByWordcount(Dictionary<string, int> wordcount)
        {
            // Implement sorting by word count here
            List<Tuple<int, string>> sortedByValueList = new List<Tuple<int, string>>();

            foreach (KeyValuePair<string, int> pair in wordcount.OrderByDescending(key => key.Value))
            {
                sortedByValueList.Add(Tuple.Create(pair.Value, pair.Key));
            }

            return sortedByValueList;
        }
        /**
         * Prints the List of Tuple<int, string>
         *
         * @param sortedList
         * @return Nothing
         */
        public static void PrintListofTuples(List<Tuple<int, string>> sortedList)
        { 
            // Implement printing here
            foreach(Tuple<int, string> tuple in sortedList)
            {
                Console.WriteLine("Word count: {0} - Character: {1}", tuple.Item1.ToString(), tuple.Item2);
            }

        }
    }
}
