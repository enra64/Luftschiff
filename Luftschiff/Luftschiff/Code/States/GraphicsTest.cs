using Luftschiff.Graphics.Lib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Luftschiff.Code.States
{
    class GraphicsTest : Global.ProtoGameState
    {
        private static Animation _walkaround;
        private static AnimatedSprite _movingSprite;
        private static RenderWindow _win;

        
        public GraphicsTest()
        {
            _win = Controller.Window;
          
            _walkaround = new Animation();
            _walkaround.Texture = new Texture("Assets/Graphics/rusty_sprites.png");
            _walkaround.AddFrame(new IntRect(0,0,100,100));
            _walkaround.AddFrame(new IntRect(0,100,100,100));
            _walkaround.AddFrame(new IntRect(100,100,100,100));
            _walkaround.AddFrame(new IntRect(100,0,100,100));

            _movingSprite = new AnimatedSprite(Time.FromSeconds(0.2f), true, false);
            _movingSprite.Position = new Vector2f(_win.Size.X / 2, _win.Size.Y / 2);
            
        }
        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw()
        {
            _win.Clear();
            _win.Draw(_movingSprite);
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
            //updates the sprite
            _movingSprite.Update(Globals.FRAME_TIME);
            if(Keyboard.IsKeyPressed(Keyboard.Key.A))
                _movingSprite.Play(_walkaround);
            else
                _movingSprite.Stop();
            _movingSprite.Move(0.2f,0.2f);
        }
    }
}
