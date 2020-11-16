using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task1_ReadersPull
{
    public class BufferPull
    {
        private readonly int _bytesConstraint;
        private readonly object _pullLocker = new object();
        public List<byte> Buffer { get; private  set; }
        public const string FileNamePull = "CommonOutputFile.txt";

        public BufferPull(int bytesConstraint)
        {
            _bytesConstraint = bytesConstraint;
            Buffer= new List<byte>(bytesConstraint);
        }

        public void WriteBytes(List<byte> bytes)
        {
            if(Buffer.Capacity>_bytesConstraint) throw new ArgumentOutOfRangeException(nameof(Buffer.Capacity));
                while (bytes.Count!=0)//пршили байты и они могут быть больше чем размер буфера
            {
                var availableBufferCapacity = Buffer.Capacity - Buffer.Count;
                if (availableBufferCapacity > 0)
                {
                    lock (_pullLocker)
                    {
                        //пока размер буфера не закончился либо набор байтов для записи
                        var i = 0;
                        var bytesToWrite = new List<byte>(availableBufferCapacity);
                        while (i < availableBufferCapacity && bytes.Count!=0)
                        {
                            bytesToWrite.Add(bytes[0]);
                            Buffer.Add(bytes[0]);
                            bytes.RemoveAt(0);
                            i++;
                        }
                        //запишем 
                        using (var streamWriter = new StreamWriter(FileNamePull, true))
                        {
                            var text = Encoding.UTF8.GetString(bytesToWrite.ToArray());
                            streamWriter.Write(text);
                        }
                    }
                    
                }
                else
                {
                    //ждем пока глав поток считает буфер и освободит место
                }
                
            }
        }

        public string ReadText()
        {
            lock (_pullLocker)
            {
                var fileBytes = File.ReadAllBytes(FileNamePull);
                var text = Encoding.UTF8.GetString(fileBytes);
                //clear file content
                File.WriteAllText(FileNamePull, string.Empty);
                Buffer.Clear();
                return text;   
            }
        }
    }
}