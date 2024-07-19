using System;
using System.Collections.Generic;

namespace DungeonQuest
{
    public class GameWorld
    {
        //Singleton Design Pattern
        private static GameWorld _instance;
        public static GameWorld Instance()
        {
            if(_instance == null)
            {
                _instance = new GameWorld();
            }
            return _instance;
        }

        private Room _exit;
        private Room _leftCorridor;
        public Room LeftCorridor { get { return _leftCorridor; } set { _leftCorridor = value; } }
        private Room _entrence;
        public Room Entrence
        {
            get
            {
                return _entrence;
            }
        }
        private Room _secreteRoom;
        private Room _teleportRoom;
        public Room TeleportRoom { get { return _teleportRoom; } set { _teleportRoom = value; } }

        private static List<Room> list = new List<Room>();

        //Notification Design Pattern
        public GameWorld()
        {
            _entrence = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerWillEnterRoom", PlayerWillEnterRoom);
            NotificationCenter.Instance.AddObserver("PlayerEnteredTeleportRoom", PlayerEnteredTeleportRoom);
            NotificationCenter.Instance.AddObserver("KazumaDestroyedAnItem", KazumaDestroyedAnItem);
            NotificationCenter.Instance.AddObserver("PlayerPlayedOcarinaLeftCorridor", PlayerPlayedOcarinaLeftCorridor);
            NotificationCenter.Instance.AddObserver("KazumaPickedUpSacredOrb", KazumaPickedUpSacredOrb);
        }
        public void KazumaDestroyedAnItem(Notification notification)
        {
            Kazuma player = (Kazuma)notification.Object;
            player.SuccessfulMessage("\nKazuma destroyed an item in the gameworld.");
            player.SuccessfulMessage("\nKazuma collected 10 coins");

        }
        //reveals secrete door if Kazuma plays ocurina
        public void PlayerPlayedOcarinaLeftCorridor(Notification notification)
        {
            Kazuma player = (Kazuma)notification.Object;
            if(player.CurrentRoom == LeftCorridor)
            {
                Room secreteRoom = new Room("inside the secrete rooom");
                Door door = Door.CreateDoor(_leftCorridor, secreteRoom, "south", "north");
                _secreteRoom = secreteRoom;
                player.SuccessfulMessage("\nKazuma played the ocurina");
                player.InformationMessage("\nA secrete door ahs appeared south of the left corridor");
            }
            
        }
        public void PlayerEnteredSecreteRoom(Notification notification)
        {
            Kazuma player = (Kazuma)notification.Object;
            if (player.CurrentRoom == _secreteRoom)
            {
                IItem chest = new ItemContainer("chest", 5, 5, 5, false, false, false);
                _secreteRoom.Drop(chest);
                IItem key = new Item("MasterKey");
                chest.AddItem(key);
            }
        }


        public void PlayerWillEnterRoom(Notification notification) //need to change name later for when defeated enemy
        {
            Kazuma player = (Kazuma)notification.Object;
            if(player.CurrentRoom == _exit)
            {
                Room room = null;// player.CurrentRoom.GetExit("vortex");
                if(room != null)
                {
                    player.CurrentRoom.SetExit("vortex", null);
                }
            }
            player.OutputMessage("\n*** The player is entering " + player.CurrentRoom.Tag);
        }


        //notification to randomize exit is player enters the room
        public void PlayerEnteredTeleportRoom(Notification notification)
        {

            Kazuma player = (Kazuma)notification.Object;            
            //player.OutputMessage("\nKazuma has entered the teleport room, now teleporting\n");
            if(player.CurrentRoom == TeleportRoom)
            {
                player.WarningMessage("\nKazuma has entered the teleport room, now teleporting\n");
                Room room = player.CurrentRoom = RandomRoom();
                Door door = Door.CreateDoor(TeleportRoom, room, "east", "west");
            }

        }

        //simple method for randomizing the exit for the teleport room
        public void KazumaPickedUpSacredOrb(Notification notification)
        {
            Kazuma player = (Kazuma)notification.Object;
            player.SuccessfulMessage("Kazuma has aquired the Spirit Orb. \nYou win");
        }
        public static Room RandomRoom()
        {
            Room teleportRoom = null;
            Random random = new Random();
            int randomInt = random.Next(list.Count);
            teleportRoom = list[randomInt];
            return teleportRoom;

        }

