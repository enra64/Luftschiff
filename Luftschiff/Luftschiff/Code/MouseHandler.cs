using Luftschiff.Code;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;
using SFML.System;
using SFML.Window;

namespace Luftschiff
{
    internal static class MouseHandler
    {
        private static Room _selectedRoom;
        private static CrewMember _selectedCrew;
        public static bool Clicking { get; private set; }
        public static bool UnhandledClick { get; set; }
        public static bool Left { get; private set; }
        public static bool Right { get; private set; }
        public static float ScrollingDelta { get; private set; }
        public static Vector2f CurrentPosition { get; private set; }
        public static Vector2f LastClickPosition { get; private set; }

        public static Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                if (value == null)
                    Cursor.CursorMode(Cursor.Mode.Standard);
                else if (value.IsAbleToTarget)
                {
                    value.StartSelectionIndicator();
                    Cursor.CursorMode(Cursor.Mode.Aim);
                }
                _selectedRoom = value;
            }
        }

        public static CrewMember SelectedCrew
        {
            get { return _selectedCrew; }
            set
            {
                if (value == null)
                    Cursor.CursorMode(Cursor.Mode.Standard);
                else
                {
                    Cursor.CursorMode(Cursor.Mode.Move);
                    value.StartSelectionIndicator();
                }
                _selectedCrew = value;
            }
        }

        internal static void Click(object sender, MouseButtonEventArgs e)
        {
            Clicking = true;
            UnhandledClick = true;
            LastClickPosition = new Vector2f(e.X, e.Y);
            //Console.WriteLine(LastClickPosition);

            if (e.Button == Mouse.Button.Left)
            {
                Left = true;
                Right = false;
            }
            if (e.Button == Mouse.Button.Right)
            {
                Left = false;
                Right = true;
            }
        }

        internal static void Scroll(object sender, MouseWheelEventArgs e)
        {
            ScrollingDelta = e.Delta;
        }

        internal static void Release(object sender, MouseButtonEventArgs e)
        {
            Clicking = false;
        }

        internal static void Move(object sender, MouseMoveEventArgs e)
        {
            CurrentPosition = new Vector2f(e.X, e.Y);
        }
    }
}