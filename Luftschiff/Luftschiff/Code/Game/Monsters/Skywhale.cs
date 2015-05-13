using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters
{
    internal class Skywhale : Monster
    {
        private readonly Animation _whalediving;
        private int _hittingInthemiddle = 0;
        private int _horncount;
        private int _roundCounter;
        public Sprite horn = new Sprite(Globals.WhaleHornTexture);

        public Skywhale(Texture t, int life) : base(life)
        {
            _whalediving = new Animation(t);
            for (var i = 0; i < 10; i++)
            {
                _whalediving.AddFrame(new IntRect(0, 546*i, 951, 546));
            }
            var pos = new Vector2f(Controller.Window.Size.X/1.7f, 100f);
            Sprite = new AnimatedSprite(Time.FromSeconds(0.12f), false, true, pos);
            Sprite.Scale = new Vector2f(0.8f, 0.8f);

            horn.Rotation = 315;
            horn.Position = new Vector2f(50, 200);
            horn.Scale = new Vector2f(7, 7);
        }

        public Skywhale() : this(Globals.SkywhaleTexture, 1000)
        {
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
            Sprite.Play(_whalediving);

            if (Life <= 0)
            {
                var restart = new TwoButtonDialog("Nochmal starten?", "Du hast den Himmelswal besiegt!").show();
                if (restart)
                    Controller.LoadState(Globals.EStates.game);
                else
                    Controller.Window.Close();
            }
        }

        public override void AttackShip(Area areaReference)
        {
            _roundCounter++;
            if (_roundCounter%3 == 0)
            {
                foreach (var room in areaReference.Rooms)
                {
                    var damage = (int) (room.MaxLife*0.4);
                    _horncount = 40;
                    room.ReceiveDamage(damage);
                }
            }
        }

        public override void ReceiveDamage(int damageAmount)
        {
            Life -= damageAmount;
        }

        public override bool HasBeenHit(Vector2f projectilePosition)
        {
            //TODO debug that after some shots hitting is in front
            if (getRect().Contains(projectilePosition.X - 350, projectilePosition.Y))
                return true;
            return false;
        }

        public override void Draw()
        {
            base.Draw();
            if (_horncount > 0)
            {
                Controller.Window.Draw(horn);
                _horncount--;
            }
        }
    }
}