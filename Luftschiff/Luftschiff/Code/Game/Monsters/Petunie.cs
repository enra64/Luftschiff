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
    class Petunie : Monster
    {
        private readonly Animation _falling;

        public Petunie() : this(Globals.PetunienTexture, 1000){}
        public Petunie(Texture t,int life) : base(life)
        {
            _falling = new Animation(t);
            for (int i = 0; i < 5; i++)
            {
                _falling.AddFrame(new IntRect(0,389 *i, 219,389));
            }

            var pos = new Vector2f(Controller.Window.Size.X / 1.3f, 100f);
            Sprite = new AnimatedSprite(Time.FromSeconds(0.12f), false, true, pos);
            Sprite.Scale = new Vector2f(1f, 1f);
        }

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
            //play whale sprite animation
            Sprite.Update(Globals.FRAME_TIME);
            Sprite.Play(_falling);

            if (Life <= 0)
            {
                bool restart = new TwoButtonDialog("Nochmal starten?", "Du hast den Himmelswal besiegt!").show();
                if (restart)
                    Controller.LoadState(Globals.EStates.game);
                else
                    Controller.Window.Close();
            }
        }

        public override void AttackShip(Area areaReference)
        {
        }

        public override void ReceiveDamage(int damageAmount)
        {
            Life -= damageAmount;
        }

        public override bool HasBeenHit(Vector2f projectilePosition)
        {
            //throw new NotImplementedException();
            return true;
        }
    }
}
