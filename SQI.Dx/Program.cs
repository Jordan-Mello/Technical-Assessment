using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQI.Dx
{
    public class Program
    {
        private string FILENAME = string.Empty;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SQI.Dx");
            // Assign strings for final output storage
            string colors = "";
            string correctSamples = "";
            LoadSampleIDs(@"input.txt", async (args) => {
                foreach (string line in args)
                {
                    // Avoid issues with print order from line-by-line processing by returning strings & accumulating
                    // Curious as to how asynchronicity would benefit program. It's likely I do not have enough experience with multithreading to realize
                    correctSamples += VerifyExpectedIDs(line);
                    colors += VerifyColors(line);
                }
            });
            Console.WriteLine(colors + correctSamples);
        }

        public static void LoadSampleIDs(string FILENAME, Action<IEnumerable<string>> func)
        {
            // Read input 
            FILENAME = Path.Combine(Directory.GetCurrentDirectory(), @"input.txt");
            Console.WriteLine(FILENAME);
            var x = File.ReadLines(FILENAME);
            func(x);
        }
        public class ExpectedIDs
        {
            public string UniqueID; public string Protocol; public string sampleID; public string sampleGroup;
            public string print() { return string.Format("{0} | {1} | {2} | {3} ", UniqueID, Protocol, sampleID, sampleGroup);             }
        }
        // Split verification into two methods - doing both in one is too complex and unnecessary
        public static string VerifyColors(dynamic givenValues)
        {
            Dictionary<string, string> ExpectedColor = new Dictionary<string, string>
            {
                {"Standard","Green"},
                {"Control", "Blue"},
                {"Sample", "White"},
            };
            // Assign givenValuesArray as individual components
            // Maybe give givenValuesArray individual strings as indexing is becoming unclear
            var givenValuesArray = (givenValues as string).Split(' ');
            string colors = "";
            foreach (KeyValuePair<string, string> item in ExpectedColor)
            {
                // Use length 
                if (givenValuesArray.Length > 2)
                {
                    if (givenValuesArray[2] == item.Key)
                    {
                        colors = string.Format("{0} | {1} | {2}\n", givenValuesArray[1], item.Key, item.Value);
                        break;
                    }
                }
            }
            return colors;
        }
        public static string VerifyExpectedIDs(dynamic givenValues)
        {
            var givenValuesArray = (givenValues as string).Split(' ');
            Dictionary<string,ExpectedIDs> ExpectedIDs = new Dictionary<string, ExpectedIDs>
            {
                {"1", new ExpectedIDs(){ UniqueID = "1", Protocol = "ABC", sampleID = "S10D1245", sampleGroup = "Standard"}},
                {"2", new ExpectedIDs(){ UniqueID = "2", Protocol = "ABC", sampleID = "S20D1245", sampleGroup = "Standard"}},
                {"3", new ExpectedIDs(){ UniqueID = "3", Protocol = "ABC", sampleID = "S30D1245", sampleGroup = "Standard"}},
                {"4", new ExpectedIDs(){ UniqueID = "4", Protocol = "ABD", sampleID = "C1POS01", sampleGroup = "Control"}},
                {"5", new ExpectedIDs(){ UniqueID = "5", Protocol = "ABD", sampleID = "C2POS01", sampleGroup = "Control"}},
                {"6", new ExpectedIDs(){ UniqueID = "6", Protocol = "ABD", sampleID = "C3POS01", sampleGroup = "Control"}},
                {"7", new ExpectedIDs(){ UniqueID = "7", Protocol = "COR", sampleID = "C2POS02", sampleGroup = "Control"}},
                {"8", new ExpectedIDs(){ UniqueID = "8", Protocol = "COV", sampleID = "C1POS01", sampleGroup = "Control"}},
            };
            string correctSamples = "";
            /*
            Verify input line by line against values in database
            "OR, a broad match is required based on a protocol configuration that is provided", so check protocol (not within scope?

            Is protocol out of scope? 4 and 8 have identical sample IDs and sample groups, with different protocols. There's no way to tell
            if you should print ABD or COV for C1POS01, so I'm going to assume it's just the first match.
            */
            foreach (var item in ExpectedIDs.Values)
            {
                // Need alternative check for empty array values
                if (givenValuesArray.Length > 2)
                {
                    if (item.sampleID == givenValuesArray[1] && item.sampleGroup == givenValuesArray[2])
                    {
                        correctSamples += string.Format("{0} | {1} | {2} | {3} \n", givenValuesArray[0], item.Protocol, givenValuesArray[1], givenValuesArray[2]);
                        break;
                    }
                }
            }
            return correctSamples;
        }
    }    
}
