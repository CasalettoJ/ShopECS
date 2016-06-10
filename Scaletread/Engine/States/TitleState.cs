using Scaletread.Engine.States.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scaletread.Engine.Levels.Interfaces;
using Microsoft.Xna.Framework.Content;
using Scaletread.Engine.Levels;

namespace Scaletread.Engine.States
{
    public class TitleState : IState
    {
        private IState _previousState;
        private Texture2D _title;
        private ContentManager _content;

        public TitleState(ContentManager content, IState previous = null)
        {
            _previousState = previous;
            _content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            _title = _content.Load<Texture2D>(DevConstants.ArtAssets.PlaceholderTitle);
        }

        public void DrawContent(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(_title, new Rectangle(0,0,camera.FullViewport.Width, camera.FullViewport.Height), Color.White);
        }

        public void SetLevel(ILevel level, Camera camera)
        {
            // Unimplemented for Title
        }

        public IState UpdateState(GameTime gameTime, Camera camera, KeyboardState currentKey, KeyboardState prevKey, MouseState currentMouse, MouseState prevMouse)
        {
            camera.ResetCamera();
            if(currentKey.IsKeyDown(Keys.Escape) && prevKey.IsKeyUp(Keys.Escape))
            {
                return null;
            }

            if(currentKey.IsKeyDown(Keys.Enter) && prevKey.IsKeyUp(Keys.Enter))
            {
                ILevel nextLevel = new TestLevel();
                return new PlayingState(this._content, camera, nextLevel, this);
            }

            return this;
        }

        public void DrawUI(SpriteBatch spriteBatch, Camera camera)
        {
            // Unimplemented for Title
        }
    }
}
