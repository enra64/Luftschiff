using System;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Global.Utils;
using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.Monsters
{
    internal class Dragon : Monster
    {
        private readonly Animation _flying;

        public Dragon(Texture t, int life) : base(life)
        {
            _flying = new Animation(t);
            _flying.AddFrame(new IntRect(0, 0, 300, 300));
            _flying.AddFrame(new IntRect(270, 0, 300, 300));
            _flying.AddFrame(new IntRect(550, 0, 300, 300));
            _flying.AddFrame(new IntRect(850, 0, 300, 300));
            _flying.AddFrame(new IntRect(1140, 0, 300, 300));
            _flying.AddFrame(new IntRect(1420, 0, 300, 300));
            _flying.AddFrame(new IntRect(1743, 0, 300, 300));
            _flying.AddFrame(new IntRect(2023, 0, 300, 300));

            var pos = new Vector2f(Controller.Window.Size.X/1.5f, 200f);

            Sprite = new AnimatedSprite(Time.FromSeconds(0.15f), true, true, pos);
        }

        /// <summary>
        ///     Overloaded Constructor using global dragon texture and 1000 life
        /// </summary>
        public Dragon() : this(Globals.DragonTexture, 1000)
        {
        }

        /// <summary>
        ///     makes damage to a room when the turn ends
        /// </summary>
        /// <param name="areaReference"></param>
        public override void AttackShip(Area areaReference)
        {
            //create projectile to attack the ship
            var attackedRoom = Globals.AreaReference.GetRandomRoom(-1);
            if (RandomHelper.FiftyFifty())
                FireAttack(attackedRoom);
            else
                ClawAttack(attackedRoom);
        }

        private void FireAttack(Room attackedRoom)
        {
            Globals.ColliderReference.AddProjectile(new FireBall(attackedRoom, this, Globals.FireBallTexture));
            new Sound(Globals.FireSound).Play();
        }

        private void ClawAttack(Room attackedRoom)
        {
            if (attackedRoom.CrewList.Count > 0)
            {
                //affected crewmember in room
                var affected = attackedRoom.CrewList[RandomHelper.RandomUpTo(attackedRoom.CrewList.Count)];

                if (RandomHelper.RandomTrue(30))
                {
                    //okay tell the area to remove that dude, which should also kill it in this room
                    Console.Write("Attack: ");
                    Globals.AreaReference.RemoveCrewFromRoom(affected);
                }
            }
            //move a claw over the room
            Globals.ColliderReference.AddProjectile(new DragonClaw(attackedRoom));

            //show a notification
            Notifications.Instance.AddNotification(attackedRoom.Position, "CLAW ATTACK");

            //damage the room
            attackedRoom.ReceiveDamage(70);
        }

        public override void ReceiveDamage(int damageAmount)
        {
            //hit boolean?
            if (true)
            {
                Notifications.Instance.AddNotification(Position, "HIT BAM SO EFFECT");
                Life -= 120;
            }
            if (Life <= 0)
                Console.WriteLine("dragon dead. much good.");
            Console.WriteLine("the dragon has been shot at. it does give 1/10 of a shit.");
        }

        //needed for ITarget compliance
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
            Sprite.Play(_flying);

            if (Life <= 0)
            {
                var restart = new TwoButtonDialog("Nochmal starten?", "Du hast den Drachen besiegt!").show();
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