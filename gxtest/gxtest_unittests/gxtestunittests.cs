using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace gxtest_unittests
{
    [TestClass]
    public class GxtestUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "An exception type of ArgumentNullException was expected but not thrown")]
        public void Name_Compare_Attempt_With_Null_Values()
        {
            gxtest.Name testName = null;
            gxtest.Name compName = new gxtest.Name();
            compName.Firstnames = "Abby Jane";
            compName.Surname = "Taylor";

            compName.CompareTo(testName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "An exception type of ArgumentException was expected but not thrown")]
        public void Name_Compare_Attempt_With_Wrong_Type()
        {
            string testName = "Hello World";
            gxtest.Name compName = new gxtest.Name();
            compName.Firstnames = "Ignatius";
            compName.Surname = "Jones";

            compName.CompareTo(testName);
        }

        [TestMethod]
        public void Name_Compare_Names_Equal()
        {
            gxtest.Name testName = new gxtest.Name();
            gxtest.Name compName = new gxtest.Name();

            testName.Firstnames = "Michael";
            testName.Surname = "Palin";
            compName.Firstnames = "Michael";
            compName.Surname = "Palin";

            Assert.AreEqual(compName.CompareTo(testName), 0);

        }

        [TestMethod]
        public void Name_Compare_Names_Not_Equal_Middle_Name()
        {
            gxtest.Name testName = new gxtest.Name();
            gxtest.Name compName = new gxtest.Name();

            testName.Firstnames = "Michael";
            testName.Surname = "Palin";
            compName.Firstnames = "Michael Edward";
            compName.Surname = "Palin";

            Assert.AreEqual(compName.CompareTo(testName), 1);

        }

        [TestMethod]
        public void Name_Compare_Names_Not_Equal()
        {
            gxtest.Name testName = new gxtest.Name();
            gxtest.Name compName = new gxtest.Name();

            testName.Firstnames = "Michael Edward";
            testName.Surname = "Palin";
            compName.Firstnames = "John Marwood";
            compName.Surname = "Cleese";

            Assert.AreEqual(compName.CompareTo(testName), -1);

        }

        [TestMethod]
        public void Name_Print_No_File()
        {
            gxtest.Name testName = new gxtest.Name();
            testName.Firstnames = "Terence Graham Parry";
            testName.Surname = "Jones";
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                testName.Print(null);
                Assert.AreEqual<string>(sw.ToString(), "Terence Graham Parry Jones\r\n");
            }
        }

        [TestMethod]
        public void Name_Print_To_File()
        {
            gxtest.Name testName = new gxtest.Name();
            testName.Firstnames = "Terrence Vance";
            testName.Surname = "Gilliam";
            string outputFile = "test-output.txt";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile))
            {
                testName.Print(sw);
            }
            string outputLine = System.IO.File.ReadLines(outputFile).First();
            Assert.AreEqual<string>(outputLine, "Terrence Vance Gilliam");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "An exception type of ArgumentNullException was expected but not thrown")]
        public void Namelist_Parse_Attempt_With_Null_Value()
        {
            gxtest.NameList nameList = new gxtest.NameList();
            nameList.ParseFile(null);
        }

        [TestMethod]
        public void Namelist_Parse_Attempt_With_Valid_Strings()
        {
            string mpalin = "Michael Edward Palin";
            string jcleese = "John Marwood Cleese";
            string tjones = "Terence Graham Perry Jones";
            string tgilliam = "Terrance Vance Gilliam";
            string eidle = "Eric Idle";
            string gchapman = "Graham Arthur Chapman";
            string[] pythons = new string[6] {
                mpalin,
                jcleese,
                tjones,
                tgilliam,
                eidle,
                gchapman };
            gxtest.NameList nameList = new gxtest.NameList();
            nameList.ParseFile(pythons);
            Assert.AreEqual(nameList.Count, 6);
            // Also check individual entries
            Assert.AreEqual(nameList[0].Firstnames, "Michael Edward");
            Assert.AreEqual(nameList[3].Surname, "Gilliam");
            Assert.AreEqual(nameList[2].Firstnames, "Terence Graham Perry");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "An exception type of FormatException was expected but not thrown")]
        public void Namelist_Parse_Only_One_Name()
        {
            string[] popstars = new string[3] {
                "Madonna",
                "Cher",
                "Beyonce"
            };
            gxtest.NameList nameList = new gxtest.NameList();
            nameList.ParseFile(popstars);
        }

        [TestMethod]
        public void Namelist_Sort()
        {
            string mpalin = "Michael Edward Palin";
            string jcleese = "John Marwood Cleese";
            string tjones = "Terence Graham Perry Jones";
            string tgilliam = "Terrance Vance Gilliam";
            string eidle = "Eric Idle";
            string gchapman = "Graham Arthur Chapman";
            string[] pythons = new string[6] {
                mpalin,
                jcleese,
                tjones,
                tgilliam,
                eidle,
                gchapman };
            gxtest.NameList nameList = new gxtest.NameList();
            nameList.ParseFile(pythons);
            nameList.Sort();
            Assert.AreEqual(nameList[0].Firstnames, "Graham Arthur");
            Assert.AreEqual(nameList[1].Firstnames, "John Marwood");
            Assert.AreEqual(nameList[2].Firstnames, "Terrance Vance");
            Assert.AreEqual(nameList[3].Firstnames, "Eric");
            Assert.AreEqual(nameList[4].Firstnames, "Terence Graham Perry");
            Assert.AreEqual(nameList[5].Firstnames, "Michael Edward");
            Assert.AreEqual(nameList[0].Surname, "Chapman");
            Assert.AreEqual(nameList[1].Surname, "Cleese");
            Assert.AreEqual(nameList[2].Surname, "Gilliam");
            Assert.AreEqual(nameList[3].Surname, "Idle");
            Assert.AreEqual(nameList[4].Surname, "Jones");
            Assert.AreEqual(nameList[5].Surname, "Palin");

        }
    }
}
