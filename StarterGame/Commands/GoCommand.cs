using System.Collections;
using System.Collections.Generic;

namespace DungeonQuest
{
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            this.Name = "go";
        }

        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                player.WaltTo(this.SecondWord);

            }
            else
            {
                player.OutputMessage("\nGo Where?");
            }
            return false;
        }
    }
}
