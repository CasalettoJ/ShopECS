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

        public void DrawContent(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_title, Vector2.Zero);
        }

        public void SetLevel(ILevel level)
        {
            // Unimplemented for Title
        }

        public IState UpdateState(GameTime gameTime, KeyboardState currentKey, KeyboardState prevKey)
        {
            if(currentKey.IsKeyDown(Keys.Escape) && prevKey.IsKeyUp(Keys.Escape))
            {
                return null;
            }

            if(currentKey.IsKeyDown(Keys.Enter) && prevKey.IsKeyUp(Keys.Enter))
            {
                ILevel nextLevel = new TestLevel();
                return new PlayingState(this._content, nextLevel, this);
            }

            return this;
        }
    }
}
