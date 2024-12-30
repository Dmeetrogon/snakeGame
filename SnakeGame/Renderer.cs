using System.Text;
class Renderer
{
    char lastPressed = 'W';
    Game.Direction direction;
    Game.Field field;
    Game game;
    public Renderer(Game game)
    {
        field = game.GetField;
        this.game = game;
    }
    void DrawFrame()
    {
        StringBuilder str = new(string.Empty);
        for (int y = 0; y < field.YSize; y++)
        {
            for (int x = 0; x < field.XSize; x++)
            {
                Game.Field.FieldObject fieldObject = field.WhatsInThePoint(new Point(x, y));
                str.Append(GetSymbol(fieldObject));
            }
            str.Append('\n');
        }
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(str);
    }
    public void Tick()
    {
        DrawFrame();
        GetDirection();
        game.Tick(direction);
    }
    void GetDirection()
    {
        var pressed = lastPressed;
        if (Console.KeyAvailable == true)
        {
            var key = Console.ReadKey().Key.ToString().First();
            if ("WASD".Contains(key))
            {
                pressed = key;
            }
        }
        if (pressed == 'A' && lastPressed != 'D')
        {
            direction = Game.Direction.Left;
        }
        if (pressed == 'W' && lastPressed != 'S')
        {
            direction = Game.Direction.Up;
        }
        if (pressed == 'D' && lastPressed != 'A')
        {
            direction = Game.Direction.Right;
        }
        if (pressed == 'S' && lastPressed != 'W')
        {
            direction = Game.Direction.Down;
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
            get => "■";
        }
        public static string FieldFiller
        {
            get => "□";
        }
        public static string Food
        {
            get => "F";
        }
    }
}