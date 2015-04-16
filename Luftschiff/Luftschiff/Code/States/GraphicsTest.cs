using System;
using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Luftschiff.Code.States
{
    //TODO
    //Get it working! 
    //Rewrite animatedSprite and Animation
    class GraphicsTest : Global.ProtoGameState
    {
        private static Animation walkaround;
        private static AnimatedSprite movingSprite;
        private static RenderWindow win;
        //private static Sprite renTest;
        
        public GraphicsTest()
        {
            win = Controller.Window;
          
            walkaround = new Animation();
            walkaround.Texture = new Texture("Assets/Graphics/rusty_sprites.png");
            walkaround.AddFrame(new IntRect(100,0,100,100));
            walkaround.AddFrame(new IntRect(200,0,100,100));
            walkaround.AddFrame(new IntRect(100,100,100,100));
            walkaround.AddFrame(new IntRect(200,100,100,100));

            movingSprite = new AnimatedSprite(Time.FromSeconds((float)0.2), true, false);
            movingSprite.Position = new Vector2f(win.Size.X / 2, win.Size.Y / 2);
            //renTest = new Sprite(new Texture("Assets/Graphics/rusty_sprites.png"));
            
        }
        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw()
        {
            win.Clear();
            win.Draw(movingSprite);
            //win.Draw(renTest);
            win.Display();
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
            //updates the sprite
            movingSprite.Update(Globals.FRAME_TIME);
            if(Keyboard.IsKeyPressed(Keyboard.Key.A))
                movingSprite.Play(walkaround);
            else
                movingSprite.Stop();
            movingSprite.Move(0.2f,0.2f);
        }
    }
}
