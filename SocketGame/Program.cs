using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace socketGame
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

            Console.Write("Host(1) or Connect(2): ");
            var option = Console.ReadLine();

            if (option == "1")
                HostServer();
            else if (option == "2")
                ConnectToServer();
        }

        static void HostServer()
        {
            var ipString = "192.168.129.48";
            var port = 8000;

            Console.WriteLine($"\nHosting Server: {ipString} on port {port}");
            var server = new TcpListener(IPAddress.Parse(ipString), port);
            server.Start();

            Console.WriteLine("Waiting for client connection.");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine($"\nClient Connected: {client.Client.RemoteEndPoint}");

            var player = new Player(0, 0);
            var opponent = new Player(10, 10);

            var keyListenerThread = new Thread(() => KeyListener(player, client));
            keyListenerThread.Start();

            var clientThread = new Thread(() => HandleClient(client, opponent));
            clientThread.Start();
        }

        static void ConnectToServer()
        {
            Console.Write("\nEnter IP: ");
            var ip = Console.ReadLine();

            Console.Write("Enter Port: ");
            var port = Console.ReadLine();

            var client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), int.Parse(port));
            Console.Write("Connected.");

            var player = new Player(10, 10);
            var opponent = new Player(0, 0);

            var keyListenerThread = new Thread(() => KeyListener(player, client));
            keyListenerThread.Start();

            var serverThread = new Thread(() => HandleServer(client, opponent));
            serverThread.Start();
        }

        static void HandleClient(TcpClient client, Player opponent)
        {
            var stream = client.GetStream();
            var buffer = new byte[256];

            while (true)
            {
                var bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    var message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    var position = message.Split(',');

                    opponent.X = int.Parse(position[0]);
                    opponent.Y = int.Parse(position[1]);
                    opponent.previousX = int.Parse(position[2]);
                    opponent.previousY = int.Parse(position[3]);

                    DrawPlayer(opponent);
                }
            }
        }

        static void HandleServer(TcpClient client, Player opponent)
        {
            var stream = client.GetStream();
            var buffer = new byte[256];

            while (true)
            {
                var bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    var message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    var position = message.Split(',');

                    opponent.X = int.Parse(position[0]);
                    opponent.Y = int.Parse(position[1]);
                    opponent.previousX = int.Parse(position[2]);
                    opponent.previousY = int.Parse(position[3]);

                    DrawPlayer(opponent);
                }
            }
        }

        static void KeyListener(Player player, TcpClient client)
        {
            var stream = client.GetStream();

            DrawPlayer(player);

            while (true)
            {
                player.Move();

                var message = $"{player.X},{player.Y},{player.previousX},{player.previousY}";
                var buffer = Encoding.ASCII.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);

                DrawPlayer(player);
            }
        }

        static void DrawPlayer(Player player)
        {
            Console.SetCursorPosition(player.previousX, player.previousY);
            Console.Write(' ');

            Console.SetCursorPosition(player.X, player.Y);
            Console.Write(player.Symbol);
        }
    }

    internal class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int previousX { get; set; }
        public int previousY { get; set; }

        public char Symbol = '@';
        public ConsoleColor Color = ConsoleColor.DarkMagenta;

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move()
        {
            previousX = X;
            previousY = Y;

            var key = Console.ReadKey(intercept: true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (Y > 0) Y--;
                    break;
                case ConsoleKey.DownArrow:
                    if (Y < Console.WindowHeight - 1) Y++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (X > 0) X--;
                    break;
                case ConsoleKey.RightArrow:
                    if (X < Console.WindowWidth - 1) X++;
                    break;
            }
        }
    }
}
