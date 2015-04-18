using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using Luftschiff.Code.Dialogs;


namespace Luftschiff.Code.States
{
    class GraphicsTest : Global.ProtoGameState
    {
        private static Animation _walkaround;
        private static Animation _turnaround;
        private static AnimatedSprite _movingSprite;
        private static RenderWindow _win;
        private static ParticleHandler _amazing;

        
        public GraphicsTest()
        {
            _win = Controller.Window;

            _walkaround = new Animation(new Texture("Assets/Graphics/rusty_sprites.png"));
            _walkaround.AddFrame(new IntRect(0,0,100,100));
            _walkaround.AddFrame(new IntRect(0,100,100,100));
            _walkaround.AddFrame(new IntRect(100,100,100,100));
            _walkaround.AddFrame(new IntRect(100,0,100,100));

            _turnaround = new Animation(new Texture("Assets/Graphics/rusty_sprites.png"));
            _turnaround.AddFrame(new IntRect(100, 0, 100, 100));
            _turnaround.AddFrame(new IntRect(100, 100, 100, 100));
            _turnaround.AddFrame(new IntRect(0, 100, 100, 100));
            _turnaround.AddFrame(new IntRect(0, 0, 100, 100));

            _movingSprite = new AnimatedSprite(Time.FromSeconds(0.5f), false, true, new Vector2f(0f,0f));         
   
            _amazing = new ParticleHandler(2000);
        }
        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw()
        {
            _win.Clear();
            _win.Draw(_movingSprite);
            _amazing.Draw();
            _win.Display();
        }

        /// <summary>
        /// This method gets called when the Gamestate is about to be stopped
        /// to notify it of the event. Could be useful for saving the game
        /// </summary>
        public override void kill()
        {

        }

        /// <summary>
        /// This is the update function that gets called for our Game-Gamestate
        /// </summary>
        public override void update()
        {
            _movingSprite.Update(Globals.FRAME_TIME);
            _movingSprite.Play(_walkaround);
            _amazing.Update();

            ////ok create a new listdialog
            //List<String> testList= new List<String>();
            //for(int i = 0; i < 10; i++)
            //    testList.Add("Daniel mag Kekse!"+i);
            
            //ListDialog test = new ListDialog(testList, "message", "titletest");
            ////return index of button in list
            //Console.WriteLine(test.show());

            
            ////construct a yes / no dialog; Ja and Nein are also available as a smaller constructor
            //TwoButtonDialog test2 = new TwoButtonDialog("Ja", "Nochmehr Ja", "Kekse lassen die Welt krümmelig werden!", "Kekse");
            ////show it, blocking all other execution until return of true/false
            //Console.WriteLine(test2.show());

        }
    }
}
