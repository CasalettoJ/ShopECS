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
using Scaletread.Engine.FileIO.Objects;

namespace Scaletread.Engine.States
{
    public class TitleState : IState
    {
        private enum Options
        {
            NEW_GAME = 0,
            LOAD_GAME = 1,
            GAME_SETTINGS = 2,
            QUIT = 3
        }

        private class OptionItem
        {
            public string Text { get; set; }
            public bool Enabled { get; set; }
        }

        private IState _previousState;
        private ILevel _currentSubMenu;
        private ContentManager _content;
        private SpriteFont _titleFont;
        private SpriteFont _optionFont;
        private OptionItem[] _optionItems;
        private int _selectedOption;

        public TitleState(ContentManager content, IState previous = null)
        {
            _previousState = previous;
            _content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            _titleFont = _content.Load<SpriteFont>(DevConstants.FontAssets.MessageLarge);
            _optionFont = _content.Load<SpriteFont>(DevConstants.FontAssets.Message);
            _selectedOption = 0;
            _optionItems = new OptionItem[Enum.GetNames(typeof(Options)).Length];
            _optionItems[(int)Options.NEW_GAME] = new OptionItem() { Enabled = true, Text = "New Game" };
            _optionItems[(int)Options.LOAD_GAME] = new OptionItem() { Enabled = false, Text = "Load Game" };
            _optionItems[(int)Options.GAME_SETTINGS] = new OptionItem() { Enabled = true, Text = "Game Settings" };
            _optionItems[(int)Options.QUIT] = new OptionItem() { Enabled = true, Text = "Quit" };
        }

        public void DrawContent(SpriteBatch spriteBatch, Camera camera)
        {
            // Not Implemented for this state
        }

        public void SetLevel(ILevel level, Camera camera)
        {
            // Unimplemented for Title
        }

        public IState UpdateState(GameTime gameTime, Camera camera, ref GameSettings gameSettings, KeyboardState currentKey, KeyboardState prevKey, MouseState currentMouse, MouseState prevMouse)
        {
            if (this._currentSubMenu == null)
            {
                camera.ResetCamera();

                if (currentKey.IsKeyDown(Keys.Up) && prevKey.IsKeyUp(Keys.Up))
                {
                    _selectedOption -= 1;
                    this.HandleOptionChange(-1);
                }

                if (currentKey.IsKeyDown(Keys.Down) && prevKey.IsKeyUp(Keys.Down))
                {
                    _selectedOption += 1;
                    this.HandleOptionChange(1);
                }

                if (currentKey.IsKeyDown(Keys.Enter) && prevKey.IsKeyUp(Keys.Enter))
                {
                    switch (_selectedOption)
                    {
                        case (int)Options.NEW_GAME:
                            TestLevel level = new TestLevel();
                            return new PlayingState(_content, camera, level, this);
                        case (int)Options.LOAD_GAME:
                            break;
                        case (int)Options.GAME_SETTINGS:
                            this._currentSubMenu = new GameSettingsLevel(ref gameSettings);
                            this._currentSubMenu.LoadLevel(this._content, camera);
                            break;
                        case (int)Options.QUIT:
                            return null;
                    }
                }
            }
            else
            {
                ILevel previousSubMenu = _currentSubMenu;
                this._currentSubMenu = this._currentSubMenu.Update(gameTime, camera, ref gameSettings, currentKey, prevKey, currentMouse, prevMouse);
                if (this._currentSubMenu != previousSubMenu && this._currentSubMenu != null)
                {
                    this._currentSubMenu.LoadLevel(this._content, camera);
                }
            }

            return this;
        }

        public void DrawUI(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.DrawString(_titleFont, "Untitled Shop Simulator", new Vector2(20, 20), Color.MonoGameOrange);
            int optionNum = 0;

            foreach (OptionItem option in _optionItems)
            {
                string message = (_selectedOption == optionNum ? ">" : string.Empty) + option.Text;
                Vector2 size = _optionFont.MeasureString(message);
                Color optionColor = (_selectedOption == optionNum) ? Color.DarkViolet : Color.MonoGameOrange;
                spriteBatch.DrawString(_optionFont, message, new Vector2(20, 150 + (optionNum++ * size.Y)), option.Enabled ? optionColor : Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            
            if (this._currentSubMenu != null)
            {
                this._currentSubMenu.DrawUI(spriteBatch, camera);
            }
        }

        private void HandleOptionChange(int changeDirection)
        {
            if (_selectedOption < 0)
            {
                _selectedOption = Enum.GetNames(typeof(Options)).Length - 1;
            }

            if (_selectedOption > Enum.GetNames(typeof(Options)).Length - 1)
            {
                _selectedOption = 0;
            }
            while (!_optionItems[_selectedOption].Enabled)
            {
                _selectedOption += changeDirection;

                if (_selectedOption < 0)
                {
                    _selectedOption = Enum.GetNames(typeof(Options)).Length - 1;
                }

                if (_selectedOption > Enum.GetNames(typeof(Options)).Length - 1)
                {
                    _selectedOption = 0;
                }
            }
        }
    }
}
