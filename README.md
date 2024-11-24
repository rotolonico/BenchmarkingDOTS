# Unity DOTS Benchmarking Project

This repository contains the Unity DOTS Benchmarking Project, designed to explore the Data-Oriented Design (DOD) paradigm implemented through Unity’s Data-Oriented Technology Stack (DOTS) and benchmark its performances over traditional MonoBehaviour

## Unity Project Description

This project investigates Unity DOTS, which implements the Data-Oriented Design paradigm. This project experiments with:
- Entity Component System (ECS) for organizing game logic using data-oriented principles.
- C# Job System for multithreaded task execution.
- Burst Compiler for high-performance low-level optimizations.

The application benchmarks three scenarios to compare OOP (MonoBehaviour) and DOD approaches in terms of performance.

### Scenarios

The project includes three scenarios designed to represent common computational challenges in video games and simulations:
- Scenario 1: Entity Movement Relative to Camera
Entities (spheres) move based on their distance from the camera. The closer an entity is to the camera, the faster it moves and changes color.
- Scenario 2: Nearest Entity Calculation
Each entity aligns its child object (Link) to point towards the nearest other entity.
- Scenario 3: Physics-Based Interaction
Entities bounce elastically within a bounded area, reacting to collisions with walls or each other. Managed using Unity Physics.

### Controls

#### Hub Scene

In the hub scene you can choose a scenario and an optimization level to test it on your own or you can start a benchmark.
In selecting benchmark mode, you can customize the following settings:
- Snapshot duration
- Base entity count
- Entity increment per snapshot
- Total snapshots

#### Normal Mode

In normal mode inside a scenario, you are able to move using the WASD or arrows keys, Space/Shift to navigate vertically and Left Control to sprint.
You can use the sliders to increase the number of entities and their spawn radius.

#### Benchmark mode

In benchmark mode you can't move the camera and also the Unity frustrum culling optimization is disabled to allow more consistent results. At the end of the process, a JSON with the average frames per seconds of every snapshot will be displayed in a copyable format.

## Thesis

The detailed documentation of the project, including methodology, technical descriptions, and benchmarking results, is available in the Thesis/ directory both in Italian (original language) and English (translated with the help of ChatGPT). This project has been my Bachelor's thesis in Software Engineering at the Polytechnic of Bari!

## License

This project is licensed under the MIT License. See the LICENSE file for details