using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main() // Server
    {
        var tcpListener = new TcpListener(IPAddress.Any, 8888);
        try
        {
            tcpListener.Start();
            Console.WriteLine("Server started!");
            while (true)
            {
                var tcpClient = await tcpListener.AcceptTcpClientAsync();
                new Thread(async () => await ProcessPlayerAsync(tcpClient)).Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            tcpListener.Stop();
        }
    }
    static int x1 = 1, y1 = 1, x2 = 1, y2 = 1;
    static async Task ProcessPlayerAsync(TcpClient tcpClient)
    {
        try
        {
            var stream = tcpClient.GetStream();
            var response = new List<byte>();
            int bytesToRead = 10;
            while (true)
            {
                while ((bytesToRead = stream.ReadByte()) != '\n')
                {
                    response.Add((byte)bytesToRead);
                }
                var simbol = Encoding.UTF8.GetString(response.ToArray());
                switch (simbol)
                {
                    case "1w":
                        if (y1 > 1) y1--;
                        break;
                    case "1a":
                        if (x1 > 1) x1--;
                        break;
                    case "1s":
                        if (y1 < 20) y1++;
                        break;
                    case "1d":
                        if (x1 < 20) x1++;
                        break;

                    case "2w":
                        if (y2 > 1) y2--;
                        break;
                    case "2a":
                        if (x2 > 1) x2--;
                        break;
                    case "2s":
                        if (y2 < 20) y2++;
                        break;
                    case "2d":
                        if (x2 < 20) x2++;
                        break;
                }
                Console.Clear();
                for (int i = 0; i <= 20; i++)
                {
                    for (int j = 0; j <= 20; j++)
                    {
                        if (x1 == j && y1 == i)
                        {
                            Console.WriteLine("O");
                        }
                        else if (x2 == j && y2 == i) Console.WriteLine("X");
                        else Console.Write(" ");
                    }
                    Console.WriteLine();
                }
                response.Clear();
            }
        }
        finally
        {
            tcpClient.Close();
        }
    }
}