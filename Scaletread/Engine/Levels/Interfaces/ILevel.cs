﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scaletread.Engine.FileIO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaletread.Engine.Levels.Interfaces
{
    public interface ILevel
    {
        void LoadLevel(ContentManager content, Camera camera);
        ILevel Update(GameTime gameTime, Camera camera, ref GameSettings gameSettings, KeyboardState currentKey, KeyboardState prevKey, MouseState currentMouse, MouseState prevMouse);
        void DrawContent(SpriteBatch spriteBatch, Camera camera);
        void DrawUI(SpriteBatch spriteBatch, Camera camera);
    }
}
