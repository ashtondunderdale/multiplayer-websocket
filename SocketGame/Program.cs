namespace socketGame;

internal class Program
{
    static void Main()
    {
        Console.CursorVisible = false;

        var pong = new Pong();
        pong.DoThePong();
    }      
}
