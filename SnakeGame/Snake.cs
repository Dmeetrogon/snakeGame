class Snake
{
    List<Point> body = [];
    bool hasEaten = false;
    public delegate void SnakeEatHandler();
    public event SnakeEatHandler Process;
    public List<Point> Body
    {
        get => body;
        set => body = value;
    }
    public bool HasEaten
    {
        get => hasEaten;
        set => hasEaten = value;
    }
    public Snake(Point head, int size, SnakeEatHandler handler)
    {
        body.Add(head);
        for (int i = 1; i < size; i++)
        {
            body.Add(new Point(head.X, head.Y + i));
        }
        Process = handler;
    }
    public void Move(Game.Direction direction, Point? food)
    {
        Point tail = body.Last();//на случай, если змейка поест
        Point head = body.First();
        Point newHead;
        switch (direction)
        {
            case Game.Direction.Up:
                {
                    newHead = new Point(head.X, head.Y - 1); ;//ордината уменьшается при движении вверх, потому что точка начала отсчета находится в левом верхнем углу(0;0), а поле является прямоугольником
                    break;
                }
            case Game.Direction.Left:
                {
                    newHead = new Point(head.X - 1, head.Y);
                    break;
                }
            case Game.Direction.Down:
                {
                    newHead = new Point(head.X, head.Y + 1); ;//ордината увеличивается при движении вниз, потому что точка начала отсчета находится в левом верхнем углу(0;0), а поле является прямоугольником
                    break;
                }
            case Game.Direction.Right:
                {
                    newHead = new Point(head.X + 1, head.Y);
                    break;
                }
            default:
                {
                    throw new Exception("Invalid direction");
                }
        }
        body.Insert(0, newHead);
        body.Remove(tail);
        if (body.First() == food)
        {
            Eat();
        }
        if (hasEaten == true)
        {
            body.Add(tail);
            hasEaten = false;
        }
    }
    public void Eat()
    {
        Process.Invoke();
        hasEaten = true;
    }


}