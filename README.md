# 🎮 2D Platformer - OOP Implementation

A robust 2D platformer game built from scratch using **C#** and **Windows Forms**. This project demonstrates the practical application of Object-Oriented Programming (OOP) patterns in game development, including inheritance, interfaces, and modular systems.

## 🚀 Key Features

*   **Progressive Level System**: Three distinct levels with increasing difficulty, unique tilesets, and environmental backgrounds.
*   **Object-Oriented Architecture**: Modular design using Interfaces and Base Classes for Entities, Movements, and Rendering.
*   **Combat System**: 
    *   Player burst-fire mechanics (Single, Double, and Triple bursts depending on the level).
    *   Collision-based damage and health pack recovery.
*   **Advanced AI**:
    *   **Level 1**: Simple patrolling enemies using Random AI.
    *   **Level 2 & 3**: Aggressive AI that tracks player movement and scales attack speed/range.
*   **Game Engine Basics**:
    *   Custom Camera System with world boundary clamping.
    *   Frame-based game loop with delta time handling.
    *   Buffered Graphics to prevent flickering.
    *   Persistent scoring across levels.

## 🛠️ Technical Stack

*   **Language**: C#
*   **Framework**: .NET / WinForms
*   **Architecture**: OOP (Interfaces, Polymorphism, Singleton Pattern for Sound/Managers)
*   **Input**: `EZInput` library for smooth keyboard handling.

## 🎮 Controls

| Action | Key |
| :--- | :--- |
| **Move Left/Right** | `A` / `D` or `Arrow Keys` |
| **Jump** | `Space` / `W` |
| **Shoot** | `X` / `Left Ctrl` |
| **Restart (on Death)** | `R` |

## 📁 Project Structure

*   `/Core`: Main Game engine and Camera logic.
*   `/Entities`: Player, Enemy, and GameObject definitions.
*   `/Level`: Level data and automated Level Loader.
*   `/Movements`: Different AI and Player movement strategies.
*   `/Rendering`: Sprite loading and animation systems.
*   `/Systems`: Sound management and collision handling.

*   ## ⚙️ Installation & Setup

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/manto187/2D_Game_OOP.git
    ```
2.  **Open in Visual Studio**:
    Open the `FirstDesktopApp.sln` file.
3.  **Restore NuGet Packages**:
    Ensure `EZInput` is restored.
4.  **Resources**:
    Ensure the `Resources/` folder is present in your output directory (`bin/Debug/`) to load sprites and sounds.

---
