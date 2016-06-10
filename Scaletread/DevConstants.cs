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
            public const string Placeholder = "Sprites/placeholder";
            public const string PlaceholderTitle = "Sprites/placeholdertitle";
            public const string Spritesheet = "Sprites/dcss";
            public const string PlaceholderHUD = "Sprites/HUD Money2";
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
    }
}
