using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scaletread.Engine.Levels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.States.Interfaces
{
    public interface IState
    {
        IState UpdateState(GameTime gameTime, KeyboardState currentKey, KeyboardState prevKey);
        void DrawContent(SpriteBatch spriteBatch);
        void SetLevel(ILevel level);
    }
}
