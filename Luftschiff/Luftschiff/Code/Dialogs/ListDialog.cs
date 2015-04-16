using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Dialogs {
    class ListDialog : Luftschiff.Code.States.Dialog
    {
        private Text _messageText, _titleText;
        private List<Button> _buttonList;
        private int _answer = -1;
        private bool _userInteracted;

        /// <summary>
        /// Minimum Constructor which will give you a list with zero-indexed buttons to click
        /// </summary>
        public ListDialog(List<String> answers, String message, String title)
            : base()
        {
            CommonConstructor(answers, message, title);
        }

        /// <summary>
        /// Constructor with more arguments which will also give you a list with zero-indexed buttons to click
        /// </summary>
        public ListDialog(Vector2f size, Vector2f pos, List<String> answers, String message, String title)
            : base(size, pos)
        {
            CommonConstructor(answers, message, title);
        }

        /// <summary>
        /// create a standard listDialog
        /// </summary>
        private void CommonConstructor(List<String> answers, String message_, String title_)
        {
            //init texts using a object initializer which resharper suggested for some reason
            _titleText = new Text(title_, Globals.DialogFont)
            {
                Position = new Vector2f(Position.X, Position.Y),
                Color = Color.Black
            };

            _messageText = new Text(message_, Globals.DialogFont)
            {
                Position = new Vector2f(Position.X, Position.Y + 40),
                Color = Color.Black
            };

            //TODO: intelligent word wrapping algorithm...
            //TODO: intelligent button size algorithm...

            _buttonList = new List<Button>();

            //init buttons
            var firstButtonPosition = new Vector2f(Position.X + Size.X / 4, Position.Y + 60);
            var buttonSize = new Vector2f(Size.X / 2, Globals.TWO_BUTTON_DIALOG_BUTTON_HEIGHT);
            int buttonPosition = 0;
            foreach(String s in answers)
            {
                _buttonList.Add(new Button(s, firstButtonPosition, buttonSize, Convert.ToString(buttonPosition)));
                firstButtonPosition.Y += buttonSize.Y;
                buttonPosition++;
            }
        }

        /// <summary>
        /// <para>When you call this, the dialog will be shown. </para>It blocks everything else,
        /// and returns the zero-indexed index of the string that was clicked
        /// </summary>
        public int show()
        {
            while (!_userInteracted)
            {
                Controller.Window.DispatchEvents();
                update();
                draw();
                Controller.Window.Display();
            }
            return _answer;
        }

        /// <summary>
        /// could be useful in the future. do not call this
        /// </summary>
        public override void kill()
        {
            //rekt
            throw new NotImplementedException();
        }

        /// <summary>
        /// You do not need to call this in anything but the dialog class
        /// </summary>
        public override void draw()
        {
            Controller.Window.Draw(background);
            Controller.Window.Draw(_messageText);
            Controller.Window.Draw(_titleText);
            foreach(Button b in _buttonList)
                b.draw();
        }

        /// <summary>
        /// this updates on the button state. 
        /// if a button has been clicked, the dialog will be closed and an appropriate value (true/false) will
        /// be returned
        /// </summary>
        public override void update()
        {
            _userInteracted = false;
            foreach (Button b in _buttonList)
            {
                if(b.update())
                {
                    _userInteracted = true;
                    _answer = Convert.ToInt32(b.Tag);
                }
            }
        }
    }
}
