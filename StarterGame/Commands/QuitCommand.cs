using System.Collections;
using System.Collections.Generic;

namespace DungeonQuest
{
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.Name = "quit";
        }

        override
        public bool Execute(Kazuma player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
