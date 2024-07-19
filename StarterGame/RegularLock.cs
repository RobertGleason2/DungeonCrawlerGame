using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class RegularLock : ILockable
    {
        private IItem _originalKey = new Item("MasterKey");
        private IItem _insertedKey;
        private bool _locked;
        public bool IsLocked { get { return _locked; } }    
        public bool IsUnlocked { get { return _locked; } }
        public bool CanOperate { get { return true; } }
        public RegularLock()
        {
            _locked = false;
            _insertedKey = null;
        }
        public void Lock()
        {
            _locked = true;
        }
        public void Unlock()
        {
            if(_insertedKey == _originalKey)
            {
                _locked = false;
            }
        }
        public IItem RemoveKey()
        {
            return InsertKey(null);
        }
        public IItem InsertKey(IItem key)
        {
            IItem oldKey = _insertedKey;
            _insertedKey = key;
            return oldKey;
        }
    }
}
