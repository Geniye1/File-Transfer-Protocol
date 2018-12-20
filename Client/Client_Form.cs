using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

/*
 * TODO
 * ################################################################
 * - Use ZipArchive to zip a selected file and send it across the 
 * connection
 *      (CLIENT)
 *      - Compress the selected file into a zip file
 *      - Get the size of the file with >>> int len = (int) new FileInfo(fileName).Length;
 *      - Send the name and size in a packet before the data
 *      - Wait for the server to respond with the OK to send the file
 *      - Send the file
 *      (SERVER)
 *      - Receive the flag of "Zip"
 *      - Receive the name and size of the file
 *      - Split it into two usable strings
 *      - Send the OK to the client that the server is ready for the file's data
 *      - Receive the file's data
 *      - Parse it into a new TXT file
 *      - WriteAllBytes into a .7z file
 *      - Extract the Zip into the wanted directory
*/

namespace Client
{
    public partial class Client_Form : Form
    {
        string fileName;
        string safeFileName;
        string filePath;
        string fileExt;
        int len;

        public Client_Form()
        {
            InitializeComponent();
        }

        private void Client_Form_Load(object sender, EventArgs e)
        {
            
            
        }

        /*
         * Abstract bottom two methods to new class to allow for one single
         * application to either be a server or a client
        */  

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                // IP and port the TCP server is running on
                string ip = "192.168.1.9";
                const int port = 9999;

                // Initialize a new TcpClient
                TcpClient client = new TcpClient();

                // Update the user
                OutputDialog.Text += "Attempting connection with " + ip + " and port " + port + "\n" +
                                     "---------------------------------------------------------------\n";
                
                // Connect the client to the given IP and port
                client.Connect("192.168.1.9", 9999);

                // Update the user
                OutputDialog.Text += "Connection successully established...\n" +                                    
                                     "---------------------------------------------------------------\n";

                // Get the NetworkStream from the connected client
                NetworkStream stream = client.GetStream();

                // Encoder to encode the message
                ASCIIEncoding asc = new ASCIIEncoding();

