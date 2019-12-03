namespace AdventOfCode
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override bool Equals(object obj)
        {
            return obj is Point other
                && other.X == X
                && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * 3
                + Y.GetHashCode() * 5;
        }
    }
}
