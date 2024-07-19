using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public  class StowWeaponCommand : Command
    {
        public StowWeaponCommand() : base()
        {
            this.Name = "stow";
        }

        override
        public bool Execute(Kazuma player)
        {
            if (HasSecondWord())
            {
                player.PutAwayWeapon(SecondWord);
            }
            else
            {
                player.WarningMessage("Stow what?");
            }
            return false;
        }
    }
}
