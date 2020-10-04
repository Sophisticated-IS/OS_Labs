using System;
using System.Text;
using System.Windows;

namespace EncryptionMessage_6_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public string Key { get; set; }
        
        /// <summary>
        /// Лабораторная работа по Защите ОС задания №6-7
        /// Автор: Сова Игорь КМБ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InputKeyLength();
        }

        private void InputKeyLength()
        {
            var inputKey = new InputKey();
            inputKey.ShowDialog();
            if (inputKey.IsValidationOk)
            {
                Key = inputKey.Key;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void ButtonBase_OnClickCryptText(object sender, RoutedEventArgs e)
        {
            var msg = InputMessage.Text;
            var encryptedMsg = GetEncryptedMessage(Key,msg);
            EncryptedMessage.Text = encryptedMsg;
        }

        private string GetEncryptedMessage(string key,string message)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (message == null) throw new ArgumentNullException(nameof(message));
            
            var encryptedMessage = new StringBuilder();
            var leftChars = message.Length;
            var startIndex = 0;
            while (leftChars != 0)
            {
                var blockLength = GetMessageLength(leftChars, key.Length);
                var block = message.Substring(startIndex, blockLength);
                var carriedBlock = CarryElements(block);
                var encryptedBlock = GetEncryptedBlock(carriedBlock, key);
                encryptedMessage.Append(encryptedBlock);
                startIndex += blockLength;
                leftChars -= blockLength;
            }

            return encryptedMessage.ToString();
        }

        /// <summary>
        /// выполнить перестановку символов так, чтобы первый символ занял место последнего,
        /// второй – предпоследнего и т.д. Последний символ должен оказаться на месте первого символа блока
        /// </summary>
        private string CarryElements(string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            
            var chars = str.ToCharArray();
            var leftIndex = 0;
            var rightIndex = chars.Length - 1;

            while (leftIndex != rightIndex && leftIndex <= rightIndex)
            {
                var tmp = chars[leftIndex];
                chars[leftIndex] = chars[rightIndex];
                chars[rightIndex] = tmp;

                leftIndex++;
                rightIndex--;
            }
            
            return new string(chars);
        }

        private string GetEncryptedBlock(string block, string key)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (key == null) throw new ArgumentNullException(nameof(key));
            
            var blockChars = block.ToCharArray();
            var encryptedBlock = new char[blockChars.Length];
            for (var i = 0; i < blockChars.Length; i++)
            {
                encryptedBlock[i] = (char) (blockChars[i] ^ key[i]);
            }

            return new string(encryptedBlock);
        }
        private int GetMessageLength(int messageLength,int keyLength)
        {
            if (messageLength/keyLength != 0)
            {
                return keyLength;
            }
            else
            {
                return messageLength % keyLength;
            }
        }

        private void ButtonBase_OnClickUnencrypt(object sender, RoutedEventArgs e)
        {
            var msg = InputEncryptedMessage.Text;
            var unencryptedMsg = GetEncryptedMessage(Key,msg);
            var unencryptedMsg2 = GetEncryptedMessage(Key,unencryptedMsg);
            var unencryptedMsg3 = GetEncryptedMessage(Key,unencryptedMsg2);
            UnencryptedMessage.Text = unencryptedMsg3;
        }

        private void ButtonBase_OnClickCopy(object sender, RoutedEventArgs e)
        {
            InputEncryptedMessage.Text = EncryptedMessage.Text;
        }
    }
}