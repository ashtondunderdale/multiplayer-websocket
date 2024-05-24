internal class Ball
{
    public int X { get; set; }
    public int Y { get; set; }
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }

    public Ball(int x, int y, int velocityX, int velocityY)
    {
        X = x;
        Y = y;
        VelocityX = velocityX;
        VelocityY = velocityY;
    }

    public void Move()
    {
        while (true) 
        {
            int previousX = X;
            int previousY = Y;

            int newX = X + VelocityX;
            int newY = Y + VelocityY;

            if (newX < 0)
            {
                newX = 0;
                VelocityX *= -1;
            }
            else if (newX >= Console.WindowWidth)
            {
                newX = Console.WindowWidth - 1;
                VelocityX *= -1;
            }

            if (newX == Pong.Player1.X && (newY == Pong.Player1.Y || newY == Pong.Player1.Y + 1 || newY == Pong.Player1.Y + 2 || newY == Pong.Player1.Y + 3 || newY == Pong.Player1.Y + 4 || newY == Pong.Player1.Y + 5 || newY == Pong.Player1.Y + 6)) 
            {
                VelocityX *= -1;
                continue;
            }

            else if (newX == Pong.Player2.X && (newY == Pong.Player2.Y || newY == Pong.Player2.Y + 1 || newY == Pong.Player2.Y + 2 || newY == Pong.Player2.Y + 3 || newY == Pong.Player2.Y + 4 || newY == Pong.Player2.Y + 5 || newY == Pong.Player2.Y + 6))
            {
                VelocityX *= -1;
                continue;
            }

            if (newY < 0)
            {
                newY = 0;
                VelocityY *= -1;
            }
            else if (newY >= Console.WindowHeight)
            {
                newY = Console.WindowHeight - 1;
                VelocityY *= -1;
            }

            if (X >= 0 && X < Console.WindowWidth && Y >= 0 && Y < Console.WindowHeight)
            {
                X = newX;
                Y = newY;

                Console.SetCursorPosition(previousX, previousY);
                Console.Write(' ');

                Console.SetCursorPosition(X, Y);
                Console.Write('O');
                Thread.Sleep(50);
            }
        }
    }
}