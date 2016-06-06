using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Scaletread.Engine.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Systems
{
    public static class MovementSystem
    {
        public static void InputMovement(KeyboardState currentKey, KeyboardState prevKey, GameTime gameTime, Position positionInfo, int velocity)
        {
            Vector2 newPosition = positionInfo.OriginPosition;

            if (currentKey.IsKeyDown(Keys.W))
            {
                newPosition.Y -= velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.A))
            {
                newPosition.X -= velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.S))
            {
                newPosition.Y += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.D))
            {
                newPosition.X += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            positionInfo.OriginPosition = newPosition;
        }
    }
}
