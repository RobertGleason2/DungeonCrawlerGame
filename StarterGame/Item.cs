using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    //prototype pattern
    public class ItemSpawner
    {
        private IItem _prototype;
        public ItemSpawner(IItem prototype)
        {
            _prototype = prototype;
        }

        public IItem Spawn()
        {
            return _prototype.Clone();
        }

    }

    public class Item : IItem
    {
        private string _name;
        public virtual string Name { set { _name = value; } get { return _name; } }
        private IItem _decorator;
        public string LongName { get { return  (_decorator != null ? _decorator.Name : "") + " " +  Name; } }

        private float _weight;
        public virtual float Weight { set { _weight = value; } get { return _weight; } }
        private int _volume;
        public virtual int Volume { set { _volume = value; } get { return _volume; } }
        private int _value;
        public virtual int Value { set { _value = value; } get { return _value; } } 
        private bool _edible;
        public bool Edible { set { _edible = value; } get { return _edible;} }
        private bool _grabable;
        public  virtual bool Grabable { set { _grabable = value; } get { return _grabable; } }
        private bool isBreakable;
        public virtual bool IsBreakable { get { return isBreakable; } set { isBreakable = value; } }

        private int _recoveryPoints;
        public int RecoveryPoints { get { return _recoveryPoints; } } 

        public Item() : this("No Name"){ }
        public Item(string name) : this(name, 1.0f){ }
        public Item(string name, float weight) : this(name, weight, 1){ }
        public Item(string name, float weight, int volume) : this(name, weight, volume, 1){ }
        public Item(string name, float weight, int volume, int value) : this(name, weight, volume, value, false) { }
        public Item(string name, float weight, int volume, int value, bool grabable) : this(name, weight, volume , value, grabable, false) { }
        public Item(string name, float weight, int volume, int value, bool grabable, bool edible) : this(name, weight, volume, value, grabable, edible, false) { }
        public Item(string name, float weight, int volume, int value, bool grabable, bool  edible, bool isBreakable)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            Value = value;
            Edible = edible;
            Grabable = grabable;
            IsBreakable = isBreakable;
            _decorator = null;
            _recoveryPoints = 10;
        }
        public virtual string Description
        {
            get 
            {
                return LongName + " , weight = " + Weight + ", volume: "+ Volume + " , value " + Value; 
            } 
        }

        public virtual void AddItem(IItem item)
        {

        }
        public virtual IItem RemoveItem(string name)
        {
            return null;
        }
        public void AddDecorator(IItem decorator)
        {
            if (_decorator == null)
            {
                _decorator = decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }

        public virtual IItem Clone()
        {
            Item clone = (Item)this.MemberwiseClone();
            clone._name = "" + _name;
            clone._weight = _weight;
            clone._volume = _volume;
            clone._decorator = _decorator != null ? _decorator.Clone() : null;

            return clone;
        }
    }
    public class Weapon : Item
    {
        private string _name;
         
        public override string Name { set { _name = value; } get { return _name; } }
        private float _weight;
        
        public override float Weight { set { _weight = value; } get { return _weight; } }
        private int _volume;
        public override int Volume { set { _volume = value; } get { return _volume; } }
        private int _value;
        public override int Value { set { _value = value; } get { return _value; } }
        private int _durablity;
        public int Durability { set { _durablity = value; } get { return _durablity; } }
        private bool _grabable;
        public override bool Grabable { set { _grabable = value; } get { return _grabable; } }
        public Weapon(string name, float weight, int volume, int value, bool grabable, bool edible, int durability)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            Value = value;
            Grabable = grabable;
            Durability = durability;
        }

        
        public override string Description
        {
            get
            {
                return LongName +", Weight: " +  Weight + ", volume: "+ Volume + ", Value: " +  Value + ", Durability: " +  Durability;
            }

        }
    }
    //Heirarchy Design Pattern
    public class ItemContainer : Item
    {
        private string _name;
        private Dictionary<string, IItem> items;
        private ILockable _lock;
        override
        public string Name { get { return _name; } set { _name = value; } }
        private float _weight;
        override
        public float Weight
        {
            get
            {
                float tempweight = _weight;
                foreach (IItem item in items.Values)
                {
                    tempweight += item.Weight;
                }
                return tempweight;
            }
        }
        private int _volume;
        override
        public int Volume
        {
            get
            {
                int tempVolume = _volume;
                foreach (IItem item in items.Values)
                {
                    tempVolume += item.Value;
                }
                return tempVolume;
            }
        }
        private int _value;
        override
        public int Value 
        { 
            get
            {
                int tempValue = _value;
                foreach (IItem item in items.Values)
                {
                    tempValue += item.Value;
                }
                return tempValue;
            }
        }
        
    
        private bool isBreakable;
        public override bool IsBreakable { get { return isBreakable; } set { isBreakable = value; } }
        public ItemContainer(string name, float weight, int volume, int value, bool grabable, bool edible, bool breakable)
        {
            items = new Dictionary<string, IItem>();
            IsBreakable = breakable;
            Name = name;
        }

        override
        public void AddItem(IItem item)
        {
            items[item.Name] = item;
        }

        override
        public IItem RemoveItem(string name)
        {
            IItem item = null;
            items.TryGetValue(name, out item);
            items.Remove(name);
            return item;
        }

        override
        public string Description
        {
            get
            {
                string description = base.Description + "\n";
                foreach (IItem item in items.Values)
                {
                    description += "\t" + item.Description + "\n";
                }
                return description;
            }
        }
        
    }


}
