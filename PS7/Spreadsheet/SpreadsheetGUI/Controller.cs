//Meysam Haml
//u0914328
//HW7
using Formulas;
using SS;
using SSGui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public class Controller
    {
        // The window being controlled
        private Interface window;
        
        // The model being used
        private Spreadsheet spreadsheet;
        
        /// Row and column to track the position of the current cell and selection
        /// </summary>
       // int row = 0; int col = 0;
        

        public Controller(Interface window)
        {
            //this.window = window;
            //this.spreadsheet = new Spreadsheet();
            //window.Title = "Test";
            //window.UpdateEvent += ApplyFormula;
            //window.closeEvent += Handleclose;
            //window.openEvent += Handleopen;
            this.window = window;
            this.spreadsheet = new Spreadsheet(new Regex(@"[A-Z][1-9](\d?)"));

            AddEvents();
        }

        public Controller (Interface window, Spreadsheet spreadsheet, string fname)
        {

            this.window = window;
            this.spreadsheet = spreadsheet;
            //window.CellValue = spreadsheet.GetCellValue(window.CellName).ToString();
            window.Title = fname;

            foreach (string cell in spreadsheet.GetNamesOfAllNonemptyCells())
            {
                string[] rowAndCol = Regex.Split(cell, @"(\d+)");

                window.SetCell((rowAndCol[0].ToCharArray()[0] - 'A'), int.Parse(rowAndCol[1]) - 1, spreadsheet.GetCellValue(cell).ToString());
            }
            window.CellContent = (spreadsheet.GetCellContents(window.CellName) is Formula ? "=" : "") +
                spreadsheet.GetCellContents(window.CellName).ToString();
            window.Title = fname;

            AddEvents();




        }
        private void HandleClose()
        {
            window.DoClose();
        }
        private void HandleDisplaySelection(SpreadsheetPanel ss)
        {
            int row, col;
            String value;
            ss.GetSelection(out col, out row);
            ss.GetValue(col, row, out value);
            window.CellName = ((char)('A' + col)).ToString() + (row + 1);
            window.CellValue = value;
            window.CellContent = (spreadsheet.GetCellContents(window.CellName) is Formula ? "=" : "") +
                spreadsheet.GetCellContents(window.CellName).ToString();
        }

        private void HandleUpdateSelection(string content)
        {
            var initialContent = spreadsheet.GetCellContents(window.CellName);

            try
            {
                foreach (string cell in spreadsheet.SetContentsOfCell(window.CellName, content))
                {
                    string[] rowAndCol = Regex.Split(cell, @"(\d+)");

                    window.SetCell((rowAndCol[0].ToCharArray()[0] - 'A'), int.Parse(rowAndCol[1]) - 1, spreadsheet.GetCellValue(cell).ToString());
                }

                window.CellValue = spreadsheet.GetCellValue(window.CellName).ToString();
                window.CellContent = (spreadsheet.GetCellContents(window.CellName) is Formula ? "=" : "") +
                    spreadsheet.GetCellContents(window.CellName).ToString();

                window.Changed = spreadsheet.Changed;
            }
            catch (Exception e)
            {
                window.Message = "Error: \n" + e.Message;
                HandleUpdateSelection(initialContent.ToString());
            }
        }
        private void HandleNew()
        {
            ViewManager.GetContext().RunNew();
        }


        private void HandleSave(string filename)
        {
            try
            {
                spreadsheet.Save(new StreamWriter(filename));
                window.Changed = spreadsheet.Changed;
            }
            catch (Exception e)
            {
                window.Message = "Unable to save file\n" + e.Message;
            }
        }
        private void HandleOpen(string filename)
        {
            try
            {
                var newSpreadsheet = new Spreadsheet(new StreamReader(filename));
                                
                ViewManager.GetContext().RunNew(newSpreadsheet, filename);
            }
            catch (Exception e)
            {
                window.Message = "Unable to open file\n" + e.Message;
            }
        }

        private void HandleHelp()
        {
            window.Message = "Turorial:\n\n"
                + "Update Cells:\n"
               
                + "New Window:\n"
                + "To open a new window click \"New\" in the help menu.  \n\n"

                + "Open :\n"
                + "To open an ss file, click \"Open\" in the file menu and choose your file.  \n\n"

                + "Save:\n"
                + "To save a spreadsheet, click \"Save\"  "
                +   ".\n\n"

                + "Close :\n"
                + "To close the spreadsheet, click \"Close\" ";
               
        }
        private void AddEvents()
        {
            window.UpdateEvent += HandleUpdateSelection;
            window.SelectionChanged += HandleDisplaySelection;
            //window.NewEvent += HandleNew;
            window.OpenEvent += HandleOpen;
            window.SaveEvent += HandleSave;
            window.CloseEvent += HandleClose;
            window.HelpEvent += HandleHelp;
        }


        //private string getCellFromColRow()
        //{
        //int ASCIICol = col + 65;
        //char column = (char)ASCIICol;
        //return column.ToString() + (row +// 1) + "";
        //}


        /// <summary>



    }
}