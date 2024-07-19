using System.Collections;
using System.Collections.Generic;
using System;

namespace DungeonQuest
{
    public class Kazuma 
    {
        private int _health;        
        public int Health { get { return _health; } set { _health = value; } }
        private int coins; //currency
        public int Coins { get { return coins; } set { coins = value; } }
        private Weapon _currentEquiptWeapon;

        private Stack<Room> rooms = new Stack<Room>();
        private Room _currentRoom = null;

        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }

        private ItemContainer _inventory;
        private float _setInventoryWeight;
        private int _setInventoryVolume;
        //maybe find a way to have the max health set andto update it to gain more health
        public Kazuma(Room room) 
        {
            _currentRoom = room;
            _inventory = new ItemContainer("Inventory", 0f, 0, 0,false,false,false);
            _currentEquiptWeapon = new Weapon("fists", 0, 0, 0, false, false, 0);
            _setInventoryWeight = 10f;
            _setInventoryVolume = 10;
            _health = 100;
        }
        public void TakeOutWeapon(string weaponName)
        {
            Weapon weapon = (Weapon)_inventory.RemoveItem(weaponName);
            _setInventoryWeight = _setInventoryWeight + weapon.Weight;
            _setInventoryVolume = _setInventoryVolume + weapon.Volume;
            _currentEquiptWeapon = weapon;

        }
        public void PutAwayWeapon(string weaponName)
        {
            Weapon weapon = null;
            weapon = _currentEquiptWeapon;
            if(weapon.Name == weaponName)
            {
                if(weapon.Name != "fists")
                {
                    _inventory.AddItem(weapon);
                    _setInventoryWeight = _setInventoryWeight - weapon.Weight;
                    _setInventoryVolume = _setInventoryVolume - weapon.Volume;
                }
                else
                {
                    this.WarningMessage("You cannot put away your fists");
                }
                _currentEquiptWeapon = new Weapon("fists",0 ,0, 0, false, false, 0);
            }
            else
            {
                this.WarningMessage("Kazuma is not holding " + weaponName);
            }
        }
        public void Destroy(string itemName, string weaponName)//, string itemName
        {
            IItem item = CurrentRoom.Remove(itemName);
            if(item != null)
            {
                if (item.IsBreakable)
                {

                    if (_currentEquiptWeapon.Name != "fists" && _currentEquiptWeapon.Name == weaponName)
                    {
                        CurrentRoom.Remove(item.Name);
                        _currentEquiptWeapon.Durability = _currentEquiptWeapon.Durability - 1;
                        this.coins += (item.Value / 2);
                        if (_currentEquiptWeapon.Durability == 0)
                        {
                            this.PutAwayWeapon(_currentEquiptWeapon.Name);
                            _inventory.RemoveItem(weaponName);

                        }
                        Notification notification = new Notification("KazumaDestroyedAnItem", this);
                        NotificationCenter.Instance.PostNotification(notification);

                    }                     
                    else if  (weaponName == "fists")
                    {
                        CurrentRoom.Remove(item.Name);
                        this.TakeDamage(10);
                        if(Health == 0)
                        {
                            Notification notification = new Notification("KazumaHasDied", this);
                            NotificationCenter.Instance.PostNotification(notification);
                        }
                        this.coins += (item.Value / 2);
                    }

                    else
                    {
                        this.WarningMessage("You don't have " + weaponName + " in your inventory");
                    }
                }
                else
                {
                    this.TakeDamage(10);
                    this.WarningMessage("You cannot break " + item.Name);
                }
            }
            else
            {
                this.WarningMessage("The container " + itemName + " is not in the room");
            }
        }
        public int TakeDamage(int damage)
        {
            return Health -= damage;
        }
        public void Stats()
        {
            this.InformationMessage("\t\n*** Stats ****\n\nHealth: " + Health + "\nCoins : " + Coins + "\nCurrent Equiped Weapon: " + _currentEquiptWeapon.Description);
        }

        public void PlayOcurina(string itemName)
        {
            IItem item = _inventory.RemoveItem(itemName);
            if(item != null && item.Name == "ocurina")
            {
                Notification notification = new Notification("PlayerPlayedOcarinaLeftCorridor", this);
                NotificationCenter.Instance.PostNotification(notification);
            }
            _inventory.AddItem(item);
        }

