using System.Collections;
using System.Collections.Generic;
using System;

namespace DungeonQuest
{
    public class Game
    {
        private Kazuma _player;
        private Parser _parser;
        private bool _playing;

        public Game()
        {
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Kazuma(GameWorld.Instance().Entrence);
            NotificationCenter.Instance.AddObserver("KazumaHasDied", KazumaHasDied);

        }
        public void KazumaHasDied(Notification notification)
        {
            Kazuma player = (Kazuma)notification.Object;
            player.ErrorMessage("Kazuma has died");
            

        }
        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n>");
                Command command = _parser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    Console.WriteLine("I don't understand...");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }


        public void Start()
        {
            _playing = true;
            _player.OutputMessage(Welcome());
        }

        public void End()
        {
            _playing = false;
            _player.OutputMessage(Goodbye());
        }

        public string Welcome()
        {
            return "Welcome to this wonderful world!\n\nYou play as a lone adventure named Kazuma. You're main goal is to explaore the dungeon and find the SacredOrb" + _player.CurrentRoom.Description();
        }

        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }

    }
}
