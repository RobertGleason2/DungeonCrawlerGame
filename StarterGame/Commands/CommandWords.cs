using System.Collections;
using System.Collections.Generic;
using System;

namespace DungeonQuest
{
    public class CommandWords
    {
        private Dictionary<string, Command> commands;
        private static Command[] commandArray = { new GoCommand(), new QuitCommand(), new BackCommand(), 
            new GrabCommand(), new DropItemCommand(), new OpenCommand(), new UnlockCommand(),
            new EatCommand(), new InventoryCommand(), new InspectCommand(),new DestroyCommand(), new StatsCommand(), new EquipWeaponCommand(),
            new StowWeaponCommand(), new PlayCommand()
        };

        public CommandWords() : this(commandArray) {}

        // Designated Constructor
        public CommandWords(Command[] commandList)
        {
            commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
