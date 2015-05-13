using System;
using Luftschiff.Code.States;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Luftschiff.Code.Dialogs
{
    internal class TwoButtonDialog : Dialog
    {
        private Text _messageText, _titleText;
        private Button _yesButton, _noButton;
        private bool _yesClicked, _userInteracted;

        /// <summary>
        ///     Constructor that lets you configure all dialog strings
        /// </summary>
        public TwoButtonDialog(string yes, string no, string message, string title)
        {
            CommonConstructor(yes, no, message, title);
        }

        /// <summary>
        ///     Minimal Dialog Constructor, using "Ja" and "Nein" for the buttons.
        /// </summary>
        public TwoButtonDialog(string message, string title)
        {
            CommonConstructor("Ja", "Nein", message, title);
        }

        /// <summary>
        ///     maximum amount of config possible with this one
        /// </summary>
        public TwoButtonDialog(Vector2f size, Vector2f pos, string yes, string no, string message, string title)
            : base(size, pos)
        {
            CommonConstructor(yes, no, message, title);
        }

        /// <summary>
        ///     create a standard yesnodialog
        /// </summary>
        private void CommonConstructor(string yes, string no, string message, string title)
        {
            //init texts using a object initializer which resharper suggested for some reason
            _titleText = new Text(title, Globals.DialogFont)
            {
                Position = new Vector2f(Position.X, Position.Y),
                Color = Color.Black
            };

            _messageText = new Text(message, Globals.DialogFont)
            {
                Position = new Vector2f(Position.X, Position.Y + 40),
                Color = Color.Black
            };

            //TODO: intelligent word wrapping algorithm...

            //init buttons
            var leftButtonPosition = new Vector2f(Position.X,
                Position.Y + Size.Y - Globals.TWO_BUTTON_DIALOG_BUTTON_HEIGHT);
            var rightButtonPosition = new Vector2f(Position.X + Size.X/2,
                Position.Y + Size.Y - Globals.TWO_BUTTON_DIALOG_BUTTON_HEIGHT);

            var buttonSize = new Vector2f(Size.X/2, Globals.TWO_BUTTON_DIALOG_BUTTON_HEIGHT);
            _yesButton = new Button(yes, leftButtonPosition, buttonSize);
            _yesButton.ActivationKey = Keyboard.Key.Num1;

            _noButton = new Button(no, rightButtonPosition, buttonSize);
            _noButton.ActivationKey = Keyboard.Key.Num2;
        }

        /// <summary>
        ///     <para>When you call this, the dialog will be shown. </para>
        ///     It blocks everything else,
        ///     and returns true if the user clicked on the 'yes' button and false otherwise.
        /// </summary>
        public bool show()
        {
            while (!_userInteracted)
            {
                Cursor.Update();
                Controller.Window.DispatchEvents();
                update();
                draw();
                Cursor.Draw();
                Controller.Window.Display();
                Controller.Window.Clear(Color.Black);
            }
            return _yesClicked;
        }

        /// <summary>
        ///     could be useful in the future. do not call this
        /// </summary>
        public override void kill()
        {
            //rekt
            throw new NotImplementedException();
        }

        /// <summary>
        ///     You do not need to call this in anything but the dialog class
        /// </summary>
        public override void draw()
        {
            Controller.Window.Draw(background);
            Controller.Window.Draw(_messageText);
            Controller.Window.Draw(_titleText);
            _noButton.Draw();
            _yesButton.Draw();
        }

        /// <summary>
        ///     this updates on the button state.
        ///     if a button has been clicked, the dialog will be closed and an appropriate value (true/false) will
        ///     be returned
        /// </summary>
        public override void update()
        {
            _userInteracted = false;
            if (_yesButton.Update())
            {
                _yesClicked = true;
                _userInteracted = true;
            }
            if (_noButton.Update())
            {
                _yesClicked = false;
                _userInteracted = true;
            }
        }
    }
}