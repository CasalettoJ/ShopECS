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
        private Camera _camera;
        private IState _currentState;

        public Scaletread()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this._camera = new Camera(GraphicsDevice.Viewport, GraphicsDevice.Viewport.Bounds.Center.ToVector2(), 0f, 1f);
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = false;
            this.Window.IsBorderless = false;
            this.Window.AllowUserResizing = false;
            this._currentState = new TitleState(Content);
            this._graphics.PreferredBackBufferWidth = 800;
            this._graphics.PreferredBackBufferHeight = 600;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive)
            {
                KeyboardState currentKey = Keyboard.GetState();
                this._currentState = this._currentState.UpdateState(gameTime, this._camera, currentKey, this._prevKey);
                this._prevKey = Keyboard.GetState();

                if(this._currentState == null)
                {
                    this.Exit();
                }

                base.Update(gameTime);
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw Entities
            this._spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: this._camera.GetMatrix());
            this._currentState.DrawContent(this._spriteBatch, this._camera);
            this._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
