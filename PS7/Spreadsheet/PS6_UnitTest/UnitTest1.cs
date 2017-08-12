////Meysam Hamel
////UnitTest_Ps6

//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SS;



//namespace PS6_UnitTest
//{
//    [TestClass]
//    public class UnitTest1
//    {

//        // NONEMPTY CELLS
//        [TestMethod()]
//        public void Test1()
//        {
//            Spreadsheet s = new Spreadsheet();
//            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());


//        }
      
//        /// Test syntactic invalidity
//        /// </summary>
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void content_empty_cell_name()
//        {
//            Spreadsheet test = new Spreadsheet();
//            string[] cells = { "_A", "____B", "__", "_", "_123123123_asdasdasdasd_bv", "ABAAD___", "AB___123", "a_1_b_2_c" };
//            foreach (string cell in cells)
//            {
//                Assert.IsTrue(test.GetCellContents(cell) == "");
//            }
//        }
       
//        /// Name of a cell must follow  order
//        /// </summary>
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void public_invalid_name3()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents("&");
//        }
//        /// Name of a cell must follow  order
//        /// </summary>
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void public_invalid_name2()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents(" ");
//        }
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void public_invalid_name1()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents("123");
//        }
//        /// The name of a cell cant be null
//        /// </summary>
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void public_null_name()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents(null);
//        }
       
//        /// The name of a cell cant be null
//        /// </summary>
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void null_name()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents(null);
//        }

//        /// Name of a cell must follow by order
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void name1()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents("123");
//        }

//        /// Name of a cell must follow by order
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]

//        public void name2()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents(" ");
//        }

//        /// Name of a cell must follow by order
//        /// </summary>
//        [TestMethod]
//        [ExpectedException(typeof(InvalidNameException))]
//        public void name3()
//        {
//            Spreadsheet test = new Spreadsheet();
//            test.GetCellContents("&");
//        }
//        [TestMethod]
//        public void getval_empty()
//        {
//            Spreadsheet test = new Spreadsheet();
//            Assert.AreEqual("", test.GetCellValue("A1"));
//        }

//    }

//}

