using System;
using System.Collections.Specialized;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters {
    class Dragon : Monster
    {
        private Animation _flying;
        
        /// <summary>
        /// makes damage to a room when the turn ends
        /// </summary>
        public override int makeTurnDamage()
        {
            Globals.AreaReference.Life -= 100;
            return 100;
        }

        public override void getTurnDamage(int type, bool hits)
        {
            if(hits)
                Life -= 100;
            if(Life <= 0)
                Console.WriteLine("dragon dead. much good.");
            Console.WriteLine("the dragon has been shot at. it does give 1/10 of a shit.");
        }

        public override void update()
        {
            //if an unhandled click is available and a room is selected, check whether the dragon is right clicked
            if (MouseHandler.UnhandledClick && MouseHandler.SelectedRoom != null)
                if (getRect().Contains(MouseHandler.LastClickPosition.X, MouseHandler.LastClickPosition.Y))
                    if (MouseHandler.Right && MouseHandler.SelectedRoom.IsAbleToTarget){
                        //consume click event and inform turnhandler of new room target
                        MouseHandler.UnhandledClick = false;
                        Globals.TurnHandler.addRoomTarget(MouseHandler.SelectedRoom, this);
                        //change cursor back to normal
                        Cursor.CursorMode(Cursor.Mode.standard);
                    }
            //play dragon sprite animation
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
