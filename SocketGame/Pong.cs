
internal class Pong
{
    public static Ball PongBall;
    public static Slider Player1;
    public static Slider Player2;

    public void DoThePong()
    {
        Player1 = new Slider(x: 5, 5, wasd: true);
        Player2 = new Slider(x: Console.WindowWidth - 5, 5, wasd: false);
        PongBall = new Ball(20, 20, 1, 1);

        Thread player1Thread = new(() => PlayerMoveLoop(Player1));
        Thread player2Thread = new(() => PlayerMoveLoop(Player2));
        Thread ballThread = new(PongBall.Move);

        player1Thread.Start();
        player2Thread.Start();
        ballThread.Start();

        player1Thread.Join();
        player2Thread.Join();
        ballThread.Join();
    }

    private void PlayerMoveLoop(Slider player)
    {
        while (true)
            player.Move();
    }
}