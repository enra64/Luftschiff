
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters {
    class Dragon : Monster
    {
        private Animation a;
        public override int makeTurnDamage()
        {
            throw new System.NotImplementedException();
        }

        public override void getTurnDamage(int type, bool hits)
        {
            throw new System.NotImplementedException("The monster overrides the getdamage, but has no idea what to do!");
        }

        public override void update()
        {
            if (MouseHandler.UnhandledClick)
            {
                if (MouseHandler.selectedRoom != null)
                {
                    Globals.TurnHandler.addRoomTarget(MouseHandler.selectedRoom, this);
                }
            }
            sprite.Update(Globals.FRAME_TIME);
            sprite.Play(a);
        }

        public Dragon(Texture t)
        {
            a = new Animation(new Texture("Assets/Graphics/dragon.png"));
            a.AddFrame(new IntRect(0, 0, 100, 674));
            a.AddFrame(new IntRect(100, 0, 200, 674));
            a.AddFrame(new IntRect(200, 0, 300, 674));
            a.AddFrame(new IntRect(300, 0, 389, 674));

            sprite = new AnimatedSprite(Time.FromSeconds(0.4f), true, true,new Vector2f(Controller.Window.Size.X / 1.5f, 0f));
        }
    }
}
