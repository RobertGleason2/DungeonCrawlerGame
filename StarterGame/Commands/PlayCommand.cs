using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public class PlayCommand : Command
    {
        public PlayCommand() : base()
        {
            this.Name = "play";
        }
        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                player.PlayOcurina(SecondWord);
                player.OutputMessage(player.CurrentRoom.Description());
            }
            else
            {
                player.ErrorMessage("Pickup waht?");
            }
            return false;
        }
    }
}
