using Luftschiff.Code.Global;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.States
{
    internal abstract class Dialog : ProtoGameState
    {
        public RectangleShape background;
        public Vector2f Size, Position;

        /// <summary>
        ///     Places a Dialog where you want
        /// </summary>
        public Dialog(Vector2f _size, Vector2f _pos)
        {
            usualInit(_size, _pos);
        }

        /// <summary>
        ///     Places a Dialog in the middle of half the screen
        /// </summary>
        /// <param name="_tag"></param>
        public Dialog()
        {
            float xSize = Controller.Window.Size.X/2;
            float ySize = Controller.Window.Size.Y/2;
            usualInit(new Vector2f(xSize, ySize), new Vector2f(xSize/2, ySize/2));
        }

        private void usualInit(Vector2f _size, Vector2f _pos)
        {
            Size = _size;
            Position = _pos;
            background = new RectangleShape(Size);
            background.FillColor = Color.White;
            background.Position = Position;
        }
    }
}