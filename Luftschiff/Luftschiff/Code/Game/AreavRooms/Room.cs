using System;
using System.Collections.Generic;
using System.Threading;
using Luftschiff.Code.Game.Crew;
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
    ///     Room does not inherit any giant classes, because they constantly interfere with its usage
    /// </summary>
    internal abstract class Room : ITarget
    {
        private readonly Sound _cracklingFireSound;

        /// <summary>
        ///     Animation for the fire
        /// </summary>
        private readonly Animation _fireAnimation;

        /// <summary>
        ///     Animated Sprite for fire
        /// </summary>
        private readonly AnimatedSprite _fireSprite;

        protected readonly List<Sprite> AdditionalRoomSprites = new List<Sprite>();
        //List to use when Crew-class implemented 
        public readonly List<CrewMember> CrewList = new List<CrewMember>();

        /// <summary>
        ///     saves the life of the room when it gets instanced
        /// </summary>
        public readonly int MaxLife;

        protected readonly Tile[,] ObjectTilemap = new Tile[4, 4];

        /// <summary>
        ///     Cooldown of the room action
        /// </summary>
        protected int _cooldown = 0;

        private List<Vector2f> _doorPositions = new List<Vector2f>();

        /// <summary>
        ///     Shape to indicate selection
        /// </summary>
        private RectangleShape _indicatorShape;

        /// <summary>
        ///     A convenience listener for the room shortcut key
        /// </summary>
        private KeyListener _keyListener;

        public int[,] IntegerTilemap = new int[4, 4];

        /// <summary>
        ///     The key this room can be selected with.
        /// </summary>
        protected Text ShortcutIdentificationHelper;

        protected Room(Vector2f position)
        {
            Position = position;
            RoomLife = 100;
            MaxLife = RoomLife;
            _nearRooms = new List<Room>();

            initializeTilemap(Area.RoomTypes.Empty);

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

        // List to save and get accses to rooms nearby
        //protected List<Room> _nearRooms = new List<Room>();
        public List<Room> _nearRooms { get; }
        public Vector2f Position { get; set; }

        /// <summary>
        ///     detect whether the room can fire, so the cursor changes appropriately
        /// </summary>
        public virtual bool IsAbleToTarget
        {
            get { return false; }
        }

        //useful variables
        /// <summary>
        ///     The number of Rounds the room should burn without user interaction
        /// </summary>
        public int FireLife { get; set; }

        /// <summary>
        ///     Life of the Room, current default is 100
        /// </summary>
        public int RoomLife { get; set; }

        /// <summary>
        ///     Used so that rooms with special stuff can override it
        /// </summary>
        public virtual bool NeedsRepair
        {
            get { return RoomLife < 100; }
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

        public bool IsWalkable { get; private set; } = true;

        public bool IsAlive
        {
            get { return RoomLife > 0; }
        }

        /// <summary>
        ///     Damages the element. Subject to change dictated by implementation of damage system
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        public virtual void ReceiveDamage(int damage)
        {
            RoomLife = RoomLife - damage/5;
            if (FireLife > 0)
            {
                RoomLife -= 30; //template int for fire damage 
            }

            //add area damage
            Globals.AreaReference.Life -= damage;

            //TODO improve randomizer and stats for crewdamage

            //giving the crew some damage
            foreach (var member in CrewList)
            {
                member.Health -= damage/10;
            }
            //TODO Add a function to look up if a crewmembe is dead
        }

        /// <summary>
        ///     Returns true if the objects rectangle contains the position
        /// </summary>
        /// <param name="projectilePosition">The position to check</param>
        /// <returns></returns>
        public bool HasBeenHit(Vector2f projectilePosition)
        {
            return IsClickInside(projectilePosition);
        }

        public Vector2f Center
        {
            get
            {
                var rect = GlobalRect;
                return new Vector2f(rect.Left + rect.Width/2, rect.Top + rect.Height/2);
            }
        }

        /// <summary>
        ///     this is called when a crewmember arrives in this room, and has no further rooms to go to
        /// </summary>
        public virtual void OnCrewArrive(CrewMember traveler)
        {
            if (FireLife > 0)
                traveler.SlackFire();
            else if (NeedsRepair)
                traveler.RepairRoom();
            else
                traveler.WorkRoom();
        }

        /// <summary>
        ///     Repair the room by a certain amount. If not overridden, the special room item will not be repaired
        /// </summary>
        /// <param name="repairAmount">How much to add to RoomLife</param>
        /// <param name="repairSpecial">Whether to repair specialties (e.g. cannon)</param>
        public virtual void ReceiveRepair(int repairAmount, int repairSpecial)
        {
            if (RoomLife + repairAmount <= MaxLife)
            {
                if (Globals.AreaReference.Life <= 1000)
                    Globals.AreaReference.Life += repairAmount;
                RoomLife += repairAmount;
            }
            else
                RoomLife = MaxLife;
        }

        /// <summary>
        ///     Some men just want to watch this room burn.
        ///     <para>I'm sorry for that.</para>
        /// </summary>
        /// <param name="roundsRoomIsBurning">Fire duration in Rounds without Slacking</param>
        public void SetOnFire(int roundsRoomIsBurning)
        {
            FireLife = roundsRoomIsBurning;
        }

        /// <summary>
        ///     Called by the turnhandler to get the damage dealt by that room
        /// </summary>
        public virtual void InflictDamage(Monster monster, bool hits)
        {
        }

        /// <summary>
        ///     the array is filled with a standard of numbers for the tilemap
        ///     0 -> empty map( everything 0)
        ///     1 -> border = 1 mid = 3 (roomspecific Item)
        /// </summary>
        protected int[,] LoadStandardTilekinds(int kind)
        {
            int[,] array;
            switch (kind)
            {
                //0: floor, 1 empty, 2 wall, 3 special
                default:
                case (0):
                    array = new[,]
                    {
                        {0, 0, 0, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
                case (1):
                    array = new[,]
                    {
                        {2, 2, 2, 2},
                        {2, 3, 3, 2},
                        {2, 3, 3, 2},
                        {2, 2, 2, 2}
                    };
                    break;
                //special to identify empty room
                case (3):
                    array = new[,]
                    {
                        {3, 2, 2, 2},
                        {2, 3, 2, 2},
                        {2, 2, 3, 2},
                        {2, 2, 2, 3}
                    };
                    break;
                case (2): // i don't care that this case has no use atm
                    array = new[,]
                    {
                        {1, 1, 1, 1},
                        {1, 3, 3, 1},
                        {1, 3, 3, 1},
                        {1, 1, 1, 1}
                    };
                    break;
            }
            //add doors
            //array = AddDoorsToTileArray(array);
            return array;
        }

        public bool IsClickInside(Vector2f position)
        {
            return GlobalRect.Contains(position.X, position.Y);
        }

        /// <summary>
        ///     Set the crew into this room, updating its position instantly
        /// </summary>
        public int SetCrewInRoom(CrewMember a)
        {
            return SetCrewInRoom(a, false);
        }

        /// <summary>
        ///     Set the crew in this room. Only update the position instantly when DoNotSetPosition == false
        /// </summary>
        public int SetCrewInRoom(CrewMember a, bool doNotSetPosition)
        {
            if (CrewList.Count < 4)
            {
                CrewList.Add(a);
                if (!doNotSetPosition)
                    a.setPosition(Position + GetCrewPositionOffset());
                if (CrewList.Count == 4)
                    IsWalkable = false;
                return CrewList.Count;
            }
            IsWalkable = false;
            //return false if no space is left
            return -1;
        }

        /// <summary>
        ///     Returns the offset an additional crewmember should have. Does _not_ care about full rooms,
        ///     and returns a bad default value.
        /// </summary>
        /// <returns>New Crewmember offset.</returns>
        public Vector2f GetCrewPositionOffset()
        {
            var offset = new Vector2f(0, 0);
            switch (CrewList.Count)
            {
                case 2:
                    return new Vector2f(64, 0);
                case 3:
                    return new Vector2f(0, 64);
                case 4:
                    return new Vector2f(64, 64);
                default:
                    return offset;
            }
        }

        /// <summary>
        ///     Crewmember will be removed from crew roomlist and given back as value for future use
        /// </summary>
        public CrewMember RemoveCrewMember(CrewMember a)
        {
            //return a (since we get that anyway) if the element could be removed, and return null otherwise
            if (CrewList.Count == 4)
            {
                IsWalkable = true;
            }
            return CrewList.Remove(a) ? a : null;
        }

        public void AddDoorsToTileArray()
        {
            //TODO add door number to tileMap numbers , needed Roomconnection list
            //check each near room//cannon, engine room have no near rooms
            foreach (var r in _nearRooms)
            {
                //♥ win+space ftw though
                //get distance between me and the nearroom
                var distanceVector = new Vector2f(r.Center.X - Center.X, r.Center.Y - Center.Y);
                //check whether we are horizontally or vertically offset
                //distancevector: neg Y -> r above me, neg X -> r to the right
                //horizontally offset
                if (Math.Abs(distanceVector.X) > Math.Abs(distanceVector.Y))
                {
                    var x = distanceVector.X < 0 ? 0 : 3;
                    var y = distanceVector.Y < 0 ? 1 : 2;
                    IntegerTilemap[x, y] = Tile.TILE_DOOR;
                }
                //vert offset
                else
                {
                    var x = distanceVector.X < 0 ? 1 : 2;
                    var y = distanceVector.Y < 0 ? 0 : 3;
                    IntegerTilemap[x, y] = Tile.TILE_DOOR;
                }
            }
        }

        /// <summary>
        ///     Returns the position of the door in the requested position
        /// </summary>
        /// <param name="direction">0t 1r 2b 3l</param>
        /// <returns></returns>
        public Vector2f GetDoorPosition(int direction)
        {
            switch (direction)
            {
                default:
                case 0:
                    if (ObjectTilemap[1, 0].Type == Tile.TILE_DOOR)
                        return ObjectTilemap[1, 0].Position;
                    return ObjectTilemap[2, 0].Position;
                case 1:
                    if (ObjectTilemap[3, 1].Type == Tile.TILE_DOOR)
                        return ObjectTilemap[3, 1].Position;
                    return ObjectTilemap[3, 2].Position;
                case 2:
                    if (ObjectTilemap[1, 3].Type == Tile.TILE_DOOR)
                        return ObjectTilemap[1, 3].Position;
                    return ObjectTilemap[2, 3].Position;
                case 3:
                    if (ObjectTilemap[0, 2].Type == Tile.TILE_DOOR)
                        return ObjectTilemap[0, 2].Position;
                    return ObjectTilemap[0, 1].Position;
            }
        }

        /// <summary>
        ///     Must call AddDoorsToTileArray and initializeTilemap.
        /// </summary>
        public abstract void FinalizeTiles();

        /// <summary>
        ///     Adds a keyboard shortcut to the room, and an identification text for beginners
        /// </summary>
        /// <param name="numkey"></param>
        public void AddKeyboardShortcut(int numkey)
        {
            _keyListener = new KeyListener(numkey);
            ShortcutIdentificationHelper = new Text
            {
                DisplayedString = numkey.ToString(),
                Position = ObjectTilemap[1, 0].Position,
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
            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    ObjectTilemap[x, y] = new Tile(IntegerTilemap[x, y],
                        new Vector2f(Position.X + 32*x, Position.Y + 32*y), roomType);
                        //TODO let the vector fit to every file
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
            //draw the tilemap
            for (var i = 0; i < 4; i++)
            {
                for (var k = 0; k < 4; k++)
                {
                    ObjectTilemap[i, k].Draw();
                }
            }

            //draw additional sprites like large weapons etc
            foreach (var s in AdditionalRoomSprites)
                Controller.Window.Draw(s);

            //draw Damage signs on room
            //TODO insert damage pictures nearly as this rectangle
            var damage = new RectangleShape(new Vector2f(128, 128));
            damage.Position = Position;
            damage.FillColor = new Color(20, 0, 0, (byte) (255 - (RoomLife/100f)*255));
            Controller.Window.Draw(damage);

            // draw der crew
            //for (int k = 0; k < CrewList.Count; k++){
            //    CrewList.ElementAt(k).Draw();
            //}

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
            new Timer(obj => { _indicatorShape.OutlineColor = Color.Transparent; }, null, (long) 200, Timeout.Infinite);
        }

        private void ShowShortcutIdentification()
        {
            ShortcutIdentificationHelper.Color = Color.Black;
            //hide warning because this is as designed
            // ReSharper disable once ObjectCreationAsStatement
            new Timer(obj => { ShortcutIdentificationHelper.Color = Color.Transparent; }, null, (long) 1600,
                Timeout.Infinite);
        }

        public void addNearRooms(Room a)
        {
            _nearRooms.Add(a);
        }

        /// <summary>
        ///     Update function;
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
                if (_cracklingFireSound.Status == SoundStatus.Stopped ||
                    _cracklingFireSound.Status == SoundStatus.Paused)
                    _cracklingFireSound.Play();
            }
        }
    }
}