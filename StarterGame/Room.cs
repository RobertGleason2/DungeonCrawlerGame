using System.Collections;
using System.Collections.Generic;
using System;

namespace DungeonQuest
{
    public class TeleportRoom : RoomDelegate
    {

        public Dictionary<string, Door> _exits { get; set; }
        private Door _randomDoor;
        private Room _containingRoom;

        public Room ContainingRoom 
        { 
            get
            {
                return _containingRoom;
            }
            set
            {
                _containingRoom = value; //sets the containing room based on the gameworld
                _randomDoor = new Door(_containingRoom, GameWorld.RandomRoom());


            }
        }
        public void SetExit(string exitName, Door door)
        {
            _exits[exitName] = door;
        }

         public Door GetExit(string ExitName)
        {
            return _randomDoor;
        }       
        
        public string Description()
        {
            return "";
        }


    }

    public class Room 
    {
        private ItemContainer _items;
        private Dictionary<string, Door> _exits;
        private string _tag;
        private RoomDelegate _delegate;
        public RoomDelegate Delegate { set { _delegate = value; } }
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
        
        public Room() : this("No Tag"){}

        // Designated Constructor
        public Room(string tag)
        {
            _delegate = null;

            _exits = new Dictionary<string, Door>();
            _items = new ItemContainer("Floor", 0.0f, 0, 0, true, false, false); //cannot pick up the floor 
            this.Tag = tag;
        }


        public void SetExit(string exitName, Door door)
        {
            _exits[exitName] = door;
        }

        public Door GetExit(string exitName)
        {
            if(_delegate == null)
            {
                Door door = null;
                _exits.TryGetValue(exitName, out door);
                return door;
            }
            else
            {
                return _delegate.GetExit(exitName);
            }

        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return exitNames;
        }

        public void Drop(IItem item)
        {
            _items.AddItem(item);
        }
        public IItem Remove(string name)
        {
            IItem returnItem = _items.RemoveItem(name);
            return returnItem;
        }

        public string Description()
        {
            return (_delegate == null ? "" : _delegate.Description() + "\n") + "You are " + this.Tag + ".\n *** " + this.GetExits() + "\n" + "Items: " + _items.Description ;
        }
    }

}


