﻿using RoomChattingClient.ChatingClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RoomChattingClient
{
    public partial class ChatForm : Form
    {
        TCPIPClient tcpIpClient = null;
        List<string> reserveStringList;
        delegate void SetTextCallback(string text, bool sound);
        bool isClosed = false;
        char[] trimData = { '\r', '\n', ' ' };
        public int result;
        bool isInternetConnected = true;
        FileStream bugFileStream;
        public StreamWriter bugPsWriter;

        public ChatForm()
        {
            reserveStringList = new List<string>();
            InitializeComponent();
            showTextBox.SelectionStart = showTextBox.Text.Length;
            showTextBox.ScrollToCaret();
        }
        private bool isAlreadySet()
        {
            FileInfo fileInfo = new FileInfo(@"name.txt");
            if (fileInfo.Exists)
                return true;
            else
                return false;
        }

        private void MainForm_Loaded(object sender, EventArgs e)
        {
            if (!isAlreadySet())
            {
                Form roomNameInputForm = new RoomNameInputForm(this);
                roomNameInputForm.ShowDialog();
                if (this.result == 0)
                {
                    MessageBox.Show("잘못된 입력입니다.");
                    Application.Exit();
                }
            }
            
            FileStream fileStream = new FileStream("name.txt", FileMode.Open, FileAccess.Read);
            StreamReader psReader = new StreamReader(fileStream, Encoding.Unicode);
            string name;
            string ip;
            try
            {
                name = psReader.ReadLine();
                ip = psReader.ReadLine();
                ip.TrimEnd('0');
                name.TrimEnd(trimData);
            }
            catch(Exception)
            {
                MessageBox.Show("잘못된 파일입니다. name.txt파일을 삭제해 주세요");
                Application.Exit();
                return;
            }
            psReader.Close();
            fileStream.Close();

             
            tcpIpClient = new TCPIPClient(ip, name, this);
            tcpIpClient.OnReceived += new TCPIPClient.MessageDisplayHandler(displayRecvData);
            tcpIpClient.OnErrorMessage += new TCPIPClient.ErrorMessageHandler(errorMessage);
            tcpIpClient.reconnectionRequest += new TCPIPClient.reConnectionRequestHandler(reconecctionRequest);
            int result = tcpIpClient.startChat();
            if( result != 0)
            {
                MessageBox.Show("서버가 연결되지 않습니다.", "오류");
                Application.Exit();
            }
            inputTextBox.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string msg = inputTextBox.Text;
            if (msg == "")
            {
                return;
            }
            bugFileStream = new FileStream(@"bug.txt", FileMode.Append, FileAccess.Write);
            bugPsWriter = new StreamWriter(bugFileStream, System.Text.Encoding.Unicode);
            inputTextBox.Clear();
            bugPsWriter.WriteLine("btn click :  " + msg);
            displayMessage("\r\n" + msg, Color.White);
            if (false == tcpIpClient.IsConnectedToInternet())
            {
                bugPsWriter.WriteLine("reserve :    " + System.DateTime.Now.ToString() + " : " + msg);
                reserveStringList.Add(msg);
                isInternetConnected = false;
            }
            else
            {
                try
                {
                    if( false == isInternetConnected )
                    {
                        Thread.Sleep(100);
                        foreach (string data in reserveStringList)
                        {
                            tcpIpClient.sendData("1" + data);
                            Thread.Sleep(100);
                        }
                        reserveStringList.Clear();
                    }
                    tcpIpClient.sendData("1" + msg);
                    bugPsWriter.WriteLine("send :   " + System.DateTime.Now.ToString() + " : " + msg);
                }
                catch (Exception)
                {
                    bugPsWriter.WriteLine("reserveEx :  " + System.DateTime.Now.ToString() + " : " + msg);
                    reserveStringList.Add(msg);
                }
            }
            bugPsWriter.WriteLine("");
            inputTextBox.Select();
            bugPsWriter.Close();
            bugFileStream.Close();
        }

        public void errorMessage(string error)
        {
            if(error == "1")
            {
                MessageBox.Show("사용자 수를 초과하였습니다.", "오류");
            }
            else if(error == "2")
            {
                MessageBox.Show("맞는 이름이 아닙니다", "오류");
            }
            else
            {
                return;
            }
            isClosed = true;
            exitChating();
        }
        public void displayRecvData(string msg, bool sound)
        {
            if (this.showTextBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(displayRecvData);
                this.Invoke(d, new object[] { msg, sound });
            }
            else
            {
                displayMessage("\r\n" + msg, Color.Yellow);
                if (sound)
                {
                    SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Alarm);
                    simpleSound.Play();
                    
                }
            }
        }

        private void displayMessage(string msg, Color color)
        {
            msg = msg.TrimEnd(trimData);
            int startIndex = showTextBox.Text.Length;
            this.showTextBox.AppendText(msg);
            int endIndex = showTextBox.Text.Length;
            showTextBox.Select(startIndex, endIndex - startIndex);
            showTextBox.SelectionColor = color;
            showTextBox.ScrollToCaret();


        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string msg = inputTextBox.Text;

                int isMacro = checkMacro(msg);
                if (isMacro == 1)
                {
                    showTextBox.Clear();
                    return;
                }
                else if (isMacro == 2)
                {
                    Close();
                }
            }
        }

        private void exitChating()
        {
            if(!isClosed) { 
                MessageBox.Show("서버에서 연결이 종료되었습니다.", "오류");
            }
        }

        private void reconecctionRequest()
        {
            tcpIpClient.closeCommunication();
            while (true)
            {
                int result = tcpIpClient.startChat();
                if (result == 0)
                {
                    break;
                }
            }
            while(tcpIpClient.isStarted == false)
            {

            }
            Thread.Sleep(100);
            foreach (string msg in reserveStringList)
            {
                try
                {
                    tcpIpClient.sendData("1" + msg);
                }
                catch (Exception) { }
                Thread.Sleep(100);
            }
            reserveStringList.Clear();
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosed = true;
            if (tcpIpClient != null)
            {
                tcpIpClient.closeAllCommunication();
            }
        }

        private int checkMacro(string text)
        {
            return 0;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!base.ProcessCmdKey(ref msg, keyData))
            {
                if (keyData.Equals(Keys.F11))
                {
                    showTextBox.Clear();
                }
                if (keyData.Equals(Keys.F12))
                {
                    isClosed = true;
                    tcpIpClient.closeAllCommunication();
                    Application.Exit();
                }
                else return false;
            }
            else
            {
                return true;
            }
            return true;
        }

        private void ChatForm_SizeChanged(object sender, EventArgs e)
        {
            changeComponentPosition();
        }

        private void changeComponentPosition()
        {
            showTextBox.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 100);

            this.chatSendButton.Location = new System.Drawing.Point(showTextBox.Right - 100 - 20, showTextBox.Bottom + 10);
            this.chatSendButton.Size = new Size(120, this.chatSendButton.Height);
            this.inputTextBox.Location = new System.Drawing.Point(showTextBox.Left, showTextBox.Bottom + 10);
            this.inputTextBox.Size = new System.Drawing.Size(showTextBox.Width - 10 - chatSendButton.Width, this.inputTextBox.Height);
        }
    }
}