namespace AdventOfCode
{
    public class ThreeDimensionalPoint
    {
        public ThreeDimensionalPoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public override bool Equals(object obj)
        {
            return obj is ThreeDimensionalPoint other
                && other.X == X
                && other.Y == Y
                && other.Z == Z;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * 3
                + Y.GetHashCode() * 5
                + Z.GetHashCode() * 7;
        }
    }
}
