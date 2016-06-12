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
using Scaletread.Engine.FileIO.Objects;
using Scaletread.Engine.FileIO;

namespace Scaletread.Engine.Levels
{
    public class GameSettingsLevel : ILevel
    {
        private enum Options
        {
            RESOLUTION = 0,
            BORDERLESS_WINDOW = 1,
            VSYNC = 2,
            ACCEPT_CHANGES = 3,
            DEFAULT_SETTINGS = 4,
            CANCEL = 5
        }

        private class OptionItem
        {
            public string Text { get; set; }
            public int Selection { get; set; }
            public List<object> OptionsCollection { get; set; }
        }

        private SpriteFont _pauseText;
        private SpriteFont _optionFont;
        private OptionItem[] _optionItems;
        private int _selectedOption;
        private GameSettings _gameSettings;

        public GameSettingsLevel(ref GameSettings gamesettings)
        {
            this._gameSettings = gamesettings;
        }

        public void DrawContent(SpriteBatch spriteBatch, Camera camera)
        {
            // Not Implemented for this level
        }

        public void DrawUI(SpriteBatch spriteBatch, Camera camera)
        {
            string message = "[GAME SETTINGS]";
            Vector2 size = this._pauseText.MeasureString(message);
            spriteBatch.DrawString(_pauseText, message, new Vector2(350, 150), Color.MonoGameOrange, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            int optionNum = 0;

            foreach (OptionItem option in _optionItems)
            {
                message = (_selectedOption == optionNum ? ">" : string.Empty) + option.Text;
                message = (_optionItems[optionNum].OptionsCollection != null) ? message + _optionItems[optionNum].OptionsCollection[option.Selection].ToString() : message;
                size = _optionFont.MeasureString(message);
                Color optionColor = (_selectedOption == optionNum) ? Color.DarkViolet : Color.MonoGameOrange;
                spriteBatch.DrawString(_optionFont, message, new Vector2(350, 250 + (optionNum++ * size.Y)), optionColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }

        public void LoadLevel(ContentManager content, Camera camera)
        {
            this._pauseText = content.Load<SpriteFont>(DevConstants.FontAssets.MessageLarge);
            this._optionFont = content.Load<SpriteFont>(DevConstants.FontAssets.Message);

            _optionItems = new OptionItem[Enum.GetNames(typeof(Options)).Length];
            // Resolution Option
            _optionItems[(int)Options.RESOLUTION] = new OptionItem() { OptionsCollection = new List<object>(), Selection = 0, Text = "Resolution: " };
            // Set to current resolution after populating resolutions
            foreach (DisplayMode display in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                Vector2 resolution = new Vector2(display.Width, display.Height);
                if (!_optionItems[(int)Options.RESOLUTION].OptionsCollection.Contains(resolution) && resolution.X > 900)
                {
                    _optionItems[(int)Options.RESOLUTION].OptionsCollection.Add(resolution);
                }
            }
            _optionItems[(int)Options.RESOLUTION].Selection =
                _optionItems[(int)Options.RESOLUTION].OptionsCollection.FindIndex(x => (Vector2)x == this._gameSettings.Resolution) > 0 ?
                _optionItems[(int)Options.RESOLUTION].OptionsCollection.FindIndex(x => (Vector2)x == this._gameSettings.Resolution) : 0;

            //Borderless Option
            _optionItems[(int)Options.BORDERLESS_WINDOW] = new OptionItem() { Text = "Borderless Window: ", OptionsCollection = new List<object>(), Selection = 0 };
            //Set to current borderless option
            _optionItems[(int)Options.BORDERLESS_WINDOW].OptionsCollection.Add(true);
            _optionItems[(int)Options.BORDERLESS_WINDOW].OptionsCollection.Add(false);
            _optionItems[(int)Options.BORDERLESS_WINDOW].Selection = _optionItems[(int)Options.BORDERLESS_WINDOW].OptionsCollection.FindIndex(x => (bool)x == this._gameSettings.Borderless);

            //Vsync Option
            _optionItems[(int)Options.VSYNC] = new OptionItem() { Text = "VSync: ", OptionsCollection = new List<object>(), Selection = 0 };
            //Set to current vsync option
            _optionItems[(int)Options.VSYNC].OptionsCollection.Add(true);
            _optionItems[(int)Options.VSYNC].OptionsCollection.Add(false);
            _optionItems[(int)Options.VSYNC].Selection = _optionItems[(int)Options.VSYNC].OptionsCollection.FindIndex(x => (bool)x == this._gameSettings.Vsync);

            //Save Changes
            _optionItems[(int)Options.ACCEPT_CHANGES] = new OptionItem() { Text = "[SAVE CHANGES]", OptionsCollection = null, Selection = 0 };

            //Restore default
            _optionItems[(int)Options.DEFAULT_SETTINGS] = new OptionItem() { Text = "[RESTORE DEFAULTS]", OptionsCollection = null, Selection = 0 };

            //Cancel
            _optionItems[(int)Options.CANCEL] = new OptionItem() { Text = "[CANCEL]", OptionsCollection = null, Selection = 0 };
        }

        public ILevel Update(GameTime gameTime, Camera camera, ref GameSettings gameSettings, KeyboardState currentKey, KeyboardState prevKey, MouseState currentMouse, MouseState prevMouse)
        {
            if (currentKey.IsKeyDown(Keys.Escape) && prevKey.IsKeyUp(Keys.Escape))
            {
                return null;
            }

            if (currentKey.IsKeyDown(Keys.Up) && prevKey.IsKeyUp(Keys.Up))
            {
                _selectedOption -= 1;
                if (_selectedOption < 0)
                {
                    _selectedOption = Enum.GetNames(typeof(Options)).Length - 2;
                }
                if (_selectedOption > Enum.GetNames(typeof(Options)).Length - 1)
                {
                    _selectedOption = 0;
                }
            }
            
            if (currentKey.IsKeyDown(Keys.Down) && prevKey.IsKeyUp(Keys.Down))
            {
                _selectedOption += 1;
                if (_selectedOption < 0)
                {
                    _selectedOption = Enum.GetNames(typeof(Options)).Length - 2;
                }
                if (_selectedOption > Enum.GetNames(typeof(Options)).Length - 1)
                {
                    _selectedOption = 0;
                }
            }


            if (currentKey.IsKeyDown(Keys.Left) && prevKey.IsKeyUp(Keys.Left) && _optionItems[_selectedOption].OptionsCollection != null)
            {
                _optionItems[_selectedOption].Selection -= 1;
                if (_optionItems[_selectedOption].Selection < 0)
                {
                    _optionItems[_selectedOption].Selection = _optionItems[_selectedOption].OptionsCollection.Count - 1;
                }
                if (_optionItems[_selectedOption].Selection >= _optionItems[_selectedOption].OptionsCollection.Count)
                {
                    _optionItems[_selectedOption].Selection = 0;
                }
            }

            if (currentKey.IsKeyDown(Keys.Right) && prevKey.IsKeyUp(Keys.Right) && _optionItems[_selectedOption].OptionsCollection != null)
            {
                _optionItems[_selectedOption].Selection += 1;
                if (_optionItems[_selectedOption].Selection < 0)
                {
                    _optionItems[_selectedOption].Selection = _optionItems[_selectedOption].OptionsCollection.Count - 1;
                }
                if (_optionItems[_selectedOption].Selection >= _optionItems[_selectedOption].OptionsCollection.Count)
                {
                    _optionItems[_selectedOption].Selection = 0;
                }
            }

            if (currentKey.IsKeyDown(Keys.Enter) && prevKey.IsKeyUp(Keys.Enter))
            {
                switch (_selectedOption)
                {
                    case (int)Options.ACCEPT_CHANGES:
                        this._gameSettings.Resolution = (Vector2)_optionItems[(int)Options.RESOLUTION].OptionsCollection[_optionItems[(int)Options.RESOLUTION].Selection];
                        this._gameSettings.Borderless = (bool)_optionItems[(int)Options.BORDERLESS_WINDOW].OptionsCollection[_optionItems[(int)Options.BORDERLESS_WINDOW].Selection];
                        this._gameSettings.Vsync = (bool)_optionItems[(int)Options.VSYNC].OptionsCollection[_optionItems[(int)Options.VSYNC].Selection];
                        FileIOSystem.SaveGameSettings(ref this._gameSettings);
                        return null;
                    case (int)Options.DEFAULT_SETTINGS:
                        FileIOSystem.ResetGameSettings();
                        FileIOSystem.LoadGameSettings(ref gameSettings);
                        return new GameSettingsLevel(ref gameSettings);
                    case (int)Options.CANCEL:
                        return null;
                }
            }

            return this;
        }
    }
}
