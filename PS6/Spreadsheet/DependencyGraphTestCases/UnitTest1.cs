//Meysam Hamel
//UID: u0914328
//PS3
// This is the test for DependencyGraph.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Dependencies;

namespace DependencyGraphTestCases
{
    [TestClass]
    public class UnitTest1
    {
        // **** test **** //

        /// <summary>
        /// Same nodes should not be duplicated and take size.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("", "");
        }


        /// <summary>
        /// Same nodes should not be duplicated and take size.
        /// </summary>
        [TestMethod()]
        public void RedundancyTest()
        {
            DependencyGraph t = new DependencyGraph();
            const int SIZE = 100;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                t.AddDependency("a", "b");
            }
            Assert.AreEqual(1, t.Size);
        }

        /// <summary>
        /// If all dependees are replaced with an empty set
        /// size must be zero
        /// </summary>
        [TestMethod()]
        public void ReplaceExistingDependeesWithEmpty()
        {
            DependencyGraph t = new DependencyGraph();
            const int SIZE = 100;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                t.AddDependency(i.ToString(), "a");
            }
            t.ReplaceDependees("a", new HashSet<string>());
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        /// Test to see if two dependees with same number of dependents
        /// have equal size of all dependents
        /// </summary>
        [TestMethod()]
        public void EqualDependents()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "a");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("a", "e");
            t.AddDependency("a", "f");
            t.AddDependency("a", "g");
            t.AddDependency("b", "a");
            t.AddDependency("b", "b");
            t.AddDependency("b", "c");
            t.AddDependency("b", "d");
            t.AddDependency("b", "e");
            t.AddDependency("b", "f");
            t.AddDependency("b", "g");

            HashSet<String> aDents = new HashSet<String>(t.GetDependents("a"));
            HashSet<String> bDents = new HashSet<String>(t.GetDependents("b"));
            Assert.IsTrue(aDents.Count == bDents.Count);
        }

        /// <summary>
        /// Test to see if two dependees with same number of dependents
        /// have equal size of all dependents
        /// </summary>
        [TestMethod()]
        public void EqualDependees()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "a");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("a", "e");
            t.AddDependency("a", "f");
            t.AddDependency("a", "g");
            t.AddDependency("b", "a");
            t.AddDependency("b", "b");
            t.AddDependency("b", "c");
            t.AddDependency("b", "d");
            t.AddDependency("b", "e");
            t.AddDependency("b", "f");
            t.AddDependency("b", "g");



            HashSet<String> aDees = new HashSet<String>(t.GetDependees("a"));
            HashSet<String> bDees = new HashSet<String>(t.GetDependees("b"));
            HashSet<String> cDees = new HashSet<String>(t.GetDependees("c"));
            HashSet<String> dDees = new HashSet<String>(t.GetDependees("d"));
            HashSet<String> eDees = new HashSet<String>(t.GetDependees("e"));
            HashSet<String> fDees = new HashSet<String>(t.GetDependees("f"));
            HashSet<String> gDees = new HashSet<String>(t.GetDependees("g"));

            Assert.IsTrue(aDees.Count == 2);
            Assert.IsTrue(bDees.Count == 2);
            Assert.IsTrue(cDees.Count == 2);
            Assert.IsTrue(dDees.Count == 2);
            Assert.IsTrue(eDees.Count == 2);
            Assert.IsTrue(fDees.Count == 2);
            Assert.IsTrue(gDees.Count == 2);
        }
        [TestMethod()]
        public void EmptyTest1()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.IsFalse(t.HasDependees("a"));
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyTest2()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.IsFalse(t.HasDependents("a"));
        }


        //Replace on an empty DG shouldn't fail

        [TestMethod()]
        public void EmptyTest3()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("b", "a");
            t.AddDependency("c", "a");
            t.AddDependency("d", "a");
            t.ReplaceDependees("a", new HashSet<string>());
            Assert.AreEqual(0, t.Size);
        }
        //[TestMethod()]
        //public void EmptyTest4()
        //{
        //    DependencyGraph t = new DependencyGraph();
        //    t.AddDependency("a", "b");
        //    t.AddDependency("a", "c");
        //    t.AddDependency("a", "d");
        //    t.ReplaceDependents("a", new HashSet<string>());
        //    Assert.AreEqual(0, t.Size);
        //}

        ///Adding an empty DG shouldn't fail
        [TestMethod()]
        public void EmptyTest5()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
        }
        /// If all dependees are replaced with an empty set
        /// size must be zero
        [TestMethod()]
        public void SReplaceExistingDependeesWithEmpty()
        {
            DependencyGraph t = new DependencyGraph();
            const int SIZE = 100;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                t.AddDependency(i.ToString(), "a");
            }
            t.ReplaceDependees("a", new HashSet<string>());
            Assert.AreEqual(0, t.Size);
        }

    }
}