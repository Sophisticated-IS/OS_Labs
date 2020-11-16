using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Task2_CryptEnglishText
{
    public sealed class ReadWriteEncrypted
    {
        public const int Capacity = 20;
        public const string ReadTextFileName = @"C:\Users\Sova IS\RiderProjects\OSLab_1\Task2_CryptEnglishText\EnglishText.txt";
        public const string WriteTextFileName = @"C:\Users\Sova IS\RiderProjects\OSLab_1\Task2_CryptEnglishText\OutputCryptedText.txt";
        public const int m_threads = 9;
        private Semaphore _semaphore = new Semaphore(0, m_threads);
        private List<char> _buffer = new List<char>(Capacity);

        private object _locker = new object();

        // private Mutex _mutex = new Mutex();
        private Thread _threadReaderBuff;

        public ReadWriteEncrypted()
        {
            _threadReaderBuff = new Thread(ReadToBuffer);
            _threadReaderBuff.Start();
            Thread.Sleep(10000);
            // new Thread(EnCryptBuffer).Start();
        }

        /// <summary>
        /// ●	строчные буквы в тексте преобразуются в заглавные и наоборот;
        /// ●	каждая буква заменяется на следующую по алфавиту, т.е. А заменяется на В, В – на С, и т.д. Буква Z должна быть заменена на A.
        /// </summary>
        private static char CryptSymbol(char symbol)
        {
            var resChar = char.IsLower(symbol)
                ? char.ToUpper(symbol)
                : char.ToLower(symbol);

            resChar++;

            return resChar;
        }

        private void ReadToBuffer()
        {
            var text = File.ReadAllText(ReadTextFileName).ToList();

            var i = 0;
            while (i < text.Count) //пока есть что читать из файла
            {
                lock (_locker)
                {
                    if (_buffer.Count < _buffer.Capacity) //есть место в буфере
                    {
                        _buffer.Add(text[i]);
                        i++;
                    }
                    else
                    {
                        EnCryptBuffer();
                    }
                }
            }

            EnCryptBuffer();
        }

        private void EnCryptBuffer()
        {
            // while (_threadReaderBuff.IsAlive)
            // {
            lock (_locker)
            {
                Parallel.For(0, _buffer.Count, i =>
                {
                    // _semaphore.WaitOne();
                    _buffer[i] = CryptSymbol(_buffer[i]);
                    // _semaphore.Release();
                });
                WriteEncryptedText();
            }

            // }
        }

        private void WriteEncryptedText()
        {
            using (var streamWriter = new StreamWriter(WriteTextFileName, true))
            {
                lock (_locker)
                {
                    if (_buffer.Count != 0)
                    {
                        var chars = _buffer.ToArray();
                        var text = new string(chars);
                        streamWriter.Write(text);
                        _buffer.Clear();
                    }
                }
            }
        }
    }


    class Program
    {        
        private static Mutex _mutexStartUnencryption = new Mutex(false,"UnencryptionMutex");

        static void Main(string[] args)
        {
            _mutexStartUnencryption.WaitOne();
            File.WriteAllText(ReadWriteEncrypted.WriteTextFileName, string.Empty);
            var test = new ReadWriteEncrypted();
            _mutexStartUnencryption.ReleaseMutex();
            Console.ReadKey();
        }
    }
}