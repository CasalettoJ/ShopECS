using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Levels.Interfaces
{
    public interface ILevel
    {
        void LoadLevel(ContentManager content);
        ILevel Update(GameTime gameTime, KeyboardState currentKey, KeyboardState prevKey);
        void DrawContent(SpriteBatch spriteBatch);
    }
}
