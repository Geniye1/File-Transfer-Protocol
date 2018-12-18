using System;
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
        //private Socket s;

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

            hasBegun = true;
        }

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

                    // Initialize a buffer for the data and receive the data
                    byte[] buffer = new byte[100];
                    int length = s.Receive(buffer);
                    

                    Debug.WriteLine("Server is receiving the flag of > {0} bytes", buffer.Length);

                    // Update the user
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        OutputDialog.Text += "Data received...\n" +
                                         "---------------------------------------------------------------\n";
                    }));

                    String flag = "";
                    for (int i = 0; i < length; i++)
                    {
                        flag += Convert.ToChar(buffer[i]);
                    }

                    if (flag == "Text")
                    {
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
                        byte[] imgLenBytes = new byte[4];
                        int res = s.Receive(imgLenBytes);

                        int imgLength = BitConverter.ToInt32(imgLenBytes, 0);
                        Debug.WriteLine(imgLength + " " + imgLenBytes.Length);

                        
                        

                        //int offset = 0;
                        //while (true)
                        //{
                        //    Debug.WriteLine("In loop");
                        //    if (s.Available - offset != 0 && s.Available != 0)
                        //    {
                        //        Debug.WriteLine("Reading... " + s.Available + " " + offset);                                                          
                        //    }
                        //    else
                        //    {
                        //        Debug.WriteLine("Reading... " + s.Available + " " + offset);
                        //        Debug.WriteLine("Broke from while loop");                         
                        //        break;
                        //    }

                        //    offset = s.Available;
    
                        //    Thread.Sleep(500);

                        //}
                        //byte[] imageData = new byte[17922];
                        //int len = s.Receive(imageData);

                        FileInfo file = new FileInfo("pleasework.png");

                        var fuckyou = new NetworkStream(s);
                        var fileStream = new FileStream(@"D:\pleasework.jpg", FileMode.Create);

                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Awaiting to receive data");        
                            fuckyou.CopyToAsync(fileStream);
                            Thread.Sleep(2000);
                            Debug.WriteLine(fileStream.Length);

                            Bitmap bitmap = (Bitmap)Image.FromStream(fileStream);
                            bitmap.Save("pleasework.png", System.Drawing.Imaging.ImageFormat.Jpeg);

                            //Image image = Image.FromStream(fileStream);
                            //image.Save("pleasework.png", ImageFormat.Png);
                        });  

                    }

                    // Encoder to send response to client
                    ASCIIEncoding asc = new ASCIIEncoding();

                    // Send the response
                    s.Send(asc.GetBytes("This was changed from the server-side code!"));

                    // Update the user
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        OutputDialog.Text += "\nThe server successfully received the acknowledgement\n" +
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
                Debug.WriteLine(ex.StackTrace);
            }

        }

        async private void GetAndSaveImage(Socket s)
        {
            
            


        }
    }
}
