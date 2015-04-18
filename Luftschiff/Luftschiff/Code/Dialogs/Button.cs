using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Dialogs
{
    class Button
    {
        private Text _buttonText;
        private RectangleShape _buttonShape;
        private FloatRect _buttonRect;
        private Color _normalColor, _hoverColor, _attentionColor;
        private bool _forceAttention = false;
        public String Tag = null;

        private void commonConstructor(String text, Vector2f position, Vector2f size, String tag)
        {
            _buttonText = new Text(text, Globals.DialogFont);
            _buttonText.Position = position;
            _buttonText.Color = Color.Black;

            _buttonRect = new FloatRect(position, size);

            _buttonShape = new RectangleShape(size);
            _buttonShape.FillColor = Globals.DIALOG_BUTTON_COLOR_NORMAL;
            _buttonShape.Position = position;
            _buttonShape.OutlineColor = Color.Black;
            _buttonShape.OutlineThickness = 2f;

            _normalColor = Globals.DIALOG_BUTTON_COLOR_NORMAL;
            _hoverColor = Globals.DIALOG_BUTTON_COLOR_HOVER;
            _attentionColor = Globals.DIALOG_BUTTON_COLOR_ATTENTIONSEEKER;
            Tag = tag;
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

        public void draw()
        {
            Controller.Window.Draw(_buttonShape);
            Controller.Window.Draw(_buttonText);
        }

        /// <summary>
        /// use to change color on attention needed
        /// </summary>
        public void ForceAttention(bool needsAttention)
        {
            _forceAttention = needsAttention;
        }

        /// <summary>
        /// Returns whether the button was clicked
        /// </summary>
        /// <returns>Whether the button was clicked</returns>
        public Boolean update(){
            //get position
            Vector2f currentMousePosition = MouseHandler.CurrentPosition;

            //change the color on hover
            _buttonShape.FillColor = _buttonRect.Contains(currentMousePosition.X, currentMousePosition.Y) 
                ? _hoverColor : _normalColor;
            //force attention to the button on special occasions
            if (_forceAttention)
                _buttonShape.FillColor = _attentionColor;
            
            //check if click is available and in the button area
            if (!MouseHandler.UnhandledClick || !_buttonRect.Contains(currentMousePosition.X, currentMousePosition.Y)) 
                return false;

            //click handled
            MouseHandler.UnhandledClick = false;
            return true;
        }
    }
}
