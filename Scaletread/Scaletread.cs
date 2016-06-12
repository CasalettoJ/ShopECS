using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scaletread.Engine;
using Scaletread.Engine.FileIO;
using Scaletread.Engine.FileIO.Objects;
using Scaletread.Engine.States;
using Scaletread.Engine.States.Interfaces;
using System;

namespace Scaletread
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Scaletread : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState _prevKey;
        private MouseState _prevMouse;
        private Camera _camera;
        private IState _currentState;
        private SpriteFont _debugText;
        private GameSettings _gameSettings;

        public Scaletread()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            FileIOSystem.LoadGameSettings(ref this._gameSettings);
            this.IsMouseVisible = false;
            this.Window.AllowUserResizing = false;
            this.Window.IsBorderless = this._gameSettings.Borderless;
            this._graphics.PreferredBackBufferWidth = (int)this._gameSettings.Resolution.X;
            this._graphics.PreferredBackBufferHeight = (int)this._gameSettings.Resolution.Y;
            this._graphics.SynchronizeWithVerticalRetrace = this._gameSettings.Vsync;
            this.IsFixedTimeStep = this._gameSettings.Vsync;
            this._graphics.ApplyChanges();


            this._camera = new Camera(GraphicsDevice.Viewport, GraphicsDevice.Viewport.Bounds.Center.ToVector2(), 0f, 1f);
            this._spriteBatch = new SpriteBatch(GraphicsDevice);
            this._debugText = Content.Load<SpriteFont>(DevConstants.FontAssets.Debug);
            this._currentState = new TitleState(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive)
            {
                this._camera.CurrentMatrix = this._camera.GetMatrix();
                this._camera.CurrentInverseMatrix = this._camera.GetInverseMatrix();
                KeyboardState currentKey = Keyboard.GetState();
                MouseState currentMouse = Mouse.GetState();
                this._currentState = this._currentState.UpdateState(gameTime, this._camera, ref this._gameSettings, currentKey, this._prevKey, currentMouse, this._prevMouse);
                this._prevKey = currentKey;
                this._prevMouse = currentMouse;

                if(this._currentState == null)
                {
                    this.Exit();
                }

                if (this._gameSettings.HasChanges)
                {
                    ResetGameSettings();
                }

                base.Update(gameTime);
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.NavajoWhite);

            // Draw Entities
            this._spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: this._camera.CurrentMatrix);
            this._currentState.DrawContent(this._spriteBatch, this._camera);
            this._spriteBatch.End();

            // Draw UI
            this._spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            this._currentState.DrawUI(this._spriteBatch, this._camera);
            this._spriteBatch.End();

            // Draw Debug
            this._spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            this._spriteBatch.DrawString(this._debugText, "FPS: " + Math.Round((1 /(decimal)gameTime.ElapsedGameTime.TotalSeconds), 2).ToString(), new Vector2(25,25), Color.DarkBlue);
            this._spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResetGameSettings()
        {

            this.Window.IsBorderless = this._gameSettings.Borderless;
            this._graphics.PreferredBackBufferWidth = (int)this._gameSettings.Resolution.X;
            this._graphics.PreferredBackBufferHeight = (int)this._gameSettings.Resolution.Y;
            this._graphics.SynchronizeWithVerticalRetrace = this._gameSettings.Vsync;
            this.IsFixedTimeStep = this._gameSettings.Vsync;
            this._graphics.ApplyChanges();
            this._gameSettings.HasChanges = false;
            this.Window.ClientBounds.Offset(new Point((int)this._graphics.GraphicsDevice.DisplayMode.Width / 2 - (int)this._gameSettings.Resolution.X / 2, (int)this._graphics.GraphicsDevice.DisplayMode.Height / 2 - (int)this._gameSettings.Resolution.Y / 2));
            this._camera.FullViewport = GraphicsDevice.Viewport;
            this._camera.Bounds = GraphicsDevice.Viewport.Bounds;
        }
    }
}