        public Room CreateWorld()
        {
            Room mainHall = new Room("inside the main hall of the dungeon");
            Room outside = new Room("outside the main entrance of the dungeon");
            Room leftCorrador = new Room("inside the left corridor");
            Room rightCorrador = new Room("inside the right corridor");
            Room armory = new Room("inside the armory");
            Room kitchen = new Room("inside the kitchen");
            Room teleportRoom = new Room("inside the teleport room");
            Room treasureRoom = new Room("inside the treasure room");
            //Room shop = new Room("inside the shop");


            list.Add(outside);
            list.Add(mainHall);
            list.Add(rightCorrador);
            list.Add(armory);
            list.Add(kitchen);

            //creates a new room delgate, one for teleport room
            RoomDelegate teleRoom = new TeleportRoom();
            teleRoom.ContainingRoom = teleportRoom;
            teleportRoom.Delegate = teleRoom;




            Door door = Door.CreateDoor(outside, mainHall, "north", "south");

            door = Door.CreateDoor(mainHall, leftCorrador, "west", "east");
  
            door = Door.CreateDoor(mainHall, rightCorrador, "east", "west");
          
            door = Door.CreateDoor(leftCorrador, teleportRoom, "west", "east");

            door = Door.CreateDoor(leftCorrador, armory, "north", "south");

            door = Door.CreateDoor(rightCorrador, kitchen, "north", "south");

            door = Door.CreateDoor(mainHall, treasureRoom, "north", "south");
            RegularLock aLock = new RegularLock();
            door.InstallLock(aLock);
            door.Lock();
            door.Close();

            //door = Door.CreateDoor(rightCorrador, shop, "east", "west");


            IItem potato = new Item("potato", 1.0f, 1, 1, true, true);
            IItem soup = new Item("soup", 1, 1, 1, true, true);
            IItem sword = new Weapon("sword", 1.0f, 2, 5, true, false, 5);
            IItem diamond = new Item("diamond", 2, 1, 50 );
            IItem ruby = new Item("ruby", 2, 1, 20);
            IItem box = new Item("box", 10, 10, 2, false, false, true);
            IItem pot = new Item("pot", 5, 5, 5, false, false, true);
            IItem wood = new Item("wood", 5, 3, 2, false);
            IItem chest = new ItemContainer("chest", 10, 10, 10, true, true, true);
            IItem broom = new Weapon("broom", 2, 2, 1, true, true, 5);
            IItem occurina = new Item("ocurina", 1, 1, 5, true);
            IItem shovel = new Item("shovel", 3, 2, 5, true);
            IItem SacredOrb = new Item("SacredOrb", 0, 0, 0, true);
            IItem rupy = new Item("rupy", 0, 0, 1, true);

            ItemSpawner spawner = new ItemSpawner(potato);
            IItem item = spawner.Spawn();
            kitchen.Drop(item);
            kitchen.Drop(soup);

            sword.AddDecorator(wood);
            mainHall.Drop(chest);
            chest.AddItem(broom);
            chest.AddItem(sword);

            sword.AddDecorator(potato);
            kitchen.Drop(sword);
            


            mainHall.Drop(box);
            mainHall.Drop(pot);
            mainHall.Drop(pot);
           
            leftCorrador.Drop(box);
            leftCorrador.Drop(box);
            leftCorrador.Drop(box);

            rightCorrador.Drop(box);
            rightCorrador.Drop(pot);
            rightCorrador.Drop(box);
            rightCorrador.Drop(pot);
            broom.AddDecorator(diamond);
            rightCorrador.Drop(broom);

            armory.Drop(chest);
            sword.AddDecorator(ruby);
            chest.AddItem(sword);
            chest.AddItem(shovel);
            chest.AddItem(rupy);
            chest.AddItem(rupy);
            sword.AddDecorator(diamond);
            armory.Drop(sword);
            broom.AddDecorator(sword);
            armory.Drop(broom);



            outside.Drop(occurina);


            treasureRoom.Drop(SacredOrb);
            treasureRoom.Drop(rupy);
            treasureRoom.Drop(rupy);
            treasureRoom.Drop(rupy);
            treasureRoom.Drop(rupy);
            treasureRoom.Drop(rupy);

            _teleportRoom = teleportRoom;
            _leftCorridor = leftCorrador;

            return outside;
        }
    }
}
