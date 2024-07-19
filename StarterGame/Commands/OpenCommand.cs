using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    internal class OpenCommand : Command
    {
        public OpenCommand() : base()
        {
            this.Name = "open";
        }

        override
        public bool Execute(Kazuma player)
        {
            
            if (this.HasSecondWord())
            {

                 player.Open(SecondWord);
            }
            else if(this.HasSecondWord() && this.HasThirdWord())
            {
                player.Open(ThirdWord);
            }
            else
            {
                player.ErrorMessage("open what?");
            }
            return false;
        }
    }
}
