using System;
using Luftschiff.Code.Game.Crew;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Luftschiff.Code.Game.Monsters;
using Luftschiff.Code.Game.Projectiles;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Game.AreavRooms
{
    /// <summary>
    /// Room does not inherit any giant classes, because they constantly interfere with its usage
    /// </summary>
    abstract class Room : ITarget
    {
        //List to use when Crew-class implemented 
        protected List<CrewMember> crewList = new List<CrewMember>();
        // List to save and get accses to rooms nearby
        //protected List<Room> _nearRooms = new List<Room>();
        public List<Room> _nearRooms { get; set; }

        public Vector2f Position { get; set; }

        /// <summary>
        /// detect whether the room can fire, so the cursor changes appropriately
        /// </summary>
        public virtual bool IsAbleToTarget { get { return false; } }

        //useful variables
        protected int _fireLife = 0;
        protected int _cooldown = 0;
        protected int _roomLife = 100;
        protected bool _walkAble = true;
        protected int[,] tilekind = new int[4, 4];
        protected Tile[,] _tilemap= new Tile[4,4];
        protected List<Sprite> _additionalRoomSprites = new List<Sprite>(); 


        /// <summary>
        /// this is called when a crewmember arrives in this room, and has no further rooms to go to
        /// </summary>
        public virtual void OnCrewArrive(CrewMember traveler)
        {
            if(_fireLife > 0)
                traveler.SlackFire();
            else if (_roomLife < 100)
                traveler.RepairRoom();
            else
                traveler.WorkRoom();
        }

        /// <summary>
        /// Damages the element. Subject to change dictated by implementation of damage system
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        public void ReceiveDamage(int damage)
        {
            _roomLife = _roomLife - damage;
            if (_fireLife > 0)
            {
                _roomLife = _roomLife - 10;  //template int for fire damage 
            }
            //add area damage
            Globals.AreaReference.Life -= 100;
            //TODO special damage for Crewdamage
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

        public void SetOnFire(int roundsRoomIsBurning)
        {
            this._fireLife = roundsRoomIsBurning;
        }

        /// <summary>
        /// Called by the turnhandler to get the damage dealt by that room
        /// </summary>
        public virtual void inflictDamage(Monster monster, bool hits) { }

        /// <summary>
        /// the array is filled with a standart of numbers for the tilemap
        /// 0 -> empty map( everything 0)
        /// 1 -> border = 1 mid = 3 (roomspecific Item)
        /// </summary>
        public int[,] loadStandardTilekinds(int kind)
        {
            int[,] array;
            switch (kind)
            {
                    //0: floor, 1 empty, 2 wall, 3 special
                default:
                case(0):
                    array = new int[4, 4] {{0,0,0,0},
                                          {0,0,0,0},
                                          {0,0,0,0},
                                          {0,0,0,0}};
                    break;
                case(1):
                    array = new int[4, 4] {{2,2,2,2},
                                          {2,3,3,2},
                                          {2,3,3,2},
                                          {2,2,2,2}};
                    break;
                case(2):  // i don't care that this case has no use atm
                    array = new int[4, 4] {{1,1,1,1},
                                          {1,3,3,1},
                                          {1,3,3,1},
                                          {1,1,1,1}};
                    break;

            }
            return array;
        }

        public FloatRect getRect()
        {
            FloatRect tileSize = _tilemap[0, 0].Rect;
            tileSize.Width *= 4;
            tileSize.Height *= 4;
            return tileSize;
        }

        public Vector2f Center
        {
            get
            {
                FloatRect rect = getRect();
                return new Vector2f(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            }
        }

        public bool IsClickInside(Vector2f position)
        {
            return getRect().Contains(position.X, position.Y);
        }

        /// <summary>
        /// sets crew in room. gives bool back true -> succes , false -> there are too many crewmembers in that room;
        /// </summary>
        public bool setCrewInRoom(CrewMember a)
        {
            if (crewList.Count < 4)
            {
                crewList.Add(a);
                switch (crewList.Count)
                {
                    case 1:
                        a.setPosition(this.Position);
                        break;
                    case 2:
                        a.setPosition(this.Position + new Vector2f(64, 0));
                        break;
                    case 3:
                        a.setPosition(this.Position + new Vector2f(0, 64));
                        break;
                    case 4:
                        a.setPosition(this.Position + new Vector2f(64, 64));
                        break;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        
        /// <summary>
        /// Crewmember will be removed from crew roomlist and given back as value for future use
        /// </summary>
        public CrewMember RemoveCrewMember(CrewMember a)
        {
            for (int i = 0; i < crewList.Count; i++)
            {
                if (crewList.ElementAt(i).Equals(a))
                {
                    CrewMember res = crewList.ElementAt(i);
                    crewList.RemoveAt(i);
                    return res;
                }
            }
            return null;
        }

        public void addDoorsToTileArray(int[,] array, Vector2f position)
        {
            //TODO add door number to tileMap numbers , needed Roomconnection list
        }

        protected Room(Vector2f position)
        {
            Position = position;
            _nearRooms = new List<Room>();
        }

        /// <summary>
        /// Initilizes tilemap in dependence of int[,] tilekind
        /// </summary>
        public void initializeTilemap(Area.RoomTypes roomType)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    _tilemap[i, k] = new Tile(tilekind[i, k], new Vector2f(this.Position.X + 32 * i, Position.Y + 32 * k), roomType); //TODO let the vector fit to every file
                }
            }
        }

        /// <summary>
        /// use this to get drawed after other rooms eg cannonballs
        /// </summary>
        public virtual void priorityDraw(){}

        public void Draw()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    _tilemap[i, k].Draw();
                }
            }
            //draw additional sprites like large weapons etc
            foreach(Sprite s in _additionalRoomSprites)
                Controller.Window.Draw(s);

            // draw der crew
            for (int k = 0; k < crewList.Count; k++)
            {
                crewList.ElementAt(k).Draw();
            }
        }

        public void addNearRooms(Room a)
        {
            _nearRooms.Add(a);
        }

        public bool iswalkable()
        {
            return _walkAble;
        }

        /// <summary>
        /// Update function; 
        /// </summary>
        public virtual void Update()
        {
        }
    }
}
