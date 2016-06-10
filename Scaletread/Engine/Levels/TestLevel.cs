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
using Scaletread.Engine.Systems;

namespace Scaletread.Engine.Levels
{
    public class TestLevel : ILevel
    {
        private Texture2D _placeholderSquare;
        private Texture2D _placeholderHUD;
        private SpriteFont _placeholderHUDFont;

        private Player _player;
        private List<Creature> _creatures;

        public void DrawContent(SpriteBatch spriteBatch, Camera camera)
        {
            // Draw Player
            DisplaySystem.DisplayEntity(spriteBatch, camera, this._player.DisplayInfo, this._player.PositionInfo, _placeholderSquare);

            // Draw Creatures
            this._creatures.ForEach(c => DisplaySystem.DisplayEntity(spriteBatch, camera, c.DisplayInfo, c.PositionInfo, _placeholderSquare));

            #region debug
            #endregion
        }

        public void LoadLevel(ContentManager content, Camera camera)
        {
            _placeholderSquare = content.Load<Texture2D>(DevConstants.ArtAssets.Placeholder);
            _placeholderHUD = content.Load<Texture2D>(DevConstants.ArtAssets.PlaceholderHUD);
            _placeholderHUDFont = content.Load<SpriteFont>(DevConstants.FontAssets.MessageLarge);
            this._creatures = new List<Creature>();

            #region Debug Creation
            this._player = new Player()
            {
                Id = Guid.NewGuid(),
                WealthInfo = new Wealth()
                {
                    Money = 10000
                },
                HealthInfo = new Health()
                {
                    CurrentHealth = 100,
                    MaxHealth = 100
                },
                MovementInfo = new Movement()
                {
                    BaseVelocity = 300,
                    MovementType = MovementType.INPUT,
                    Velocity = 300
                },
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
                    Width = DevConstants.Grid.CellSize,
                    Height = DevConstants.Grid.CellSize
                }
            };
            camera.TargetEntity = this._player.Id;
            #endregion
        }

        public ILevel Update(GameTime gameTime, Camera camera, KeyboardState currentKey, KeyboardState prevKey, MouseState currentMouse, MouseState prevMouse)
        {
            #region Debug
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
            MovementSystem.InputMovement(currentKey, prevKey, gameTime, this._player.PositionInfo, this._player.MovementInfo);
            this._creatures.ForEach(c =>
            {
                switch(c.MovementInfo.MovementType)
                {
                    case MovementType.AI:
                        //AI Movement System Call
                        break;
                    case MovementType.INPUT:
                        MovementSystem.InputMovement(currentKey, prevKey, gameTime, c.PositionInfo, c.MovementInfo);
                        break;
                }
            });

            // Entity Information Updates

            // Set up for next frame
            CameraSystem.UpdateCameraTarget(this._player, this._creatures, camera);
            
            return this;
        }

        public void DrawUI(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(_placeholderHUD, new Vector2(20, 20));
            spriteBatch.DrawString(_placeholderHUDFont, this._player.WealthInfo.Money.ToString(), new Vector2(175, 30), Color.MonoGameOrange);
        }
    }
}
