﻿
using System;
using System.Collections.Specialized;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters {
    class Dragon : Monster
    {
        private Animation flying;
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
                if (MouseHandler.selectedRoom != null)
                {
                    if (getRect().Contains(MouseHandler.LastClickPosition.X, MouseHandler.LastClickPosition.Y))
                    {
                        MouseHandler.UnhandledClick = false;
                        Globals.TurnHandler.addRoomTarget(MouseHandler.selectedRoom, this);    
                    }
                }
            }
            Sprite.Update(Globals.FRAME_TIME);
            Sprite.Play(flying);
        }

        public Dragon(Texture t)
        {
            flying = new Animation(new Texture("Assets/Graphics/dragon.png"));
            flying.AddFrame(new IntRect(0, 0, 100, 674));
            flying.AddFrame(new IntRect(100, 0, 200, 674));
            flying.AddFrame(new IntRect(200, 0, 300, 674));
            flying.AddFrame(new IntRect(300, 0, 389, 674));

            Sprite = new AnimatedSprite(Time.FromSeconds(0.4f), true, true,new Vector2f(Controller.Window.Size.X / 1.5f, 0f));
        }
    }
}
