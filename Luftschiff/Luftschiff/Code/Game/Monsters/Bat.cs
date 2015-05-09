using System;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Crew;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Global.Utils;
using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters
{
    class Bat : Monster
    {
        private readonly Animation _chewing;
        public Bat(Vector2f pos) : this(Globals.BatTexture, 1000, pos){}
        public Bat(Texture t, int life, Vector2f _pos): base(life)
        {
            _chewing = new Animation(t);
            _chewing.AddFrame(new IntRect(0, 0, 32, 32));
            _chewing.AddFrame(new IntRect(32, 0, 32, 32));
            _chewing.AddFrame(new IntRect(64, 0, 32, 32));

            var pos = _pos;

            Sprite = new AnimatedSprite(Time.FromSeconds(0.15f), true, true, pos);
            Sprite.Scale = new Vector2f(2.5f,2.5f);
        }
        public override void AttackShip(AreavRooms.Area areaReference)
        {
            Room attackedRoom = Globals.AreaReference.GetRandomRoom(-1);
            biteAttack(attackedRoom);
        }

        private void biteAttack(Room attackedRoom)
        {
            if (attackedRoom.CrewList.Count > 0)
            {
                //affected crewmember in room
                CrewMember affected = attackedRoom.CrewList[RandomHelper.RandomUpTo(attackedRoom.CrewList.Count)];
                if (RandomHelper.RandomTrue(30))
                {
                    //okay tell the area to remove that dude, which should also kill it in this room
                    Console.Write("Attack: ");
                    Globals.AreaReference.RemoveCrewFromRoom(affected);
                }
            }
            attackedRoom.ReceiveDamage(20);
        }
        public override void ReceiveDamage(int damageAmount)
        {
            //hit boolean?
            if (true)
            {
                Notifications.Instance.AddNotification(Position, "Sadness preveils!");
                Life -= 120;
            }
            if (Life <= 0)
                Console.WriteLine("Bat is dead");
            Console.WriteLine("Batty the Bat");
        }
        public override bool HasBeenHit(Vector2f projectilePosition)
        {
            return IsClickInside(projectilePosition);
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
            //play dragon sprite animation
            Sprite.Update(Globals.FRAME_TIME);
            Sprite.Play(_chewing);

            if (Life <= 0)
            {
                bool restart = new TwoButtonDialog("Nochmal starten?", "Du hast den Drachen besiegt!").show();
                if (restart)
                    Controller.LoadState(Globals.EStates.game);
                else
                    Controller.Window.Close();
            }
        }

        public override void Draw()
        {
            if (Life > 0)
            {
                Controller.Window.Draw(Sprite);
            }
        }
    }
}
