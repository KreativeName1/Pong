# Pong (C# WPF)

## Description
A simple Pong game made in C# using WPF. There are two paddles, one controlled by the player and the other by the computer. The goal is to hit the ball with the paddle and make the ball go past the opponent's paddle. After each point, the ball will reset to the center of the screen and move in a random direction. The speed of the ball will increase after each point scored.

## Features
- Score tracking.
- Pause functionality.
- Settings menu to change different values like paddle speed, ball speed, etc.
- Computer AI to play against.

## Controls
- Use W or Arrow Up to move the paddle up.
- Use S or Arrow Down to move the paddle down.
- Use ESC to pause the game.

## Planned Features
- Start menu
- Local multiplayer
- Highscore tracking
- Sound effects
- Different game modes (maybe)
- Online multiplayer (maybe)

## Known Issues
- The ball sometimes gets stuck in the paddle, causing it to bounce around in weird ways. Happens mostly when the paddle hits the ball at the top/bottom edge or corners.
- If the ball is to fast, it can go through the paddle.

## Configuration Options
  - computerMoveSpeed: The speed of the computer's paddle
  - computerReactionTime: The time it takes for the computer to react to the ball
  - computerReactionDelayInSeconds: The delay in seconds before the computer reacts to the ball
  - sameHeightMarginOfError: Used to prevent the computer from moving up and down too much when the ball is at the same height as the computer's paddle
  - playerMoveSpeed: The speed of the player's paddle (Currently different from the computer's paddle speed)
  - ballMoveSpeed: The speed of the ball
  - ballDirectionMultiplier: How much the balls direction changes when it hits a moving paddle
  - gameTickInMS: The time in milliseconds between each game tick
  - ballSpeedIncrease: The amount the ball's speed increases by when the paddle misses the ball
  - computerSpeedIncrease: The amount the computer's speed increases by when the paddle misses the ball

## Screenshots
![Game](https://github.com/KreativeName1/Pong/assets/115576847/7dab7225-e124-46f0-af89-ff925b8898ef)

![Pause Menu](https://github.com/KreativeName1/Pong/assets/115576847/6c102af7-1ad0-46c4-8b9c-ce27264a2580)

![Settings Menu](https://github.com/KreativeName1/Pong/assets/115576847/c809c4ac-5a12-4767-b323-266cb2dd4f0c)
