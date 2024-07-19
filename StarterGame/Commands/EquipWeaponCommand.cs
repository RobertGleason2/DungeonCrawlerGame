using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class EquipWeaponCommand : Command
    {
        public EquipWeaponCommand() : base()
        {
            this.Name = "equip";
        }

        override
        public bool Execute(Kazuma player)
        {
            if (HasSecondWord())
            {
                player.TakeOutWeapon(SecondWord);
            }
            else
            {
                player.WarningMessage("Equip what?");
            }
            return false;
        }
    }
}
