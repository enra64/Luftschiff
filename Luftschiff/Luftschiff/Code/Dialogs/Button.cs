using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Luftschiff.Code.Dialogs
{
    class Button
    {
        private Text _buttonText;
        private RectangleShape _buttonShape;
        private FloatRect _buttonRect;
        private Color _normalColor, _hoverColor, _attentionColor;
        public String Tag = null;
        private bool _activationButtonConsumed;
        public bool Enable = true;

        //ERRORSOURCE: uses F15 as null key
        /// <summary>
        /// Key used to activate this button
        /// </summary>
        public Keyboard.Key ActivationKey { get; set; }
        
        /// <summary>
        /// set whether a click sound is played when the user clicks that button
        /// </summary>
        public bool ClickSound { get; set; }

        /// <summary>
        /// use to change color on attention needed
        /// </summary>
        public bool ForceAttention { get; set; }

        private void commonConstructor(String text, Vector2f position, Vector2f size, String tag)
        {
            //init activation key as f15 because no one will use that
            ActivationKey = Keyboard.Key.F15;

            _buttonText = new Text(text, Globals.DialogFont)
            {
                Position = position,
                Color = Color.Black
            };

            _buttonRect = new FloatRect(position, size);

            _buttonShape = new RectangleShape(size)
            {
                FillColor = Globals.DIALOG_BUTTON_COLOR_NORMAL,
                Position = position,
                OutlineColor = Color.Black,
                OutlineThickness = 2f
            };

            _normalColor = Globals.DIALOG_BUTTON_COLOR_NORMAL;
            _hoverColor = Globals.DIALOG_BUTTON_COLOR_HOVER;
            _attentionColor = Globals.DIALOG_BUTTON_COLOR_ATTENTIONSEEKER;
            
            Tag = tag;
            ClickSound = true;
        }

        public Button(String text, Vector2f position, Vector2f size) {
            commonConstructor(text, position, size, null);
        }

        public Button(String text, Vector2f position, Vector2f size, String tag) {
            commonConstructor(text, position, size, tag);
        }

        /// <summary>
        /// Call to change this buttons colors. null values are ignored
        /// </summary>
        public void setColors(Color normalColor, Color hoverColor)
        {
            if (normalColor != null)
                _normalColor = normalColor;
            if (hoverColor != null)
                _hoverColor = hoverColor;
        }

        public void Draw()
        {
            Controller.Window.Draw(_buttonShape);
            Controller.Window.Draw(_buttonText);
        }

        /// <summary>
        /// Returns whether the button was clicked or its key pressed
        /// </summary>
        /// <returns>Whether the button was activated</returns>
        public Boolean Update()
        {
            //check whether a button has been set
            if (ActivationKey != Keyboard.Key.F15 && Enable)
            {
                //check whether the correct button is pressed and not yet consumed
                if (Keyboard.IsKeyPressed(ActivationKey) && !_activationButtonConsumed)
                {
                    _activationButtonConsumed = true;
                    return true;
                }
                //deconsume the buttonpress
                if(!Keyboard.IsKeyPressed(ActivationKey))
                    _activationButtonConsumed = false;
            }
            //get position
            Vector2f currentMousePosition = MouseHandler.CurrentPosition;

            //change the color on hover
            _buttonShape.FillColor = _buttonRect.Contains(currentMousePosition.X, currentMousePosition.Y) 
                ? _hoverColor : _normalColor;

            //force attention to the button on special occasions
            if (ForceAttention)
                _buttonShape.FillColor = _attentionColor;

            //make text gray when disabled
            _buttonText.Color = Enable ? Color.Black : new Color(200, 200, 200);
            
            //check if click is available and in the button area and whether the button is enabled at all
            if (!Enable || !MouseHandler.UnhandledClick || !_buttonRect.Contains(currentMousePosition.X, currentMousePosition.Y)) 
                return false;

            //play click sound
            if (ClickSound)
            {
                Sound clickSound = new Sound(Globals.ClickSound);
                clickSound.Volume = 80;
                clickSound.Play();    
            }

            //click handled
            MouseHandler.UnhandledClick = false;
            return true;
        }
    }
}
