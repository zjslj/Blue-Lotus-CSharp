using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bule_lotus_file_regedit
{
    public partial class Form1 : Form
    {
        private string currentFolderPath;
        public Form1()
        {
            InitializeComponent();
        }


        protected void ClearAllFileds()
        {
            listBoxFolders.Items.Clear();
            listBoxFiles.Items.Clear();

            textBoxFolder.Text = "";
            textBoxFileName.Text = "";
            textBoxFileSize.Text = "";
            textBoxCreationTime = "";
            textBoxLastAccessTime.Text = "";
            textBoxLastWriteTime.Text = "";
        }

        protected void DisplayFileInfo(string fileFullName)
        {
            FileInfo theFile = new FileInfo(fileFullName);
            if(!theFile.Exists)
            {
                throw new FileNotFoundException("File not found : " + fileFullName);
            }

            textBoxFileName.Text = theFile.Name;
            textBoxCreationTime.Text = theFile.CreationTime.ToLongTimeString();
            textBoxLastAccessTime.Text = theFile.LastAccessTime.ToLongTimeString();
            textBoxLastWriteTime.Text = theFile.LastWriteTime.ToLongTimeString();
            textBoxFileSize.Text = theFile.Length.ToString() + "bytes";
        }

        protected void DisplayFolderList(string folderFullName)
        {
            DirectoryInfo theFolder = new DirectoryInfo(folderFullName);
            if(!theFolder.Exists)
            {
                throw new DirectoryNotFoundException("Folder not found:" + folderFullName);
            }
            ClearAllFileds();
            textBoxFolder.Text = theFolder.FullName;
            currentFolderPath = theFolder.FullName;
            //list all subfolders in folder
            foreach(DirectoryInfo nextFolder in theFolder.GetDirectories())
            {
                listBoxFolders.Items.Add(nextFolder.Name);
            }

            foreach(FileInfo nextFile in theFolder.GetFiles())
            {
                listBoxFiles.Items.Add(nextFile.Name);
            }
        }

        protected void OnDispayButtonClick(object sender, EventArgs e)
        {
            try
            {
                string folderPath = textBoxInput.Text;
                DirectoryInfo theFolder = new DirectoryInfo(folderPath);
                if(theFolder.Exists)
                {
                    DisplayFolderList(theFolder.FullName);
                    return;
                }
                FileInfo theFile = new FileInfo(folderPath);
                if(theFile.Exists)
                {
                    DisplayFolderList(theFile.Directory.FullName);
                    int index = listBoxFiles.Items.IndexOf(theFile.Name);
                    listBoxFiles.SetSelected(index, true);
                    return;
                }

                throw new FileNotFoundException("There is no file or folder with this name :" + textBoxInput.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
