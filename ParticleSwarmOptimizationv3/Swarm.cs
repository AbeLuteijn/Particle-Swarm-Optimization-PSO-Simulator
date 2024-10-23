using System.Numerics;

namespace ParticleSwarmOptimizationv3
{
    public class Swarm
    {
        public List<Particle> Particles { get; private set; }
        public Particle? BestParticle { get; private set; }
        public double[] BestPosition { get; private set; }
        public double BestFitness { get; private set; }
        public double[] BestGlobalPosition { get; private set; }
        public double BestGlobalFitness { get; private set; }
        public double InertiaWeight { get; private set; }
        public double CognitiveCoefficient { get; private set; }
        public double SocialCoefficient { get; private set; }

        private static readonly Random Random = new Random();

        public Swarm(int numberOfParticles, double[] lowerBound, double[] upperBound, double inertiaWeight, double cognitiveCoefficient, double socialCoefficient)
        {
            Particles = InitializeParticles(numberOfParticles, lowerBound, upperBound);
            BestParticle = null;
            BestPosition = new double[2];
            BestFitness = 0;
            BestGlobalPosition = new double[2];
            BestGlobalFitness = 0;
            InertiaWeight = inertiaWeight;
            CognitiveCoefficient = cognitiveCoefficient;
            SocialCoefficient = socialCoefficient;
        }

        private List<Particle> InitializeParticles(int numberOfParticles, double[] lowerBound, double[] upperBound)
        {
            return Enumerable.Range(0, numberOfParticles).Select(i =>
            {
                double[] position = new double[lowerBound.Length];
                for (int j = 0; j < lowerBound.Length; j++)
                {
                    position[j] = Random.NextDouble() * (upperBound[j] - lowerBound[j]) + lowerBound[j];
                }
                Vector2 velocity = new Vector2((float)Random.NextDouble(), (float)Random.NextDouble());
                return new Particle(position, velocity);
            }).ToList();
        }


        /// <summary>
        /// Updates all particles in the swarm. Updates the best particle and global best position.
        /// </summary>
        /// <param name="searchSpace"></param>
        public void UpdateSwarm(SearchSpace searchSpace)
        {
            foreach (Particle particle in Particles)
            {
                UpdateParticleVelocity(particle);
                particle.UpdatePosition();

                double newFitness = CalculateFitness(particle.Position, searchSpace);
                particle.UpdateFitness(newFitness);

                UpdateBestParticle(particle, newFitness);
            }
        }


        /// <summary>
        /// Calculates the new velocity of a particle based on its current velocity, best position, and global best position.
        /// </summary>
        /// <param name="particle"></param>
        /// <inheritdoc cref="https://en.wikipedia.org/wiki/Particle_swarm_optimization"/>
        private void UpdateParticleVelocity(Particle particle)
        {
            Vector2 inertia = particle.Velocity * (float)InertiaWeight;
            Vector2 cognitive = new Vector2(
                (float)(CognitiveCoefficient * Random.NextDouble() * (particle.BestPosition[0] - particle.Position[0])),
                (float)(CognitiveCoefficient * Random.NextDouble() * (particle.BestPosition[1] - particle.Position[1])));
            Vector2 social = new Vector2(
                (float)(SocialCoefficient * Random.NextDouble() * (BestGlobalPosition[0] - particle.Position[0])),
                (float)(SocialCoefficient * Random.NextDouble() * (BestGlobalPosition[1] - particle.Position[1])));

            Vector2 newVelocity = inertia + cognitive + social;
            particle.UpdateVelocity(newVelocity);
        }

        /// <summary>
        /// Updates the best position of a particle and the global best position if the new fitness is better.
        /// </summary>
        /// <param name="particle"></param>
        /// <param name="newFitness"></param>
        private void UpdateBestParticle(Particle particle, double newFitness)
        {
            if (newFitness > particle.BestFitness)
            {
                particle.UpdateBestFitness(newFitness);
                particle.UpdateBestPosition((double[])particle.Position.Clone());

                if (newFitness > BestGlobalFitness)
                {
                    BestGlobalFitness = newFitness;
                    BestGlobalPosition = (double[])particle.Position.Clone();
                    BestParticle = particle;
                }
            }
        }

        private double CalculateFitness(double[] position, SearchSpace searchSpace)
        {
            return searchSpace.InfluenceFactors.Sum(factor => factor.CalculateInfluence((position[0], position[1])));
        }

        public void MoveToBestPositions()
        {
            foreach (Particle particle in Particles)
            {
                particle.UpdatePosition(particle.BestPosition);
            }
        }

        public void UpdateFitnesses(SearchSpace searchSpace)
        {
            foreach (var particle in Particles)
            {
                double fitness = CalculateFitness(particle.Position, searchSpace);
                particle.UpdateFitness(fitness);
                particle.UpdateBestFitness(fitness);
            }
        }

        public void PrintSwarm()
        {
            Console.WriteLine($"Swarm with {Particles.Count} particles");
            foreach (var particle in Particles)
            {
                particle.PrintParticle();
            }
        }
    }
}
