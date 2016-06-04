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

        public void DrawContent(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_bg, Vector2.Zero);
            spriteBatch.Draw(_testPlayer, new Vector2((int)_testPosX, (int)_testPosY), Color.Red);
        }

        public void LoadLevel(ContentManager content)
        {
            _bg = content.Load<Texture2D>(DevConstants.ArtAssets.PlaceholderBG);
            _testPlayer = content.Load<Texture2D>(DevConstants.ArtAssets.Placeholder);
        }

        public ILevel Update(GameTime gameTime, KeyboardState currentKey, KeyboardState prevKey)
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
            return this;
        }
    }
}
