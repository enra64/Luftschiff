using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game
{
    /// <summary>
    ///     Class wrapping notifications for easy use
    /// </summary>
    internal class Notifications
    {
        /// <summary>
        ///     Variable holding the singleton instance for the notifications
        /// </summary>
        private static Notifications _singletoNotifications;

        /// <summary>
        ///     List of the current Notifications
        /// </summary>
        private readonly List<Notification> _list = new List<Notification>();

        /// <summary>
        ///     Private constructor for singleton
        /// </summary>
        private Notifications()
        {
        }

        /// <summary>
        ///     Get an instance of this singleton
        /// </summary>
        public static Notifications Instance
        {
            get
            {
                if (_singletoNotifications == null)
                    _singletoNotifications = new Notifications();
                return _singletoNotifications;
            }
        }

        /// <summary>
        ///     Add a notification with the given text and position
        /// </summary>
        /// <param name="pos">Position to start at</param>
        /// <param name="text">String to dispaly</param>
        public void AddNotification(Vector2f pos, string text)
        {
            //use the fadespeed method with an invalid value to trigger disabling the fadespeed change
            AddNotification(pos, text, -1);
        }

        /// <summary>
        ///     Add a notification with the given text and position specifying a fadeout speed
        /// </summary>
        /// <param name="pos">Position to start at</param>
        /// <param name="text">String to dispaly</param>
        /// <param name="fadeSpeed">How fast to fade out, from 0 (fast) to 1 (slow)</param>
        // ReSharper disable once UnusedMember.Global
        public void AddNotification(Vector2f pos, string text, float fadeSpeed)
        {
            //create new notification
            var newNotification = new Notification(pos, text, fadeSpeed);
            /*
            //check for overlap
            foreach (Notification n in _list)
            {
                if (newNotification.GlobalRect.Intersects(n.GlobalRect))
                    //offset to avoid the other notification
                    newNotification = new Notification(new Vector2f(pos.X, pos.Y + newNotification.GlobalRect.Height), text, fadeSpeed);
            }
            */
            _list.Add(newNotification);
        }

        /// <summary>
        ///     Draw all notifications
        /// </summary>
        public void Draw()
        {
            foreach (var notification in _list)
                notification.Draw();
        }

        /// <summary>
        ///     Update all notifications
        /// </summary>
        public void Update()
        {
            foreach (var notification in _list)
                notification.Update();
            _list.RemoveAll(s => s.IsDone);
        }

        /// <summary>
        ///     Inner Helper class to simplify showing the notifications
        /// </summary>
        private class Notification
        {
            /// <summary>
            ///     How fast the Notification fades out. Between 0 and 1. Higher means slower.
            /// </summary>
            private readonly float _fadeOutFactor = .97f;

            /// <summary>
            ///     Text used to display the notification
            /// </summary>
            private readonly Text _text;

            /// <summary>
            ///     Current text alpha value
            /// </summary>
            private byte _alphaValue = 255;

            /// <summary>
            ///     The notification color
            /// </summary>
            private Color _color = Color.Yellow;

            /// <summary>
            ///     Construct a new notification
            /// </summary>
            /// <param name="pos">Starting posiiton</param>
            /// <param name="text">String to display</param>
            public Notification(Vector2f pos, string text)
            {
                _text = new Text(text, Globals.NotificationFont, 28)
                {
                    Position = pos
                };
            }

            /// <summary>
            ///     Construct a new notification
            /// </summary>
            /// <param name="pos">Starting position</param>
            /// <param name="text">String to display</param>
            /// <param name="fadeFactor">How fast to fade out, 0->1</param>
            public Notification(Vector2f pos, string text, float fadeFactor) : this(pos, text)
            {
                //avoid invalid values
                if (fadeFactor > 0 && fadeFactor <= 1)
                    _fadeOutFactor = fadeFactor;
            }

            /// <summary>
            ///     Global rectangle of the text
            /// </summary>
            public FloatRect GlobalRect
            {
                get { return _text.GetGlobalBounds(); }
            }

            /// <summary>
            ///     Whether the projectile is faded out or not in view anymore
            /// </summary>
            public bool IsDone
            {
                get { return _alphaValue <= 0 || _text.Position.Y + _text.GetGlobalBounds().Height < 0; }
            }

            /// <summary>
            ///     Just draw
            /// </summary>
            public void Draw()
            {
                Controller.Window.Draw(_text);
            }

            /// <summary>
            ///     Move up and decrease alpha
            /// </summary>
            public void Update()
            {
                //move up
                _text.Position -= new Vector2f(0, 1.8f);

                //change start color alpha value
                _color.A = _alphaValue;

                //save faded out color into Text
                _text.Color = _color;

                //slowly fade out
                _alphaValue = (byte) (_alphaValue*_fadeOutFactor);
            }
        }
    }
}