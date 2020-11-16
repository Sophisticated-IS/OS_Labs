using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task1_ReadersPull
{
    public class ReaderFileToBuffer
    {
        private static readonly object Locker = new object();
        public string FileNameToRead { get; set; }

        public BufferPull Pull { get;}
        public ReaderFileToBuffer(BufferPull bufferPull)
        {
            Pull = bufferPull;
            ReadFromFileWriteToBuffer();
        }

        private void ReadFromFileWriteToBuffer()
        {
            new Thread(() =>
            {
                while (true)
                {
                    // Thread.Sleep(100);
                    var text = File.ReadAllText(FileNameToRead);

                    var bytes = Encoding.UTF8.GetBytes(text).ToList();
                    lock (Locker)//не дает другим ридерам перехватывать запись в пулл
                    {
                        Pull.WriteBytes(bytes);
                    }
                }
                
            }).Start();
            
        }
    }
}