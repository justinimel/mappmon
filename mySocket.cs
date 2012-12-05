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
        private static string command = "null";
        private static string result = "null";
        private static int BUFFER_SIZE = 40240;
        private static int port = 11436;

        //time format is YYYY-MM-DD HH:MM:SS
        //for example, to get location of user with uid = 2 for the day of december 4th, 
        //getLocations(2, "2012-12-04 00:00:00", "2012-12-05 00:00:00");
        public static string[] getLocations(int uid, string time_from, string time_to)
        {
            result = "null";
            command = "getLocations:" + uid + "$" + time_from + "$" + time_to + "|iam|socool\n";

            send_receive_through_socket();

            if (!result.Contains("[END]"))
            {
                clientDone.Reset();
                string[] error = { "error" };
                return error;
            }

            string tmpRes = result.Substring(0, result.IndexOf("[END]"));


            clientDone.Reset();
            return tmpRes.Split('\n');
        }

        public static bool addLocation(int uid, double longitude, double latitude, double horizontal_acc)
        {
            result = "null";
            command = "addLocation:" + uid + "$" + longitude + "$" + latitude + "$" + horizontal_acc + "|iam|socool\n";

            send_receive_through_socket();

            string tmpRes = result.Substring(0, result.IndexOf("\n"));

            if (tmpRes.Equals("true"))
            {
                clientDone.Reset();
                return true;
            }
            else
            {
                clientDone.Reset();
                return false;
            }
        }

        public static int login(string user, string pass)
        {
            result = "null";
            command = "login:" + user + "|iam|socool\n";

            send_receive_through_socket();


            string tmpRes = result.Substring(0, result.IndexOf("\n"));

            if (tmpRes.Equals("error"))
            {
                clientDone.Reset();
                return -1;
            }

            string[] tmpResArr = tmpRes.Split('|');
            result = tmpResArr[1];

            if (pass.Length == 0)
            {
                clientDone.Reset();
                return -1;
            }
            if (pass.Length != result.Length)
            {
                clientDone.Reset();
                return -1;
            }
            for (int i = 0; i < pass.Length; i++)
            {
                if (pass[i] != result[i])
                {
                    clientDone.Reset();
                    return -1;
                }
            }
            clientDone.Reset();
            return Convert.ToInt32(tmpResArr[0]);
        }









        private static void send_receive_through_socket()
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            DnsEndPoint hostEntry = new DnsEndPoint("sslab07.cs.purdue.edu", port);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.UserToken = sock;
            sock.ConnectAsync(socketEventArg);

            clientDone.WaitOne();
        }

        private static ManualResetEvent clientDone = new ManualResetEvent(false);

        private static void SocketEventArg_Completed(object sender, SocketAsyncEventArgs e)
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

        private static void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Successfully connected to the server

                // Send 'Hello World' to the server
                byte[] buffer = new byte[BUFFER_SIZE];
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

                //Debug.WriteLine(result);

                // Data has now been sent and received from the server. 
                // Disconnect from the server
                Socket sock = e.UserToken as Socket;
                sock.Shutdown(SocketShutdown.Send);
                sock.Close();
                clientDone.Set();
                return;
            }
            else
            {
                //return "false";
                throw new SocketException((int)e.SocketError);
            }
        }

        private static void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Sent "Hello World" to the server successfully

                //Read data sent from the server
                Socket sock = e.UserToken as Socket;

                byte[] bytes = new byte[512];
                int byteCount = sock.ReceiveBufferSize;
                //Debug.WriteLine("byteCount = " + byteCount);
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