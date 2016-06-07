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
using Scaletread.Engine.Entities;
using Scaletread.Engine.Entities.Components;
using Scaletread.Engine.Systems;

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
        public Vector2 currentTile;
        private List<TestEntity> testSprites;
        private int money = 1000;

        private Texture2D _testPlayer;
        private Texture2D _tileSheet;
        private Texture2D _testHUD;
        private SpriteFont _testHUDFont;

        private List<Creature> _creatures;

        public void DrawContent(SpriteBatch spriteBatch, Camera camera)
        {
            // Draw Creatures
            this._creatures.ForEach(c => DisplaySystem.DisplayEntity(spriteBatch, camera, c.DisplayInfo, c.PositionInfo, _tileSheet));

            #region debug
            //Draw Mouse
            spriteBatch.Draw(_tileSheet, position: camera.ScreenToWorld(Mouse.GetState().Position), sourceRectangle: new Rectangle((int)(currentTile.X * DevConstants.Grid.CellSize), (int)(currentTile.Y * DevConstants.Grid.CellSize), DevConstants.Grid.CellSize, DevConstants.Grid.CellSize), origin: new Vector2(8, 8), color: Color.White * .5f);
            #endregion
        }

        public void LoadLevel(ContentManager content, Camera camera)
        {
            _testPlayer = content.Load<Texture2D>(DevConstants.ArtAssets.Placeholder);
            _tileSheet = content.Load<Texture2D>(DevConstants.ArtAssets.Spritesheet);
            _testHUD = content.Load<Texture2D>(DevConstants.ArtAssets.PlaceholderHUD);
            _testHUDFont = content.Load<SpriteFont>(DevConstants.FontAssets.MessageLarge);
            this._creatures = new List<Creature>();

            #region Debug Creation
            currentTile = new Vector2(0, 0);
            testSprites = new List<TestEntity>();
            this._creatures.Add(new Creature()
            {
                Id = Guid.NewGuid(),
                BaseVelocity = 300,
                Velocity = 300,
                Health = 100,
                MaxHealth = 100,
                Money = 1000,
                DisplayInfo = new Display()
                {
                    Color = Color.White,
                    Opacity = 1f,
                    Origin = Vector2.Zero,
                    Rotation = 0f,
                    Scale = 1f,
                    SpriteEffect = SpriteEffects.None,
                    SpriteSource = new Rectangle(23*DevConstants.Grid.CellSize, 42*DevConstants.Grid.CellSize, DevConstants.Grid.CellSize, DevConstants.Grid.CellSize),
                    Layer = DisplayLayer.FLOOR
                },
                PositionInfo = new Position()
                {
                    OriginPosition = new Vector2(20,20),
                    TileHeight = 1,
                    TileWidth = 1
                }
            });
            camera.TargetEntity = this._creatures[0].Id;
            #endregion
        }

        public ILevel Update(GameTime gameTime, Camera camera, KeyboardState currentKey, KeyboardState prevKey, MouseState currentMouse, MouseState prevMouse)
        {
            #region Debug
            Random random = new Random();
            if(currentMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                this.money -= 25;
                this._creatures.Add(new Creature()
                {
                    BaseVelocity = random.Next(),
                    Velocity = random.Next(0, 400),
                    Health = 100,
                    Id = Guid.NewGuid(),
                    MaxHealth = 100,
                    Money = 100,
                    MovementType = MovementType.INPUT,
                    DisplayInfo = new Display()
                    {
                        Color = Color.White,
                        Layer = DisplayLayer.FLOOR,
                        Opacity = 1f,
                        Origin = Vector2.Zero,
                        Scale = 1f,
                        Rotation = 0f,
                        SpriteEffect = SpriteEffects.None,
                        SpriteSource = new Rectangle((int)(currentTile.X * DevConstants.Grid.CellSize), (int)(currentTile.Y * DevConstants.Grid.CellSize), DevConstants.Grid.CellSize, DevConstants.Grid.CellSize)
                    },
                    PositionInfo = new Position()
                    {
                        OriginPosition = camera.ScreenToWorld(Mouse.GetState().Position),
                        TileHeight = 1,
                        TileWidth = 1
                    }
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
            #endregion

            // Level input
            if (currentKey.IsKeyDown(Keys.Escape))
            {
                return null;
            }

            // Camera Updates
            CameraSystem.ControlCamera(currentKey, prevKey, camera, gameTime);
            CameraSystem.PanCamera(camera, gameTime);

            // Entity Movement Updates
            this._creatures.ForEach(c =>
            {
                switch(c.MovementType)
                {
                    case MovementType.AI:
                        //AI Movement System Call
                        break;
                    case MovementType.INPUT:
                        MovementSystem.InputMovement(currentKey, prevKey, gameTime, c.PositionInfo, c.Velocity);
                        break;
                }
            });

            // Entity Information Updates

            // Set up for next frame
            CameraSystem.UpdateCameraTarget(this._creatures, camera);
            
            return this;
        }

        public void DrawUI(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(_testHUD, new Vector2(20, 20));
            spriteBatch.DrawString(_testHUDFont, this.money.ToString(), new Vector2(175, 30), Color.MonoGameOrange);
        }
    }
}
