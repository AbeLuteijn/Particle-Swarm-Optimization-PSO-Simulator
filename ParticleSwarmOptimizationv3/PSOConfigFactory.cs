using ParticleSwarmOptimizationv3;

public static class PSOConfigFactory
{
    /// <summary>
    /// Creates the default PSO configuration.
    /// </summary>
    public static PSOConfig CreateDefaultConfig()
    {
        return new PSOConfig
        {
            NumInfluenceFactors = 40,
            SearchSpaceWidth = 150,
            SearchSpaceHeight = 80,
            MaxRange = 15,
            NumParticles = 100,
            NumIterations = 1000,
            InertiaWeight = 0.8,
            CognitiveWeight = 1,
            SocialWeight = 1,
            ShowIterationSteps = false
        };
    }
}
