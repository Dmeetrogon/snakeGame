namespace SnakeGame;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "SnakeGame";
        Console.Write("Введите скорость игры: ");
        int speed = int.Parse(Console.ReadLine());
        int height = 15;
        int width = 50;
        Console.CursorVisible = false;
        Game game = new(width, height);
        game.GetField.GenerateFood();
        Renderer renderer = new(game);
        var autoEvent = new AutoResetEvent(true);
        var fallTimer = new Timer(renderer.GameTick,
                               autoEvent, speed, speed);
        while (true)
        {
            renderer.RenderFrame();
        }


    }
}
