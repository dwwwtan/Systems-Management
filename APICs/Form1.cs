using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace APICs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    //Input Box
        private string ShowInputBox(string prompt, string title)
        {
            Form inputForm = new Form();
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.Text = title;

            Label lblPrompt = new Label();
            lblPrompt.Text = prompt;
            lblPrompt.SetBounds(10, 10, 200, 20);
            inputForm.Controls.Add(lblPrompt);

            TextBox txtInput = new TextBox();
            txtInput.SetBounds(10, 40, 200, 20);
            inputForm.Controls.Add(txtInput);

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.DialogResult = DialogResult.OK;
            btnOk.SetBounds(10, 70, 75, 27);
            inputForm.Controls.Add(btnOk);

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.SetBounds(90, 70, 75, 27);
            inputForm.Controls.Add(btnCancel);

            string input = string.Empty;

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                input = txtInput.Text;
            }

            return input;
        }

    //Open File
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string fileName = ShowInputBox("Enter file name:", "Open File");
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.GetFullPath(fileName);

                if (File.Exists(filePath))
                {
                    MessageBox.Show($"Opening file: {filePath}", "Open File");
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(filePath);
                        psi.UseShellExecute = true;
                        psi.WindowStyle = ProcessWindowStyle.Normal;
                        Process.Start(psi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening file: {ex.Message}", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("File not found!", "Error");
                }
            }
        }

    //Copy File
        private void btnCopyFile_Click(object sender, EventArgs e)
        {
            string[] fileNames = ShowInputBoxCopy("Enter source and destination file names:", "Copy File");
            if (fileNames.Length == 2)
            {
                string sourceFileName = fileNames[0];
                string destinationFileName = fileNames[1];

                string sourceFilePath = Path.GetFullPath(sourceFileName);
                string destinationFilePath = Path.GetFullPath(destinationFileName);

                if (File.Exists(sourceFilePath))
                {
                    if (File.Exists(destinationFilePath))
                    {
                        DialogResult result = MessageBox.Show("The destination file already exists. Do you want to overwrite it?", "File Exists", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                        {
                            MessageBox.Show("File copy operation canceled.", "Copy File");
                            return;
                        }
                    }

                    try
                    {
                        File.Copy(sourceFilePath, destinationFilePath, true);
                        MessageBox.Show($"File copied successfully from {sourceFilePath} to {destinationFilePath}", "Copy File");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error copying file: {ex.Message}", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Source file not found!", "Error");
                }
            }
        }

        //Input Box Copy File
        private string[] ShowInputBoxCopy(string prompt, string title)
        {
            Form inputForm = new Form();
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.Text = title;

            Label lblPrompt = new Label();
            lblPrompt.Text = prompt;
            lblPrompt.SetBounds(10, 10, 200, 20);
            inputForm.Controls.Add(lblPrompt);

            TextBox txtSourceInput = new TextBox();
            txtSourceInput.SetBounds(10, 40, 200, 20);
            inputForm.Controls.Add(txtSourceInput);

            TextBox txtDestinationInput = new TextBox();
            txtDestinationInput.SetBounds(10, 70, 200, 20);
            inputForm.Controls.Add(txtDestinationInput);

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.DialogResult = DialogResult.OK;
            btnOk.SetBounds(10, 100, 75, 27);
            inputForm.Controls.Add(btnOk);

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.SetBounds(90, 100, 75, 27);
            inputForm.Controls.Add(btnCancel);

            string[] inputs = new string[2];

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                inputs[0] = txtSourceInput.Text;
                inputs[1] = txtDestinationInput.Text;
            }

            return inputs;
        }

    //Delete File
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            string fileName = ShowInputBox("Enter file name:", "Delete File");
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.GetFullPath(fileName);

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                        MessageBox.Show($"File deleted: {filePath}", "Delete File");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting file: {ex.Message}", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("File not found!", "Error");
                }
            }
        }

    // Get File Attributes
        private void btnGetFileAttributes_Click(object sender, EventArgs e)
        {
            string fileName = ShowInputBox("Enter file name:", "Delete File");
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.GetFullPath(fileName);

                if (File.Exists(filePath))
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        DateTime created = fileInfo.CreationTime;
                        DateTime modified = fileInfo.LastWriteTime;
                        DateTime accessed = fileInfo.LastAccessTime;
                        string attributeText = $"Name: {fileInfo.Name}\n" +
                                               $"Size: {(fileInfo.Length / (1024.0 * 1024.0)):0.00} MB\n" +
                                               $"Type: {fileInfo.Extension}\n" +
                                               $"Path: {filePath}\n\n" +
                                               $"Created: {created}\n" +
                                               $"Modified: {modified}\n" +
                                               $"Accessed: {accessed}\n";

                        MessageBox.Show(attributeText, "File Attributes");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while getting the file attributes: {ex.Message}", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("File not found!", "Error");
                }
            }
        }

    //Create File
        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            string fileName = ShowInputBox("Enter file name:", "Create File");
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.GetFullPath(fileName);
                try
                {
                    using (File.Create(filePath))
                    {
                        MessageBox.Show($"Created file: {filePath}", "Create File");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while creating the file: {ex.Message}", "Error");
                }
            }
        }

    //Systems Information
        private void btnInfo_Click(object sender, EventArgs e)
        {
            string systemInfo = $"Operating System: {Environment.OSVersion.VersionString}\n" +
                $"Processor: {Environment.ProcessorCount} cores\n" +
                $".NET Framework Version: {Environment.Version}\n" +
                $"Current Directory: {Environment.CurrentDirectory}";

            MessageBox.Show(systemInfo, "System Information");
        }

        // Khai báo DllImport để nhập hàm GetDiskFreeSpaceEx
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
            out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes,
            out ulong lpTotalNumberOfFreeBytes);

        // Khai báo struct MEMORYSTATUSEX
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        // Khai báo DllImport để nhập hàm GlobalMemoryStatusEx
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    //Task Manager
        private void btnTaskManager_Click(object sender, EventArgs e)
        {
        //ROM
            string driveName = "C:\\";
            ulong freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes;
            //Gọi hàm API GetDiskFreeSpaceEx
            bool diskSuccess = GetDiskFreeSpaceEx(driveName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
        //RAM
            MEMORYSTATUSEX memoryStatus = new MEMORYSTATUSEX();
            memoryStatus.dwLength = (uint)Marshal.SizeOf(memoryStatus);
            // Gọi hàm API GlobalMemoryStatusEx
            bool memorySuccess = GlobalMemoryStatusEx(ref memoryStatus);
            bool success = diskSuccess && memorySuccess;

            if (success)
            {
                string taskManager = $"ROM:\n" +
                    $"Drive: {driveName}\n" +
                    $"Free Space: {(freeBytesAvailable / (1024.0 * 1024.0 * 1024.0)):0.00} GB\n" +
                    $"Total Space: {(totalNumberOfBytes / (1024.0 * 1024.0 * 1024.0)):0.00} GB\n\n\n" +
                    $"RAM:\n" +
                    $"Total Memory: {(memoryStatus.ullTotalPhys / (1024.0 * 1024.0 * 1024.0)):0.00} GB\n" +
                    $"Available Memory: {(memoryStatus.ullAvailPhys / (1024.0 * 1024.0 * 1024.0)):0.00} GB\n\n\n";

                MessageBox.Show(taskManager, "Task Manager");
            }
            else
            {
                int errorCode = Marshal.GetLastWin32Error();
                Console.WriteLine("Failed to get disk information. Error code: " + errorCode);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}