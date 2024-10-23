using Pastel;
using System.Drawing;

namespace ParticleSwarmOptimizationv3
{
    public class SearchSpace
    {
        public int Width { get; }
        public int Height { get; }
        public List<InfluenceFactor> InfluenceFactors { get; private set; } = new List<InfluenceFactor>();

        public SearchSpace((int width, int height) dimensions)
        {
            if (dimensions.width <= 0)
                throw new ArgumentOutOfRangeException(nameof(dimensions.width), "Width must be greater than 0");
            if (dimensions.height <= 0)
                throw new ArgumentOutOfRangeException(nameof(dimensions.height), "Height must be greater than 0");

            Width = dimensions.width;
            Height = dimensions.height;
        }

        public void AddInfluenceFactor(InfluenceFactor influenceFactor)
        {
            if (influenceFactor == null)
                throw new ArgumentNullException(nameof(influenceFactor), "Influence factor cannot be null");

            InfluenceFactors.Add(influenceFactor);
        }

        /// <summary>
        /// Renders the search space with particles and influence factors.
        /// </summary>
        /// <param name="swarm">The swarm of particles to display.</param>
        public void PrintSearchSpace(Swarm swarm)
        {
            HashSet<(int, int)> particlePositions = new HashSet<(int, int)>(
                swarm.Particles.Select(p => ((int)Math.Floor(p.Position[0]), (int)Math.Floor(p.Position[1])))
            );

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    string displayText = GetDisplayTextForPosition(j, i, particlePositions);
                    Console.Write(displayText);
                }
                Console.WriteLine();
            }

            Console.WriteLine(new string(' ', Width * 2 + 4));
        }

        private string GetDisplayTextForPosition(int x, int y, HashSet<(int, int)> particlePositions)
        {
            bool particlePresent = particlePositions.Contains((x, y));
            double influence = CalculateTotalInfluenceAtPosition(x, y);
            string color = CalculateColor(influence, InfluenceFactors.Max(f => f.Strength));
            return (particlePresent ? "[]" : "  ").PastelBg(color);
        }

        private double CalculateTotalInfluenceAtPosition(int x, int y)
        {
            return InfluenceFactors.Sum(factor => factor.CalculateInfluence((x, y)));
        }

        /// <summary>
        /// Calculates the color based on influence.
        /// </summary>
        /// <param name="influence">The calculated influence value.</param>
        /// <param name="maxStrength">The maximum influence strength.</param>
        /// <returns>Hex color string for the background.</returns>
        private string CalculateColor(double influence, double maxStrength)
        {
            double normalizedInfluence = Math.Min(Math.Abs(influence) / maxStrength, 1.0);
            Color color = influence > 0
                ? Color.FromArgb(0, 0, (int)(255 * normalizedInfluence))
                : influence < 0
                    ? Color.FromArgb((int)(255 * normalizedInfluence), 0, 0)
                    : Color.Black;

            return ConvertColorToHex(color);
        }

        /// <summary>
        /// Converts a color to its hex representation.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>A string representing the hex value of the color.</returns>
        public string ConvertColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// Checks whether a given position is within the bounds of the search space.
        /// </summary>
        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
