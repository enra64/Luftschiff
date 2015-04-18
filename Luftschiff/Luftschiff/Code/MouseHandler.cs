using SFML.Window;
using Luftschiff.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;
using SFML.System;

namespace Luftschiff {
    static class MouseHandler {
        public static bool Clicking { get; private set; }
        public static bool UnhandledClick { get; set; }
        public static bool Left { get; private set; }
        public static bool Right { get; private set; }
        public static float ScrollingDelta { get; private set; }
        public static Vector2f CurrentPosition { get; private set; }
        public static Vector2f LastClickPosition { get; private set; }
        public static Room SelectedRoom { get; set; }
        public static CrewMember SelectedCrew { get; set; }


        internal static void Click(object sender, MouseButtonEventArgs e) {
            Clicking = true;
            UnhandledClick = true;
            LastClickPosition = new Vector2f(e.X, e.Y);
            Console.WriteLine(LastClickPosition);

            if (e.Button == Mouse.Button.Left) {
                Left = true;
                Right = false;
            }
            if (e.Button == Mouse.Button.Right) {
                Left = false;
                Right = true;
            }
        }

        internal static void Scroll(object sender, MouseWheelEventArgs e) {
            ScrollingDelta = e.Delta;
        }

        internal static void Release(object sender, MouseButtonEventArgs e) {
            Clicking = false;
        }

        internal static void Move(object sender, MouseMoveEventArgs e) {
            CurrentPosition = new Vector2f(e.X, e.Y);
        }
    }
}
