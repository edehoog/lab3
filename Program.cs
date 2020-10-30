using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // map and mutex for thread safety
            Mutex mutex = new Mutex();
            Dictionary<string, int> wcountsSingleThread = new Dictionary<string, int>();
            Dictionary<string, int> wcountsMultiThreaded = new Dictionary<string, int>();

            Stopwatch stopwatch = new Stopwatch();

            var filenames = new List<string> {
                /*"data/testing_purposes.txt",*/
                "data/shakespeare_antony_cleopatra.txt",
                "data/shakespeare_hamlet.txt",
                "data/shakespeare_julius_caesar.txt",
                "data/shakespeare_king_lear.txt",
                "data/shakespeare_macbeth.txt",
                "data/shakespeare_merchant_of_venice.txt",
                "data/shakespeare_midsummer_nights_dream.txt",
                "data/shakespeare_much_ado.txt",
                "data/shakespeare_othello.txt",
                "data/shakespeare_romeo_and_juliet.txt",
                /*"data/shakespeare_king_john.txt",
                "data/shakespeare_king_lear.txt"*/
            };

            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE THREAD
            //=============================================================

            stopwatch.Start();
            foreach (string name in filenames)
            {
                lab3.HelperFunctions.CountCharacterWords(name, mutex, wcountsSingleThread);
            }

            var finalListSingle = lab3.HelperFunctions.SortCharactersByWordcount(wcountsSingleThread);

            lab3.HelperFunctions.PrintListofTuples(finalListSingle);
            stopwatch.Stop();

            long duration = stopwatch.ElapsedMilliseconds;
            string formatedTime = String.Format("Miliseconds: {0}", duration);
            Console.WriteLine("Runtime: " + formatedTime);
            stopwatch.Reset();

            Console.WriteLine("SingleThread is Done!");
            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN MULTIPLE THREADS
            //=============================================================

            List<Thread> threads = new List<Thread>();

            int count = 0;

            stopwatch.Start();
            foreach (string name in filenames)
            {  
                Thread thread = new Thread(() => lab3.HelperFunctions.CountCharacterWords(name, mutex, wcountsMultiThreaded));
                thread.Name = string.Format("Thread{0}", count + 1);
                thread.Start();
                threads.Add(thread);
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            var finalList = lab3.HelperFunctions.SortCharactersByWordcount(wcountsMultiThreaded);

            lab3.HelperFunctions.PrintListofTuples(finalList);
            stopwatch.Stop();

            duration = stopwatch.ElapsedMilliseconds;
            formatedTime = String.Format("Miliseconds: {0}", duration);
            Console.WriteLine("Runtime: " + formatedTime);

            Console.WriteLine("MultiThread is Done!");
            return;
        }
    }
}
