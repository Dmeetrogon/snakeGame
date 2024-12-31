namespace SnakeGame;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "SnakeGame";
        Console.CursorVisible = false;
        Console.SetWindowSize(40, 40);
        Console.WindowHeight = 40;
        Console.WindowWidth = 40;
        Game game = new(15,15);
        Renderer renderer = new(game);
        var autoEvent = new AutoResetEvent(true);
        var fallTimer = new Timer(renderer.GameTick,
                               autoEvent, 500, 500);
        while (true)
        {
            renderer.Tick();
        }


    }
}
