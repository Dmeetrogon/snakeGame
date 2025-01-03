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

        this.xSize = xSize;//-1 потому что отсчет начинается с 0. чтобы к примеру поле размером в 7 клеток, было размером в 7 клеток, а не в 8 #(0,1,2,3,4,5,6,7) = 8
        this.ySize = ySize;
        int snakeX = xSize / 2;//змейка появляется в центре
        int snakeY = ySize / 2;
        field = new Field(xSize, ySize);
        snake = new(new Point(snakeX, snakeY), 3, field.GenerateFood);
        field.AddSnake(snake);
        field.GenerateFood();
    }
    public void Tick(Direction direction)
    {
        snake.Move(direction, field.Food);
        if (IsDead())
        {
            Console.Clear();
            Console.WriteLine($"Игра окончена!Ваш счет - {snake.Body.Count}");
            Environment.Exit(0);
        }
    }
    bool IsDead()
    {
        Point head = snake.Body[0];
        for (int i = 1; i < snake.Body.Count; i++)
        {
            if (snake.Body[i] == head)
                return true;
        }
        if (field.WhatsInThePoint(head).Contains(Field.FieldObject.Wall))
        {
            return true;
        }
        return false;

    }

    public enum Direction
    {
        Up, Down, Left, Right
    }
    public Field GetField
    {
        get => field;
    }
    public class Field(int xSize, int ySize)
    {
        Snake? snake;
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
            get => snake!;
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
            for (int x = 1; x < xSize; x++)
            {
                for (int y = 1; y < ySize; y++)
                {
                    field.Add(new Point(x, y));
                }
            }
            List<Point> acessiblePoints = (from p in field.Except(snake!.Body)
                                           select p).ToList();
            Random random = new();
            food = acessiblePoints[random.Next(0, acessiblePoints.Count)];
        }
        public void AddSnake(Snake snake)
        {
            Snake = snake;
        }
        public List<FieldObject> WhatsInThePoint(Point point)
        {
            List<FieldObject> pointContains = [];
            if (snake.Body.Contains(point))
            {
                pointContains.Add(FieldObject.SnakeBody);
            }
            if (food! == point)
            {
                pointContains.Add(FieldObject.Food);
            }
            if (point.X == 0 || point.Y == 0 || point.X == xSize || point.Y == ySize)
            {
                pointContains.Add(FieldObject.Wall);
            }
            if (point.X >= 0 && point.Y >= 0 && point.X <= xSize && point.Y <= ySize)//Принадлежит ли точка полю. точка должна быть положительной и не вылезать за рамки поля
            {
                pointContains.Add(FieldObject.Field);
            }
            pointContains.Add(FieldObject.Undefined);
            return pointContains;
        }
        public enum FieldObject
        {
            SnakeBody,
            Field,
            Food,
            Undefined,
            Wall
        }
    }
}