using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using System.Collections.Generic;
using Formulas;

namespace UnitTestProject1
    
{
    
    namespace UnitTestProject1
    {
        [TestClass]
        public class UnitTest1
        {
            //empty cells have and empty string as contents
            [TestMethod]
            public void public_content_empty_cell()
            {
                Spreadsheet test = new Spreadsheet();
                string[] cells = { "A1", "B1", "C1", "D1" };
                foreach (string cell in cells)
                {
                    Assert.IsTrue(test.GetCellContents(cell).ToString() == "");
                }
            }
            
            //It does not matter complexity as long as order is OK
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_content_empty_cell_name()
            {
                Spreadsheet test = new Spreadsheet();
                string[] cells = { "_A", "____B", "__", "_", "_32812757_ListenToPunkRockMusic_If", "ABAb___", "AB___123", "a_1_b_2_c" };
                foreach (string cell in cells)
                {
                    Assert.IsTrue(test.GetCellContents(cell).ToString() == "");
                }
            }
            //Name of order should be syntatic order 
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_invalid_name1()
            {
                Spreadsheet test = new Spreadsheet();
                test.GetCellContents("123");
            }

            /// <summary>
            /// Name of a cell must follow syntactic order
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_invalid_name2()
            {
                Spreadsheet test = new Spreadsheet();
                test.GetCellContents(" ");

            }
            //If a cell is set to be empty, return proper dependents set. 
            //Update the list of non empty cells.
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_get_dependents2()
            {
                Spreadsheet test = new Spreadsheet();
                Formula a = new Formula("B+C");
                Formula b = new Formula("C+D");
                Formula c = new Formula("D+E");
                Formula d = new Formula("E+F");
                Formula e = new Formula("F+G");
                Formula f = new Formula("G+H");
                Formula g = new Formula("H");
                Formula h = new Formula("I");
                test.SetContentsOfCell("A", "=" + a.ToString());
                test.SetContentsOfCell("B", "=" + b.ToString());
                test.SetContentsOfCell("C", "=" + c.ToString());
                test.SetContentsOfCell("D", "=" + d.ToString());
                test.SetContentsOfCell("E", "=" + e.ToString());
                test.SetContentsOfCell("F", "=" + f.ToString());
                test.SetContentsOfCell("G", "=" + g.ToString());
                test.SetContentsOfCell("H", "=" + h.ToString());
                Assert.IsTrue(test.SetContentsOfCell("I", "").Count == 9);
                Assert.IsTrue(test.GetNamesOfAllNonemptyCells().ToHashSet<string>().Count == 8);
            }
            /// Simple direct and indirect dependents test
            [TestMethod]
            //[ExpectedException(typeof(InvalidNameException))]
            public void public_get_dependents()
            {
                Spreadsheet test = new Spreadsheet();
                test.SetContentsOfCell("B1", "=Q1");
                test.SetContentsOfCell("C1", "=Q1");
                test.SetContentsOfCell("D1", "=Q1");
                test.SetContentsOfCell("E1", "=Q1");
                Assert.IsTrue(test.SetContentsOfCell("Q", "1").Count == 5);
            }
            /// <summary>
            /// No matter how many time we change the content of a cell,
            /// the number of dependents should be the same.
            /// </summary>
            [TestMethod]

            public void public_get_dependents_repeat()
            {
                AbstractSpreadsheet test = new Spreadsheet();
                test.SetContentsOfCell("B1", "=A1*A1");
                test.SetContentsOfCell("C1", "=B1+A1");
                test.SetContentsOfCell("D1", "=B1-C1");
                Assert.IsTrue(test.SetContentsOfCell("A1", "1").Count == 4);
                Assert.IsTrue(test.SetContentsOfCell("A1", "2").Count == 4);
            }
          
            /// Deep direct and indirect dependents test
            /// </summary>
            [TestMethod]


            public void public_get_dependents1()
            {
                Spreadsheet test = new Spreadsheet();
                test.SetContentsOfCell("A1", "=B1+C1");
                test.SetContentsOfCell("B1", "=C1+D1");
                test.SetContentsOfCell("C1", "=D1+E1");
                test.SetContentsOfCell("D1", "=E1+F1");
                test.SetContentsOfCell("E1", "=F1+G1");
                test.SetContentsOfCell("F1", "=G1+H1");
                test.SetContentsOfCell("G1", "=H1");
                test.SetContentsOfCell("H1", "=I1");
                Assert.IsTrue(test.SetContentsOfCell("I1", "1").Count == 9);
            }

