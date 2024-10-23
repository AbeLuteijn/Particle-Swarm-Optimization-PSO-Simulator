using System.Numerics;

namespace ParticleSwarmOptimizationv3
{
    public class InfluenceFactor
    {
        private readonly SearchSpace _searchSpace;

        public int X { get; }
        public int Y { get; }
        public double Strength { get; }
        public double Range { get; }
        public bool IsAttractive { get; }

        private readonly double _rangeSquared;

        public InfluenceFactor((int x, int y) position, SearchSpace searchSpace, bool isAttractive, double strength = 1, double range = 1)
        {
            if (position.x < 0 || position.y < 0)
                throw new ArgumentException("Position cannot have negative coordinates.");
            if (strength <= 0 || strength > 5)
                throw new ArgumentOutOfRangeException(nameof(strength), "Strength must be between 1 and 5");
            if (range <= 0 || range > 40)
                throw new ArgumentOutOfRangeException(nameof(range), "Range must be between 1 and 40");
            if (!searchSpace.IsWithinBounds(position.x, position.y))
                throw new ArgumentException("Position must be within the search space bounds");

            X = position.x;
            Y = position.y;
            IsAttractive = isAttractive;
            Strength = isAttractive ? strength : -strength;
            Range = range;
            _searchSpace = searchSpace;
            _rangeSquared = Math.Pow(Range, 2);
        }

        /// <summary>
        /// Calculates the influence of a factor at a given position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>influence for a position</returns>

        public double CalculateInfluence((double x, double y) position)
        {
            if (!_searchSpace.IsWithinBounds((int)position.x, (int)position.y))
            {
                return 0;
            }

            Vector2 currentPos = new Vector2((float)position.x, (float)position.y);
            Vector2 influencePos = new Vector2(X, Y);

            double distanceSquared = Vector2.DistanceSquared(currentPos, influencePos);

            if (distanceSquared > _rangeSquared)
            {
                return 0;
            }

            return Strength * (1 - (distanceSquared / _rangeSquared));
        }

        public override string ToString()
        {
            return $"Influence Factor at ({X}, {Y}) with strength {Strength} and range {Range}";
        }
    }
}
