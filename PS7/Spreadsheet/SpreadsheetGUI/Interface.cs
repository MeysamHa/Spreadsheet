using SSGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    public interface Interface
    {
        //string Title { set; }
        //event Action<string> UpdateEvent;
        //event Action closeEvent;
        //event Action <string> openEvent;

        //void Doclose();
        //string CellValue { set; }
        //string CellName { get; set; }
        //string CellContent { set; }
        //void SetCell(int col, int row, string content);
        //bool Changed { set; get; }
        //string Message { set; }
        //void DoClose();
        /// <summary>
        /// 
        /// </summary>
        event Action<string> UpdateEvent;

        /// <summary>
        /// 
        /// </summary>
        //event Action NewEvent;

        /// <summary>
        /// 
        /// </summary>
        event Action<string> OpenEvent;

        event Action<string> SaveEvent;

        event Action CloseEvent;

        event Action HelpEvent;
        SelectionChangedHandler SelectionChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //SelectionChangedHandler SelectionChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string CellName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string CellValue { set; }

        /// <summary>
        /// 
        /// </summary>
        string CellContent { set; }

        /// <summary>
        /// 
        /// </summary>
        string Title { set; }

        /// <summary>
        /// 
        /// </summary>
        string Message { set; }

        bool Changed { set; get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="content"></param>
        void SetCell(int col, int row, string content);

        void DoClose();
    }

}

