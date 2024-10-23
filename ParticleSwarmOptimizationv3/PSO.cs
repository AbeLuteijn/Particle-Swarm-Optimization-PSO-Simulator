namespace ParticleSwarmOptimizationv3
{
    public class PSO
    {
        private readonly PSOConfig _config;
        private readonly Random _random;
        private readonly SearchSpace _searchSpace;
        private readonly Swarm _swarm;

        public PSO(PSOConfig config)
        {
            _config = config;
            _random = new Random();
            _searchSpace = new SearchSpace((_config.SearchSpaceWidth, _config.SearchSpaceHeight));

            _swarm = new Swarm(
                _config.NumParticles,
                new double[] { 0, 0 },
                new double[] { _config.SearchSpaceWidth, _config.SearchSpaceHeight },
                _config.InertiaWeight,
                _config.CognitiveWeight,
                _config.SocialWeight
            );

            InitializeInfluenceFactors();
            _swarm.UpdateFitnesses(_searchSpace);
        }


        public void Run()
        {
            for (int i = 0; i < _config.NumIterations; i++)
            {
                _swarm.UpdateSwarm(_searchSpace);

                if (_config.ShowIterationSteps)
                {
                    DisplayIteration();
                }
            }

            _swarm.MoveToBestPositions();
            DisplayFinalResult();
        }

        /// <summary>
        /// Initializes the influence factors in the search space according to the configuration.
        /// </summary>
        private void InitializeInfluenceFactors()
        {
            for (int i = 0; i < _config.NumInfluenceFactors; i++)
            {
                int x = _random.Next(0, _config.SearchSpaceWidth);
                int y = _random.Next(0, _config.SearchSpaceHeight);
                double strength = Math.Pow(_random.NextDouble(), 2) * 5;
                int range = _random.Next(1, _config.MaxRange);
                bool isAttractive = _random.NextDouble() > 0.2;

                _searchSpace.AddInfluenceFactor(new InfluenceFactor((x, y), _searchSpace, isAttractive, strength, range));
            }
        }

        private void DisplayIteration()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            _searchSpace.PrintSearchSpace(_swarm);
        }

        private void DisplayFinalResult()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            _searchSpace.PrintSearchSpace(_swarm);
        }
    }
}
