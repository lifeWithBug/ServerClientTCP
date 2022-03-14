using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ClientTCP
{
    class Program
    {
        static void Main()
        {
            Socket clientSocket=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.100"), 8081));

            byte[] clientBuf = new byte[1024];
            int count = clientSocket.Receive(clientBuf);
            string msgReceive=Encoding.UTF8.GetString(clientBuf,0,count);
            Console.WriteLine("收到的消息为："+msgReceive);

            while (true)
            {
                string msgSend = Console.ReadLine();
                clientSocket.Send(Encoding.UTF8.GetBytes(msgSend));
            }
            

            Console.ReadKey();
            clientSocket.Close();
        }
    }
}