            /// Test for assessing how properly formulas are set in cells.
            /// </summary>
            //[TestMethod]
            //    public void public_get_formula()
            //    {
            //        Spreadsheet test = new Spreadsheet();
            //        Formula basic = new Formula("B+C");
            //        test.SetContentsOfCell("A", basic);
            //        Assert.IsTrue(test.GetCellContents("A").ToString() == "B+C");
            //    }

            /// Name of a cell must follow syntactic order
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_invalid_name3()
            {
                Spreadsheet test = new Spreadsheet();
                test.GetCellContents("&");
            }
            /// A cell cant contain null content
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void public_null_cell()
            {
                Spreadsheet test = new Spreadsheet();
                test.SetContentsOfCell("A", (string)null);
            }
            /// The name of a cell cant be null
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_null_name()
            {
                Spreadsheet test = new Spreadsheet();
                test.GetCellContents(null);
            }
            /// A single cell in spreadsheet has only one dependent.
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_set_single_double()
            {
                Spreadsheet test = new Spreadsheet();
                Assert.IsTrue(test.SetContentsOfCell("A", "1").Count == 1);
                Assert.IsTrue((double)test.GetCellContents("A") == 1);
            }
            /// A single cell in spreadsheet has only one dependent.
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_set_single_string()
            {
                Spreadsheet test = new Spreadsheet();
                Assert.IsTrue(test.SetContentsOfCell("A", "hi").Count == 1);
            }
            /// A deep circular dependency is not allowed
            /// </summary>
            //[TestMethod]
            //[ExpectedException(typeof(CircularException))]
            //public void public_try_circular_direct2()
            //{
            //    Spreadsheet test = new Spreadsheet();
            //    Formula a = new Formula("B+C");
            //    Formula b = new Formula("C+D");
            //    Formula c = new Formula("D+E");
            //    Formula d = new Formula("B+C");
            //    test.SetContentsOfCell("A", "=B+C");
            //    test.SetContentsOfCell("B", "=C+D");
            //    test.SetContentsOfCell("C", "=D+E");
            //    test.SetContentsOfCell("D", "=B+C");
            //}
            [TestMethod]
            [ExpectedException(typeof(CircularException))]
            public void public_try_circular_direct2()
            {
                Spreadsheet test = new Spreadsheet();
                test.SetContentsOfCell("A1", "=B1+C1");
                test.SetContentsOfCell("B1", "=C1+D1");
                test.SetContentsOfCell("C1", "=D1+E1");
                test.SetContentsOfCell("D1", "=B1+C1");
            }
            /// A deep circular dependency is not allowed
            /// </summary>
            [TestMethod]
           [ExpectedException(typeof(CircularException))]
            public void public_try_circular_indirect()
            {
                Spreadsheet test = new Spreadsheet();
                Formula a = new Formula("G+Q");
                Formula b = new Formula("C+D");
                Formula c = new Formula("D+E");
                Formula d = new Formula("E+F");
                Formula e = new Formula("F+G");
                Formula f = new Formula("G+H");
                Formula g = new Formula("A+Q");
                test.SetContentsOfCell("A", "=G+Q");
                test.SetContentsOfCell("B", "=C+D");
                test.SetContentsOfCell("C", "=D+E");
                test.SetContentsOfCell("D", "=E+F");
                test.SetContentsOfCell("E", "=F+G");
                test.SetContentsOfCell("F", "=G+H");
                test.SetContentsOfCell("G", "=A+Q");
            }
            /// A cell cannot depent on self
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(CircularException))]
            public void public_try_circular_self()
            {
                Spreadsheet test = new Spreadsheet();
                test.SetContentsOfCell("A1", "=A1+B1");
            }
            //The number of dependents is update and contains proper cells
            [TestMethod]
            [ExpectedException(typeof(InvalidNameException))]
            public void public_try_dependees()
            {
                Spreadsheet test = new Spreadsheet();
                Formula first = new Formula("B+C");
                test.SetContentsOfCell("A", "first");
                Assert.IsTrue(test.SetContentsOfCell("B", "1").Contains("A"));
                Assert.IsTrue(test.SetContentsOfCell("B", "1").Count == 2);

            }
            [TestMethod]
            public void public_getval1()
            {
                Spreadsheet test = new Spreadsheet();
                test.SetContentsOfCell("A1", "0");
                Assert.AreEqual(0.0, test.GetCellValue("A1"));
            }
            /// <summary>
            /// Get value string
            /// </summary>
           
