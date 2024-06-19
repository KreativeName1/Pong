using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int computerScore = 0;
        private double currentComputerMoveSpeed;


        private int playerScore = 0;
        private double curPlayerMoveSpeed;

        private double balldirection;

        Config? Config = new Config();
        Random random = new Random();


        private DispatcherTimer? gameTimer;
        public MainWindow()
        {
            InitializeComponent();
            InitializeConfig();
            if (Config == null)
            {
                MessageBox.Show("Error reading config file");
                this.Close();
                return;

            }
            InitializeGameTime();
            if (gameTimer == null)
            {
                MessageBox.Show("Error setting up game timer");
                this.Close();
                return;
            }
            reset();

            this.SizeChanged += (sender, e) =>
            {
                Canvas.SetLeft(computer, game.ActualWidth - computer.Width - 10);
                Canvas.SetLeft(Scoreboard, game.ActualWidth / 2 - Scoreboard.Width / 2);

                Canvas.SetLeft(PauseMenu, game.ActualWidth / 2 - PauseMenu.Width / 2);
                Canvas.SetTop(PauseMenu, game.ActualHeight / 2 - PauseMenu.Height / 2);


            };

            Resume.Click += (sender, e) =>
            {
                gameTimer.Start();
                PauseMenu.Visibility = Visibility.Hidden;
            };

            Quit.Click += (sender, e) =>
            {
                this.Close();
            };

            Restart.Click += (sender, e) =>
            {
                playerScore = 0;
                computerScore = 0;
                PlayerScore.Content = 0;
                ComputerScore.Content = 0;
                reset();
                gameTimer.Start();
                PauseMenu.Visibility = Visibility.Hidden;
            };

            Settings.Click += (sender, e) =>
            {
                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.ShowDialog();
                Config? newConfig = Config.ReadConfig();
                if (newConfig != null)
                {
                    Config = newConfig;
                    gameTimer.Interval = TimeSpan.FromMilliseconds(Config.GameTickInMS);
                }
            };
            this.Loaded += (sender, e) =>
            {
                Restart.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            };

        }

        private void InitializeGameTime()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(Config.GameTickInMS);
            gameTimer.Tick += GameTick;
            gameTimer.Start();

        }

        private void GameTick(object? sender, EventArgs e)
        {
            MoveBall();
            MoveComputer();
            MovePlayer();
            CheckCollision();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (gameTimer.IsEnabled)
                {
                    gameTimer.Stop();
                    PauseMenu.Visibility = Visibility.Visible;
                }
                else
                {
                    gameTimer.Start();
                    PauseMenu.Visibility = Visibility.Hidden;

                }
            }
        }

        private void MovePlayer()
        {
            if (IsKeyDown(ConsoleKey.W) || IsKeyDown(ConsoleKey.UpArrow))
            {
                double y = Canvas.GetTop(player);
                if (y > 0)
                {
                    Canvas.SetTop(player, y - Config.PlayerMoveSpeed);
                    curPlayerMoveSpeed = -Config.PlayerMoveSpeed;
                }
            }
            else if (IsKeyDown(ConsoleKey.S) || IsKeyDown(ConsoleKey.DownArrow))
            {
                double y = Canvas.GetTop(player);
                if (y < game.ActualHeight - player.Height)
                {
                    Canvas.SetTop(player, y + Config.PlayerMoveSpeed);
                    curPlayerMoveSpeed = Config.PlayerMoveSpeed;
                }
            }
            else curPlayerMoveSpeed = 0;
        }

        private void MoveBall()
        {
            double x = Canvas.GetLeft(ball);
            double y = Canvas.GetTop(ball);

            double angle = balldirection * Math.PI / 180;
            double dx = Config.BallMoveSpeed * Math.Cos(angle);
            double dy = Config.BallMoveSpeed * Math.Sin(angle);

            Canvas.SetLeft(ball, x + dx);
            Canvas.SetTop(ball, y + dy);

            if (Canvas.GetTop(ball) < 0 || Canvas.GetTop(ball) > game.ActualHeight - ball.Height)
            {
                balldirection = 360 - balldirection;
            }
            if (Canvas.GetLeft(ball) < 0 || Canvas.GetLeft(ball) > game.ActualWidth - ball.Width)
            {
                balldirection = (180 - balldirection) % 360;
            }
        }

        private void CheckCollision()
        {
            // Check collision with player
            if (Canvas.GetLeft(ball) < Canvas.GetLeft(player) + player.Width && Canvas.GetLeft(ball) + ball.Width > Canvas.GetLeft(player) && Canvas.GetTop(ball) < Canvas.GetTop(player) + player.Height && Canvas.GetTop(ball) + ball.Height > Canvas.GetTop(player))
            {
                balldirection = 180  - balldirection + curPlayerMoveSpeed *  Config.BallDirectionMultiplier;

            }

            // Check collision with computer
            if (Canvas.GetLeft(ball) < Canvas.GetLeft(computer) + computer.Width && Canvas.GetLeft(ball) + ball.Width > Canvas.GetLeft(computer) && Canvas.GetTop(ball) < Canvas.GetTop(computer) + computer.Height && Canvas.GetTop(ball) + ball.Height > Canvas.GetTop(computer))
            {
                balldirection = 180 - balldirection + currentComputerMoveSpeed * Config.BallDirectionMultiplier;
            }

            // Check collision with left wall
            if (Canvas.GetLeft(ball) < 0)
            {
                ComputerScore.Content = ++computerScore;
                reset();
            }

            // Check collision with right wall
            if (Canvas.GetLeft(ball) > game.ActualWidth - ball.Width)
            {
                PlayerScore.Content = ++playerScore;
                reset();
            }

        }

        private void reset()
        {
            Canvas.SetLeft(ball, game.ActualWidth / 2);
            Canvas.SetTop(ball, game.ActualHeight / 2);
            balldirection = 0;
            Config.BallMoveSpeed += Config.BallSpeedIncrease;
            Config.ComputerMoveSpeed += Config.ComputerSpeedIncrease;

            Canvas.SetTop(player, game.ActualHeight / 2 - player.Height / 2);
            Canvas.SetTop(computer, game.ActualHeight / 2 - computer.Height / 2);
        }

        private void MoveComputer()
        {
            double y = Canvas.GetTop(computer);
            double ballY = Canvas.GetTop(ball);

            if (random.NextDouble() < Config.ComputerReactionTime)
            {
                if (Config.ComputerReactionDelayInSeconds <= 0)
                {
                    // If the ball is below the computer, move the computer down
                    if (ballY > y + computer.Height / 2)
                    {
                        if (y < game.ActualHeight - computer.Height)
                        {
                            Canvas.SetTop(computer, y + Config.ComputerMoveSpeed);
                            currentComputerMoveSpeed = Config.ComputerMoveSpeed;
                        }
                    }

                    // If the ball is above the computer, move the computer up
                    else if (ballY < y + computer.Height / 2)
                    {
                        if (y > 0)
                        {
                            Canvas.SetTop(computer, y - Config.ComputerMoveSpeed);
                            currentComputerMoveSpeed = -Config.ComputerMoveSpeed;
                        }
                    }
                    // If the ball is at the same height as the computer, don't move (with margin of error)
                    // This is to prevent the computer from moving rapidly up and down when the ball is at the same height
                    else if (ballY > y - Config.SameHeightMarginOfError && ballY < y + Config.SameHeightMarginOfError)
                    {
                        currentComputerMoveSpeed = 0;
                    }
                    Config.ComputerReactionDelayInSeconds = 1;
                }
                else
                {
                    Config.ComputerReactionDelayInSeconds--;
                }
            }
        }

        private void InitializeConfig()
        {
            Config = Config.ReadConfig();
            if (Config == null)
            {
                Config.CreateConfig();
                Config = Config.ReadConfig();
            }
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(ConsoleKey vKey);

        private bool IsKeyDown(ConsoleKey key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }
    }
}
