using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class GrabCommand : Command
    {
        public GrabCommand() : base() //might change later
        {
            this.Name = "grab";
        }
        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                player.PickUpItem(SecondWord);

            }
            else
            {
                player.ErrorMessage("Pickup waht?");
            }
            return false;
        }
    }
}