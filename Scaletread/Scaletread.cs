using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scaletread.Engine;
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
            this._spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = false;
            this.Window.IsBorderless = false;
            this.Window.AllowUserResizing = false;
            this._currentState = new TitleState(Content);
            this._graphics.PreferredBackBufferWidth = 1440;
            this._graphics.PreferredBackBufferHeight = 900;
            this._graphics.ApplyChanges();
            this._camera = new Camera(GraphicsDevice.Viewport, GraphicsDevice.Viewport.Bounds.Center.ToVector2(), 0f, 1f);
            this._debugText = Content.Load<SpriteFont>(DevConstants.FontAssets.Debug);
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
                this._currentState = this._currentState.UpdateState(gameTime, this._camera, currentKey, this._prevKey, currentMouse, this._prevMouse);
                this._prevKey = currentKey;
                this._prevMouse = currentMouse;

                if(this._currentState == null)
                {
                    this.Exit();
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
    }
}
