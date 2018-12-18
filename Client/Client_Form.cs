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

namespace Client
{
    public partial class Client_Form : Form
    {
        public enum DataTransferType {
            Text = 0,
            Image = 1
        }

        public Client_Form()
        {
            InitializeComponent();
        }

        private void Client_Form_Load(object sender, EventArgs e)
        {
            int o = (int)DataTransferType.Text;
            
        }

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

                // Populate a byte array with the encoded message
                byte[] data = asc.GetBytes(MessageDialog.Text);

                // Update the user
                OutputDialog.Text += "Transmitting message...\n" +
                                     "---------------------------------------------------------------\n";

                // Write the message to the stream and the TCP server
                stream.Write(data, 0, data.Length);

                // Prepare and receive a response to the server
                byte[] response = new byte[100];
                int length = stream.Read(response, 0, 100);

                // Push the response to the user
                for (int i = 0; i < length; i++)
                {
                    OutputDialog.Text += Convert.ToChar(response[i]);
                }

                OutputDialog.Text += "\n---------------------------------------------------------------\n";

                // Clean up
                client.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
