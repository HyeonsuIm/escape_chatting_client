using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomChattingClient
{
    public partial class RoomNameInputForm : Form
    {
        ChatForm parent;
        public RoomNameInputForm(ChatForm form)
        {
            parent = form;
            parent.result = 0;
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            saveName();
            
        }

        private void saveName()
        {
            if(textBox1.Name.Length == 0)
            {
                MessageBox.Show("이름이 입력되지 않았습니다.", "오류");
                return;
            }
            if(ipAddressInput.Text.Length == 0)
            {
                MessageBox.Show("IP가 입력되지 않았습니다.", "오류");
                return;
            }
            FileStream fileStream = new FileStream(@"name.txt", FileMode.Create, FileAccess.Write);
            StreamWriter psWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);
            String name = this.textBox1.Text;
            string ip = this.ipAddressInput.Text;
            psWriter.WriteLine(name);
            psWriter.WriteLine(ip);
            psWriter.Close();
            parent.result = 1;
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                saveName();
                this.Close();
            }
        }
    }
}