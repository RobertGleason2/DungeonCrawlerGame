using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class InspectCommand : Command
    {
        public InspectCommand() : base()
        {
            this.Name = "inspect";

        }
        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {

                player.Inspect(SecondWord);
            }
            else
            {
                player.OutputMessage("\nGo Where?");
            }
            return false;

        }
    }
}
