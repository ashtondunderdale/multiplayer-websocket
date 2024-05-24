internal class Slider
{
    public int X { get; set; }
    public int Y { get; set; }
    public int SliderLength { get; set; } = 7;
    public int PreviousX { get; set; }
    public int PreviousY { get; set; }
    public char Symbol { get; set; } = '|';
    public ConsoleColor Color { get; set; } = ConsoleColor.White;
    private readonly bool wasd;

    public Slider(int x, int y, bool wasd)
    {
        X = x;
        Y = y;
        this.wasd = wasd;
    }

    public void Move()
    {
        PreviousX = X;
        PreviousY = Y;

        ConsoleKey key = wasd ? ReadWasdKey() : ReadArrowKey();

        switch (key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                if (Y > 2) Y -= 2;
                break;

            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                if (Y < Console.WindowHeight - SliderLength) Y += 2;
                break;
        }

        for (int i = 0; i < SliderLength; i++)
        {
            Console.SetCursorPosition(PreviousX, PreviousY + i);
            Console.Write(' ');
        }

        Console.ForegroundColor = Color;
        for (int i = 0; i < SliderLength; i++)
        {
            Console.SetCursorPosition(X, Y + i);
            Console.Write(Symbol);
        }
    }

    private static ConsoleKey ReadArrowKey()
    {
        var key = Console.ReadKey(intercept: true).Key;

        while (key != ConsoleKey.UpArrow && key != ConsoleKey.DownArrow)
            key = Console.ReadKey(intercept: true).Key;

        return key;
    }

    private static ConsoleKey ReadWasdKey()
    {
        var key = Console.ReadKey(intercept: true).Key;

        while (key != ConsoleKey.W && key != ConsoleKey.S)
            key = Console.ReadKey(intercept: true).Key;

        return key;
    }
}