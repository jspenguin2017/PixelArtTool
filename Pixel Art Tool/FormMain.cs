using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pixel_Art_Tool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        #region Constants and Variables

        /// <summary>
        /// The title of this software
        /// </summary>
        public const string TITLE = "Pixel Art Tool";

        /// <summary>
        /// The project page
        /// </summary>
        public const string PROJECT_PAGE = "https://github.com/jspenguin2017/PixelArtTool";

        /// <summary>
        /// Block struct
        /// </summary>
        public struct Block
        {
            public string name;
            public Color color;
        }

        /// <summary>
        /// This will hold every Block of current database
        /// </summary>
        public Block[] blocks;

        #endregion

        #region Event Handlers

        /// <summary>
        /// Form load, initialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            //Set default output directory to be startup path
            TBOutDir.Text = Application.StartupPath;
            //Put default database into dropdown box (this would be the first entry)
            //This will also trigger database loading
            CBDatabase.SelectedIndex = 0;
            //Set default options
            BtnRestoreDefault.PerformClick();
        }

        /// <summary>
        /// Change mouse pointer when a file is dragged onto the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Event argument, used to check if item dragged onto the form is a file</param>
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                //Get dragged file path
                string droppedFilePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                //Check if it is a directory
                if (!File.GetAttributes(droppedFilePath).HasFlag(FileAttributes.Directory))
                {
                    //It is a file, change mouse pointer
                    e.Effect = DragDropEffects.Copy;
                }
            }
            catch (Exception err) when (err is NullReferenceException || err is ArgumentException || err is PathTooLongException ||
                                        err is FileNotFoundException || err is DirectoryNotFoundException || err is IOException ||
                                        err is UnauthorizedAccessException)
            {
                //Fail silently
            }
        }

        /// <summary>
        /// Load path of the file that is dropped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Event argument, used to get file path</param>
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //Get dropped file path
                string droppedFilePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                //Check if it is a directory
                if (!File.GetAttributes(droppedFilePath).HasFlag(FileAttributes.Directory))
                {
                    //It is a file, put its path into textboxes
                    TBInImage.Text = droppedFilePath;
                    TBProjectName.Text = Path.GetFileNameWithoutExtension(droppedFilePath);
                }
            }
            catch (Exception err) when (err is NullReferenceException || err is ArgumentException || err is PathTooLongException ||
                                        err is FileNotFoundException || err is DirectoryNotFoundException || err is IOException ||
                                        err is UnauthorizedAccessException)
            {
                //Fail silently
            }
        }

        /// <summary>
        /// Output folder browsing button click, let user pick an output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOutBrowse_Click(object sender, EventArgs e)
        {
            if (DialogFolderBrowsing.ShowDialog() == DialogResult.OK)
            {
                TBOutDir.Text = DialogFolderBrowsing.SelectedPath;
            }
        }

        /// <summary>
        /// Input file browsing button click
        /// Let user pick an input file then put default prefix into the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInBrowse_Click(object sender, EventArgs e)
        {
            DialogFileOpen.Filter = "Supported Formats|*.bmp;*.gif;*.jpg;*.png;*.tiff|All Files|*.*";
            if (DialogFileOpen.ShowDialog() == DialogResult.OK)
            {
                TBInImage.Text = DialogFileOpen.FileName;
                TBProjectName.Text = Path.GetFileNameWithoutExtension(DialogFileOpen.FileName);
            }
        }

        /// <summary>
        /// Database dropdown list change, load database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //This is a dropdown list, so "default:" is not needed
            switch (CBDatabase.SelectedIndex)
            {
                case 0:
                    LoadDB(Properties.Resources.Trove_Neutral_by_Summer_Haas);
                    break;
            }
        }

        /// <summary>
        /// Generate button click, validate input then generate construction plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnGenerate_Click(object sender, EventArgs e)
        {
            //Update UI
            this.Text = TITLE + " - Please wait... ";
            this.Enabled = false;
            //Validate input then call generate function
            if (ValidateInputs(out string projectFolder))
            {
                switch(await new ImLib(blocks).Generate(TBInImage.Text, int.Parse(TBMaxHeight.Text), int.Parse(TBMaxWidth.Text), CBAllowUpscale.Checked,
                                                        projectFolder, CBSaveDownscaled.Checked, CBSavePixelated.Checked, CBSaveFiltered.Checked,
                                                        CBSaveFilteredPixelated.Checked))
                {
                    case ImLib.ImLibResult.Succeed:
                        if (CBOpenDirWhenDone.Checked)
                        {
                            Process.Start(TBOutDir.Text);
                        }
                        break;
                    case ImLib.ImLibResult.TooLarge:
                        MessageBox.Show("Error: Image too large. ");
                        break;
                    case ImLib.ImLibResult.ReadError:
                        MessageBox.Show("Could not read source image. ");
                        break;
                    case ImLib.ImLibResult.WriteError:
                        MessageBox.Show("Could not write to project folder. ");
                        break;
                }
            }
            //Update UI
            this.Text = TITLE;
            this.Enabled = true;
        }

        /// <summary>
        /// Save current database index button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveIndex_Click(object sender, EventArgs e)
        {
            //Prepare file save dialog
            DialogFileSave.Filter = "Text Files|*.txt|All Files|*.*";
            DialogFileSave.FileName = CBDatabase.Text + " index.txt";
            //Show dialog and see if "Save" is clicked
            if (DialogFileSave.ShowDialog() == DialogResult.OK)
            {
                //Prepare data to write
                string[] lines = new string[blocks.Count()];
                //Loop thorugh blocks and prepare lines to write out
                for (int i = 0; i < blocks.Count(); i++)
                {
                    lines[i] = ImLib.ToTwoDigitHex(i) + " - " + blocks[i].name;
                }
                //Write lines out
                try
                {
                    //Write lines
                    File.WriteAllLines(DialogFileSave.FileName, lines);
                    //Launch folder when finished if needed
                    if (CBOpenDirWhenDone.Checked)
                    {
                        Process.Start(Path.GetDirectoryName(DialogFileSave.FileName));
                    }
                }
                catch (Exception err) when (err is IOException || err is UnauthorizedAccessException)
                {
                    //Couldn't save file, notify the user
                    MessageBox.Show("Failed to save full index, error message: " + err.Message);
                }
            }
        }

        /// <summary>
        /// Restore default click, restore options to their default state
        /// Current database will not be changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRestoreDefault_Click(object sender, EventArgs e)
        {
            TBMaxHeight.Text = "150";
            TBMaxWidth.Text = "999999";
            CBAllowUpscale.Checked = false;
            CBSaveDownscaled.Checked = false;
            CBSavePixelated.Checked = false;
            CBSaveFiltered.Checked = false;
            CBSaveFilteredPixelated.Checked = true;
            CBOpenDirWhenDone.Checked = true;
        }

        /// <summary>
        /// Label about click, take user to the project page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(PROJECT_PAGE);
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Load a blocks database
        /// </summary>
        /// <param name="database">The database, formatted with \r\n and \t</param>
        private void LoadDB(string database)
        {
            //The new line character of resource file should not change when changing platform, however, it is not tested
            //The following line works on Windows
            string[] entries = database.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //Allocate memory
            blocks = new Block[entries.Count()];
            //Initialize each entry
            for (int i = 0; i < entries.Count(); i++)
            {
                //Temporary variable temp will have structure like this: [name, r, g, b]
                string[] temp = entries[i].Split('\t');
                //Initialize this Block
                blocks[i] = new Block
                {
                    name = temp[0],
                    color = Color.FromArgb(int.Parse(temp[1]), int.Parse(temp[2]), int.Parse(temp[3]))
                };
            }
        }

        /// <summary>
        /// Validate input fields
        /// This method will also try to create project folder then validate other inputs
        /// Error messages will be shown to the user if needed
        /// </summary>
        /// <param name="projectFolder">The path of the project folder will be returned, if there is an error, an empty string will be returned</param>
        /// <returns>true if tests passed, false otherwise</returns>
        private bool ValidateInputs(out string projectFolder)
        {
            //=====Project folder=====
            //Combine project folder path
            try
            {
                projectFolder = Path.Combine(TBOutDir.Text, TBProjectName.Text);
            }
            catch (ArgumentException)
            {
                //We must assign out parameter
                projectFolder = "";
                //Show error message to the user
                MessageBox.Show("Error: Your project name contains special characters. ");
                //Focus textbox and return
                TBProjectName.Focus();
                return false;
            }
            //Check if project folder is already there, and ask user for confirmation if needed
            if (Directory.Exists(projectFolder) &&
                MessageBox.Show("Warning: Project folder already exists, do you wish to continue anyway and overwrite existing files? ", "",
                                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return false;
            }
            //Create project folder
            try
            {
                //This will not throw even if the directory already exist
                Directory.CreateDirectory(projectFolder);
            }
            catch (Exception err) when (err is IOException || err is UnauthorizedAccessException || err is DirectoryNotFoundException)
            {
                //Could not write
                MessageBox.Show("Error: Could not create project folder, please check if you have write permission. ");
                //Focus textbox and return
                TBProjectName.Focus();
                return false;
            }
            catch (Exception err) when (err is ArgumentException)
            {
                //Path is not valid
                MessageBox.Show("Error: Your project name contains special characters. ");
                //Focus textbox and return
                TBProjectName.Focus();
                return false;
            }
            catch (Exception err) when (err is PathTooLongException)
            {
                //Path is too long
                MessageBox.Show("Error: The combined length of output folder path and project name is too long. ");
                //Focus textbox and return
                TBProjectName.Focus();
                return false;
            }
            //=====Max height=====
            //Check if entered max height is valid
            int maxHeight;
            try
            {
                maxHeight = int.Parse(TBMaxHeight.Text);
            }
            catch (Exception err) when (err is FormatException || err is OverflowException)
            {
                //Max height is not valid
                MessageBox.Show("Error: Max height is not a valid integer. ");
                //Focus textbox and return
                TBMaxHeight.Focus();
                return false;
            }
            //Check if max height is larger than 0, upscale check will be done later
            if (maxHeight < 1)
            {
                maxHeight = 1;
            }
            //Write back to the textbox for future use
            TBMaxHeight.Text = maxHeight.ToString();
            //=====Max width=====
            //Check if entered max width is valid
            int maxWidth;
            try
            {
                maxWidth = int.Parse(TBMaxWidth.Text);
            }
            catch (Exception err) when (err is FormatException || err is OverflowException)
            {
                //Max width is not valid
                MessageBox.Show("Error: Max width is not a valid integer. ");
                //Focus textbox and return
                TBMaxWidth.Focus();
                return false;
            }
            //Check if max width is larger than 0, upscale check will be done later
            if (maxWidth < 1)
            {
                maxWidth = 1;
            }
            //Write back to the textbox for future use
            TBMaxWidth.Text = maxWidth.ToString();
            //=====Passed validation=====
            return true;
        }

        #endregion

    }
}
