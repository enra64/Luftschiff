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
        ///     List of the current Notifications
        /// </summary>
        private readonly List<Notification> _list = new List<Notification>();

        /// <summary>
        ///     Add a notification with the given text and position
        /// </summary>
        /// <param name="pos">Position to start at</param>
        /// <param name="text">String to dispaly</param>
        // ReSharper disable once UnusedMember.Global
        public void AddNotification(Vector2f pos, string text)
        {
            _list.Add(new Notification(pos, text));
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
            _list.Add(new Notification(pos, text, fadeSpeed));
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
            ///     How fast the Notification fades out. Between 0 and 1.
            /// </summary>
            private readonly float _fadeOutFactor = .95f;

            /// <summary>
            ///     Text used to display the notification
            /// </summary>
            private readonly Text _text;

            /// <summary>
            ///     Current text alpha value
            /// </summary>
            private byte _alphaValue = 255;

            /// <summary>
            ///     Construct a new notification
            /// </summary>
            /// <param name="pos">Starting posiiton</param>
            /// <param name="text">String to display</param>
            public Notification(Vector2f pos, string text)
            {
                _text = new Text(text, Globals.NotificationFont, 40)
                {
                    Color = Color.Yellow,
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
                _fadeOutFactor = fadeFactor;
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
                _text.Position -= new Vector2f(0, 10f);

                //save faded out color
                _text.Color = new Color(255, 255, 255, _alphaValue);

                //slowly fade out
                _alphaValue = (byte)(_alphaValue * _fadeOutFactor);
            }
        }
    }
}