using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scaletread.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Systems
{
    public static class DisplaySystem
    {
        public static void DisplayEntity(SpriteBatch spriteBatch, Camera camera, Display displayInfo, Position positionInfo, Texture2D spriteSheet)
        {
            if(positionInfo != null && displayInfo != null)
            {
                Vector2 bottomRight = Vector2.Transform(new Vector2((positionInfo.OriginPosition.X) + positionInfo.Width, (positionInfo.OriginPosition.Y) + positionInfo.Height), camera.CurrentMatrix);
                Vector2 topLeft = Vector2.Transform(new Vector2(positionInfo.OriginPosition.X, positionInfo.OriginPosition.Y), camera.CurrentMatrix);
                Rectangle cameraBounds = new Rectangle((int)topLeft.X, (int)topLeft.Y, (int)bottomRight.X - (int)topLeft.X, (int)bottomRight.Y - (int)topLeft.Y);

                if (camera.IsInView(camera.CurrentMatrix, cameraBounds))
                {
                    spriteBatch.Draw(spriteSheet, positionInfo.OriginPosition, displayInfo.SpriteSource, displayInfo.Color * displayInfo.Opacity, displayInfo.Rotation, displayInfo.Origin, displayInfo.Scale, displayInfo.SpriteEffect, 0f);
                }
                else
                {
                    spriteBatch.Draw(spriteSheet, positionInfo.OriginPosition, displayInfo.SpriteSource, Color.Red * displayInfo.Opacity, displayInfo.Rotation, displayInfo.Origin, displayInfo.Scale, displayInfo.SpriteEffect, 0f);
                }
            }
        }

        public static void DisplayLabel()
        {
            
        }
    }
}
