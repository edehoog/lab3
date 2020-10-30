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
                line.Add("This is a? test that!! has, 4  words..");
                startIdx.Add(4);
                expectedResults.Add(7);
                line.Add("This is a? test that!! has, 'many' many many many words.. This is the start of a second line.");
                startIdx.Add(11);
                expectedResults.Add(16);
                line.Add("Sir, He fell upon me ere admitted. Then Three kings I had newly feasted, and did want, Of what I was i' th' morning; but next day I told him of myself, which was as much As to have ask'd him pardon. Let this fellow Be nothing of our strife; if we contend, Out of our question wipe him.");
                startIdx.Add(0);
                expectedResults.Add(59);
                line.Add("This-is-a test");
                startIdx.Add(0);
                expectedResults.Add(2);
                line.Add("Nay, hear them, Antony. Fulvia perchance is angry; or who knows If the scarce-bearded Caesar have not sent His pow'rful mandate to you: 'Do this or this; Take in that kingdom and enfranchise that; Perform't, or else we damn thee.'");
                startIdx.Add(0);
                expectedResults.Add(40);
                line.Add("Perchance? Nay, and most like, [You] must not stay here longer; your dismission Is come from Caesar; therefore hear it, Antony.  Where's Fulvia's process? Caesar's I would say? Both?  Call in the messengers. As I am Egypt's Queen, Thou blushest, Antony, and that blood of thine Is Caesar's homager. Else so thy cheek pays shame When shrill-tongu'd Fulvia scolds. The messengers!");
                startIdx.Add(0);
                expectedResults.Add(61);

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