using System;
using System.Threading;

namespace CommonMutexLib
{
    public static class CommonFileMutex
    {
        public static Mutex Mutex = new Mutex();
        public const string FilePath = @"C:\Users\Sova IS\RiderProjects\OSLab_1\CommonMutexLib\ReadWriteFile.txt";
    }
}