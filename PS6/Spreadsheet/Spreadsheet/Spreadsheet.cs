// Written by Meysam Hamel, PS6

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Formulas;
using Dependencies;
using System.IO;
using System.Xml;

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
        private bool _Changed;


        /// <summary>
        /// A dependecy graph responsible for retaining the relations of cells
        /// to one another
        /// </summary>
        private readonly DependencyGraph Graph;

       
        private Regex regex;
        /// <summary>
        /// A dictionary containing every single cell that holds a value. 
        /// A cell object is mapped to the key name of every cell within.
        /// </summary>
        private readonly Dictionary<string, Cell> NonEmptyCells;
        private string path;

        /// Public accessor to track if spreadsheet has changed or not
        public override bool Changed
        {
            get
            {
                return _Changed;
            }

            protected set
            {
                _Changed = value;
            }
        }

        /// <summary>
        /// A zero-argument constructor that creates an empty spreadsheet.
        /// </summary>
        public Spreadsheet()
        {

            Graph = new DependencyGraph();
            NonEmptyCells = new Dictionary<string, Cell>();
            regex = new Regex(".*");



        }


        /// Default constructor for the spreadsheet class. Recording its variable
        /// validity test, its normalization method, and its version information.



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
            name = ValidateNormalize(name);
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
        protected override ISet<string> SetCellContents(string name, double number)
        {
            NonEmptyCells[name] = new Cell(number);
            // Update the value of cell
            NonEmptyCells[name].SetValue(number);
            Changed = true;
            Graph.ReplaceDependents(name, new HashSet<string>());
            return new HashSet<string>(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// Sets the content of the named cell to be string of text.
        /// </summary>
        /// <param name="name">Name of cell to set the content</param>
        /// <param name="text">Content of the cell to be placed</param>
        /// <returns>A set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.</returns>
        protected override ISet<string> SetCellContents(string name, string text)
        {
            if (text == "")
            {
                NonEmptyCells[name] = new Cell(text);
                NonEmptyCells.Remove(name);
                HashSet<string> dependees = new HashSet<string>(GetCellsToRecalculate(name));
                Graph.ReplaceDependents(name, new HashSet<string>());
                return dependees;
            }

            NonEmptyCells[name] = new Cell(text);
            // Update the value of cell
            NonEmptyCells[name].SetValue(text);
            Changed = true;
            Graph.ReplaceDependents(name, new HashSet<string>());
            return new HashSet<string>(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// Sets the content of the named cell to be formula of type Formula.
        /// </summary>
        /// <param name="name">Name of cell to set the content</param>
        /// <param name="formula">Content of the cell to be placed</param>
        /// <returns>A set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.</returns>
        protected override ISet<string> SetCellContents(string name, Formula formula)
        {
            // Find all the variables (cell names) in the formula.
            HashSet<string> variables = formula.GetVariables().ToHashSet<string>();

            // Ensure the validity of each variable
            foreach (string var in variables)
                ValidateNormalize(var);

            // For each cell name, find all dependent cells
            // and at any point if one had "name" as a dependent throw a circular
            // exception.
            foreach (string dependee in this.GetCellsToRecalculate(name))
            {
                if (variables.Contains(dependee))
                    throw new CircularException();
            }

            // Update dependencies in graph.
            Graph.ReplaceDependents(name, variables);
            NonEmptyCells[name] = new Cell(formula);
            // Update the value of cell using GetCellValue method.
            GetCellValue(name);
            Changed = true;
            return new HashSet<string>(GetCellsToRecalculate(name));
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
                throw new ArgumentNullException();

            name = ValidateNormalize(name);

            // Return the dependees from the graph.
            HashSet<string> DirectDependents = new HashSet<string>(Graph.GetDependees(name));
            return DirectDependents;
        }
        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        /// <param name="filename">Path of the file to be checked</param>
        /// <returns>The version of the saved file</returns>
        //public override string GetSavedVersion(TextWriter filename)
        //{
        //    using (XmlReader reader = ReadFile(filename))
        //    {
        //        if (reader.ReadToFollowing("spreadsheet"))
        //        {
        //            if (reader.MoveToFirstAttribute())
        //                return reader.Value;
        //        }
        //        throw new SpreadsheetReadException("Spreadsheet format error. No version found");
        //    }
        //}



        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// <param name="filename">Path of the file to be saved</param>
        /// 
       
        public override void Save(TextWriter filename)
        {
            using (XmlWriter writer = WriteFile(filename))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("spreadsheet");
                //writer.WriteAttributeString("version", Version);

                foreach (var name in NonEmptyCells)
                {
                    double d;
                    var contents = name.Value.GetContent();

                    writer.WriteStartElement("cell");
                    writer.WriteElementString("name", name.Key);

                    if (double.TryParse(contents.ToString(), out d))
                        writer.WriteElementString("contents", d.ToString());

                    // If content is formula, append to "="
                    else if (contents is Formula)
                        writer.WriteElementString("contents", "=" + contents.ToString());

                    else
                        writer.WriteElementString("contents", contents.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            Changed = false;
        }


        /// <summary>
        /// Helper method to handle error in reading files
        /// </summary>
        /// <param name="path">Path of the file to be read</param>
        /// <returns>And XmlReader instance with the specific path</returns>
        private static XmlReader ReadFile(string path)
        {
            try
            {
                return XmlReader.Create(path);
            }
            catch
            {
                throw new SpreadsheetReadException("Error reading file from path.");
            }
        }


        /// <summary>
        /// Helper method to handle error in writing files
        /// </summary>
        /// <param name="path">Path of the file to be written</param>
        /// <returns>And XmlWriter instance with the specific path</returns>
        private static XmlWriter WriteFile(TextWriter path)
        {
            try
            {
                return XmlWriter.Create(path);
            }
            catch
            {
                throw new SpreadsheetReadException("Error writing file to path.");
            }
        }

        /// <summary>
        /// An extension of strings. If a string is NOT of correct syntactic formart,
        /// starting with a one or more letters, followed by one or more
        /// or digits this method will throw an InvalidNameException.
        /// </summary>
        /// <param name="str">The string to check for its validity</param>
        public string ValidateNormalize(string str)
        {
            if (str == null)
                throw new InvalidNameException();
            else if (!Regex.IsMatch(str, @"\b(([a-z]|[A-Z]){1,})[\d]{1,}"))
                throw new InvalidNameException();
            else if (!IsValid(str))
                throw new InvalidNameException();
            return Normalize(str);
        }


        private string Normalize(string str)
        {
            return str.ToUpper();
        }

        private bool IsValid(string str)
        {
            string valid = @"^[a-zA-Z][1-9][0-9]*$";
            return (Regex.IsMatch(str, valid));
        }

        //private string Normalize(string str)
        //{

        //}

        public override object GetCellValue(string name)
        {
            // Check validity of the name
            name = ValidateNormalize(name);

            // If name does not exist the value is an empty string
            if (!NonEmptyCells.ContainsKey(name))
            {
                return "";
            }

            // Otherwise check to see if content is a double
            object content = NonEmptyCells[name].GetContent();
            double d;
            if (double.TryParse(content.ToString(), out d))
                return d;

            // Or a string
            else if (content is string)
                return content;

            // If not, it has to be a formula
            else
            {
                // For each cell that depends on "name" cell
                foreach (string str in GetCellsToRecalculate(name))
                {
                    // Construct a temporary formula from the content so 
                    // we don't need to cast the content
                    Formula temp = (Formula)NonEmptyCells[str].GetContent();
                    // Recursively* evaluate the formula, and the value of each variable
                    // And ser the value to the corresponding evaluation.
                    NonEmptyCells[str].SetValue(temp.Evaluate(s => (double)GetCellValue(s)));
                    return NonEmptyCells[str].GetValue();
                }
                return NonEmptyCells[name].GetValue();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <returns></returns>

        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            if (content == null)
                throw new ArgumentNullException("Provided content is null");

            name = ValidateNormalize(name);
            content = Normalize(content);

            // If content is double use SetCellContents(string, double) format 
            double d;
            if (double.TryParse(content, out d))
                return SetCellContents(name, d);

            // If content is formula use SetCellContents(string, formula) format 
            else if (content.Length > 0 && content[0] == '=')
            {
                // Remove the "=" and create a formula from the rest of the string
                Formula formula = new Formula(content.Substring(1, content.Length - 1));
                return SetCellContents(name, formula);
            }

            // Otherwise content is string, use SetCellContents(string, string) format 
            else
                return SetCellContents(name, content);
        }
    }

    /// <summary>
    /// An internal cell, creating the foundations of a cell to be used in our
    /// Spreadsheet.
    /// </summary>
    /// 
    public static class Extensions
    {
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

    internal class Cell
    {
        /// <summary>
        /// The content of every cell is initially and epty string.
        /// </summary>
        private readonly object Content = "";


        /// <summary>
        /// Value of cell
        /// </summary>

        private object value = "";
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

        /// <summary>
        /// Sets the value of the cell
        /// </summary>
        /// <param name="val">Value of the cell</param>
        public void SetValue(object val)
        {
            value = val;
        }


        /// <summary>
        /// Gets the value of the cell
        /// </summary>
        /// <returns>Value of the cell</returns>
        public object GetValue()
        {
            return value;
        }

        /// <summary>
        /// An extension class to ease comparisons and conversion for our spreadsheet.
        /// </summary>

    }
}