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
    /// Interaktionslogik für GameModeSelectionWindow.xaml
    /// </summary>
    public partial class GameModeSelectionWindow : Window
    {
        public GameModeEnum SelectedGameMode;
        public GameModeSelectionWindow()
        {
            InitializeComponent();
            SelNormal.Click += (sender, e) =>
            {
                SelectedGameMode = GameModeEnum.Normal;
                Close();
            };
            SelMulitplayerNormal.Click += (sender, e) =>
            {
                SelectedGameMode = GameModeEnum.MultiplayerNormal;
                Close();
            };
        }
    }
}
