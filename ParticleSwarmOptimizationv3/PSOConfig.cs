namespace ParticleSwarmOptimizationv3
{
    public class PSOConfig
    {
        public int NumInfluenceFactors { get; set; }
        public int SearchSpaceWidth { get; set; }
        public int SearchSpaceHeight { get; set; }
        public int MaxRange { get; set; }
        public int NumParticles { get; set; }
        public int NumIterations { get; set; }
        public double InertiaWeight { get; set; }
        public double CognitiveWeight { get; set; }
        public double SocialWeight { get; set; }
        public bool ShowIterationSteps { get; set; }
    }
}
