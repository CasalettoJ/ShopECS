﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Scaletread.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Systems
{
    public static class CameraSystem
    {
        public static void ControlCamera(KeyboardState currentKey, KeyboardState prevKey, Camera camera, GameTime gameTime)
        {
            if (currentKey.IsKeyDown(Keys.OemPlus) && prevKey.IsKeyUp(Keys.OemPlus))
            {
                camera.Scale += .25f;
            }
            if (currentKey.IsKeyDown(Keys.OemMinus) && prevKey.IsKeyUp(Keys.OemMinus))
            {
                camera.Scale -= .25f;
            }
            if (currentKey.IsKeyDown(Keys.Q))
            {
                camera.Rotation -= 5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.E))
            {
                camera.Rotation += 5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.R))
            {
                camera.Rotation = 0f;
            }
        }

        public static void PanCamera(Camera camera, GameTime gameTime)
        {
            if (Vector2.Distance(camera.Position, camera.TargetPosition) > 0)
            {
                float distance = Vector2.Distance(camera.Position, camera.TargetPosition);
                Vector2 direction = Vector2.Normalize(camera.TargetPosition - camera.Position);
                float velocity = distance * 2.5f;
                if (distance > 10f)
                {
                    camera.Position += direction * velocity * (camera.Scale >= 1 ? camera.Scale : 1) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public static void UpdateCameraTarget(Player player, List<Creature> creatures, Camera camera)
        {
            Creature targetCreature = creatures.Where(x => x.Id == camera.TargetEntity).FirstOrDefault();
            
            if(targetCreature != null)
            {
                SetCameraPosition(targetCreature.PositionInfo, camera);
            }
            else if (player.Id == camera.TargetEntity)
            {
                SetCameraPosition(player.PositionInfo, camera);
            }
        }

        private static void SetCameraPosition(Position positionInfo, Camera camera)
        {
            camera.TargetPosition = positionInfo.OriginPosition + determineCenter(positionInfo);
        }

        private static Vector2 determineCenter(Position positionInfo)
        {
            Rectangle objectSize = new Rectangle(0, 0, positionInfo.Width, positionInfo.Height);
            return objectSize.Center.ToVector2();
        }
    }
}
