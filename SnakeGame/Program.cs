namespace SnakeGame;

class Program
{
    static void Main(string[] args)
    {
        Game game = new(10, 10);
        Console.SetWindowSize(10, 10);
        Renderer renderer = new(game);
        while (true)
        {
            renderer.Tick();
            Thread.Sleep(2000);
        }
    }
}
