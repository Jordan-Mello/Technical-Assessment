using NUnit.Framework;
namespace TestProject1
{
    public class Tests
    {
        [Test]
        public void TestColors()
        {
            Assert.Multiple(() =>
            {
                string colorOutput = SQI.Dx.Program.VerifyColors("1 S10D1245 Standard");
                Assert.AreEqual(colorOutput, "S10D1245 | Standard | Green\n");

                colorOutput = SQI.Dx.Program.VerifyColors("1 N/A Sample");
                Assert.AreEqual(colorOutput, "N/A | Sample | White\n");

                colorOutput = SQI.Dx.Program.VerifyColors("1 C1POS01 Control");
                Assert.AreEqual(colorOutput, "C1POS01 | Control | Blue\n");
            });
        }
        [Test]
        public void TestIDs()
        {
            // Testing revealed the requirement of a break statement to prevent repeated sample verification
            Assert.Multiple(() =>
            {
                // Space before newline due to given print() function
                string sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("1 S10D1245 Standard");
                Assert.AreEqual(sampleOutput, "1 | ABC | S10D1245 | Standard \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("2 S20D1245 Standard");
                Assert.AreEqual(sampleOutput, "2 | ABC | S20D1245 | Standard \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("3 S30D1245 Standard");
                Assert.AreEqual(sampleOutput, "3 | ABC | S30D1245 | Standard \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("4 C1POS01 Control");
                Assert.AreEqual(sampleOutput, "4 | ABD | C1POS01 | Control \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("5 C2POS01 Control");
                Assert.AreEqual(sampleOutput, "5 | ABD | C2POS01 | Control \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("6 C3POS01 Control");
                Assert.AreEqual(sampleOutput, "6 | ABD | C3POS01 | Control \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("7 C2POS02 Control");
                Assert.AreEqual(sampleOutput, "7 | COR | C2POS02 | Control \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("8 C1POS01 Control");
                Assert.AreEqual(sampleOutput, "8 | ABD | C1POS01 | Control \n");

                // UniqueID doesn't matter for detecting valid sample, so test jargon instead
                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("314512 C1POS01 Control");
                Assert.AreEqual(sampleOutput, "314512 | ABD | C1POS01 | Control \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("eeeeeeeeeeeeeeee C1POS01 Control");
                Assert.AreEqual(sampleOutput, "eeeeeeeeeeeeeeee | ABD | C1POS01 | Control \n");

                sampleOutput = SQI.Dx.Program.VerifyExpectedIDs("null C1POS01 Control");
                Assert.AreEqual(sampleOutput, "null | ABD | C1POS01 | Control \n");

            });
        }
        [Test]
        public void OutOfRangeTest()
        {
            SQI.Dx.Program.VerifyExpectedIDs("");
            SQI.Dx.Program.VerifyColors("");

            Assert.Pass();
        }
    }
}
/*
 S10D1245 | Standard | Green
S298512 | Standard | Green
COR015B1 | Sample | White
COR015B2 | Sample | White
C1POS01 | Control | Blue
C2POS01 | Control | Blue
C2POS02 | Control | Blue
1 | ABC | S10D1245 | Standard
4 | ABD | C1POS01 | Control
8 | COV | C1POS01 | Control
5 | ABD | C2POS01 | Control
7 | COR | C2POS02 | Control


1 S10D1245 Standard
2 S298512 Standard
3 COR015B1 Sample
4 COR015B2 Sample
5 
6 C1POS01 Control
7 C2POS01 Control
8 C2POS02 Control
9

 */