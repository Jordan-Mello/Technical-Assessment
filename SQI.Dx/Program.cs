using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQI.Dx
{
    class Program
    {
        public string FILENAME = string.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SQI.Dx");

            load_sample_ids(@"input.txt", (args) => {
                foreach (string line in args)
                {
                    blabla(line);
                }
            });
        }

        public static void load_sample_ids(string FILENAME, Action<IEnumerable<string>> func)
        {
            // Read Input
            FILENAME = Path.Combine(Directory.GetCurrentDirectory(), @"input.txt");
            Console.WriteLine(FILENAME);
            var x = File.ReadLines(FILENAME);
            func(x);
        }

        public class ExpectedIDs
        {
            public string UniqueID; public string Protocol; public string sampleID; public string sampleGroup;

            public string print() { return string.Format("{0} | {1} | {2} | {3} ", UniqueID, Protocol, sampleID, sampleGroup); }
        }

        private static async Task blabla(dynamic bla)
        {
            Dictionary<string, ExpectedIDs> ExpectedIDs = new Dictionary<string, ExpectedIDs> {

                {"1", new ExpectedIDs(){ UniqueID = "1", Protocol = "ABC", sampleID = "S10D1245", sampleGroup = "Standard"}},
                {"2", new ExpectedIDs(){ UniqueID = "2", Protocol = "ABC", sampleID = "S20D1245", sampleGroup = "Standard"}},
                {"3", new ExpectedIDs(){ UniqueID = "3", Protocol = "ABC", sampleID = "S30D1245", sampleGroup = "Standard"}},
                {"4", new ExpectedIDs(){ UniqueID = "4", Protocol = "ABD", sampleID = "C1POS01", sampleGroup = "Control"}},
                {"5", new ExpectedIDs(){ UniqueID = "5", Protocol = "ABD", sampleID = "C2POS01", sampleGroup = "Control"}},
                {"6", new ExpectedIDs(){ UniqueID = "6", Protocol = "ABD", sampleID = "C3POS01", sampleGroup = "Control"}},
                {"7", new ExpectedIDs(){ UniqueID = "7", Protocol = "COR", sampleID = "C2POS02", sampleGroup = "Control"}},
                {"8", new ExpectedIDs(){ UniqueID = "8", Protocol = "COV", sampleID = "C1POS01", sampleGroup = "Control"}},
            };

            Dictionary<string, string> ExpectedColor = new Dictionary<string, string> {

                {"Standard","Green"},
                {"Control", "Blue"},
                {"Sample", "White"},

            };

            var parts = (bla as string).Split(' ');
            foreach (var item in (ExpectedIDs.Values))
            {
                if (item.sampleID == parts[1])
                {
                    Console.WriteLine(ExpectedIDs[item.UniqueID].print());
                }
            }
        }
    }
}
