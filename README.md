# Breakout Game

A classic Breakout game built with Unity.

## About

This is a Unity implementation of the classic arcade game Breakout. Players control a paddle to bounce a ball and destroy blocks. The game includes persistent data storage, player name tracking, and bonus block mechanics.

## Features

- **Classic Breakout Gameplay**: Control a paddle to keep the ball in play and destroy all blocks
- **Bonus Block System**: Special blocks with multiple hit points for extra scoring opportunities
- **Persistent Scoring**: Your scores are saved between game sessions
- **Player Profiles**: Enter your name or play anonymously
- **Cross-Scene Data**: Scores and player data persist across menu and game scenes

## Getting Started

### Prerequisites

- Unity 6.1
- Git

### Installation

1. Clone the repository:

   ```bash
   git clone <repository-url>
   cd Breakout
   ```

2. Open the project in Unity:

   - Launch Unity Hub
   - Click "Open" and select the project folder
   - Unity will import all assets and dependencies

3. Open the main scene in `Assets/Scenes/` and press Play to start the game

## How to Play

### Controls
- **Left/Right** arrow keys or **A/D** keys to move the paddle
- **Enter** to confirm name input
- **Mouse** to interact with menu buttons

### Gameplay
- Keep the ball in play by bouncing it off your paddle
- Destroy all regular blocks to complete the level
- **Regular blocks**: Destroyed in three, two or one hit
- **Bonus blocks**: Require multiple hits but give extra points
- Don't let the ball fall off the bottom of the screen!
