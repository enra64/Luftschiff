using System;
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
            _key = translateIntToNumKey(numKeyIndex);
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

        private Keyboard.Key translateIntToNumKey(int keyIndex)
        {
            switch (keyIndex)
            {
                case 0:
                    return Keyboard.Key.Num0;
                case 1:
                    return Keyboard.Key.Num1;
                case 2:
                    return Keyboard.Key.Num2;
                case 3:
                    return Keyboard.Key.Num3;
                case 4:
                    return Keyboard.Key.Num4;
                case 5:
                    return Keyboard.Key.Num5;
                case 6:
                    return Keyboard.Key.Num6;
                case 7:
                    return Keyboard.Key.Num7;
                case 8:
                    return Keyboard.Key.Num8;
                case 9:
                    return Keyboard.Key.Num9;
                default:
                    throw new IndexOutOfRangeException("Bad key index");
            }
        }
    }
}