using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionMessage_6_7
{
    public partial class InputKey : Form
    {
        public string Key { get; set; }
        public bool IsValidationOk { get; set; } = false;

        public InputKey()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Key = textBox1.Text;
                IsValidationOk = true;       
                Close();             
            }
            else
            {
                MessageBox.Show("Введите не пустую строку");
            }
        }
    }
}
