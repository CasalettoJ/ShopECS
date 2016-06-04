using Scaletread.Engine.Levels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scaletread.Engine.Levels
{
    public class TestLevel : ILevel
    {
        private Texture2D _bg;
        private Texture2D _testPlayer;
        private float _testPosX = 20;
        private float _testPosY = 20;

        public void DrawContent(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(_bg, Vector2.Zero);
            spriteBatch.Draw(_testPlayer, new Vector2((int)_testPosX, (int)_testPosY), Color.Red);
        }

        public void LoadLevel(ContentManager content)
        {
            _bg = content.Load<Texture2D>(DevConstants.ArtAssets.PlaceholderBG);
            _testPlayer = content.Load<Texture2D>(DevConstants.ArtAssets.Placeholder);
        }

        public ILevel Update(GameTime gameTime, Camera camera, KeyboardState currentKey, KeyboardState prevKey)
        {
            if(currentKey.IsKeyDown(Keys.W))
            {
                this._testPosY -= 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.A))
            {
                this._testPosX -= 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.S))
            {
                this._testPosY += 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.D))
            {
                this._testPosX += 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentKey.IsKeyDown(Keys.Escape))
            {
                return null;
            }
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
            camera.TargetPosition = new Vector2(_testPosX+16, _testPosY+16);
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

            return this;
        }
    }
}
