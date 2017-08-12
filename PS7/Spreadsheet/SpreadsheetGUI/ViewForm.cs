//Meysam Haml
//u0914328
//HW7
using SSGui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class ViewForm : Form, Interface
    {
        private SelectionChangedHandler _selectionChanged;

        public ViewForm()
        {
            InitializeComponent();

        }

        /// Sets the title in the UI
        /// </summary>
        public string Title
        {
            set { Text = value; }


        }
        //public event Action NewEvent;
        public event Action<string> UpdateEvent;
        public event Action CloseEvent;
        public event Action<string> OpenEvent;
        public event Action<string> SaveEvent;
        public event Action HelpEvent;


        public string CellName
        {
            set
            {
                nameTextBox.Text = value.ToString();
            }

            get
            {
                return nameTextBox.Text;
            }
        }
        public string CellValue
        {
            set
            {
                valueTextBox.Text = value.ToString();
            }
        }
         public string CellContent
        {
            set
            {
                formulatxt.Text = value.ToString();
            }
        }
        
        /// Shows the message in the UI.
        /// </summary>
        public string Message
        {
            set { MessageBox.Show(value); }
        }
        public bool Changed
        {
            set
            {
                if (value)
                {
                    if (Text.Last() != '*')
                    {
                        Text += "*";
                    }
                }
                else
                {
                    Text = saveFileDialog1.FileName;
                }
            }

            get
            {
                return Text.Last() == '*';
            }
        }
        public SelectionChangedHandler SelectionChanged
        {
            get
            {
                return _selectionChanged;
            }

            set
            {
                _selectionChanged = value;
                spreadsheetpanel.SelectionChanged += SelectionChanged;
            }
        }
        public void SetCell(int col, int row, string content)
        {
            spreadsheetpanel.SetValue(col, row, content);
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var context = ViewManager.GetContext();
            //open new form
            ViewManager.GetContext().RunNew();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            {
                UpdateEvent(formulatxt.Text);
                if (Text.Last() != '*')
                {
                    Text += "*";
                }
            }


        }

        private void ViewForm_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = openFileDialog1.ShowDialog();

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }



        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (OpenEvent != null)
            {
                OpenEvent(openFileDialog1.FileName);
            }
        }

        private void SpreadsheetWindow_FormClosing(object sender, CancelEventArgs e)
        {
            if (Changed)
            {
                DialogResult result = MessageBox.Show("Unsaved changes.\nDo you wish to save your spreadsheet?",
                        "Unsaved Spreadsheet",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    saveFileDialog1.Filter = "Spreadsheet (*.ss)|*.ss";
                    saveFileDialog1.DefaultExt = "ss";
                    saveFileDialog1.ShowDialog();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (UpdateEvent != null)
            {
                UpdateEvent(formulatxt.Text);
                if (Text.Last() != '*')
                {
                    Text += "*";
                }
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Spreadsheet (*.ss)|*.ss";
            saveFileDialog1.DefaultExt = "ss";
            DialogResult = saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
            if (SaveEvent != null)
            {
                SaveEvent(saveFileDialog1.FileName); 
            }
        }

        private void closeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent();
            }
        }
      
        public void DoClose()
        {

            Close();
            



        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void cellLabel_Click(object sender, EventArgs e)
        {

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void spreadsheetpanel_Load(object sender, EventArgs e)
        {

        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HelpEvent != null)
            {
                HelpEvent();
            }
        }

    }
   

}
