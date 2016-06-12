using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Scaletread.Engine.FileIO.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.FileIO
{
    public static class FileIOSystem
    {
        #region Game Settings
        public static void LoadGameSettings(ref GameSettings gameSettings)
        {
            try
            {
                Directory.CreateDirectory(DevConstants.FileIOConstants.GameSettings.SettingsDirectory);
                if (!File.Exists(DevConstants.FileIOConstants.GameSettings.CurrentSettings))
                {
                    FileIOSystem.ResetGameSettings();
                    gameSettings.HasChanges = true;
                }
                using (StreamReader fs = File.OpenText(DevConstants.FileIOConstants.GameSettings.CurrentSettings))
                {
                    JsonSerializer js = new JsonSerializer();
                    gameSettings = (GameSettings)js.Deserialize(fs, typeof(GameSettings));
                }
            }
            catch
            {
                FileIOSystem.ResetGameSettings();
                FileIOSystem.LoadGameSettings(ref gameSettings);
            }
        }

        public static void SaveGameSettings(ref GameSettings gameSettings)
        {
            Directory.CreateDirectory(DevConstants.FileIOConstants.GameSettings.SettingsDirectory);
            string jsonSettings = JsonConvert.SerializeObject(gameSettings);
            File.WriteAllText(DevConstants.FileIOConstants.GameSettings.CurrentSettings, jsonSettings);
            gameSettings.HasChanges = true;
        }

        public static void ResetGameSettings()
        {
            try
            {
                Directory.CreateDirectory(DevConstants.FileIOConstants.GameSettings.SettingsDirectory);
                if (!File.Exists(DevConstants.FileIOConstants.GameSettings.DefaultGameSettings))
                {
                    FileIOSystem.CreateDefaultSettingsFile();
                }
                File.Copy(DevConstants.FileIOConstants.GameSettings.DefaultGameSettings, DevConstants.FileIOConstants.GameSettings.CurrentSettings, true);
            }
            catch
            {
                FileIOSystem.CreateDefaultSettingsFile();
                FileIOSystem.ResetGameSettings();
            }
        }

        private static void CreateDefaultSettingsFile()
        {
            GameSettings defaultSettings = new GameSettings()
            {
                HasChanges = false,
                Resolution = new Vector2(1024, 768),
                Borderless = false,
                Vsync = false
            };
            string defaultSettingsJson = JsonConvert.SerializeObject(defaultSettings);
            Directory.CreateDirectory(DevConstants.FileIOConstants.GameSettings.SettingsDirectory);
            File.WriteAllText(DevConstants.FileIOConstants.GameSettings.DefaultGameSettings, defaultSettingsJson);
        }

        #endregion
    }
}
