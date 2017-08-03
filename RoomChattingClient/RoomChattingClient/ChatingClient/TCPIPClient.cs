using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomChattingClient.ChatingClient
{
    class TCPIPClient
    {
        string ip;
        string name;
        TcpClient client;
        Thread workerThread;
        Thread connectionCheckThread;
        public bool isStarted = false;
        bool isFirstStart = true;
        ChatForm chatForm;
        public delegate void MessageDisplayHandler(string text, bool sound);
        public event MessageDisplayHandler OnReceived;
        public delegate void ErrorMessageHandler(string error);
        public event ErrorMessageHandler OnErrorMessage;
        public delegate void reConnectionRequestHandler();
        public event reConnectionRequestHandler reconnectionRequest;

        private TCPIPClient()
        {
        }

        public TCPIPClient(string ip, string name, ChatForm form) : this()
        {
            this.ip = ip;
            this.name = name;
            chatForm = form;
            connectionCheckThread = new Thread(connectionCheckProcess);
            connectionCheckThread.Start();
        }

        public int startChat()
        {
            client = new TcpClient();

            try
            {
                client.Connect(ip, 10002);
            }
            catch(Exception)
            {
                return 1;
            }
            
            NetworkStream stream = client.GetStream();
            byte[] sbuffer = Encoding.Unicode.GetBytes(name);
            stream.Write(sbuffer, 0, sbuffer.Length);
            stream.Flush();
            isStarted = true;
            workerThread = new Thread(recvData);
            workerThread.Start();
            if (isFirstStart)
            {
                OnReceived("서버에 접속하였습니다.", false);
                isFirstStart = false;
            }
            return 0;
        }

        private void connectionCheckProcess()
        {
            Thread.Sleep(1000);
            bool connectChat = true;
            bool connectInternet = true;
            {
                FileStream fileStream = new FileStream(@"log.txt", FileMode.Append, FileAccess.Write);
                StreamWriter psWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

                psWriter.WriteLine("");
                psWriter.WriteLine("=============Start Chatting(" + System.DateTime.Now.ToString() + ")=============");
                connectInternet = IsConnectedToInternet();
                connectChat = client.Connected;
                if (client.Connected == true)
                {
                    psWriter.WriteLine("Chatting access success!! : " + System.DateTime.Now.ToString());
                }
                else
                {
                    psWriter.WriteLine("Chatting access fail!!    : " + System.DateTime.Now.ToString());
                }

                if (IsConnectedToInternet() == true)
                {
                    psWriter.WriteLine("internet access success!! : " + System.DateTime.Now.ToString());
                }
                else
                {
                    psWriter.WriteLine("internet access fail!!    : " + System.DateTime.Now.ToString());
                }
                psWriter.Close();
                fileStream.Close();

            }
            try
            {
                while (true)
                {
                    if (client != null)
                    {
                        bool currenctConnectionStatusTemp = client.Connected;
                        
                        if (connectChat != currenctConnectionStatusTemp)
                        {
                            FileStream fileStream = new FileStream(@"log.txt", FileMode.Append, FileAccess.Write);
                            StreamWriter psWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

                            if (currenctConnectionStatusTemp == true)
                            {
                                psWriter.WriteLine("Chatting access success!! : " + System.DateTime.Now.ToString());
                            }
                            else
                            {
                                psWriter.WriteLine("Chatting access fail!!    : " + System.DateTime.Now.ToString());
                            }
                            psWriter.Close();
                            fileStream.Close();
                            connectChat = currenctConnectionStatusTemp;
                        }
                    }
                    
                    bool currentInternetStatusTemp = IsConnectedToInternet();
                    if (connectInternet != currentInternetStatusTemp)
                    {
                        FileStream fileStream = new FileStream(@"log.txt", FileMode.Append, FileAccess.Write);
                        StreamWriter psWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

                        if (currentInternetStatusTemp == true)
                        {
                            psWriter.WriteLine("internet access success!! : " + System.DateTime.Now.ToString());
                        }
                        else
                        {
                            psWriter.WriteLine("internet access fail!!    : " + System.DateTime.Now.ToString());
                        }
                        connectInternet = currentInternetStatusTemp;

                        psWriter.Close();
                        fileStream.Close();
                    }

                    Thread.Sleep(1000);
                }
            }catch(Exception)
            {

            }
        }

        public void sendData(string data)
        {
            NetworkStream stream = client.GetStream();
            byte[] sbuffer = Encoding.Unicode.GetBytes(data);
            stream.Write(sbuffer, 0, sbuffer.Length);
            stream.Flush();
        }

        private void recvData()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    string msg = string.Empty;
                    int bytes = 0;
                    NetworkStream stream = client.GetStream();
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    if (bytes == 0)
                    {
                        break;
                    }
                    msg = Encoding.Unicode.GetString(buffer, 0, bytes);
                    if (msg[0] == '0')
                    {
                        if (OnReceived != null)
                            OnReceived(msg.Substring(1), true);
                    }
                    else
                    {
                        OnErrorMessage(msg);
                    }
                    stream.Flush();
                }
            }catch(Exception)
            {
            }
            client.Close();
            reconnectionRequest();
        }
        public void closeCommunication()
        {
            if (client != null)
            {
                client.Close();
            }

            isStarted = false;

        }

        public void closeAllCommunication()
        {
            closeCommunication();
            if (workerThread != null)
            {
                if (workerThread.ThreadState != ThreadState.Unstarted)
                {
                    workerThread.Abort();
                }
            }
            if (connectionCheckThread != null)
            {
                if (connectionCheckThread.ThreadState != ThreadState.Unstarted)
                {
                    connectionCheckThread.Abort();
                }
            }
        }
        public bool IsConnectedToInternet()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
    }
}