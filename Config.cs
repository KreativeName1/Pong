using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pong
{
    internal class Config
    {
        public double ComputerMoveSpeed { get; set; }
        public double ComputerReactionTime { get; set; }
        public double ComputerReactionDelayInSeconds { get; set; }
        public double SameHeightMarginOfError { get; set; }
        public double PlayerMoveSpeed { get; set; }
        public double BallDirectionMultiplier { get; set; }
        public double BallMoveSpeed { get; set; }

        public int GameTickInMS { get; set; }
        public double BallSpeedIncrease { get; set; }
        public double ComputerSpeedIncrease { get; set; }

        public static Config? ReadConfig()
        {
            string json = System.IO.File.ReadAllText("Config.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(json);
        }
        public static void CreateConfig()
        {
            Config config = new Config
            {
                ComputerMoveSpeed = 8,
                ComputerReactionTime = 0.5,
                ComputerReactionDelayInSeconds = 1,
                SameHeightMarginOfError = 10,
                PlayerMoveSpeed = 10,
                BallMoveSpeed = 6,
                GameTickInMS = 15,
                BallSpeedIncrease = 0.5,
                ComputerSpeedIncrease = 0.5,
                BallDirectionMultiplier = 2,
            };
            System.IO.File.WriteAllText("Config.json", Newtonsoft.Json.JsonConvert.SerializeObject(config));
        }
    }
}
