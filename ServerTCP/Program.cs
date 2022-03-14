using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerTCP
{
    class Program
    {
        static void Main()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse("192.168.0.100"), 8081));
            serverSocket.Listen(10);

            //向客户端发送消息
            Socket clientSocket = serverSocket.Accept();
            string MsgSend = "Hello,你好啊！";
            byte[] clientBuf = Encoding.UTF8.GetBytes(MsgSend);
            clientSocket.Send(clientBuf);

            StartServerAsync(clientSocket);

            Console.ReadKey();
            clientSocket.Close();
            serverSocket.Close();
        }
        static byte[] serverBuf = new byte[1024];
        static void StartServerAsync(Socket clientSocket)
        {
            //接受客户端消息
            
            clientSocket.BeginReceive(serverBuf,0,1024,SocketFlags.None, ReceiveCallBack, clientSocket);
            
        }

        static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = ar.AsyncState as Socket;

            int count = clientSocket.EndReceive(ar);
            string MsgReceive = Encoding.UTF8.GetString(serverBuf, 0, count);
            Console.WriteLine($"接收到的数据为：{MsgReceive}");
            clientSocket.BeginReceive(serverBuf, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
        }
    }
}