            // EMPTY SPREADSHEETS
            [TestMethod()]
            [ExpectedException(typeof(InvalidNameException))]
            public void Test1()
            {
                Spreadsheet s = new Spreadsheet();
                s.GetCellContents(null);
            }
            /// <summary>
            /// Get value string
            /// </summary>
            [TestMethod]
            public void public_getval_empty()
            {
                Spreadsheet test = new Spreadsheet();
                Assert.AreEqual("", test.GetCellValue("A1"));
            }


            /// Get value multi-layer sophisticated dependency
            /// </summary>


            [TestMethod()]
            [ExpectedException(typeof(InvalidNameException))]
            public void Test2()
            {
                Spreadsheet s = new Spreadsheet();
                s.GetCellContents("1AA");
            }


            [TestMethod()]
            public void Test3()
            {
                Spreadsheet s = new Spreadsheet();
                Assert.AreEqual("", s.GetCellContents("A2"));
            }


            // SETTING CELL TO A DOUBLE
            [TestMethod()]
            [ExpectedException(typeof(InvalidNameException))]
            public void Test4()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell(null, "1.5");
            }

            [TestMethod()]
            [ExpectedException(typeof(InvalidNameException))]
            public void Test5()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("1A1A", "1.5");
            }

            [TestMethod()]
            public void Test6()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("Z7", "1.5");
                Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
            }

            // SETTING CELL TO A STRING
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Test7()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A8", (string)null);
            }

            [TestMethod()]
            [ExpectedException(typeof(InvalidNameException))]
            public void Test8()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell(null, "hello");
            }

            [TestMethod()]
            [ExpectedException(typeof(InvalidNameException))]
            public void Test9()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("1AZ", "hello");
            }

            [TestMethod()]
            public void Test10()
            {
                Spreadsheet s = new Spreadsheet();
                
                s.SetContentsOfCell("A5", "hello");
                Assert.AreEqual("hello", s.GetCellContents("A5"));
            }
           

            [TestMethod()]
            [ExpectedException(typeof(InvalidNameException))]
            public void Test11()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("1AZ", "=2");
            }
            // CIRCULAR FORMULA DETECTION
            [TestMethod()]
            [ExpectedException(typeof(CircularException))]
            public void Test12()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A1", "=A2");
                s.SetContentsOfCell("A2", "=A1");
            }

            [TestMethod()]
            [ExpectedException(typeof(CircularException))]
            public void Test13()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A1", "=1");
                s.SetContentsOfCell("A3", "=A1");
                s.SetContentsOfCell("A7", "=A3+A3");
                s.SetContentsOfCell("A1", "=A7");

            }

            [TestMethod()]
            [ExpectedException(typeof(CircularException))]
            public void Test14()
            {
                Spreadsheet s = new Spreadsheet();
                try
                {
                    s.SetContentsOfCell("A1", "=A2+A3");
                    s.SetContentsOfCell("A2", "15");
                    s.SetContentsOfCell("A3", "30");
                    s.SetContentsOfCell("A2","=A3*A1");
                }
                catch (CircularException e)
                {
                    Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
                    throw e;
                }
            }

            // NONEMPTY CELLS
            [TestMethod()]
            public void Test15()
            {
                Spreadsheet s = new Spreadsheet();
                Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
            }

            [TestMethod()]
            public void Test16()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("B1", "");
                Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
            }
            [TestMethod()]
            public void Test17()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("B1", "=3.5");
                Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
            }
            [TestMethod()]

            public void Test18()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("B1", "52.25");
                Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
            }
            [TestMethod()]
            public void Test19()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A1", "17.2");
                s.SetContentsOfCell("C1", "hello");
                s.SetContentsOfCell("B1", "3.5");
                Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "A1", "B1", "C1" }));
            }

            // RETURN VALUE OF SET CELL CONTENTS
            [TestMethod()]
            public void Test20()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("B1", "hello");
                s.SetContentsOfCell("C1", "5");
                Assert.IsTrue(s.SetContentsOfCell("A1", "17.2").SetEquals(new HashSet<string>() { "A1" }));
            }
            [TestMethod()]
            public void Test21()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A1", "17.2");
                s.SetContentsOfCell("C1","5");
                Assert.IsTrue(s.SetContentsOfCell("B1", "hello").SetEquals(new HashSet<string>() { "B1" }));
            }

            [TestMethod()]
            public void Test22()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A1", "17.2");
                s.SetContentsOfCell("B1", "hello");
                Assert.IsTrue(s.SetContentsOfCell("C1", "5").SetEquals(new HashSet<string>() { "C1" }));
            }

            [TestMethod()]
           // [ExpectedException(typeof(CircularException))]
            public void Test23()
            {
               Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A2", "=6");
            s.SetContentsOfCell("A3", "=A2+A4");
            s.SetContentsOfCell("A4", "=A2+A5");
            Assert.IsTrue(s.SetContentsOfCell("A5", "82.5").SetEquals(new HashSet<string>() { "A5", "A4", "A3", "A1" }));
            }
            // CHANGING CELLS
            [TestMethod()]

            public void Test24()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A1","=A2+A3");
                s.SetContentsOfCell("A1", "=2.5");
                Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
            }

            [TestMethod()]
            public void Test25()
            {
                Spreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("A1", "=A2+A3");
                s.SetContentsOfCell("A1", "Hello");
                Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
            }
            // For setting a spreadsheet cell.
            public IEnumerable<string> Set(AbstractSpreadsheet sheet, string name, string contents)
            {
                List<string> result = new List<string>(sheet.SetContentsOfCell(name, contents));
                return result;
            }
            [TestMethod()]

            public void Test26()
            {
                Spreadsheet s = new Spreadsheet();
                ISet<String> cells = new HashSet<string>();
                for (int i = 1; i < 200; i++)
                {
                    cells.Add("A" + i);
                    Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "A" + (i + 1))));
                }

            }
           
            //[TestMethod()]
            //public void Test27()
            //{
            //    Test26();
            //}

           
            [TestMethod()]

            public void Test28()
            {
                Spreadsheet s = new Spreadsheet();
                for (int i = 1; i < 200; i++)
                {
                    s.SetContentsOfCell("A" + i, "=A" + (i + 1));
                }
                try
                {
                    s.SetContentsOfCell("A150", "=A50");
                    Assert.Fail();
                }
                catch (CircularException)
                {
                }

            }

            [TestMethod()]
            public void Test29()
            {
                {
                    Spreadsheet s = new Spreadsheet();
                    for (int i = 0; i < 500; i++)
                    {
                        s.SetContentsOfCell("A1" + i, "=A1" + (i + 1));
                    }
                    HashSet<string> firstCells = new HashSet<string>();
                    HashSet<string> lastCells = new HashSet<string>();
                    for (int i = 0; i < 250; i++)
                    {
                        firstCells.Add("A1" + i);
                        lastCells.Add("A1" + (i + 250));
                    }
                    Assert.IsTrue(s.SetContentsOfCell("A1249", "25.0").SetEquals(firstCells));
                    Assert.IsTrue(s.SetContentsOfCell("A1499", "0").SetEquals(lastCells));
                }
            }
            private String randomName(Random rand)
            {
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
            }
            private String randomFormula(Random rand)
            {
                String f = randomName(rand);
                for (int i = 0; i < 10; i++)
                {
                    switch (rand.Next(4))
                    {
                        case 0:
                            f += "+";
                            break;
                        case 1:
                            f += "-";
                            break;
                        case 2:
                            f += "*";
                            break;
                        case 3:
                            f += "/";
                            break;
                    }
                    switch (rand.Next(2))
                    {
                        case 0:
                            f += 7.2;
                            break;
                        case 1:
                            f += randomName(rand);
                            break;
                    }

                }
                return f;

            }
           
        }

    }
}

 
