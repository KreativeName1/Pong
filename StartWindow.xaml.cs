using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace Pong
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private GameModeEnum gameMode = GameModeEnum.Normal;
        public StartWindow()
        {
            InitializeComponent();
            Quit.Click += (sender, e) =>
            {
                Close();
            };

            Settings.Click += (sender, e) =>
            {
                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.Show();
            };

            GameMode.Click += (sender, e) =>
            {
                GameModeSelectionWindow gameModeSelectionWindow = new GameModeSelectionWindow();
                gameModeSelectionWindow.Show();
                gameMode = gameModeSelectionWindow.SelectedGameMode;
            };

            Start.Click += (sender, e) =>
            {
                switch (gameMode)
                {
                    case GameModeEnum.Normal:
                        Normal game = new Normal();
                        game.Show();
                        break;
                }
                Close();
            };

            Credits.Click += (sender, e) =>
            {
                CreditsWindow creditsWindow = new CreditsWindow();
                creditsWindow.Show();
            };


        }
    }
}
