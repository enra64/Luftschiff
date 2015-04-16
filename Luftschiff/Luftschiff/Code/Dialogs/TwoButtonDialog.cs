using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Dialogs {
    class TwoButtonDialog : Luftschiff.Code.States.Dialog
    {
        private String yes { get; set; }
        private String no { get; set; }
        private String message { get; set; }
        private String title { get; set; }

        private Text messageText, titleText;
        private Button yesButton, noButton;

        public bool ClickedYes { get; private set; }
        public bool ClickedNo { get; private set; }
        private bool userInteracted = false;

        public TwoButtonDialog(string _tag
            , String yes_, String no_, String message_, String title_)
            : base(_tag)
        {
            commonConstructor(yes_, no_, message_, title_);
        }

        public TwoButtonDialog(Vector2f _size, Vector2f _pos, string _tag
            , String yes_, String no_, String message_, String title_)
            : base(_size, _pos, _tag)
        {
            commonConstructor(yes_, no_, message_, title_);
        }

        /// <summary>
        /// create a standard yesnodialog
        /// </summary>
        private void commonConstructor(String yes_, String no_, String message_, String title_)
        {
            //init strings
            yes = yes_;
            no = no_;
            message = message_;
            title = title_;

            //init texts
            titleText = new Text(title, Globals.DialogFont);
            titleText.Position = new Vector2f(Position.X, Position.Y);
            titleText.Color = Color.Black;

            messageText = new Text(message, Globals.DialogFont);
            messageText.Position = new Vector2f(Position.X, Position.Y + 40);
            messageText.Color = Color.Black;
            
            //init buttons
            var leftButtonPosition = new Vector2f(Position.X,               Position.Y + Size.Y - Globals.TWO_BUTTON_DIALOG_BUTTON_HEIGHT);
            var rightButtonPosition = new Vector2f(Position.X + Size.X / 2, Position.Y + Size.Y - Globals.TWO_BUTTON_DIALOG_BUTTON_HEIGHT);

            var buttonSize = new Vector2f(Size.X / 2, Globals.TWO_BUTTON_DIALOG_BUTTON_HEIGHT);
            yesButton = new Button(yes, leftButtonPosition, buttonSize);
            noButton = new Button(no, rightButtonPosition, buttonSize);
        }


        public override void show()
        {
            while (!userInteracted)
            {
                Controller.Window.DispatchEvents();
                update();
                draw();
                Controller.Window.Display();
            }
        }

        public override void kill()
        {
            //rekt
            throw new NotImplementedException();
        }

        public override void draw()
        {
            Controller.Window.Draw(background);
            Controller.Window.Draw(messageText);
            Controller.Window.Draw(titleText);
            noButton.draw();
            yesButton.draw();
        }

        public Boolean getResultIsPositive()
        {
            if(!userInteracted)
                throw  new Exception("getResult: Diese Funktion erst nach Dialog.show aufrufen!");
            return ClickedYes;
        }

        /// <summary>
        /// this updates on the button state. 
        /// if a button has been clicked, the reference in the
        /// controller will be deleted, and the ClickedYes
        /// and ClickedNo will be update accordingly.
        /// </summary>
        public override void update()
        {
            userInteracted = false;
            if (yesButton.update())
            {
                ClickedYes = true;
                ClickedNo = false;
                userInteracted = true;
            }
            if (noButton.update())
            {
                ClickedNo = true;
                ClickedYes = false;
                userInteracted = true;
            }
            //kill the dialog when the user clicked a button
            if(userInteracted)
                Controller.killCurrentDialog();
        }
    }
}
