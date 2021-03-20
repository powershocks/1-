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


namespace Client_TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string ip = "127.0.0.1"; //v predelax odnogo pc
                const int port = 8080;
                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("vvedite soobschenie");
                var message = Console.ReadLine();

                var data = Encoding.UTF8.GetBytes(message);

                //otkrit socket sdelat' podkluchenie
                tcpSocket.Connect(tcpEndPoint);
                Console.WriteLine("Sending file...");
                using (FileStream stream = new FileStream("c:\\1.sql", FileMode.Open, FileAccess.Read))
                {
                    byte[] vs = new byte[stream.Length];
                    int length = stream.Read(vs, 0, vs.Length);
                    NetFile file = new NetFile();
                    file.FileName = Path.GetFileName(stream.Name);
                    file.Data = vs;

                    byte[] to = file.ToArray();
                    tcpSocket.Send(to);
                }
                Console.WriteLine("File sended");

                tcpSocket.Shutdown(SocketShutdown.Both);
                tcpSocket.Close();
                Console.ReadKey();
            }

            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
                Console.ReadKey();
            }
        }
    }
}
