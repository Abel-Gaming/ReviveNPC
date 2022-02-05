using CitizenFX.Core;
using SharpConfig;
using System;
using static CitizenFX.Core.Native.API;

namespace server
{
    public class Config
    {
        public static Load Load;
    }

    public struct Load
    {
        public string reviveCommand;

        public static void Config()
        {
            Load config = new Load()
            {
                reviveCommand = "revive"
            };

            var configFile = Configuration.LoadFromFile(string.Format("{0}/config/config.ini", GetResourcePath(GetCurrentResourceName())));

            try
            {
                bool isFileNotNull = !string.IsNullOrWhiteSpace(configFile.ToString());

                if (isFileNotNull)
                {
                    //Get commands settings section
                    var CommandSection = configFile["COMMANDS"];

                    //Get the revive command
                    config.reviveCommand = CommandSection["ReviveCommand"].StringValue;
                }
                else
                {
                    //Empty config error
                    Debug.WriteLine("The configuration file is empty");
                    return;
                }
            }
            catch (Exception ex)
            {
                //Error with config
                Debug.WriteLine($"Something went wrong while loading the configuration file. {ex}");
                return;
            }
        }
    }
}
