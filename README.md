# Discrete-Event-Simulator
An open source C#-based discrete event simulation library.

## Overview
This library provides a framework for discrete event simulation in C#. It allows you to simulate time-based events in a controlled environment, making it useful for testing time-dependent scenarios without waiting for real time to pass.

## Features
- Thread-based discrete event simulation
- Priority queue implementation for event scheduling
- Support for multiple threads with independent timelines
- Proper resource management with IDisposable implementation

## Installation
Add the library to your .NET project targeting .NET Standard 2.0 or higher.

## Example
```csharp
using DiscreteEventSimulator.Simulation;
using System;
using System.Threading;

// Create a simulator instance
using (var simulator = new Simulator())
{
    // Create and add a thread
    var thread = new Thread(() =>
    {
        // Simulate a delay
        simulator.Delay(TimeSpan.FromSeconds(5));
        Console.WriteLine($"Time: {simulator.GetTime()}");
    });
    
    simulator.AddThread(thread);
    
    // Start the simulation
    simulator.Start();
    thread.Start();
    
    // Wait for completion
    var finalTime = simulator.Stop();
    Console.WriteLine($"Simulation completed at: {finalTime}");
}
```

## API Reference

### Simulator
- `AddThread(Thread thread)`: Add a thread to the simulator
- `Delay(TimeSpan delay)`: Delay the current thread by the specified duration
- `GetTime()`: Get the current simulation time for the current thread
- `Start()`: Start the simulation
- `Stop()`: Stop the simulation and return the final time

## Contributing
Contributions are welcome! Please feel free to submit pull requests or open issues.

## License
Open source - see repository for license details.