using System.Numerics;

namespace ParticleSwarmOptimizationv3
{
    public class Particle
    {
        public Vector2 Velocity { get; private set; }
        public double[] Position { get; private set; }
        public double[] BestPosition { get; private set; }
        public double BestFitness { get; private set; }
        public double Fitness { get; private set; }

        public Particle(double[] position, Vector2 velocity)
        {
            Velocity = velocity;
            Position = position;
            BestPosition = (double[])position.Clone();
            BestFitness = 0;
            Fitness = 0;
        }

        public void UpdateBestFitness(double bestFitness)
        {
            BestFitness = bestFitness;
        }

        public void UpdateBestPosition(double[] bestPosition)
        {
            BestPosition = (double[])bestPosition.Clone();
        }

        public void UpdateFitness(double fitness)
        {
            Fitness = fitness;
        }

        public void UpdateVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void UpdatePosition()
        {
            Position[0] += Velocity.X;
            Position[1] += Velocity.Y;
        }

        public void UpdatePosition(double[] position)
        {
            Position = (double[])position.Clone();
        }

        public void PrintParticle(bool minimized = false)
        {
            string positionStr = $"[{string.Join(", ", Position)}]";
            string bestPositionStr = $"[{string.Join(", ", BestPosition)}]";

            if (minimized)
            {
                Console.Write($"Position: {positionStr} ");
                Console.Write($"Velocity: {Velocity} ");
                Console.Write($"Best Position: {bestPositionStr} ");
                Console.Write($"Best Fitness: {BestFitness} ");
                Console.Write($"Fitness: {Fitness} ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Position: {positionStr}");
                Console.WriteLine($"Velocity: {Velocity}");
                Console.WriteLine($"Best Position: {bestPositionStr}");
                Console.WriteLine($"Best Fitness: {BestFitness}");
                Console.WriteLine($"Fitness: {Fitness}");
            }
        }
    }
}