                byte[] flag = new byte[20];
                if (TextRadio.Checked)
                {
                    flag = asc.GetBytes("Text");

                    // Update the user
                    OutputDialog.Text += "Transmitting Flag...\n" +
                                         "---------------------------------------------------------------\n";

                    stream.Write(flag, 0, flag.Length);

                    // Populate a byte array with the encoded message
                    byte[] data = asc.GetBytes(MessageDialog.Text);

                    // Update the user
                    OutputDialog.Text += "Transmitting Data...\n" +
                                         "---------------------------------------------------------------\n";

                    // Write the message to the stream and the TCP server
                    stream.Write(data, 0, data.Length);
                }
                else if (ImageRadio.Checked)
                {
                    flag = asc.GetBytes("Image");

                    // Update the user
                    OutputDialog.Text += "Transmitting Flag...\n" +
                                         "---------------------------------------------------------------\n";

                    stream.Write(flag, 0, flag.Length);
                    stream.Flush();

                    byte[] imageBytes = File.ReadAllBytes(fileName);
                    //Debug.WriteLine("Client is sending > {0} bytes", imageBytes.Length);

                    byte[] len = BitConverter.GetBytes(imageBytes.Length);
                    //Debug.WriteLine(len.Length);
                    stream.Write(len, 0, len.Length);
                    stream.Flush();


                    stream.Write(imageBytes, 0, imageBytes.Length);
                    
                }
                else if (singleFileRadio.Checked)
                {
                    // Get the bytes for the flag to alert the server
                    flag = asc.GetBytes("Single");

                    // Update the user
                    OutputDialog.Text += "Transmitting Flag...\n" +
                                         "---------------------------------------------------------------\n";

                    // Write the flag and flush the stream to make room for later data
                    stream.Write(flag, 0, flag.Length);
                    stream.Flush();

                    // Get bytes of the safe file name and the size 
                    byte[] extBytes = asc.GetBytes(safeFileName + " " + len);                  

                    // Update the user
                    OutputDialog.Text += "Transmitting extension of file...\n" +
                                         "---------------------------------------------------------------\n";

                    // Write the extension and size to the stream
                    stream.Write(extBytes, 0, extBytes.Length);
                    stream.Flush();

                    // Stop the client here and wait for the server to respond with the OK to send the 
                    // rest of the data
                    byte[] wait = new byte[2];
                    int length = stream.Read(wait, 0, wait.Length);

                    // Convert the bytes 
                    string response = "";
                    for (int i = 0; i < length; i++)
                    {
                        response += Convert.ToChar(wait[i]);
                    }

                    // Check if the server is ready
                    if (response == "OK")
                    {
                        // Read the bytes of the file itself
                        byte[] fileDataRaw = File.ReadAllBytes(fileName);

                        // Update the user
                        OutputDialog.Text += "Transmitting raw file data...\n" +
                                             "---------------------------------------------------------------\n";

                        // Write the extension to the stream
                        stream.Write(fileDataRaw, 0, fileDataRaw.Length);
                        stream.Flush();

                        // Update the user
                        OutputDialog.Text += "File successfully sent to the server!\n" +
                                             "---------------------------------------------------------------\n";
                    }               
                }
                else if (folderRadio.Checked)
                {
                    // Get the bytes for the flag to alert the server
                    flag = asc.GetBytes("Zip");

                    // Update the user
                    OutputDialog.Text += "Transmitting Flag...\n" +
                                         "---------------------------------------------------------------\n";

                    // Write the flag and flush the stream to make room for later data
                    stream.Write(flag, 0, flag.Length);
                    stream.Flush();

                    // TODO: Ensure that there isn't already a file with the same name otherwise
                    // ZipFile will have a stroke
                    ZipFile.CreateFromDirectory(fileName, @"C:\TCP_Messages\result.7z");
                    fileName = @"C:\TCP_Messages\result.7z";
                    byte[] zipRaw = File.ReadAllBytes(@"C:\TCP_Messages\result.7z");

                    len = zipRaw.Length;
                    
                    // Get bytes of the safe file name and the size 
                    byte[] extBytes = asc.GetBytes(safeFileName + " " + len);

                    // Update the user
                    OutputDialog.Text += "Transmitting file information...\n" +
                                         "---------------------------------------------------------------\n";

                    // Write the extension and size to the stream
                    stream.Write(extBytes, 0, extBytes.Length);
                    stream.Flush();

                    // Stop the client here and wait for the server to respond with the OK to send the 
                    // rest of the data
                    byte[] wait = new byte[2];
                    int length = stream.Read(wait, 0, wait.Length);

                    // Convert the bytes 
                    string response = "";
                    for (int i = 0; i < length; i++)
                    {
                        response += Convert.ToChar(wait[i]);
                    }

                    // Check if the server is ready
                    if (response == "OK")
                    {
                        // Read the bytes of the file itself
                        byte[] fileDataRaw = File.ReadAllBytes(fileName);

                        // Update the user
                        OutputDialog.Text += "Transmitting raw file data...\n" +
                                             "---------------------------------------------------------------\n";

                        // Write the extension to the stream
                        stream.Write(fileDataRaw, 0, fileDataRaw.Length);
                        stream.Flush();

                        // Update the user
                        OutputDialog.Text += "File successfully sent to the server!\n" +
                                             "---------------------------------------------------------------\n";
                    }

                }

                // Prepare and receive a response to the server
                //byte[] response = new byte[100];
                //int length = stream.Read(response, 0, 100);

                //// Push the response to the user
                //for (int i = 0; i < length; i++)
                //{
                //    OutputDialog.Text += Convert.ToChar(response[i]);
                //}

                //OutputDialog.Text += "\n---------------------------------------------------------------\n";

                //// Clean up
                //client.Dispose();
                //client.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        // Image file chooser
        private void ChooseFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;

            Debug.WriteLine(fileName);
        }

        // Single file choose
        private void singleFileChooser_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            fileExt = Path.GetExtension(fileName);
            len = (int) new FileInfo(fileName).Length;
            safeFileName = openFileDialog1.SafeFileName;

            Debug.WriteLine(fileName + " and the extension has been read as " + fileExt);
        }

        // Folder chooser
        private void folderChooserButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            fileName = folderBrowserDialog1.SelectedPath;
            safeFileName = Path.GetFileName(fileName);
            Debug.WriteLine(fileName);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void singleFileRadio_CheckedChanged(object sender, EventArgs e)
        {

        }

        
    }
}
