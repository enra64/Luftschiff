using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Global;

namespace Luftschiff.Code.States {
    abstract class Dialog : ProtoGameState{
        public Vector2f Size, Position;
        public String Tag { get; set; }
        internal Game gReference;

        /// <summary>
        /// Places a Dialog where you want
        /// </summary>
        public Dialog(Vector2f _size, Vector2f _pos, String _tag) {
            Tag = _tag;
            Size = _size;
            Position = _pos;
        }

        /// <summary>
        /// Places a Dialog in the middle of half the screen
        /// </summary>
        /// <param name="_tag"></param>
        public Dialog(String _tag) {
            Tag = _tag;
            float xSize = Controller.Window.Size.X / 2;
            float ySize = Controller.Window.Size.Y / 2;
            Size = new Vector2f(xSize, ySize);
            Position = new Vector2f(xSize / 2, ySize / 2);
        }
    }
}
