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

    public class TestEntity
    {
        public Vector2 position;
        public Vector2 spriteSheetPosition;
    }

    public class TestLevel : ILevel
    {
        public const int testsheetX = 26;
        public const int testsheetY = 17;
        List<TestEntity> testSprites;
        private Texture2D _testPlayer;
        private Texture2D _tileSheet;
        private float _testPosX = 20;
        private float _testPosY = 20;

        public Vector2 currentTile;

        public void DrawContent(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(_tileSheet, position: camera.ScreenToWorld(Mouse.GetState().Position), sourceRectangle: new Rectangle((int)(currentTile.X * DevConstants.Grid.CellSize), (int)(currentTile.Y * DevConstants.Grid.CellSize), DevConstants.Grid.CellSize, DevConstants.Grid.CellSize), origin: new Vector2(8, 8), color: Color.White * .5f);
            foreach(TestEntity entity in testSprites)
            {
                spriteBatch.Draw(_tileSheet, position: entity.position, sourceRectangle: new Rectangle((int)(entity.spriteSheetPosition.X * DevConstants.Grid.CellSize), (int)(entity.spriteSheetPosition.Y * DevConstants.Grid.CellSize), DevConstants.Grid.CellSize, DevConstants.Grid.CellSize), origin: new Vector2(8, 8), color: Color.White);
            }
            spriteBatch.Draw(_testPlayer, new Vector2((int)_testPosX, (int)_testPosY), Color.Red);
        }

        public void LoadLevel(ContentManager content)
        {
            _testPlayer = content.Load<Texture2D>(DevConstants.ArtAssets.Placeholder);
            _tileSheet = content.Load<Texture2D>(DevConstants.ArtAssets.Spritesheet);
            currentTile = new Vector2(0, 0);
            testSprites = new List<TestEntity>();
        }

        public ILevel Update(GameTime gameTime, Camera camera, KeyboardState currentKey, KeyboardState prevKey)
        {
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                testSprites.Add(new TestEntity()
                {
                    position = camera.ScreenToWorld(Mouse.GetState().Position),
                    spriteSheetPosition = currentTile
                });
            }
            if(currentKey.IsKeyDown(Keys.Left) && prevKey.IsKeyUp(Keys.Left))
            {
                int x = (int)currentTile.X - 1;
                int y = (int)currentTile.Y;
                if (x > testsheetX)
                {
                    x = 0;
                    y = y + 1;
                }
                if (x < 0)
                {
                    x = 0;
                    y = y - 1;
                }
                if (y < 0)
                {
                    y = testsheetY;
                    x = testsheetX;
                }
                if (y > testsheetY)
                {
                    x = 0;
                    y = 0;
                }
                currentTile = new Vector2(x, y);
            }
            if (currentKey.IsKeyDown(Keys.Right) && prevKey.IsKeyUp(Keys.Right))
            {
                int x = (int)currentTile.X + 1;
                int y = (int)currentTile.Y;
                if (x > testsheetX)
                {
                    x = 0;
                    y = y + 1;
                }
                if (x < 0)
                {
                    x = 0;
                    y = y - 1;
                }
                if (y < 0)
                {
                    y = testsheetY;
                    x = testsheetX;
                }
                if (y > testsheetY)
                {
                    x = 0;
                    y = 0;
                }
                currentTile = new Vector2(x, y);
            }
            

            if (currentKey.IsKeyDown(Keys.W))
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
