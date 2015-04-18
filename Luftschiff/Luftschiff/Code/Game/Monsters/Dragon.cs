
using System;
using System.Collections.Specialized;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters {
    class Dragon : Monster
    {
        private Animation _flying;
        public override int makeTurnDamage()
        {
            throw new System.NotImplementedException();
        }

        public override void getTurnDamage(int type, bool hits)
        {
            if(hits)
                Life -= 10;
            Console.WriteLine("the dragon has been shot at. it does not give a shit.");
            //throw new System.NotImplementedException("The monster overrides the getdamage, but has no idea what to do!");
        }

        public override void update()
        {
            if (MouseHandler.UnhandledClick)
            {
                if (MouseHandler.SelectedRoom != null)
                {
                    if (getRect().Contains(MouseHandler.LastClickPosition.X, MouseHandler.LastClickPosition.Y))
                    {
                        MouseHandler.UnhandledClick = false;
                        Globals.TurnHandler.addRoomTarget(MouseHandler.SelectedRoom, this);
                        Cursor.CursorMode(Cursor.Mode.standard);
                    }
                }
            }
            Sprite.Update(Globals.FRAME_TIME);
            Sprite.Play(_flying);
        }

        public Dragon(Texture t)
        {
            _flying = new Animation(new Texture("Assets/Graphics/dragon2.png"));
            _flying.AddFrame(new IntRect(0, 0, 300, 300));
            _flying.AddFrame(new IntRect(270, 0, 300, 300));
            _flying.AddFrame(new IntRect(550, 0, 300, 300));
            _flying.AddFrame(new IntRect(850, 0, 300, 300));
            _flying.AddFrame(new IntRect(1140, 0, 300, 300));
            _flying.AddFrame(new IntRect(1420, 0, 300, 300));
            _flying.AddFrame(new IntRect(1743, 0, 300, 300));
            _flying.AddFrame(new IntRect(2023, 0, 300, 300));

            Vector2f pos = new Vector2f(Controller.Window.Size.X/1.5f, 200f);

            Sprite = new AnimatedSprite(Time.FromSeconds(0.15f), true, true, pos);
        }
    }
}
