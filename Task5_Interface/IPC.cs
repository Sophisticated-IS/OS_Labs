using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace Task5_Interface
{
    public sealed class IPC
    {
        public Process PipeClient { get; }

        public IPC()
        {
            PipeClient = new Process();
            PipeClient.StartInfo.FileName =
                @"C:\Users\Sova IS\RiderProjects\OSLab_1\Task5_Logic\bin\Debug\netcoreapp3.1\Task5_Logic.exe";
        }

        private void StartServer(AnonymousPipeServerStream PipeServer)
        {
            PipeClient.StartInfo.Arguments =
                PipeServer.GetClientHandleAsString();
            PipeClient.StartInfo.UseShellExecute = false;
            PipeClient.Start();
            PipeServer.DisposeLocalCopyOfClientHandle();
        }

        public void SendMessageToClient(string msg)
        {
            try
            {
                using (var pipeServerStream =
                    new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
                {
                    StartServer(pipeServerStream);
                    // Read user input and send that to the client process.
                    using (StreamWriter sw = new StreamWriter(pipeServerStream))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine(msg);
                        pipeServerStream.WaitForPipeDrain();
                    }
                }
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                Console.WriteLine("[SERVER] Error: {0}", e.Message);
            }
        }

        public void CloseServer()
        {
            PipeClient.WaitForExit();
            PipeClient.Close();
            PipeClient.Dispose();
        }
    }
}