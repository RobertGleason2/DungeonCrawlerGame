using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class UnlockCommand : Command
    {
        public UnlockCommand()
        {
            this.Name = "unlock";
        }

        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                player.Unlock(SecondWord);
            }
            else
            {
                player.WarningMessage("unlock what?");
            }
            return false;
        }
    }
}
