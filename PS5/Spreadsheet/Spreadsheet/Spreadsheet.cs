// Written by Meysam Hamel, PS5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Formulas;
using Dependencies;

namespace SS
{
    /// <summary>
    /// Class and methods extending and implementing AbstractSpreadsheet class.
    /// 
    /// An AbstractSpreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.
    /// 
    /// The implementation prevents circular dependency. Assures proper naming scheme for
    /// cells and properly handles setting cell values.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// A dependecy graph responsible for retaining the relations of cells
        /// to one another
        /// </summary>
        private DependencyGraph Graph;

        /// <summary>
        /// A dictionary containing every single cell that holds a value. 
        /// A cell object is mapped to the key name of every cell within.
        /// </summary>
        private Dictionary<string, Cell> NonEmptyCells;

        /// <summary>
        /// A zero-argument constructor that creates an empty spreadsheet.
        /// </summary>
        public Spreadsheet()
        {
            Graph = new DependencyGraph();
            NonEmptyCells = new Dictionary<string, Cell>();
        }
       
        /// <summary>
        /// Provides an IEnumerable collection of all cells holding a value.
        /// </summary>
        /// <returns>IEnumerable collection of all non-empty cells</returns>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            return NonEmptyCells.Keys.ToList<string>();
        }

        /// <summary>
        /// The contents (as opposed to the value) of the named cell.
        /// </summary>
        /// <param name="name">Name of cell to get the content</param>
        /// <returns>Returns the content of the set as a generic object</returns>
        public override object GetCellContents(string name)
        {
            name.isLegalVar();
            if (!NonEmptyCells.ContainsKey(name))
            {
                return "";
            }
            return NonEmptyCells[name].GetContent();
        }

        /// <summary>
        /// Sets the content of the named cell to be a floating point double precision number.
        /// </summary>
        /// <param name="name">Name of cell to set the content</param>
        /// <param name="number">Content of the cell to be placed</param>
        /// <returns>A set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.</returns>
        public override ISet<string> SetCellContents(string name, double number)
        {
            name.isLegalVar();
            NonEmptyCells[name] = new Cell(number);
            return GetAllDependees(name);
        }
  
        /// <summary>
        /// Sets the content of the named cell to be string of text.
        /// </summary>
        /// <param name="name">Name of cell to set the content</param>
        /// <param name="text">Content of the cell to be placed</param>
        /// <returns>A set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.</returns>
        public override ISet<string> SetCellContents(string name, string text)
        {
            name.isLegalVar();
            if (text == "")
            {
                NonEmptyCells[name] = new Cell(text);
                NonEmptyCells.Remove(name);
                HashSet<string> dependees = GetAllDependees(name).ToHashSet<string>();
                Graph.ReplaceDependents(name, new HashSet<string>());
                return dependees;

            }
            NonEmptyCells[name] = new Cell(text);
            return GetAllDependees(name);
        }

        /// <summary>
        /// Sets the content of the named cell to be formula of type Formula.
        /// </summary>
        /// <param name="name">Name of cell to set the content</param>
        /// <param name="formula">Content of the cell to be placed</param>
        /// <returns>A set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.</returns>
        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            name.isLegalVar();
            // Find all the variables (cell names) in the formula.
            List<string> variables = formula.GetVariables().ToList();

            // For each cell name, find all direct and indirect dependent cells
            // and at any point if one had "name" as a dependent throw a circular
            // exception.
            foreach (string var in variables)
            {
                if (GetAllDependents(var).Contains(name))
                {
                    throw new CircularException();
                }
            }

            // Update dependencies in graph.
            Graph.ReplaceDependents(name, variables);
            NonEmptyCells[name] = new Cell(formula);
            return GetAllDependees(name);
        }

        /// <summary>
        /// Returns and enumeration without duplicates of all the direct dependents of
        /// the named cell.
        /// </summary>
        /// <param name="name">Cell to look for all direct dependents</param>
        /// <returns>An enumerable collection of direct dependents without duplicates.</returns>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            name.isLegalVar();

            // Simply get the dependees from the graph.
            HashSet<string> DirectDependents = new HashSet<string>(Graph.GetDependees(name));
            return DirectDependents;
        }

        /// <summary>
        /// A helper method to obtain all direct and indirect cells, whose values depends 
        /// on the named cell.
        /// </summary>
        /// <param name="name">Named cell</param>
        /// <returns>Set of all direct and indirect cells whose value depends on "name"</returns>
        private ISet<string> GetAllDependees(string name)
        {
            HashSet<string> AllDependees = new HashSet<string>();
            AllDependees.Add(name);

            if (!Graph.HasDependees(name))
            {
                return AllDependees;
            }

            GetDependeesRecursive(name, AllDependees);
            return AllDependees;
        }

        /// <summary>
        /// A recursive helper method for GetAllDependees, to obtain all direct and
        /// indirect cells, whose values depends on the named cell.
        /// </summary>
        /// <param name="name">Current cell to find dependents for</param>
        /// <param name="AllDependees">Set of dependents to which this method will add currently
        /// found dependents</param>
        /// <returns>An empty set.</returns>
        private ISet<string> GetDependeesRecursive(string name, HashSet<string> AllDependees)
        {
            if (!Graph.HasDependees(name))
            {
                return new HashSet<string>();
            }
            else
            {
                HashSet<string> CurrentDependees = Graph.GetDependees(name).ToHashSet<string>();
                AllDependees.UnionWith(CurrentDependees);
                foreach (string x in CurrentDependees)
                {
                    GetDependeesRecursive(x, AllDependees);
                }
            }
            return new HashSet<string>();
        }

        /// <summary>
        /// A helper method to obtain all direct and indirect cells that named cell depends on.
        /// Used for finding circular dependency in graph.
        /// </summary>
        /// <param name="name">Named cell</param>
        /// <returns>Set of all direct and indirect cells on which "name"'s value depends on</returns>
        private ISet<string> GetAllDependents(string name)
        {
            HashSet<string> AllDependents = new HashSet<string>();
            AllDependents.Add(name);

            if (!Graph.HasDependents(name))
            {
                return AllDependents;
            }

            GetDependentsRecursive(name, AllDependents);
            return AllDependents;
        }

        /// <summary>
        /// A recursive helper method for GetAllDepeGetAllDependentsndees, to obtain all direct and
        /// indirect cells, on which named cells value depends on.
        /// </summary>
        /// <param name="name">Current cell</param>
        /// <param name="AllDependees">Set of cells on which we depend, and this method
        /// will add more to if found</param>
        /// <returns>An empty set.</returns>
        private ISet<string> GetDependentsRecursive(string name, HashSet<string> AllDependents)
        {
            if (!Graph.HasDependents(name))
            {
                return new HashSet<string>();
            }
            else
            {
                HashSet<string> CurrentDependents = Graph.GetDependents(name).ToHashSet<string>();
                AllDependents.UnionWith(CurrentDependents);
                foreach (string x in CurrentDependents)
                {
                    GetDependentsRecursive(x, AllDependents);
                }
            }
            return new HashSet<string>();
        }
    }

    /// <summary>
    /// An internal cell, creating the foundations of a cell to be used in our
    /// Spreadsheet.
    /// </summary>
    internal class Cell
    {
        /// <summary>
        /// The content of every cell is initially and epty string.
        /// </summary>
        private readonly object Content = "";

        /// <summary>
        /// Constructor for cells, given an object as parameter for content.
        /// Content can be a double, string, or a formula of type Formula.
        /// </summary>
        /// <param name="content">Double, string, or a formula of type Formula</param>
        public Cell(object content)
        {
            if (content == null)
            {
                throw new ArgumentNullException();
            }
            Content = content;
        }

        /// <summary>
        /// Returns the content of this cell as a generic object.
        /// </summary>
        /// <returns>Content of this cell.</returns>
        public object GetContent()
        {
            return Content;
        }

    }

    /// <summary>
    /// An extension class to ease comparisons and conversion for our spreadsheet.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// An extension of strings. If a string is NOT of correct syntactic formart,
        /// starting with a letter (upper or lowercase) or an underscore, followed by letters, underscores
        /// or digits in any order this method will throw an InvalidNameException.
        /// </summary>
        /// <param name="name">The string to check for its validity</param>
        public static void isLegalVar(this string name)
        {
            if (name == null)
            {
                throw new InvalidNameException();
            }
            else if (Regex.IsMatch(name, @"[^a-zA-Z_\d]"))
            {
                throw new InvalidNameException();
            }
            else if (!Regex.IsMatch(name, @"^((([a-z]|[A-Z]|_){1,}(\d|_|[a-z]|[A-Z]){1,})|([A-Z]|_))\b"))
            {
                throw new InvalidNameException();
            }
        }

        /// <summary>
        /// Converts any enumerable collection to a HashSet of generic type.
        /// </summary>
        /// <typeparam name="T">The type of the HashSet of interest</typeparam>
        /// <param name="source">Enumerable collection to be converted</param>
        /// <returns>HashSet of type T from source collection</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

    }
}