# Particle Swarm Optimization (PSO) Simulator

This project implements a Particle Swarm Optimization (PSO) algorithm in C# to simulate the behavior of particles in a 2D search space influenced by various factors. PSO is a popular optimization technique inspired by the social behavior of birds flocking or fish schooling, where particles explore a search space in search of an optimal solution based on their own experience and the experience of the swarm.

## Features
- **Configurable PSO parameters**: Set the number of particles, iterations, inertia weight, and cognitive/social coefficients to customize the behavior of the swarm.
- **Influence Factors**: Define attractive or repulsive influence factors in the search space that affect the movement of particles.
- **Fitness Calculation**: Particles evaluate their fitness based on their proximity to influence factors, moving toward the optimal solution.
- **Dynamic Swarm Updates**: Particles adjust their velocities based on their personal best positions and the global best position of the swarm, leading to collective optimization.
- **Customizable Search Space**: The search space can be configured with different boundaries and influence factors to create unique environments for the swarm.
- **Visualization**: Optionally visualize the movement of particles and track the optimization process step-by-step.

## Key Components
- **Swarm**: A collection of particles that explore the search space and collaborate to find the optimal solution.
- **Particle**: Each particle maintains its position, velocity, and best-known fitness and adjusts its trajectory based on personal and global best positions.
- **Search Space**: A 2D grid where particles navigate, with various influence factors scattered throughout.
- **Influence Factor**: Points within the search space that either attract or repel particles, based on their distance and range.

## How it Works
1. The swarm is initialized with a set number of particles, each with random positions and velocities.
2. Influence factors are placed in the search space, either attracting or repelling particles.
3. At each iteration, particles adjust their velocity based on their previous best position, the global best position of the swarm, and random exploration.
4. Particles move to new positions, evaluate their fitness, and update their personal and global bests.
5. The algorithm continues for a set number of iterations or until the optimal solution is found.

## Configuration
The PSO parameters can be easily adjusted through the `PSOConfig` class, including:
- Number of particles
- Inertia weight
- Cognitive and social coefficients
- Number of iterations
- Search space dimensions and influence factor properties

## Getting Started
Clone the repository and explore the simulation with different configurations. Customize the search space and influence factors to see how they affect the behavior of the swarm and the resulting optimization process.

```bash
git clone https://github.com/AbeLuteijn/Particle-Swarm-Optimization-PSO-Simulator.git
cd pso-simulator
```

Run the project in your preferred C# development environment and start experimenting with PSO!

## License
This project is licensed under the MIT License. Feel free to use, modify, and distribute it as needed.
