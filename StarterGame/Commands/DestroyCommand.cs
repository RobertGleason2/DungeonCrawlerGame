
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonQuest
{
    public  class DestroyCommand : Command
    {
        public DestroyCommand() : base()
        {
            this.Name = "destroy";
        }

        override
        public bool Execute(Kazuma player)
        {
            if (this.HasSecondWord())
            {
                if (this.HasThirdWord() && ThirdWord == "using")
                {

                    if (this.HasFourthWord()) {
                        {
                            player.Destroy(SecondWord, ForthWord);
                        } 
                    }
                }
                else
                {

                }
            }
            else
            {
                player.OutputMessage("\nAttack what?");
            }
            return false;
        }
    }
}
