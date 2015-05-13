using SFML.Window;

namespace Luftschiff.Code.Global.Utils
{
    /// <summary>
    ///     Convenience class to reduce code when listening to keyboard keys
    /// </summary>
    internal class KeyListener
    {
        /// <summary>
        ///     save the key to listen to
        /// </summary>
        private readonly Keyboard.Key _key;

        /// <summary>
        ///     Has the pressdown been registered?
        /// </summary>
        private bool _clickWasRegistered;

        /// <summary>
        ///     Constructor for creating a listener to the top row num keys, index meaning the number itself,
        ///     so 1 => 1 and 0 => 0
        /// </summary>
        /// <param name="numKeyIndex"></param>
        public KeyListener(int numKeyIndex)
        {
            _key = Util.TranslateIntegerToNumKey(numKeyIndex);
        }

        /// <summary>
        ///     True _once_ after the button has been pressed down, and is reset on key up
        /// </summary>
        public bool IsClicked
        {
            get
            {
                //check for keypress
                if (IsPressed)
                {
                    //key press was already consumed - return false
                    if (_clickWasRegistered)
                        return false;
                    //the key was not registered yet - now it is, because we gave back true
                    _clickWasRegistered = true;
                    return true;
                }
                //the key was not pressed, so there is no click that could have been consumed - reset the flag
                _clickWasRegistered = false;
                return false;
            }
        }

        /// <summary>
        ///     Whether (and only that)the key is currently pressed down
        /// </summary>
        public bool IsPressed
        {
            get { return Keyboard.IsKeyPressed(_key); }
        }
    }
}