using System;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Dialogs
{
    class Button
    {
        private Text buttonText;
        private RectangleShape buttonShape;
        private FloatRect buttonRect;
        private Color normalColor, hoverColor;

        public Button(String text_, Vector2f position, Vector2f size, Color _normalColor, Color _hoverColor)
        {
            buttonText = new Text(text_, Globals.DialogFont);
            buttonText.Position = position;

            buttonRect = new FloatRect(position, size);

            buttonShape = new RectangleShape(size);
            buttonShape.FillColor = _normalColor;
            buttonShape.Position = position;

            normalColor = _normalColor;
            hoverColor = _hoverColor;
        }

        public Button(String text_, Vector2f position, Vector2f size)
        {
            buttonText = new Text(text_, Globals.DialogFont);
            buttonText.Position = position;
            buttonText.Color = Color.Black;

            buttonRect = new FloatRect(position, size);

            buttonShape = new RectangleShape(size);
            buttonShape.FillColor = Globals.DIALOG_BUTTON_COLOR_NORMAL;
            buttonShape.Position = position;

            normalColor = Globals.DIALOG_BUTTON_COLOR_NORMAL;
            hoverColor = Globals.DIALOG_BUTTON_COLOR_HOVER;
        }

        public void draw()
        {
            Controller.Window.Draw(buttonShape);
            Controller.Window.Draw(buttonText);
        }

        /// <summary>
        /// </summary>
        /// <returns>Whether the button was clicked</returns>
        public Boolean update()
        {
            Vector2f currentMousePosition = MouseHandler.CurrentPosition;
            buttonShape.FillColor = buttonRect.Contains(currentMousePosition.X, currentMousePosition.Y) 
                ? hoverColor : normalColor;
            if (!MouseHandler.UnhandledClick || !buttonRect.Contains(currentMousePosition.X, currentMousePosition.Y)) 
                return false;
            MouseHandler.UnhandledClick = false;
            return true;
        }
    }
}
