using System;
using Luftschiff.Code.Dialogs;
using SFML.Audio;

namespace Luftschiff.Code.States
{
    class Menu : Global.ProtoGameState
    {
        public override void kill()
        {
            throw new NotImplementedException();
        }

        public override void draw()
        {
        }

        public override void update()
        {
            
            TwoButtonDialog menu = new TwoButtonDialog("Game", "GraphicsTest", "Entscheide dich!", "Settings");
            menu.show();
            if (menu.show())
            {
                TwoButtonDialog mute = new TwoButtonDialog("Mute", "Sound","Spiele mit oder ohne ton","Menu");
                mute.show();
                if (mute.show())
                {
                    Globals.CannonSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.ClickSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.BoomSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.FireSound = new SoundBuffer("Assets/Audio/stumm.wav");
                    Globals.FireCrackleSound = new SoundBuffer("Assets/Audio/stumm.wav");


                }
                Controller.LoadState(Globals.EStates.game);
            }
            if (!menu.show())
            {
                Controller.LoadState(Globals.EStates.graphicstest);
            }
        }
    }
}
