using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    
    public interface IItem
    {
        string Name { set; get; }
        string LongName { get; }
        float Weight { set; get; }
        int Volume { set; get; }
        int Value { set; get; }
        bool Edible { set; get; }
        bool Grabable { set; get; }
        int RecoveryPoints { get; }
        string Description { get; }
        bool IsBreakable { get; set; }
        //string LongDescription { get; }
        void AddItem(IItem item);
        IItem RemoveItem(string name);
        void AddDecorator(IItem decorator);
        IItem Clone();
    }

    public interface RoomDelegate
    {
        Door GetExit(string ExitName);
        Room ContainingRoom { set; get; }
        Dictionary<string, Door> _exits { set; get; }
        string Description();
    }

    public interface ILockable
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        bool CanOperate { get; }
        void Lock();
        void Unlock();
        IItem RemoveKey();
        IItem InsertKey(IItem key);

    }
    public interface IClosable : ILockable
    {
        bool IsOpen { get; }
        bool IsClosed { get; }
        void Open();
        void Close();
    }

}
