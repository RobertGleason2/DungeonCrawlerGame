using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class DropItemCommand : Command
    {
        public DropItemCommand() : base()
        {
            this.Name = "drop";
        }

        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                player.DropItem(SecondWord);
            }
            else
            {
                player.WarningMessage("Drop what?");
            }
            return false;
        }
    }
}
