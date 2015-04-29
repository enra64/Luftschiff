using System;
using Luftschiff.Code.Game.Crew;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using Luftschiff.Code.Global.Utils;
using Luftschiff.Graphics.Lib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
//god dammit i do not want to use a var if i dont need to
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Luftschiff.Code.Game.AreavRooms
{
    /// <summary>
    /// Room does not inherit any giant classes, because they constantly interfere with its usage
    /// </summary>
    abstract class Room : ITarget
    {
        /// <summary>
        /// Shape to indicate selection
        /// </summary>
        private RectangleShape _indicatorShape;

        /// <summary>
        ///     Animated Sprite for fire
        /// </summary>
        private readonly AnimatedSprite _fireSprite;

        /// <summary>
        ///     Animation for the fire
        /// </summary>
        private readonly Animation _fireAnimation;

        //List to use when Crew-class implemented 
        public readonly List<CrewMember> CrewList = new List<CrewMember>();
        // List to save and get accses to rooms nearby
        //protected List<Room> _nearRooms = new List<Room>();
        public List<Room> _nearRooms { get; private set; }

        private Sound _cracklingFireSound;

        /// <summary>
        /// A convenience listener for the room shortcut key
        /// </summary>
        private KeyListener _keyListener;

        public Vector2f Position { get; set; }

        /// <summary>
        /// detect whether the room can fire, so the cursor changes appropriately
        /// </summary>
        public virtual bool IsAbleToTarget { get { return false; } }

        //useful variables
        /// <summary>
        ///     The number of Rounds the room should burn without user interaction
        /// </summary>
        public int FireLife;

        /// <summary>
        ///     Cooldown of the room action
        /// </summary>
        protected int _cooldown = 0;

        /// <summary>
        ///     Life of the Room, current default is 100
        /// </summary>
        public int RoomLife = 100;

        private bool _walkAble = true;
        protected int[,] IntegerTilemap = new int[4, 4];
        protected readonly Tile[,] ObjectTilemap= new Tile[4,4];
        protected readonly List<Sprite> AdditionalRoomSprites = new List<Sprite>();
        
        /// <summary>
        ///     The key this room can be selected with.
        /// </summary>
        protected Text ShortcutIdentificationHelper;

        /// <summary>
        ///     saves the life of the room when it gets instanced
        /// </summary>
        public readonly int MaxLife;


        /// <summary>
        /// this is called when a crewmember arrives in this room, and has no further rooms to go to
        /// </summary>
        public virtual void OnCrewArrive(CrewMember traveler)
        {
            if(FireLife > 0)
                traveler.SlackFire();
            else if (RoomLife < 100)
                traveler.RepairRoom();
            else
                traveler.WorkRoom();
        }

        /// <summary>
        /// Damages the element. Subject to change dictated by implementation of damage system
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        public virtual void ReceiveDamage(int damage)
        {
            RoomLife = RoomLife - damage/5;
            if (FireLife > 0)
            {
                RoomLife -= 30;  //template int for fire damage 
            }

            //add area damage
            Globals.AreaReference.Life -= 90;

            //randomise ignition chance to make game more playable
            if(RandomHelper.RandomTrue(33))
                SetOnFire(3);

            //TODO improve randomizer and stats for crewdamage
            Random a = new Random();
            //kills a random dude, and removes him
            if (CrewList.Count > 0)
            {
                //whether to kill that dude
                if (RandomHelper.RandomTrue(30))
                    //okay tell the area to remove that dude, which should also kill it in this room
                    Globals.AreaReference.RemoveCrewFromRoom(CrewList.ElementAt(a.Next(CrewList.Count)));
            }
        }

        /// <summary>
        /// Returns true if the objects rectangle contains the position
        /// </summary>
        /// <param name="projectilePosition">The position to check</param>
        /// <returns></returns>
        public bool HasBeenHit(Vector2f projectilePosition)
        {
            return IsClickInside(projectilePosition);
        }

        /// <summary>
        ///     Someone just wants to see this room burn.
        ///     <para>I'm sorry for that</para>
        /// </summary>
        /// <param name="roundsRoomIsBurning">Fire duration in Rounds without Slacking</param>
        public void SetOnFire(int roundsRoomIsBurning)
        {
            FireLife = roundsRoomIsBurning;
        }

        /// <summary>
        /// Called by the turnhandler to get the damage dealt by that room
        /// </summary>
        public virtual void InflictDamage(Monster monster, bool hits) { }

        /// <summary>
        /// the array is filled with a standard of numbers for the tilemap
        /// 0 -> empty map( everything 0)
        /// 1 -> border = 1 mid = 3 (roomspecific Item)
        /// </summary>
        protected int[,] LoadStandardTilekinds(int kind)
        {
            int[,] array;
            switch (kind)
            {
                    //0: floor, 1 empty, 2 wall, 3 special
                default:
                case(0):
                    array = new [,] {{0,0,0,0},
                                     {0,0,0,0},
                                     {0,0,0,0},
                                     {0,0,0,0}};
                    break;
                case(1):
                    array = new [,] {{2,2,2,2},
                                     {2,3,3,2},
                                     {2,3,3,2},
                                     {2,2,2,2}};
                    break;
                case(2):  // i don't care that this case has no use atm
                    array = new[,] {{1,1,1,1},
                                    {1,3,3,1},
                                    {1,3,3,1},
                                    {1,1,1,1}};
                    break;

            }
            return array;
        }

        public FloatRect GlobalRect
        {
            get
            {
                var tileSize = ObjectTilemap[0, 0].Rect;
                tileSize.Width *= 4;
                tileSize.Height *= 4;
                return tileSize;    
            }
        }

        public Vector2f Center
        {
            get
            {
                FloatRect rect = GlobalRect;
                return new Vector2f(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            }
        }

        public bool IsClickInside(Vector2f position)
        {
            return GlobalRect.Contains(position.X, position.Y);
        }

        /// <summary>
        /// sets crew in room. gives bool back true -> succes , false -> there are too many crewmembers in that room;
        /// </summary>
        public bool SetCrewInRoom(CrewMember a)
        {
            if (CrewList.Count < 4)
            {
                CrewList.Add(a);
                switch (CrewList.Count)
                {
                    case 1:
                        a.setPosition(Position);
                        break;
                    case 2:
                        a.setPosition(Position + new Vector2f(64, 0));
                        break;
                    case 3:
                        a.setPosition(Position + new Vector2f(0, 64));
                        break;
                    case 4:
                        a.setPosition(Position + new Vector2f(64, 64));
                        break;
                }
                return true;
            }
            //return false if no space is left
            return false;
        }
        
        /// <summary>
        /// Crewmember will be removed from crew roomlist and given back as value for future use
        /// </summary>
        public CrewMember RemoveCrewMember(CrewMember a)
        {
            //return a (since we get that anyway) if the element could be removed, and return null otherwise
            return CrewList.Remove(a) ? a : null;
        }

        public void AddDoorsToTileArray(int[,] array, Vector2f position)
        {
            //TODO add door number to tileMap numbers , needed Roomconnection list
        }

        protected Room(Vector2f position)
        {
            Position = position;
            MaxLife = RoomLife;
            _nearRooms = new List<Room>();

            //sound the room makes when on fire
            _cracklingFireSound = new Sound(Globals.FireCrackleSound);

            //initialize the indicator shape
            _indicatorShape = new RectangleShape();

            //initialize the fire sprite
            _fireSprite = new AnimatedSprite(Time.FromSeconds(0.3f), false, false, Position);
            _fireSprite.Position = position;
            _fireAnimation = new Animation(Globals.RoomFireTexture);
            _fireAnimation.AddFrame(new IntRect(0, 0, 64, 64));
            _fireAnimation.AddFrame(new IntRect(64, 0, 64, 64));
            _fireAnimation.AddFrame(new IntRect(128, 0, 64, 64));
            _fireAnimation.AddFrame(new IntRect(192, 0, 64, 64));
            _fireAnimation.AddFrame(new IntRect(256, 0, 64, 64));
        }

        /// <summary>
        /// Adds a keyboard shortcut to the room, and an identification text for beginners
        /// </summary>
        /// <param name="numkey"></param>
        public void AddKeyboardShortcut(int numkey)
        {
            _keyListener = new KeyListener(numkey);
            ShortcutIdentificationHelper = new Text
            {
                DisplayedString = numkey.ToString(),
                Position = ObjectTilemap[1,0].Position,
                CharacterSize = 90,
                Color = Color.Transparent,
                Font = Globals.DialogFont
            };
            //have to show that, too
            ShowShortcutIdentification();
        }

        /// <summary>
        ///     Initilizes tilemap in dependence of int[,] IntegerTilemap
        /// </summary>
        protected void initializeTilemap(Area.RoomTypes roomType)
        {
            for (var i = 0; i < 4; i++){
                for (var k = 0; k < 4; k++){
                    ObjectTilemap[i, k] = new Tile(IntegerTilemap[i, k], new Vector2f(this.Position.X + 32 * i, Position.Y + 32 * k), roomType); //TODO let the vector fit to every file
                }
            }

            //get room size
            var rect = GlobalRect;

            //because we only now have valid tile sizes, init the indicator rectangle now
            _indicatorShape = new RectangleShape(new Vector2f(rect.Width, rect.Height))
            {
                Position = Position,
                FillColor = Color.Transparent,
                OutlineColor = Color.Transparent,
                OutlineThickness = 2
            };
        }

        public void Draw()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    ObjectTilemap[i, k].Draw();
                }
            }

            //draw additional sprites like large weapons etc
            foreach(Sprite s in AdditionalRoomSprites)
                Controller.Window.Draw(s);

            //draw Damage signs on room
            //TODO insert damage pictures nearly as this rectangle
            RectangleShape damage = new RectangleShape(new Vector2f(128,128));
            damage.Position = Position;
            damage.FillColor = new Color(20,0,0,(byte)(255-(RoomLife/100f)*255));         
            Controller.Window.Draw(damage);

            // draw der crew
            for (int k = 0; k < CrewList.Count; k++){
                CrewList.ElementAt(k).Draw();
            }

            //draw one fire first, since fire spreading should be difficult
            if (FireLife > 0)
            {
                _fireSprite.Update(Globals.FRAME_TIME);
                _fireSprite.Play(_fireAnimation);
                Controller.Window.Draw(_fireSprite);
            }
            //draw the selection indicator rectangle
            Controller.Window.Draw(_indicatorShape);

            //draw the shortcut help text
            Controller.Window.Draw(ShortcutIdentificationHelper);
        }

        /// <summary>
        ///     Show and hide the selectionindicator
        /// </summary>
        public void StartSelectionIndicator()
        {
            _indicatorShape.OutlineColor = Color.Green;
            //hide warning because this is as designed
            // ReSharper disable once ObjectCreationAsStatement
            new System.Threading.Timer(obj => { _indicatorShape.OutlineColor = Color.Transparent; }, null, (long) 200, System.Threading.Timeout.Infinite);
        }

        private void ShowShortcutIdentification()
        {
            ShortcutIdentificationHelper.Color = Color.Black;
            //hide warning because this is as designed
            // ReSharper disable once ObjectCreationAsStatement
            new System.Threading.Timer(obj => { ShortcutIdentificationHelper.Color = Color.Transparent; }, null, (long) 1600, System.Threading.Timeout.Infinite);
        }

        public void addNearRooms(Room a)
        {
            _nearRooms.Add(a);
        }

        public bool IsWalkable{get{return _walkAble;}}

        public bool IsAlive
        {
            get
            {
                return RoomLife > 0;    
            }
        }

        /// <summary>
        /// Update function; 
        /// </summary>
        public virtual void Update()
        {
            //when the shortcut key is pressed, activate this rrom
            if (_keyListener.IsClicked)
                MouseHandler.SelectedRoom = this;

            //handle room burn
            if (FireLife > 0)
            {
                //only make fire sound when the room is on fire and not yet playing a sound
                if(_cracklingFireSound.Status == SoundStatus.Stopped || 
                    _cracklingFireSound.Status == SoundStatus.Paused)
                    _cracklingFireSound.Play();


            }
        }
    }
}
