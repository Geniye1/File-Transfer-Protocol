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

/*
 * TODO
 * ################################################################
 * - Send a single text file with a flag to let the server know
 * what the format of the file is
 *      (CLIENT)
 *      - Send flag to server first saying what the extension is
 *      - Send data to server
 *      (SERVER)
 *      - Construct the file name/path from the flag and populate 
 *      it with the data received
 * - Use ZipArchive to zip a selected file and send it across the 
 * connection
*/ 

namespace Client
{
    public partial class Client_Form : Form
    {
        string fileName;
        string filePath;
        string fileExt;

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
                    Debug.WriteLine("Client is sending > {0} bytes", imageBytes.Length);

                    byte[] len = BitConverter.GetBytes(imageBytes.Length);
                    Debug.WriteLine(len.Length);
                    stream.Write(len, 0, len.Length);
                    stream.Flush();


                    stream.Write(imageBytes, 0, imageBytes.Length);
                    
                }
                else if (singleFileRadio.Checked)
                {
                    flag = asc.GetBytes("Single");

                    // Update the user
                    OutputDialog.Text += "Transmitting Flag...\n" +
                                         "---------------------------------------------------------------\n";

                    stream.Write(flag, 0, flag.Length);
                    stream.Flush();

                    // Write to the stream the extension of the coming file
                    byte[] extBytes = asc.GetBytes(fileExt);

                    // Update the user
                    OutputDialog.Text += "Transmitting extension of file...\n" +
                                         "---------------------------------------------------------------\n";

                    stream.Write(extBytes, 0, extBytes.Length);
                    stream.Flush();


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

        private void ChooseFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;

            Debug.WriteLine(fileName);
        }

        private void singleFileChooser_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            fileExt = Path.GetExtension(fileName);

            Debug.WriteLine(fileName + " and the extension has been read as " + fileExt);
        }

        private void folderChooserButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            fileName = folderBrowserDialog1.SelectedPath;
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
