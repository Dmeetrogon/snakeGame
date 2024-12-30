class Snake
{
    List<Point> body = [];
    bool hasEaten = false;
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
    public Snake(Point head, int size)
    {
        body.Add(head);
        for (int i = 1; i < size; i++)
        {
            body.Add(new Point(head.X, head.Y + i));
        }
    }
    public void Move(Game.Direction direction, Point? food)
    {

        Point tail = body.Last();//на случай, если змейка поест
        switch (direction)
        {
            case Game.Direction.Up:
                {
                    body[0].Y--;//ордината уменьшается при движении вверх, потому что точка начала отсчета находится в левом верхнем углу(0;0), а поле является прямоугольником
                    break;
                }
            case Game.Direction.Left:
                {
                    body[0].X--;
                    break;
                }
            case Game.Direction.Down:
                {
                    body[0].Y++;//ордината увеличивается при движении вниз, потому что точка начала отсчета находится в левом верхнем углу(0;0), а поле является прямоугольником
                    break;
                }
            case Game.Direction.Right:
                {
                    body[0].X++;
                    break;
                }
        }
        for (int i = 1; i < body.Count; i++)
        {
            body[i] = body[i - 1];
        }
        if (body.First() == food)
        {
            Eat();
        }
        if (hasEaten == true)
        {
            body.Append(tail);
        }
    }
    public void Eat()
    {
        hasEaten = true;
    }


}