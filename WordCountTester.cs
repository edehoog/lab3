using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace lab3
{
    public class WordCountTester
    {
        static int Main(string[] args)
        {
            // put into seperate try catch blocks
            try
            {


                //=================================================
                // Implement your tests here. Check all the edge case scenarios.
                // Create a large list which iterates over WCTester
                //=================================================
                List<string> line = new List<string>();
                List<int> startIdx = new List<int>();
                List<int> expectedResults = new List<int>();

                line.Add("This is a test that has 8 words.");
                startIdx.Add(0);
                expectedResults.Add(8);
                line.Add("This is a? test that!! has, 8  words..");
                startIdx.Add(0);
                expectedResults.Add(8);
                line.Add("This is a? test that!! has, 8  words..");
                startIdx.Add(4);
                expectedResults.Add(7);
                line.Add("This is a? test that!! has, 'many' many many many words.. This is the start of a second line.");
                startIdx.Add(11);
                expectedResults.Add(16);

                int count = 0;
                foreach(string element in line)
                {
                    WCTester(line[count], startIdx[count], expectedResults[count]);
                    count++;
                }
                

            }
            catch (UnitTestException e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }


        /**
         * Tests word_count for the given line and starting index
         * @param line line in which to search for words
         * @param start_idx starting index in line to search for words
         * @param expected expected answer
         * @throws UnitTestException if the test fails
         */
        static void WCTester(string line, int start_idx, int expected)
        {
            int result = lab3.HelperFunctions.WordCount(ref line, start_idx);

            Console.WriteLine("Test results:");
            Console.WriteLine("WordCount return value: " + result);
            Console.WriteLine("Expected return value: " + expected);

            //=================================================
            // Implement: comparison between the expected and
            // the actual word counter results
            //=================================================

            if (result != expected)
            {
                throw new lab3.UnitTestException(ref line, start_idx, result, expected, String.Format("UnitTestFailed: result:{0} expected:{1}, line: {2} starting from index {3}", result, expected, line, start_idx));
            }

        }
    }
}