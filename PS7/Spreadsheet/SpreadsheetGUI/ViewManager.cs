//Meysam Haml
//u0914328
//HW7
using SS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{


    class ViewManager : ApplicationContext
    {
        /// Returns the one DemoApplicationContext.
        /// </summary>
        private int windowCount = 0;
       


        // Singleton ApplicationContext
        private static ViewManager context;
        private ViewManager()
        {
        }

        public static ViewManager GetContext()
        {
            if (context == null)
            {
                context = new ViewManager();
            }
            return context;
        }
        /// <summary>
        /// Runs a form in this application context
        /// </summary>
        public void RunNew()
        {
            // Create the window and the controller
            ViewForm window = new ViewForm();
            new Controller(window);

            // One more form is running
            windowCount++;

            // When this form closes, we want to find out
            window.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            // Run the form
            window.Show();


    }

        public void RunNew(Spreadsheet spreadsheet, string filename)
        {
            // Create the window and the controller
            ViewForm window = new ViewForm();
            System.Diagnostics.Debug.Print(filename);
            new Controller(window, spreadsheet, filename);

            // One more form is running
            windowCount++;

            // When this form closes, we want to find out
            window.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            // Run the form
            window.Show();
        }
    }
}
