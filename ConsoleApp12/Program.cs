using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class Program
{
    static async Task Main()
    {// CLIENT
        using TcpClient tcpClient = new TcpClient();
        await tcpClient.ConnectAsync("127.0.0.1", 8888);
        NetworkStream stream = tcpClient.GetStream();
        Console.WriteLine("Выберите игрока. 1/2");
        string playerNumber = Console.ReadLine();
        while (true)
        {
            var key = Console.ReadKey();
            char keyChar = key.KeyChar;
            string strKeyChar = keyChar.ToString();

            byte[] bytesToWrite = Encoding.UTF8.GetBytes($"{playerNumber}{strKeyChar}\n");
            await stream.WriteAsync(bytesToWrite);
        }

    }
}