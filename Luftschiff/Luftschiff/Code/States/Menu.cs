using System;
using System.Collections.Generic;
using Luftschiff.Code.Dialogs;
using Luftschiff.Code.Global;
using SFML.Audio;

namespace Luftschiff.Code.States
{
    internal class Menu : ProtoGameState
    {
        private readonly List<string> _fightList;
        private readonly List<string> _menuList;

        public Menu()
        {
            _menuList = new List<string>();
            _menuList.Add("Settings");
            _menuList.Add("Graphicstest");
            _menuList.Add("Fightmatch");
            _menuList.Add("Open-World");
            _fightList = new List<string>();
            _fightList.Add("Batswarm");
            _fightList.Add("Dragon");
            _fightList.Add("Pottwal");
            _fightList.Add("Petunientopf");
            _fightList.Add("Harpie");
        }

        public override void kill()
        {
            throw new NotImplementedException();
        }

        public override void draw()
        {
        }

        public override void update()
        {
            var mainMenu = new ListDialog(_menuList, "Waehle zwischen den verschiedenen Optionen!", "Mainmenu");
            var fightMenu = new ListDialog(_fightList, "Wähle deinen Gegner!", "Kampfmenu");
            if (mainMenu.show() == 0)
            {
                var mute = new TwoButtonDialog("Mute", "Sound", "Sound Einstellungen", "Menu");
                mute.show();
                if (mute.show())
                {
                    Globals.CannonSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.ClickSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.BoomSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.FireSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.FireCrackleSound = new SoundBuffer("Assets/Audio/stumm.wav");
                }
                Controller.LoadState(Globals.EStates.menu);
            }
            if (mainMenu.show() == 1)
            {
                Controller.LoadState(Globals.EStates.graphicstest);
            }
            if (mainMenu.show() == 2)
            {
                if (fightMenu.show() == 0)
                    Controller.LoadState(Globals.EStates.batfight);
                if (fightMenu.show() == 1)
                    Controller.LoadState(Globals.EStates.dragonfight);
                if (fightMenu.show() == 2)
                    Controller.LoadState(Globals.EStates.whalefight);
                if (fightMenu.show() == 3)
                    Controller.LoadState(Globals.EStates.petuniefight);
            }
        }
    }
}