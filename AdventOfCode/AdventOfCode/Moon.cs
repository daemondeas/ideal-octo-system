using System;

namespace AdventOfCode
{
    public class Moon
    {
        public Moon(int x, int y, int z)
            : this(new ThreeDimensionalPoint(x, y, z))
        {
        }

        public Moon(ThreeDimensionalPoint position)
        {
            Position = position;
            StartingPosition = position;
            Velocity = new ThreeDimensionalPoint(0, 0, 0);
        }

        public ThreeDimensionalPoint Position { get; set; }
        public ThreeDimensionalPoint Velocity { get; set; }
        public ThreeDimensionalPoint StartingPosition { get; set; }
        public int Energy => CalculateEnergy(Position) * CalculateEnergy(Velocity);
        public bool IsAtStartX => Position.X == StartingPosition.X && Velocity.X == 0;
        public bool IsAtStartY => Position.Y == StartingPosition.Y && Velocity.Y == 0;
        public bool IsAtStartZ => Position.Z == StartingPosition.Z && Velocity.Z == 0;

        public void ChangeVelocity(int x, int y, int z)
        {
            Velocity = new ThreeDimensionalPoint(
                Velocity.X + x,
                Velocity.Y + y,
                Velocity.Z + z);
        }

        public void ApplyVelocity()
        {
            Position = new ThreeDimensionalPoint(
                Position.X + Velocity.X,
                Position.Y + Velocity.Y,
                Position.Z + Velocity.Z);
        }

        private int CalculateEnergy(ThreeDimensionalPoint point)
        {
            return Math.Abs(point.X) + Math.Abs(point.Y) + Math.Abs(point.Z);
        }
    }
}
