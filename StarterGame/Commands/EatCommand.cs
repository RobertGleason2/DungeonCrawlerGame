using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class EatCommand : Command
    {
        public EatCommand() : base()
        {
            this.Name = "eat";
        }

        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                player.Eat(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nEat what?");
            }
            return false;
        }
    }
}
