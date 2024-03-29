﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.IO.Compression;

/*
 * Features still-to-be-made:
 * --------------------------------------------------
 * Send and save images
 * Send and save files (code, text files)
 * Be able to choose if you want the application to
 * host a TCP server or be a TCP client
*/

namespace TCP_Messenger
{
    public partial class Server_Form : Form
    {

        private bool hasBegun = false;
        private TcpListener listener;
        private string fileName;
        private string safePathName;

        public Server_Form()
        {
            InitializeComponent();
        }

        private void Server_Form_Load(object sender, EventArgs e)
        {

        }

        private void BeginButton_Click(object sender, EventArgs e)
        {
            // Turn off the button
            BeginButton.Enabled = false;

            // Begin the recursive server algorithm
            RunServer();   
        }

        private void pathbtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            fileName = folderBrowserDialog1.SelectedPath;
            safePathName = Path.GetFileName(fileName);
            pathText.Text = fileName;
        }

        /*
         * Abstract RunServer() to a new class to allow for one single
         * application to either be a server or a client
        */

        async private void RunServer()
        {
            try
            {
                if (!hasBegun)
                {
                    // Define the IP and port of the TCP server
                    IPAddress ip = IPAddress.Parse("192.168.1.9");
                    const int port = 9999;

                    // Intialize a listener to listen on the given IP and port
                    listener = new TcpListener(ip, port);

                    // Update the user
                    OutputDialog.Text += "Server begun and listening on port " + port + "\n" +
                                          "Local endpoint is " + listener.LocalEndpoint + "\n" +
                                          "Awaiting a connection...\n" +
                                          "--------------------------------------------------------------\n";
                    hasBegun = true;

                    // Begin listening for connections
                    listener.Start();
                }

                // Push a new thread to run the given delegate asynchronously to prevent UI hang
                await Task.Run(async () =>
                {
                    // Accept the connection's socket
                    Socket s = listener.AcceptSocket();            
                    
                    // Update user (Invoke is used as two threads cannot be updating the same variable at once
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        OutputDialog.Text += "Connection established from " + s.RemoteEndPoint + "\n" +
                                         "Attempting to read data...\n" +
                                         "---------------------------------------------------------------\n";
                    }));

                    // Initialize a buffer for the data and receive the flag
                    byte[] buffer = new byte[100];
                    int length = s.Receive(buffer);                 

                    // Update the user
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        OutputDialog.Text += "Flag received...\n" +
                                         "---------------------------------------------------------------\n";
                    }));

                    // Convert the flag data
                    String flag = "";
                    for (int i = 0; i < length; i++)
                    {
                        flag += Convert.ToChar(buffer[i]);
                    }
         
                    // Decide if the flag is for text or and image
                    if (flag == "Text")
                    {
                        // Initiailize the text byte array and populate it with data
                        byte[] text = new byte[100];
                        int textLength = s.Receive(text);

                        // Push the message to the Output dialog box
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            for (int i = 0; i < textLength; i++)
                            {
                                OutputDialog.Text += Convert.ToChar(text[i]);
                            }

                            OutputDialog.Text += "\n---------------------------------------------------------------";
                        }));
                    }
                    else if (flag == "Image")
                    {
                        // Receive the size of the coming image
                        byte[] imgLenBytes = new byte[4];
                        int res = s.Receive(imgLenBytes);

                        // Convert it to a usable int
                        int imgLength = BitConverter.ToInt32(imgLenBytes, 0);
                        Debug.WriteLine(imgLength + " " + imgLenBytes.Length);

                        // Initialize the stream and File Stream to create the new file
                        var ns = new NetworkStream(s);
                        var fileStream = new FileStream(fileName + @"\" + "pleasework.jpg", FileMode.Create);

                        // Asynchronously receive the image
                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Awaiting to receive data");

                            // Copy the NetworkStream read to the FileStream
                            ns.CopyToAsync(fileStream);     
                            
                            // Give the copy a chance to fully receive the data
                            Thread.Sleep(2000);
                            Debug.WriteLine(fileStream.Length);

                            /*
                             * These two lines will manually save a bitmap from the filestream, 
                             * if needed enable these
                            */ 

                            // Create a new bitmap from the filestream
                            //Bitmap bitmap = (Bitmap)Image.FromStream(fileStream);

                            // Save the bitmap to the file system
                            //bitmap.Save("pleasework.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        });  

                    }
                    else if (flag == "Single")
                    {
                        // Update the user
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            OutputDialog.Text += "Preparing for download...\n" +
                                             "---------------------------------------------------------------\n";
                        }));

                        // Receive the file extension 
                        byte[] fileExtRaw = new byte[50];
                        int res = s.Receive(fileExtRaw);

                        // Encoder to send response to client
                        ASCIIEncoding asci = new ASCIIEncoding();

                        // Alert the client the server is ready to receive the data
                        s.Send(asci.GetBytes("OK"));

                        // Convert the file extension bytes to a usable string
                        string fileInfoGlued = "";
                        for (int i = 0; i < res; i++)
                        {
                            fileInfoGlued += Convert.ToChar(fileExtRaw[i]);
                        }

                        // Split the received information to abstract the name of the file and the size
                        string[] fileInfo = fileInfoGlued.Split(null);

                        // Update the user
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            OutputDialog.Text += "Downloading the file...\n" +
                                             "---------------------------------------------------------------\n";
                        }));

                        // Initialize the buffer array with the size from info and receive the file data
                        byte[] test = new byte[Convert.ToInt32(fileInfo[1])];
                        int resp = s.Receive(test);

                        // Decode the received data 
                        ASCIIEncoding enc = new ASCIIEncoding();
                        string data = enc.GetString(test);
                        
                        // Write the string to a file with the same name
                        File.WriteAllText(fileName + @"\" + fileInfo[0], data);

                        // Update the user
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            OutputDialog.Text += "File succesfully received and saved to the computer!\n" +
                                             "---------------------------------------------------------------\n";
                        }));
                    }
                    else if (flag == "Zip")
                    {
                        // Update the user
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            OutputDialog.Text += "Preparing for download...\n" +
                                             "---------------------------------------------------------------\n";
                        }));

                        // Receive the file extension 
                        byte[] fileExtRaw = new byte[50];
                        int res = s.Receive(fileExtRaw);

                        // Encoder to send response to client
                        ASCIIEncoding asci = new ASCIIEncoding();

                        // Alert the client the server is ready to receive the data
                        s.Send(asci.GetBytes("OK"));

                        // Convert the file extension bytes to a usable string
                        string fileInfoGlued = "";
                        for (int i = 0; i < res; i++)
                        {
                            fileInfoGlued += Convert.ToChar(fileExtRaw[i]);
                        }

                        // Split the received information to abstract the name of the file and the size
                        string[] fileInfo = fileInfoGlued.Split(null);

                        // Update the user
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            OutputDialog.Text += "Downloading the file...\n" +
                                             "---------------------------------------------------------------\n";
                        }));

                        // Initialize the buffer array with the size from info and receive the file data
                        byte[] zipRaw = new byte[Convert.ToInt32(fileInfo[1])];
                        int resp = s.Receive(zipRaw);

                        File.WriteAllBytes(fileName + @"\" + fileInfo[0] + ".7z", zipRaw);

                        ZipFile.ExtractToDirectory(fileName + @"\" + fileInfo[0] + ".7z", @"D:\" + fileInfo[0]);

                        // Update the user
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            OutputDialog.Text += "File succesfully received and saved to the computer!\n" +
                                             "---------------------------------------------------------------\n";
                        }));
                    }

                    // Encoder to send response to client
                    ASCIIEncoding asc = new ASCIIEncoding();

                    // Send the response
                    s.Send(asc.GetBytes("This was changed from the server-side code!"));

                    // Update the user
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        OutputDialog.Text += "The server successfully received the acknowledgement\n" +
                                        "---------------------------------------------------------------\n";
                    }));

                    // Clean up 
                    s.Close();

                    // Recursively run the server again
                    RunServer();
                });
            }
            catch (Exception ex)
            {
                // If something went wrong, write to a log and save it
                string logData = ex.StackTrace;
                File.WriteAllText(@"D:\log.txt", logData);
            }
        }
    }
}
