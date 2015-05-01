using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters
{
    class Skywhale : Monster
    {
        private int _hittingInthemiddle = 0;
        private readonly Animation _whalediving;
        public Skywhale(Texture t, int life) : base(life)
        {
            _whalediving = new Animation(t);
            for (int i = 0; i < 10; i++)
            {
                _whalediving.AddFrame(new IntRect(0,546 *i,951,546));
            }
            var pos = new Vector2f(Controller.Window.Size.X / 1.8f, 50f);
            Sprite = new AnimatedSprite(Time.FromSeconds(0.12f),false,true,pos);
        }
        public Skywhale() : this(Globals.SkywhaleTexture, 1000){}

        public override void Update()
        {
            //if an unhandled click is available and a room is selected, check whether the dragon is right clicked
            if (MouseHandler.UnhandledClick && MouseHandler.SelectedRoom != null)
                if (getRect().Contains(MouseHandler.LastClickPosition.X, MouseHandler.LastClickPosition.Y))
                    if (MouseHandler.Right && MouseHandler.SelectedRoom.IsAbleToTarget)
                    {
                        //consume click event and inform turnhandler of new room target
                        MouseHandler.UnhandledClick = false;
                        Globals.TurnHandler.AddWeaponTarget(MouseHandler.SelectedRoom, this);
                        //change cursor back to normal
                        Cursor.CursorMode(Cursor.Mode.Standard);
                    }
            //play dragon sprite animation
            Sprite.Update(Globals.FRAME_TIME);
            Sprite.Play(_whalediving);

            if (Life <= 0)
            {
                bool restart = new TwoButtonDialog("Nochmal starten?", "Du hast den Himmelswal besiegt!").show();
                if (restart)
                    Controller.LoadState(Globals.EStates.game);
                else
                    Controller.Window.Close();
            }
        }

        public override int AttackShip(Area areaReference)
        {
            return 10000;
        }

        public override void ReceiveDamage(int damageAmount)
        {
            Life -= damageAmount;
        }

        public override bool HasBeenHit(Vector2f projectilePosition)
        {
            //TODO debug that after some shots hitting is in front
            if (getRect().Intersects(new FloatRect(projectilePosition, new Vector2f(1f, 1f))))
            {
                _hittingInthemiddle++;
            }
            if (_hittingInthemiddle > 50)
            {
                _hittingInthemiddle = 0;
                return true;
            }
            return false;
        }
    }
}
