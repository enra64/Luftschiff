using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Luftschiff.Code {
    static class Cursor
    {
        public enum Mode{
            standard, 
            aim,
            move
        }
        private static Sprite _Sprite;
        private static Texture _StandardTexture;
        private static Texture _AimTexture;
        private static Texture _MoveTexture;

        public static void Update()
        {
            var mousePos = Mouse.GetPosition(Controller.Window);
            _Sprite.Position = new Vector2f(mousePos.X, mousePos.Y);
        }

        public static void Initialize()
        {
            _StandardTexture = new Texture("Assets/Graphics/standardCursor.png");
            _AimTexture = new Texture("Assets/Graphics/aimCursor.png");
            _MoveTexture = new Texture("Assets/Graphics/moveCursor.png");

            _Sprite = new Sprite(_StandardTexture);
        }

        public static void setCursorMode(Mode cursorType)
        {
            switch (cursorType) {
                case Mode.standard:
                    _Sprite.Texture = _StandardTexture;
                    break;
                case Mode.aim:
                    _Sprite.Texture = _AimTexture;
                    break;
                case Mode.move:
                    _Sprite.Texture = _MoveTexture;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("cursorType", cursorType, null);
            }
        }

        public static void Draw()
        {
            Controller.Window.Draw(_Sprite);
        }
    }
}
