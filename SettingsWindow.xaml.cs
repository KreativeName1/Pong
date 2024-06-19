using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Pong
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Dictionary<string, TextBox> textBoxes;
        private Config? config;
        public SettingsWindow()
        {
            InitializeComponent();

            textBoxes = new Dictionary<string, TextBox>();

            InitializeConfig();
            if (config == null)
            {
                MessageBox.Show("Error reading config file");
                Close();
                return;
            }

            foreach (var property in config.GetType().GetProperties())
            {
                Label label = new Label
                {
                    Content = property.Name,
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 18,
                };
                TextBox textBox = new TextBox
                {
                    Text = property.GetValue(config).ToString(),
                    Name = property.Name,
                    Width = 100,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 18,
                    Foreground = System.Windows.Media.Brushes.White,
                    Padding = new Thickness(5),
                    Background = System.Windows.Media.Brushes.Black,
                };
                textBoxes.Add(property.Name, textBox);
                panel.Children.Add(label);
                panel.Children.Add(textBox);
            }

            Back.Click += (sender, e) =>
            {
                Close();
            };

            Reset.Click += (sender, e) =>
            {
                Config.CreateConfig();
                config = Config.ReadConfig();
                if (config == null)
                {
                    MessageBox.Show("Error reading config file");
                    Close();
                    return;
                }
                foreach (var property in config.GetType().GetProperties())
                {
                    textBoxes[property.Name].Text = property.GetValue(config).ToString();
                }
            };

            Save.Click += (sender, e) =>
            {
                try
                {
                    foreach (var property in config.GetType().GetProperties())
                    {
                        var value = textBoxes[property.Name].Text;
                        var type = property.PropertyType;
                        var convertedValue = System.Convert.ChangeType(value, type);
                        property.SetValue(config, convertedValue);
                    }
                    System.IO.File.WriteAllText("Config.json", Newtonsoft.Json.JsonConvert.SerializeObject(config));
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter valid values");
                }
            };

        }

        private void InitializeConfig()
        {
            config = Config.ReadConfig();
            if (config == null)
            {
                Config.CreateConfig();
                config = Config.ReadConfig();
            }
        }
    }
}
