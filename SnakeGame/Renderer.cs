using System.Text;
class Renderer
{
    Game.Direction currentDirection = Game.Direction.Up;
    Game.Field field;
    Game game;
    public Renderer(Game game)
    {
        field = game.GetField;
        this.game = game;
    }
    void DrawFrame()
    {
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y <= field.YSize; y++)
        {
            for (int x = 0; x <= field.XSize; x++)
            {
                Game.Field.FieldObject fieldObject = field.WhatsInThePoint(new Point(x, y));
                Console.Write(GetSymbol(fieldObject));
            }
            Console.WriteLine();
        }
    }
    public void GameTick(object? stateInfo)
    {
        game.Tick(currentDirection);
    }
    public void RenderFrame()
    {
        DrawFrame();
        GetDirection();
    }
    public void GetDirection()
    {
        if (Console.KeyAvailable == true)
        {
            var key = Console.ReadKey().Key.ToString();
            switch (key)
            {
                case "W":
                    {
                        if (currentDirection == Game.Direction.Down)
                        {
                            return;
                        }
                        currentDirection = Game.Direction.Up;
                        return;
                    }
                case "A":
                    {
                        if (currentDirection == Game.Direction.Right)
                        {
                            return;
                        }
                        currentDirection = Game.Direction.Left;
                        return;
                    }
                case "S":
                    {
                        if (currentDirection == Game.Direction.Up)
                        {
                            return;
                        }
                        currentDirection = Game.Direction.Down;
                        return;
                    }
                case "D":
                    {
                        if (currentDirection == Game.Direction.Left)
                        {
                            return;
                        }
                        currentDirection = Game.Direction.Right;
                        return;
                    }
            }
        }
    }
    static string GetSymbol(Game.Field.FieldObject fieldObject)
    {
        switch (fieldObject)
        {
            case Game.Field.FieldObject.SnakeBody:
                {
                    return Config.Body;
                }
            case Game.Field.FieldObject.Field:
                {
                    return Config.FieldFiller;
                }
            case Game.Field.FieldObject.Food:
                {
                    return Config.Food;
                }
            case Game.Field.FieldObject.Wall:
                {
                    return Config.Wall;
                }
            default:
                {
                    throw new Exception("Requested undefined point of the field!");
                }
        }
    }
    static class Config
    {
        public static string Body
        {
            get => "*";
        }
        public static string FieldFiller
        {
            get => " ";
        }
        public static string Food
        {
            get => "@";
        }
        public static string Wall
        {
            get => "#";
        }
    }
}