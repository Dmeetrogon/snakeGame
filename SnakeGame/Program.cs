namespace SnakeGame;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "SnakeGame";
        Console.CursorVisible = false;

        Game game = new(60, 15);
        game.GetField.GenerateFood();
        Renderer renderer = new(game);
        var autoEvent = new AutoResetEvent(true);
        var fallTimer = new Timer(renderer.GameTick,
                               autoEvent, 300, 300);
        while (true)
        {
            renderer.RenderFrame();
        }


    }
}
