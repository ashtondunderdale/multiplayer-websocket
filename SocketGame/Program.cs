using System.Net.Sockets;
using System.Net;

namespace socketGame;

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


        var player = new Player(0, 0);

        var keyListenerThread = new Thread(() => KeyListener(player));
        keyListenerThread.Start();


        while (true)
        {


        }
    }

    static void HostServer()
    {
        Console.WriteLine("\nHosting Server: 127.0.0.0 on port 80");
        var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
        server.Start();

        Console.WriteLine("Waiting for client connection.");
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine($"Client Connected: {client.Client.RemoteEndPoint}");
    }

    static void ConnectToServer()
    {
        Console.Write("\nEnter IP: ");
        var ip = Console.ReadLine();

        Console.Write("Enter Port: ");
        var port = Console.ReadLine();

        var client = new TcpClient();
        client.Connect(IPAddress.Parse(ip), int.Parse(port));
    }

    static void KeyListener(Player player)
    {
        Console.SetCursorPosition(player.X, player.Y);
        Console.Write(player.Symbol);

        while (true)
        {
            player.Move();
        }
    }
}
internal class Player
{
    public int X { get; set; }

    public int Y { get; set; }

    public char Symbol = '@';

    public ConsoleColor Color = ConsoleColor.DarkMagenta;

    public Player(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move()
    {
        var originalX = X;
        var originalY = Y;

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

        Console.SetCursorPosition(originalX, originalY);
        Console.Write(' ');

        Console.SetCursorPosition(X, Y);
        Console.Write(Symbol);
    }

}
