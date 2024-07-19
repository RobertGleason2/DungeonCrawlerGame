using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "back";
        }

        override
        public  bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("I cannot go to " + SecondWord);
                
            }
            else
            {
                player.Back();
            }
            return false;
        }
    }
}
