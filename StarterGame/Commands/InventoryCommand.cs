using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class InventoryCommand : Command
    {
        public InventoryCommand() : base()
        {
            this.Name = "inventory";
        }

        override
        public bool Execute(Kazuma player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nI cannot inventory " + this.SecondWord);
            }
            else
            {
                player.Inventory();
            }
            return false;
        }
    }
}
