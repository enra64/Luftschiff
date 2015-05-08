
using System;
using Luftschiff.Code.Game.AreavRooms;
using SFML.System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using Luftschiff.Code.Global;
using SFML.Graphics;

namespace Luftschiff.Code.Game.Crew {
    class CrewMember : Entity
    {
        private RectangleShape _indicatorShape;
        private Sprite useAnAnimatedSprite;
        public Room CurrentRoom{ get; set; }

        private static Vector2f drawOffset = new Vector2f(16, 16);
        //possible abilities
        //TODO add or remove abilities
        public int _health{get;set;}

        /// <summary>
        ///     List containing waypoints for the crew to move through when walking from room a to b
        /// </summary>
        private readonly List<Waypoint> _wayPointList = new List<Waypoint>(); 

        /// <summary>
        /// return whether the crew is still in the process of moving, so that the monster can wait until attacking
        /// </summary>
        public bool IsStillMoving { get { return _wayPointList.Count > 0; } }

        private int _actionPoints = 1;
        private int _repairSpeed = 4;
        private int _slackFireSpeed = 1;
        private int _weaponSkills = 1;
        private int _targetRoom = 0;


        public CrewMember(Room firstRoom)
        {
            CurrentRoom = firstRoom;
            useAnAnimatedSprite = new Sprite(Globals.CrewTexture);
            useAnAnimatedSprite.Scale = new Vector2f(.15f, .15f);
            _health = 100;
            useAnAnimatedSprite.Origin = new Vector2f(128, 128);
            //init indicator rectangle
            Vector2f size = new Vector2f(useAnAnimatedSprite.Scale.X*useAnAnimatedSprite.Texture.Size.X,
                useAnAnimatedSprite.Scale.Y*useAnAnimatedSprite.Texture.Size.Y);
            _indicatorShape = new RectangleShape(size)
            {
                Position = useAnAnimatedSprite.Position,
                FillColor = Color.Transparent,
                OutlineColor = Color.Transparent,
                OutlineThickness = 2
            };
        }

        public void StartSelectionIndicator()
        {
            _indicatorShape.OutlineColor = Color.Green;
            new System.Threading.Timer(obj => { _indicatorShape.OutlineColor = Color.Transparent; }, null, (long) 400, System.Threading.Timeout.Infinite);
        }

        public void Draw()
        {
            // damage sign for crew health debug
            Shape test = new CircleShape(10);
            test.Position = useAnAnimatedSprite.Position;
            test.FillColor = new Color(20, 0, 0, (byte)(255 - (_health / 100f) * 255));  
            if(_health <= 0)
                test.FillColor = Color.Red; 
            Controller.Window.Draw(test);

            useAnAnimatedSprite.Position += drawOffset;
            Controller.Window.Draw(useAnAnimatedSprite);
            useAnAnimatedSprite.Position -= drawOffset;
            Controller.Window.Draw(_indicatorShape);
        }

        public override FloatRect getRect()
        {
            FloatRect rect = useAnAnimatedSprite.GetGlobalBounds();
            rect.Left += drawOffset.X;
            rect.Top += drawOffset.Y;
            return rect;
        }

        /// <summary>
        ///     Repairs the CurrentRoom by a certain amount
        /// </summary>
        public void RepairRoom()
        {
            //debug output
            //Console.WriteLine("repairing");
            CurrentRoom.ReceiveRepair(10 * _repairSpeed, 8 * _repairSpeed);
        }

        /// <summary>
        ///     Reduces the currentRoom FireLife by a certain amount
        /// </summary>
        public void SlackFire()
        {
            //Console.WriteLine("slacking fire");
            //start animation
            //detect impssible fire life values
            if (CurrentRoom.FireLife > 0)
                CurrentRoom.FireLife -= 1*_slackFireSpeed;
        }

        /// <summary>
        /// gets called when the crewmember should just work the room, as in use the cannon or whatever
        /// </summary>
        public void WorkRoom()
        {
            //Console.WriteLine("working in room");
            //animation
        }

        public override void Update(){
            //abort if no waypoints have been set
            if (_wayPointList.Count <= 0)
            {
                useAnAnimatedSprite.Rotation = 0;
                return;
            }
            //calculate delta between this and the waypoint
            Vector2f targetDelta = _wayPointList[0].TargetVector - useAnAnimatedSprite.Position;
            useAnAnimatedSprite.Rotation = _wayPointList[0].Rotation;
            //check whether the waypoint has already been arrived at
            if (Math.Abs(targetDelta.X) < 4 && Math.Abs(targetDelta.Y) < 4)
                //kk delete it from the list and bail
                _wayPointList.RemoveAt(0);
            //if the delta is null, we need not add the movement vector
            else
            {
                //normalise and multiply the vector for consistent movement speed
                Vector2f movementVector = Util.NormaliseVector(targetDelta) * 2;


                //add position to current position
                useAnAnimatedSprite.Position += movementVector;
                _indicatorShape.Position += movementVector;
            }
        }

        public void setPosition(Vector2f newPosition)
        {
            useAnAnimatedSprite.Position = newPosition;
            _indicatorShape.Position = newPosition;
        }

        /// <summary>
        ///     Creates a list of Vector waypoints for the update to move through
        /// </summary>
        /// <param name="moveAction"></param>
        public void Walk(CrewTarget moveAction)
        {
            _wayPointList.Clear();
            //add three wp: this rooms door, target room door, target room posiiton
            //get whether the target room is t r b l from origin
            //get distance between me and the nearroom
            Vector2f distanceVector = new Vector2f(moveAction.Target.Center.X - moveAction.Origin.Center.X, 
                moveAction.Target.Center.Y - moveAction.Origin.Center.Y);
            //distancevector: neg Y -> r above me, neg X -> r to the right
            int startDirection;
            
            //  0
            //3   1
            //  2

            //hor offset
            if (Math.Abs(distanceVector.X) > Math.Abs(distanceVector.Y))
                startDirection = distanceVector.X > 0 ? 1 : 3;
            //vert offset
            else 
                startDirection = distanceVector.Y < 0 ? 0 : 2;
            
            //start to origin door
            _wayPointList.Add(new Waypoint(moveAction.Origin.GetDoorPosition(startDirection), Position));

            //start door to target door, invert direction
            _wayPointList.Add(new Waypoint(moveAction.Target.GetDoorPosition((startDirection + 2) % 4), _wayPointList[0].TargetVector));

            //target door to endposition
            _wayPointList.Add(new Waypoint(moveAction.Target.Position + moveAction.Target.GetCrewPositionOffset(), _wayPointList[1].TargetVector));
        }
    }

    class Waypoint
    {
        public Vector2f TargetVector { get; set; }
        public Vector2f OriginVector { get; set; }
        public float Rotation { get; set; }

        public Waypoint(Vector2f targetVector, Vector2f originVector)
        {
            
            TargetVector = targetVector;
            OriginVector = originVector;

            Vector2f refe = new Vector2f(1, 0);
            Vector2f diff = targetVector - originVector;

            bool overstepp = false;
            float grade =
            (float)(refe.X*diff.X + refe.Y*diff.Y)/(float)(Util.GetVector2fLength(refe)*Util.GetVector2fLength(diff));
            if (grade < 0 ||(grade ==0 && diff.Y < 0))
            {
                overstepp = true;
            }
            grade = (float)Math.Acos(grade);
            grade = (grade*360)/(2*(float) Math.PI);

            if (overstepp)
                Rotation = -grade ;
            else
            {
                Rotation = grade;  
            }
        }
    }
}
