using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    internal class StatsCommand : Command
    {
        public StatsCommand() : base()
        {
            this.Name = "stats";
        }
        override
        public bool Execute(Kazuma player)
        {
            bool answer = false;
            player.Stats();
            if (this.HasSecondWord())
            {
                
                answer = false;
            }
            return answer;
        }
    }
}
