# Pong (C# WPF)

## Description
A simple Pong game made in C# using WPF. There are two paddles, one controlled by the player and the other by the computer. The goal is to hit the ball with the paddle and make the ball go past the opponent's paddle. After each point, the ball will reset to the center of the screen and move in a random direction. The speed of the ball will increase after each point scored.

## Features
- Sound effects for ball hitting the paddles and the walls.
- Start screen
- Gamemode selection (Currently only one gamemode, Player vs Computer)
- Score tracking.
- Pause functionality.
- Settings menu to change different values like paddle speed, ball speed, etc.
- Computer AI to play against.

## Controls
- Use W or Arrow Up to move the paddle up.
- Use S or Arrow Down to move the paddle down.
- Use ESC to pause the game.

## Planned Features
- Local multiplayer
- Different game modes (maybe)
- Online multiplayer (maybe)

## Known Issues
- The ball sometimes gets stuck in the paddle, causing it to bounce around in weird ways. Happens mostly when the paddle hits the ball at the top/bottom edge or corners.
- If the ball is to fast, it can go through the paddle.
- Link to Github crashes the game.

## Configuration Options
  - computerMoveSpeed: The speed of the computer's paddle
  - computerReactionTime: The time it takes for the computer to react to the ball (higher value = faster reaction)
  - computerReactionDelayInSeconds: The delay in seconds before the computer reacts to the ball
  - sameHeightMarginOfError: Used to prevent the computer from moving up and down too much when the ball is at the same height as the computer's paddle
  - playerMoveSpeed: The speed of the player's paddle (Currently different from the computer's paddle speed)
  - ballMoveSpeed: The speed of the ball
  - ballDirectionMultiplier: How much the balls direction changes when it hits a moving paddle
  - gameTickInMS: The time in milliseconds between each game tick
  - ballSpeedIncrease: The amount the ball's speed increases by when the paddle misses the ball
  - computerSpeedIncrease: The amount the computer's speed increases by when the paddle misses the ball

## Screenshots
![Start](https://github.com/KreativeName1/Pong/assets/115576847/edf8421f-16ab-4aef-8641-d779b150ccc0)

![Gamemode Selection](https://github.com/KreativeName1/Pong/assets/115576847/19cebdba-419a-4b71-a6d7-5327a0b7ad1a)

![Game](https://github.com/KreativeName1/Pong/assets/115576847/7dab7225-e124-46f0-af89-ff925b8898ef)

![Pause Menu](https://github.com/KreativeName1/Pong/assets/115576847/87eaa88b-97a1-46c5-afed-35f64d017c06)

![Settings Menu](https://github.com/KreativeName1/Pong/assets/115576847/c809c4ac-5a12-4767-b323-266cb2dd4f0c)
