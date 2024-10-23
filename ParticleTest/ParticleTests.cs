using System.Numerics;

namespace ParticleTest
{
    public class Tests
    {
        [Test]
        public void TestUpdateBestFitness()
        {
            Particle particle = new Particle(new double[] { 0, 0 }, new Vector2(0, 0));
            particle.UpdateBestFitness(5);
            Assert.That(particle.BestFitness, Is.EqualTo(5));
        }

        [Test]
        public void TestUpdateBestPosition()
        {
            Particle particle = new Particle(new double[] { 0, 0 }, new Vector2(0, 0));
            particle.UpdateBestPosition(new double[] { 5, 5 });
            Assert.That(particle.BestPosition, Is.EqualTo(new double[] { 5, 5 }));
        }

        [Test]
        public void TestUpdateFitness()
        {
            Particle particle = new Particle(new double[] { 0, 0 }, new Vector2(0, 0));
            particle.UpdateFitness(5);
            Assert.That(particle.Fitness, Is.EqualTo(5));
        }

        [Test]
        public void TestUpdateVelocity()
        {
            Particle particle = new Particle(new double[] { 0, 0 }, new Vector2(0, 0));
            particle.UpdateVelocity(new Vector2(5, 5));
            Assert.That(particle.Velocity, Is.EqualTo(new Vector2(5, 5)));
        }

        [Test]
        public void TestUpdatePosition()
        {
            Particle particle = new Particle(new double[] { 0, 0 }, new Vector2(0, 0));
            particle.UpdatePosition(new double[] { 5, 5 });
            Assert.That(particle.Position, Is.EqualTo(new double[] { 5, 5 }));
        }

        [Test]
        public void TestCalculateNewVelocity()
        {
            Particle particle = new Particle(new double[] { 10, 10 }, new Vector2(0.5f, 0.5f));
            particle.UpdateBestPosition(new double[] { 7, 7 });
            double InertiaWeight = 0.5;
            double CognitiveCoefficient = 2;
            double SocialCoefficient = 1;
            double[] BestGlobalPosition = new double[] { 5, 5 };

            Vector2 Intertia = new Vector2((float)(InertiaWeight * particle.Velocity.X), (float)(InertiaWeight * particle.Velocity.Y));
            Vector2 Cognitive = new Vector2((float)(CognitiveCoefficient * 0.5 * (particle.BestPosition[0] - particle.Position[0])),
                (float)(CognitiveCoefficient * 0.5 * (particle.BestPosition[1] - particle.Position[1])));
            Vector2 Social = new Vector2((float)(SocialCoefficient * 0.5 * (BestGlobalPosition[0] - particle.Position[0])),
                (float)(SocialCoefficient * 0.5 * (BestGlobalPosition[1] - particle.Position[1])));

            Vector2 newVelocity = Intertia + Cognitive + Social;

            Vector2 expectedVelocity = new Vector2(0.25f, 0.25f) + new Vector2(-3f, -3f) + new Vector2(-2.5f, -2.5f);

            Assert.That(newVelocity, Is.EqualTo(expectedVelocity));
        }

        [Test]
        public void TestFitness()
        {
            SearchSpace searchSpace = new SearchSpace((30, 30));
            searchSpace.AddInfluenceFactor(new InfluenceFactor((5, 5), searchSpace, true, 3, 5));
            searchSpace.AddInfluenceFactor(new InfluenceFactor((15, 15), searchSpace, true, 5, 10));
            searchSpace.AddInfluenceFactor(new InfluenceFactor((25, 25), searchSpace, true, 3, 5));
            searchSpace.AddInfluenceFactor(new InfluenceFactor((1, 28), searchSpace, false, 2, 10));
            searchSpace.AddInfluenceFactor(new InfluenceFactor((25, 5), searchSpace, false, 3, 7));


            double[] position = { 25, 5 };
            double result = searchSpace.InfluenceFactors.Sum(influenceFactor =>
            {
                double influence = influenceFactor.CalculateInfluence(((int)position[0], (int)position[1]));
                return influence;
            });

            Assert.That(result, Is.EqualTo(-3));
        }


    }
}