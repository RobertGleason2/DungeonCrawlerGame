using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class Door : IClosable
    {
        private Room _roomA;
        private Room _roomB;
        private bool _open;
        private ILockable _lock;
        public bool IsOpen { get { return _open; } }
        public bool IsClosed { get { return !_open; } }
        public bool IsLocked { get { return _lock == null ? false : _lock.IsLocked; } }
        public bool IsUnlocked { get { return _lock == null ? true : _lock.IsUnlocked; } }
        public bool CanOperate { get { return _lock == null ? true : _lock.CanOperate; } }
        

        public Door(Room roomA, Room roomB)
        {
            _roomA = roomA;
            _roomB = roomB;
            _open = true;
            _lock = null;
        }
        public void Open()
        {
            if (IsUnlocked)
            {
                _open = true;
            }
        }

        public void Close()
        {
            if(IsOpen && CanOperate)
            {
                _open = false;
            }
        }
        
        public void Lock()
        {
            if(_lock != null)
            {
                _lock.Lock();
            }
        }

        public void Unlock()
        {
            if(_lock != null)
            {
                _lock.Unlock();
            }
        }

        public ILockable InstallLock(ILockable theLock)
        {
            ILockable oldlock = _lock;
            _lock = oldlock;
            return oldlock;
        }
        public IItem RemoveKey()
        {
            return null;
        }

        public IItem InsertKey(IItem key)
        {
            return null;
        }
        public Room GetRoomOnTheOtherSide(Room ofThisRoom)
        {
            if (ofThisRoom == _roomA)
            {
                return _roomB;
            }
            else
            {
                return _roomA;
            }
        }

        public static Door CreateDoor(Room room1, Room room2, string lable1, string lable2)
        {
            Door door = new Door(room1, room2);
            room1.SetExit(lable1, door);
            room2.SetExit(lable2, door);
            return door;
        }
    }
}
