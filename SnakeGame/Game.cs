using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

class Game
{
    int xSize;
    int ySize;
    Snake snake;
    Field field;
    public Game(int xSize, int ySize)
    {

        this.xSize = xSize - 1;//-1 потому что отсчет начинается с 0. чтобы к примеру поле размером в 7 клеток, было размером в 7 клеток, а не в 8 #(0,1,2,3,4,5,6,7) = 8
        this.ySize = ySize - 1;
        int snakeX = xSize / 2;//змейка появляется в центре
        int snakeY = ySize / 2;
        snake = new(new Point(snakeX, snakeY), 3);
        field = new Field(snake, xSize, ySize); 
        field.GenerateFood();
    }
    public void Tick(Direction direction)
    {
        snake.Move(direction, field.Food);
        if (!IsGameContinuing())
        {
            Console.Clear();
            Console.WriteLine("Игра окончена!");
            Environment.Exit(0);
        }
    }
    bool IsGameContinuing()
    {
        var head = snake.Body.First();
        var headL = new List<Point>() { head };
        if (snake.Body.Except(headL).Contains(snake.Body.First()))//если змейка в себя врезалась. TODO: убрать эту страшную херотень, а то она меня пугает. Она непонятная ничерта
        {
            Console.WriteLine("Бошка");
            return false;
        }
        if (head.X < 0 || head.X > xSize || head.Y < 0 || head.Y > ySize)// если змеиная черепушка все еще в пределах поля
        {
            Console.WriteLine("Стенка");
            return false;
        }
        return true;
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }
    public Field GetField
    {
        get => field;
    }
    public class Field(Snake snake, int xSize, int ySize)
    {
        Snake snake = snake;
        Point? food = default;
        int xSize = xSize;
        int ySize = ySize;

        public int XSize
        {
            get => xSize;
            set => xSize = value;
        }
        public int YSize
        {
            get => ySize;
            set => ySize = value;
        }
        public Snake Snake
        {
            get => snake;
            set => snake = value;
        }
        public Point? Food
        {
            get => food;
            set => food = value;
        }
        public void GenerateFood()
        {
            List<Point> field = [];
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    field.Add(new Point(x, y));
                }
            }
            List<Point> acessiblePoints = (from p in field.Except(snake.Body)
                                           select p).ToList();
            Random random = new();
            food = acessiblePoints[random.Next(0, acessiblePoints.Count)];
        }
        public FieldObject WhatsInThePoint(Point point)
        {
            if (snake.Body.Contains(point))
            {
                return FieldObject.SnakeBody;
            }
            if (food == point)
            {
                return FieldObject.Food;
            }
            if (point.X >= 0 && point.Y >= 0 && point.X <= xSize && point.Y <= ySize)//Принадлежит ли точка полю. точка должна быть положительной и не вылезать за рамки поля
            {
                return FieldObject.Field;
            }
            return FieldObject.Undefined;
        }
        public enum FieldObject
        {
            SnakeBody,
            SnakeHead,
            Field,
            Food,
            Undefined,
            Wall
        }
    }
}