using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace MappMon
{
    public class mySocket
    {

        //static SocketAsyncEventArgs E;
        static string command = "null";
        static string result = "null";
        static int port = 11438;

        public static Boolean login(string user, string pass)
        {
            command = "*select password from users where username='" + user + "'|iam|socool\n";
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            DnsEndPoint hostEntry = new DnsEndPoint("sslab07.cs.purdue.edu", port);

            // Create a socket and connect to the server
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.UserToken = sock;
            sock.ConnectAsync(socketEventArg);


            clientDone.WaitOne();

            for (int i = 0; i < pass.Length; i++)
            {
                if (pass[i] != result[i])
                {
                    return false;
                }
            }
            return true;


        }

        public static string getUsers()
        {
            command = "get-all-users|iam|socool\n";

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            DnsEndPoint hostEntry = new DnsEndPoint("sslab07.cs.purdue.edu", port);

            // Create a socket and connect to the server
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.UserToken = sock;
            sock.ConnectAsync(socketEventArg);


            clientDone.WaitOne();


            return result;
        }

        static ManualResetEvent clientDone = new ManualResetEvent(false);
        // A single callback is used for all socket operations. 
        // This method forwards execution on to the correct handler 
        // based on the type of completed operation
        public static void SocketEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new Exception("Invalid operation completed");
            }
        }

        // Called when a ConnectAsync operation completes
        private static void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Successfully connected to the server

                // Send 'Hello World' to the server
                byte[] buffer = new byte[1024];
                //string command = "get-all-users|iam|socool\n";
                //string command = "GET-ALL-PETS|iam|socool\n";
                char[] C_arr = command.ToCharArray();
                for (int x = 0; x < command.Length; x++)
                {
                    buffer[x] = (byte)C_arr[x];
                }
                //buffer = Encoding.UTF8.GetBytes("GET-ALL-PETS|user|password\n");
                e.SetBuffer(buffer, 0, buffer.Length);
                Socket sock = e.UserToken as Socket;
                bool willRaiseEvent = sock.SendAsync(e);
                //return willRaiseEvent;
                if (!willRaiseEvent)
                {
                    ProcessSend(e);
                }
            }
            else
            {
                throw new SocketException((int)e.SocketError);
            }
        }

        // Called when a ReceiveAsync operation completes
        // </summary>
        private static void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Received data from server

                char[] data = new char[e.Buffer.Length];

                for (int n = 0; n < e.Buffer.Length; n++)
                {
                    data[n] = (char)e.Buffer[n];
                }


                //Debug.WriteLine("e.BufferList size = "+e.BufferList.Count);

                result = new String(data);

                Debug.WriteLine(result);

                // Data has now been sent and received from the server. 
                // Disconnect from the server
                Socket sock = e.UserToken as Socket;
                sock.Shutdown(SocketShutdown.Send);
                sock.Close();
                clientDone.Set();
                //return text;
            }
            else
            {
                //return "false";
                throw new SocketException((int)e.SocketError);
            }
        }


        // Called when a SendAsync operation completes
        private static void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Sent "Hello World" to the server successfully

                //Read data sent from the server
                Socket sock = e.UserToken as Socket;

                byte[] bytes = new byte[512];
                int byteCount = sock.ReceiveBufferSize;
                Debug.WriteLine("byteCount = " + byteCount);
                bool willRaiseEvent = sock.ReceiveAsync(e);
                //return willRaiseEvent;
                //try to use sock.Receive or somethign similar?
                //cycle through all the data one byte at a time..
                //need to get all of data, not just first line!
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
            else
            {
                throw new SocketException((int)e.SocketError);
            }
        }



    }
}