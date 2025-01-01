class Point
{
    int x;
    int y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int X
    {
        get => x;
        set => x = value;
    }
    public int Y
    {
        get => y;
        set => y = value;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Point)
        {
            return false;
        }
        else
        {
            return x == ((Point)obj).X && y == ((Point)obj).Y;
        }
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public static Point operator +(Point a, Point b) 
    {
        return new Point((a.X + b.X), a.Y + b.Y);
    }
    public static Point operator -(Point a, Point b)
    {
        return new Point((a.X - b.X), a.Y - b.Y);
    }
    public static bool operator !=(Point a, Point b)
    {
        return a.X != b.X || a.Y != b.Y;
    }
    public static bool operator ==(Point a, Point b)
    {
        return a.X == b.X && a.Y == b.Y;
    }


}