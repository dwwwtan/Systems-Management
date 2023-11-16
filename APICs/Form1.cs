using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace APICs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

//Input Box to enter the file path
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
    //Import the ShellExecute function  from the shell32 library
        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

    //OpenFile_btn
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
                        IntPtr result = ShellExecute(IntPtr.Zero, "open", filePath, "", "", 1);
                        //Check if an error occurs when opening the file
                        if (result.ToInt32() <= 32)
                        MessageBox.Show("Error opening file: " + result.ToString());
                    }
                    catch (Exception ex)
                    { MessageBox.Show($"Error opening file: {ex.Message}", "Error"); }
                }
                else MessageBox.Show("File not found!", "Error");
            }
        }

//Copy File
    //Import the CopyFile function from the kernel32 library.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CopyFile(string lpExistingFileName, string lpNewFileName, bool bFailIfExists);

    //CopyFile_btn
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
                        bool success = CopyFile(sourceFilePath, destinationFilePath, false);
                        // if (success == true)
                        if (success)
                        MessageBox.Show($"File copied successfully from {sourceFilePath} to {destinationFilePath}", "Copy File");
                        else
                        {
                            int error = Marshal.GetLastWin32Error();
                            MessageBox.Show($"Error copying file. Error code: {error}", "Error");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error copying file: {ex.Message}", "Error");
                    }
                }

                else
                MessageBox.Show("Source file not found!", "Error");
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
    //Import the DeleteFile function from the kernel 32 library.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeleteFile(string lpFileName);

    //DeleteFile_btn
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
                        bool success = DeleteFile(filePath);
                        //if (success == true)
                        if (success)
                        MessageBox.Show($"File deleted: {filePath}", "Delete File");
                        else
                        {
                            int errorCode = Marshal.GetLastWin32Error();
                            MessageBox.Show($"Error deleting file. Error code: {errorCode}", "Error");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting file: {ex.Message}", "Error");
                    }
                }
                else
                MessageBox.Show("File not found!", "Error");
            }
        }

//File Attributes
    //Import the GetFileAttributesEx function from the kernel32 library.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetFileAttributes(string lpFileName, out FileAttributes fileAttributes);

    //GetFileAttributes_btn
        private void btnGetFileAttributes_Click(object sender, EventArgs e)
        {
            string fileName = ShowInputBox("Enter file name:", "Get File Attributes");
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.GetFullPath(fileName);

                if (File.Exists(filePath))
                {
                    try
                    {
                        FileAttributes fileAttributes;
                        bool success = GetFileAttributes(filePath, out fileAttributes);
                        if (success)
                        {
                            FileInfo fileInfo = new FileInfo(filePath);
                            DateTime created = fileInfo.CreationTime;
                            DateTime modified = fileInfo.LastWriteTime;
                            DateTime accessed = fileInfo.LastAccessTime;

                            string attributeText = $"Name: {fileInfo.Name}\n" + $"Size: {(fileInfo.Length / (1024.0 * 1024.0)):0.00} MB\n" +
                                                   $"Type: {fileInfo.Extension}\n" + $"Location: {filePath}\n\n" + $"Created: {created}\n" +
                                                   $"Modified: {modified}\n" + $"Accessed: {accessed}\n";
                            MessageBox.Show(attributeText, "File Attributes");
                        }
                        else MessageBox.Show("Error getting file attributes", "Error");
                    }
                    catch (Exception ex)
                    { MessageBox.Show($"An error occurred while getting the file attributes: {ex.Message}", "Error"); }
                }
                else { MessageBox.Show("File not found!", "Error"); }
            }
        }

//Create File
    //Import the CreateFile function from the kernel32 library.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile
        );

        // Constants for file access and creation disposition
        public const int GENERIC_WRITE = 0x40000000;
        public const int OPEN_ALWAYS = 4;

    //CreateFile_btn
        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            string fileName = ShowInputBox("Enter file name:", "Create File");
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.GetFullPath(fileName);
                try
                {
                    // Call the CreateFile function
                    IntPtr handle = CreateFile(
                        filePath,
                        GENERIC_WRITE,
                        0,
                        IntPtr.Zero,
                        OPEN_ALWAYS,
                        0,
                        IntPtr.Zero
                    );
                    //check whether the file creation was successful or not.
                    if (handle != IntPtr.Zero && handle.ToInt64() != -1)
                    {
                        // File creation successful
                        MessageBox.Show($"Created file: {filePath}", "Create File");
                        CloseHandle(handle);
                    }
                    else throw new Exception("Failed to create the file.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while creating the file: {ex.Message}", "Error");
                }
            }
        }

//Systems Information
    //GetComputerName
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetComputerName(StringBuilder lpBuffer, ref uint lpnSize);

    //Info_btn
        private void btnInfo_Click(object sender, EventArgs e)
        {
            StringBuilder systemInfoBuilder = new StringBuilder(256);
            // Computer Name
            uint bufferSize = (uint)systemInfoBuilder.Capacity;
            if (GetComputerName(systemInfoBuilder, ref bufferSize))
            {
                string computerName = systemInfoBuilder.ToString();
                systemInfoBuilder.AppendLine($"Computer Name: {computerName}");
            }
            // Operating System
            OperatingSystem os = Environment.OSVersion;
            systemInfoBuilder.AppendLine($"Operating System: {os.VersionString}");
            // Processor
            int processorCount = Environment.ProcessorCount;
            systemInfoBuilder.AppendLine($"Processor: {processorCount} cores");
            // .NET Framework Version
            Version dotNetVersion = Environment.Version;
            systemInfoBuilder.AppendLine($".NET Framework Version: {dotNetVersion}");
            // Current Directory
            string currentDirectory = Environment.CurrentDirectory;
            systemInfoBuilder.AppendLine($"Current Directory: {currentDirectory}");

            MessageBox.Show(systemInfoBuilder.ToString(), "System Information");
        }

//Task Manager.
    //Import the GetDiskFreeSpaceEx function.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
            out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes,
            out ulong lpTotalNumberOfFreeBytes);

    //Import the MEMORYSTATUSEX struct.
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

    //Import the GlobalMemoryStatusEx function.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    //Task Manager
        private void btnTaskManager_Click(object sender, EventArgs e)
        {
        //ROM
            string driveName = "C:\\";
            ulong freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes;
            //Call the GetDiskFreeSpaceEx function.
            bool diskSuccess = GetDiskFreeSpaceEx(driveName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
        //RAM
            MEMORYSTATUSEX memoryStatus = new MEMORYSTATUSEX();
            memoryStatus.dwLength = (uint)Marshal.SizeOf(memoryStatus);
            //Call the GlobalMemoryStatusEx function.
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
                MessageBox.Show($"Failed to get disk information. Error code: " + errorCode);
            }
        }

// Import the CloseHandle function from the kernel32 library
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}