        public virtual void WaltTo(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpen)
                {

                    Room nextRoom = door.GetRoomOnTheOtherSide(CurrentRoom);
                    rooms.Push(_currentRoom);
                    this.CurrentRoom = nextRoom;
                    if(this.CurrentRoom.Tag == "inside the teleport room")
                    {
                       Notification notification1 = new Notification("PlayerEnteredTeleportRoom", this);
                       NotificationCenter.Instance.PostNotification(notification1);
                    }
                    Notification notification = new Notification("PlayerWillEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    notification = new Notification("PlayerDidEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.InformationMessage("\n" + this.CurrentRoom.Description());                    

                    
                }
                else
                {
                    WarningMessage("The door on " + direction + "is locked");
                }
            }
            else
            {
                this.WarningMessage("\nThere is no door on " + direction);
            }
        }
        public void Back()
        {
            if(rooms.Count > 0)
            {
                this.CurrentRoom = rooms.Pop();
                this.InformationMessage("\n" + this._currentRoom.Description());
            }
        }
        public void Inspect(string ItemName)
        {
            IItem item = CurrentRoom.Remove(ItemName);
            if (item != null)
            {
                this.InformationMessage("\n " + item.Description + "\n");
                CurrentRoom.Drop(item);
            }
            else
            {
                this.WarningMessage("The item " + ItemName + " is not in the room");
            }
        }

        //seperate method for adding to inventory
        public void Grab(IItem item)
        {
            _inventory.AddItem(item);
        }

        //needs to be void for command to work or else game exits
        public void PickUpItem(string itemName)
        {

            IItem item = CurrentRoom.Remove(itemName);
            if(item != null)
            {
                if(!(item.Weight > _setInventoryWeight))
                {
                    if(!(item.Volume > _setInventoryVolume))
                    {
                        if(item.Grabable == true)
                        {

                            Grab(item); //might change it later to seperate pickup with grab but not sure
                            if (item.Name == "SacredOrb")
                            {
                                Notification notification = new Notification("KazumaPickedUpSacredOrb", this);
                                NotificationCenter.Instance.PostNotification(notification);

                            }
                            _setInventoryWeight = _setInventoryWeight - item.Weight;
                            _setInventoryVolume = _setInventoryVolume - item.Volume;
                            this.InformationMessage("\n" + this._currentRoom.Description());


                        }
                        else
                        {
                            WarningMessage("You can't grab that");
                        }
                    }
                    else
                    {
                        WarningMessage("You don't have enough space for that item");
                    }
                }
                else
                {
                    WarningMessage("That item is too heavy for you to pick up");
                }
            }
        }
        //seperate method for grabbing item from inventory
        public IItem GrabItemFromInv(string name) //might expand later for specific item
        {
            return _inventory.RemoveItem(name);
        }

        public void DropItem(string name)
        {
            IItem item = GrabItemFromInv(name);
            if(item != null)
            {
                CurrentRoom.Drop(item);
                _setInventoryVolume = _setInventoryVolume - item.Volume;
                _setInventoryWeight = _setInventoryWeight - item.Weight;
                this.InformationMessage("\n" + this._currentRoom.Description());
            }
            else
            {
                WarningMessage(item + " is not in your inventory");
            }
        }

        public virtual void Open(string ExitName)
        {
            Door door = CurrentRoom.GetExit(ExitName);
            if (door != null)
            {
                door.Open();
                if (door.IsOpen)
                {
                    this.InformationMessage("\nThe door to " + ExitName + " is opened");
                    this.InformationMessage("\n" + this._currentRoom.Description());
                }
                else
                {
                    this.WarningMessage("\nThe door to " + ExitName + "is still closed");
                }
            }
            else
            {
                this.WarningMessage("\nThere is no door on " + ExitName);
            }

        }
        //need to update unlock
        public void Unlock(string ExitName)
        {
            Door door = CurrentRoom.GetExit(ExitName);
            if (door != null)
            {
                if (door.IsLocked)
                {
                    door.Unlock();
                    this.InformationMessage("\n" + this._currentRoom.Description());
                    if (door.IsUnlocked)
                    {
                        this.WarningMessage("\nThe door to " + ExitName + " is now Unlocked, but closed");
                    }
                    else
                    {
                        this.WarningMessage("\nThe door to " + ExitName + "is still closed");
                    }
                }
                this.WarningMessage("\nThe door is already unlocked");
            }
            else
            {
                this.WarningMessage("\nThere is no door on " + ExitName);
            }

        }
        public void IncreaseHelath(IItem item)
        {
            _health += item.RecoveryPoints;
        }
        public void Eat(string itemName)
        {
            IItem item = GrabItemFromInv(itemName);
            if(item != null)
            {
                if (item.Edible)
                {
                    if (this.Health < 100 || this.Health > 0)
                    {
                        IncreaseHelath(item);
                        this.InformationMessage("\n" + this._currentRoom.Description());
                    }
                    else if (this._health < 100)
                    {
                        this.ErrorMessage("Kazuma has the maximum ammount of health.");
                    }
                }
                else
                {
                    this.ErrorMessage("The item " + itemName + " is not edible");
                }
            }
            else
            {
                this.ErrorMessage("The item " + itemName + " is not in your inventory");
            }
        }
        public void Inventory()
        {
            this.InformationMessage("\nInventory" + _inventory.Description + "\n");
        }
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void ColorMessage(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }
        public void InformationMessage(string message)
        {
            ColorMessage(message, ConsoleColor.Cyan);
        }
        //changes the color of error messages
        public void ErrorMessage(string message)
        {
            ColorMessage(message, ConsoleColor.Red);
        }
        //changes color of warning messages
        public void WarningMessage(string message)
        {
            ColorMessage(message, ConsoleColor.Yellow);
        }
        public void SuccessfulMessage(string message)
        {
            ColorMessage(message, ConsoleColor.Green);
        }
    }
    
}
