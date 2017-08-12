//Meysam Hamel
//u0914328
//PS3

//PURPOSE: Implementtion of the dependency graph. A dependency Graph can be modeled as a set of ordered pairs of strings.

// Skeleton implementation written by Joe Zachary for CS 3500, January 2015.
// Revised for CS 3500 by Joe Zachary, January 29, 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Dependencies
{
    /// <summary>
    /// A DependencyGraph can be modeled as a set of dependencies, where a dependency is an ordered 
    /// pair of strings.  Two dependencies (s1,t1) and (s2,t2) are considered equal if and only if 
    /// s1 equals s2 and t1 equals t2.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that the dependency (s,t) is in DG 
    ///    is called the dependents of s, which we will denote as dependents(s).
    ///        
    ///    (2) If t is a string, the set of all strings s such that the dependency (s,t) is in DG 
    ///    is called the dependees of t, which we will denote as dependees(t).
    ///    
    /// The notations dependents(s) and dependees(s) are used in the specification of the methods of this class.
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b" "c"}
    ///     dependents("b") = {"d"}
    ///     dependents("c") = {}
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {},
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///     
    /// All of the methods below require their string parameters to be non-null.  This means that 
    /// the behavior of the method is undefined when a string parameter is null.  
    ///
    /// IMPORTANT IMPLEMENTATION NOTE
    /// 
    /// The simplest way to describe a DependencyGraph and its methods is as a set of dependencies, 
    /// as discussed above.
    /// 
    /// However, physically representing a DependencyGraph as, say, a set of ordered pairs will not
    /// yield an acceptably efficient representation.  DO NOT USE SUCH A REPRESENTATION.
    /// 
    /// You'll need to be more clever than that.  Design a representation that is both easy to work
    /// with as well acceptably efficient according to the guidelines in the PS3 writeup. Some of
    /// the test cases with which you will be graded will create massive DependencyGraphs.  If you
    /// build an inefficient DependencyGraph this week, you will be regretting it for the next month.
    /// </summary>
    public class DependencyGraph
    {
        /// <summary>
        /// Creates a DependencyGraph containing no dependencies

        // Contains dependees, all strings t such that (t,s) is in graph is called dependees(s)
        private readonly Dictionary<string, HashSet<string>> dependees;
        /// Contains dependents, all strings t such that (s,t) is in graph is called dependent(s)
        private readonly Dictionary<string, HashSet<string>> dependents;

        //Match in the dependancy graph
        private int dg_size;
        private HashSet<KeyValuePair<string, string>> dg;
        public DependencyGraph()
        {
            //Create empty Depandancy graph
            dependees = new Dictionary<string, HashSet<string>>();
            dependents = new Dictionary<string, HashSet<string>>();
            dg_size = 0;

        }
        public DependencyGraph(DependencyGraph dg1)
        {
            //Create empty Depandancy graph
            dependees = new Dictionary<string, HashSet<string>>();
            dependents = new Dictionary<string, HashSet<string>>();
            foreach (string s in dg1.dependees.Keys)
            {
                foreach (string t in dg1.dependees[s])
                    this.AddDependency(s, t);
            }
            dg_size = dg1.dg_size;
        }


        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return dg_size; }
        }

        public int this[string s]
        {
            get
            {
                if (dependents.ContainsKey(s))
                    return dependents[s].Count;
                else
                    return 0;
            }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                ////// Accidentaly had dependents instead of dependees
                if (dependees.ContainsKey(s))
                    return true;
                else
                    return false;
            }
            else {
                throw new ArgumentNullException("Input cannot be null or empty.");
            }
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {


                if (dependents.ContainsKey(s))
                    return true;
                else
                    return false;

            }
            else {
                throw new ArgumentNullException("Input cannot be null or empty.");
            }
        }

        /// <summary>
        /// Enumerates dependents(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            //If dependees include 's'
            if (dependees.ContainsKey(s))
            {
                return new HashSet<string>(dependees[s]);

            }
            else
                //return empty hash set
                return new HashSet<string>();
        }

        /// <summary>
        /// Enumerates dependees(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            //If dependents dictionary include 's'
            if (dependents.ContainsKey(s))
            {
                // copy its dependees into an IEnumerable variable and return it
                return new HashSet<String>(dependents[s]);
            }
            else

                // return and empty hash set
                return new HashSet<string>();


            //return null;
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph.
        /// This has no effect if (s,t) already belongs to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>

        //public void AddDependency(string s, string t)
        //{
        //    if (t.has(dependees) && !dependees[t].Contains(s))
        //    {
        //        dependees[t].Add(s);
        //        dg_size++;
        //    }
        //    else if (!t.has(dependees))
        //    {
        //        dependees.Add(t, new HashSet<string>());
        //        dependees[t].Add(s);
        //        dg_size++;
        //    }
        //    if (s.has(dependents) && !dependents[s].Contains(t))
        //    {
        //        dependents[s].Add(t);
        //    }
        //    else if (!s.has(dependents))
        //    {
        //        dependents.Add(s, new HashSet<string>());
        //        dependents[s].Add(t);
        //    }
        //}

        public void AddDependency(string s, string t)
        {


            if (!String.IsNullOrEmpty(s) && !String.IsNullOrEmpty(t))
            {



                if (t.has(dependents) && !dependents[t].Contains(s))
                {
                    dependents[t].Add(s);
                    dg_size++;
                }
                else if (!t.has(dependents))
                {
                    dependents.Add(t, new HashSet<string>());
                    dependents[t].Add(s);
                    dg_size++;
                }
                if (s.has(dependees) && !dependees[s].Contains(t))
                {
                    dependees[s].Add(t);
                }
                else if (!s.has(dependees))
                {
                    dependees.Add(s, new HashSet<string>());
                    dependees[s].Add(t);
                }


            }
            else {
                throw new ArgumentNullException("Input cannot be null or empty.");
            }

        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph.
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            if (!String.IsNullOrEmpty(s) && !String.IsNullOrEmpty(t))
            {
                // if the ordered pair is in the dependency graph
                if (dependees.ContainsKey(s) && dependents.ContainsKey(t))
                    // decrement the count
                    dg_size--;

                if (dependees.ContainsKey(s))
                {
                    //remove t from HashSet of s
                    dependees[s].Remove(t);
                    if (dependees[s].Count == 0)
                        dependees.Remove(s);

                }
                //the dependents contains t
                if (dependents.ContainsKey(t))
                {
                    // add s the value HashSet of t
                    dependents[t].Remove(s);
                    if (dependents[t].Count == 0)
                        dependents.Remove(t);

                }
            }
            else {
                throw new ArgumentNullException("Input cannot be null or empty.");
            }

        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            if (!String.IsNullOrEmpty(s) && newDependents != null)
            {

                foreach (string x in GetDependents(s))
                    RemoveDependency(s, x);
                // Add the new dependents for giving parameter
                foreach (string x in newDependents)
                    AddDependency(s, x);
            }
            else {
                throw new ArgumentNullException("Input cannot be null or empty.");
            }
        }


        /// <summary>
        /// Removes all existing dependencies of the form (r,t).  Then, for each 
        /// s in newDependees, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            //&& newDependees != null && newDependees.Any()
            if (!String.IsNullOrEmpty(s) && newDependees != null)
            {
                foreach (string x in GetDependees(s))
                    RemoveDependency(x, s);
                // Add the new dependees for giving parameter
                foreach (string x in newDependees)
                    AddDependency(x, s);
            }
            else {
                throw new ArgumentNullException("Input cannot be null or empty.");
            }

        }
    }



    /// <summary>
    /// Helper class for adding extention methods to strings
    /// </summary>
    /// 
    public static class Extensions
    {
        /// <summary>
        /// Check for a given string to have dependents or dependees based on user specification.
        /// Returns true if string has specified dictionary, false otherwise.
        /// </summary>
        /// <param name="s">String to be assessed</param>
        /// <param name="d">Dependents or dependees based on specification</param>
        /// <returns></returns>
        public static bool has(this string s, Dictionary<string, HashSet<string>> d)
        {
            return d.ContainsKey(s);
        }
    }
}


