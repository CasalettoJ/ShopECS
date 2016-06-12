using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread
{
    public static class DevConstants
    {
        public static class ArtAssets
        {
            public const string Spritesheet = "Sprites/dcss";
        }

        public static class Grid
        {
            public const int CellSize = 32;
            public const int WallWeight = 10000;
        }

        public static class FontAssets
        {
            public const string Message = "Fonts/Message";
            public const string MessageLarge = "Fonts/MessageLarge";
            public const string Debug = "Fonts/Debug";
        }

        public static class ComponentConstants
        {
            public const int DistanceUntilLabels = 75;
            public const int DistanceBeforeFarLabels = 150;
        }

        public static class FileIOConstants
        {
            public static class GameSettings
            {
                public const string SettingsDirectory = @"Settings/";
                public const string CurrentSettings = SettingsDirectory + "Config.json";
                public const string DefaultGameSettings = SettingsDirectory + "DefaultConfig.json";
            }
        }
    }
}
