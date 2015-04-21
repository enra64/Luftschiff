using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Luftschiff.Code
{
    internal static class Cursor
    {
        public enum Mode
        {
            Standard,
            Aim,
            Move
        }

        private static Sprite _sprite;
        private static Texture _standardTexture;
        private static Texture _aimTexture;
        private static Texture _moveTexture;

        public static void Update()
        {
            var mousePos = Mouse.GetPosition(Controller.Window);
            _sprite.Position = new Vector2f(mousePos.X, mousePos.Y);
        }

        public static void Initialize()
        {
            _standardTexture = Globals.CursorStandard;
            _aimTexture = Globals.CursorAim;
            _moveTexture = Globals.CursorCrewMove;

            _sprite = new Sprite(_standardTexture);
        }

        public static void CursorMode(Mode cursorType)
        {
            switch (cursorType)
            {
                case Mode.Standard:
                    _sprite.Texture = _standardTexture;
                    break;
                case Mode.Aim:
                    _sprite.Texture = _aimTexture;
                    break;
                case Mode.Move:
                    _sprite.Texture = _moveTexture;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("cursorType", cursorType, null);
            }
        }

        public static void Draw()
        {
            Controller.Window.Draw(_sprite);
        }
    }
}