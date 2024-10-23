using ParticleSwarmOptimizationv3;

public class PSORunner
{
    public void Run()
    {
        bool runAgain = true;

        while (runAgain)
        {
            PSOConfig config = PSOConfigFactory.CreateDefaultConfig();

            PSO optimizer = new PSO(config);
            optimizer.Run();

            runAgain = PromptToRunAgain();
        }
    }

    /// <summary>
    /// Prompts the user if they want to run the optimizer again.
    /// </summary>
    /// <returns>True if the user wants to run again, false otherwise.</returns>
    private bool PromptToRunAgain()
    {
        Console.WriteLine("Press any key to run again or 'q' to quit...");
        var key = Console.ReadKey().KeyChar;
        return key != 'q' && key != 'Q';
    }
}
