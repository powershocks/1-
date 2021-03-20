using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




namespace lab1_timp
{
    class Program
    {

        static void Main(string[] args)
        {
            try {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

        Socket listenSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        listenSoc.Bind(endPoint);
                listenSoc.Listen(10);
                Console.WriteLine("Server is running...");

                int bytes = 0;
        const int bufferSize = 8192;
                while (true) {
                    Socket handler = listenSoc.Accept();

                    Netfile file;
                    using (MemoryStream memStream = new MemoryStream()) {
                        byte[] buffer = new byte[bufferSize];
                        do {
                            int received = handler.Receive(buffer);
    //File.AppendAllText("Log.log", string.Format("Received={0}\r\n", received));
    memStream.Write(buffer, 0, received);
                            bytes += received;
                        }
while (handler.Available > 0) ;
file = new NetFile(memStream.ToArray());
                    }
                    Console.WriteLine("Size of received data: " + bytes.ToString() + " bytes");

using (FileStream stream = new FileStream(file.FileName, FileMode.Create, FileAccess.Write))
{
    stream.Write(file.Data, 0, file.Data.Length);
}

handler.Shutdown(SocketShutdown.Both);
handler.Close();

bytes = 0;
                }
            }
            catch (Exception error)
{
    Console.WriteLine(error.ToString());
    Console.ReadKey();
}
        }
    }
}